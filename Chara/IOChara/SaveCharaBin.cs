using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing;


namespace ScriptEditor
{
	using BD_ImgDt = BindingDictionary < ImageData >;

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
//			WriteListImage ( bw, chara.behavior.BD_Image );
//			WriteListImage ( bw, chara.garnish.BD_Image );

			//--------------------------------------------------------
			bw.Flush ();
			//--------------------------------------------------------

#if false

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
#endif

			//最後にファイルに書出
			using ( FileStream fs = new FileStream ( filepath, FileMode.Create, FileAccess.Write ) )
			using ( BufferedStream bwFl = new BufferedStream( fs ) )
			{

			//バージョン(uint)
			byte[] byte_VER = BitConverter.GetBytes ( IO_CONST.VER );
			bwFl.Write ( byte_VER, 0, byte_VER.Length );

			//サイズ(uint)4,294,967,296[byte]まで
			byte[] byte_Length = BitConverter.GetBytes ( (uint) ms.Length );
			bwFl.Write ( byte_Length, 0, byte_Length.Length );

			bwFl.Write ( ms.ToArray(), 0, (int)ms.Length );

			}	//using

			}	//using
		}

		private void WriteListImage ( BinaryWriter bw, BD_ImgDt bdImg )
		{
			//イメージ個数
			bw.Write ( (uint)bdImg.Count() );


			//実データ
			foreach ( ImageData id in bdImg.GetEnumerable () )
			{
#if false
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
				bw.Flush ();	//一時書出

#endif

//				WriteImage ( bw, id );
			}
		}

		private void WriteImage ( BinaryWriter bw, ImageData id  )
		{
			//ストリーム読込→書出用
//			const int size = 4096;	//バッファサイズ
//			const int size = 16384;	//バッファサイズ
//			byte [] buffer = new byte [ size ];	//バッファ
			int numBytes = 0;	//書込バイト数

//			using ( BufferedStream msImg = new BufferedStream ( buffer ) )


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

//			Debug.WriteLine ( (uint)msImg.Length );

			//バッファ
			byte [] buffer = new byte [ msImg.Length ];
			numBytes = msImg.Read ( buffer, 0, (int)msImg.Length );

			//書出
			bw.Write ( buffer, 0, buffer.Length );

#if false
				msImg.Seek ( 0, SeekOrigin.Begin );
			while ( ( numBytes = msImg.Read ( buffer, 0, size ) ) > 0 )
			{ 
				bw.Write ( buffer, 0, numBytes );
			}

#endif

			}	//using
		}
	}
}
