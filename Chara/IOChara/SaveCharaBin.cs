using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Drawing.Imaging;


namespace ScriptEditor
{
	//==================================================================================
	//	SaveCharaBin
	//		キャラデータをbinaryで.datファイルに保存
	//		Document形式を介さない
	//==================================================================================
	public partial class SaveCharaBin
	{
		//エラーメッセージ
		public string ErrMsg { get; set; } = "Err Msg.";

		//-------------------------------------------------------------
		//	コンストラクタ
		//-------------------------------------------------------------
		public SaveCharaBin ()
		{
		}

		//-------------------------------------------------------------
		//	実行
		//引数　filepath : 保存ファイルパス, chara : 保存対象キャラデータ
		//-------------------------------------------------------------
		public void Do ( string filepath, Chara chara )
		{
			try
			{
				_Save ( filepath, chara );
			}
			catch ( ArgumentException e )
			{
				Debug.WriteLine ( e );
			}
		}

		private void _Save ( string filepath, Chara chara )
		{ 
			string dir = Directory.GetCurrentDirectory () ;
			Debug.WriteLine ( dir );

			//データが無かったら何もしない
			if ( chara == null ) { return; }

			//一時メモリストリーム
			using ( MemoryStream ms = new MemoryStream () )
			using ( BinaryWriter bw = new BinaryWriter ( ms, Encoding.UTF8 ) )
			{

			//--------------------------------------------------------
			//chara 各種データ書出
			SaveBinBehavior ( bw, chara );	//behavior
			SaveBinGarnish ( bw, chara );	//garnish
			SaveBinCommand ( bw, chara );	//Command
			SaveBinBranch ( bw, chara );	//Branch
			SaveBinRoute ( bw, chara );		//Route

			long script_size = ms.Length; 

			//イメージ部
			WriteListImage ( bw, chara.behavior.BD_Image );
			WriteListImage ( bw, chara.garnish.BD_Image );

			//--------------------------------------------------------
			bw.Flush ();
			//--------------------------------------------------------
			

			//最後にファイルに書出
			using ( FileStream fs = new FileStream ( filepath, FileMode.Create, FileAccess.Write ) )
			using ( BinaryWriter bwFl = new BinaryWriter( fs ) )
			{

			//バージョン(uint)
			bwFl.Write ( IO_CONST.VER );

			//サイズ(uint)4,294,967,296[byte]まで
			bwFl.Write ( (uint) ms.Length );

			const int SIZE = 4096;	//バッファサイズ
			byte[] buf = new byte [ SIZE ];	//バッファ
			int numBytes = 0;

			ms.Seek ( 0, SeekOrigin.Begin );
			while ( ( numBytes = ms.Read ( buf, 0, SIZE ) ) > 0 )
			{
				bwFl.Write ( buf, 0, numBytes );
			}


			}	//using

			}	//using
		}

		private void WriteListImage ( BinaryWriter bw, BindingDictionary < ImageData > bdImg )
		{
			//ストリーム読込→書出用
			const int size = 4096;	//バッファサイズ
			byte [] buffer = new byte [ size ];	//バッファ
			int numBytes = 0;	//書込バイト数

			//イメージ個数
			bw.Write ( (uint)bdImg.Count() );

			//実データ
			foreach ( ImageData id in bdImg.GetEnumerable () )
			{
				//イメージを一時領域に書出
				using ( MemoryStream msImg = new MemoryStream () )
				{			
				//名前
				bw.Write ( id.Name );		//string (length , [UTF8])

				//------------
				//@info 先にメモリに書き出してmsImg.Lengthにサイズを記録する
				//実データ
				id.Img.Save ( msImg, ImageFormat.Png );

				//サイズ
				bw.Write ( (uint)msImg.Length );

				msImg.Seek ( 0, SeekOrigin.Begin );
				while ( ( numBytes = msImg.Read ( buffer, 0, size ) ) > 0 )
				{ 
					bw.Write ( buffer, 0, numBytes );
				}

				}	//using
			}
		}
	}
}
