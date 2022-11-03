using System;
using System.Diagnostics;
using System.IO;
using System.Text;


namespace ScriptEditor
{
	//==================================================================================
	//	SaveCharaBin
	//		キャラデータをbinaryで.datファイルに保存
	//		Document形式を介さない
	//==================================================================================
	public partial class SaveCharaBin
	{
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
			MemoryStream ms = new MemoryStream ();
			BinaryWriter bw = new BinaryWriter ( ms, Encoding.UTF8 );

			//test
			int i = 0x01234567;
			bw.Write ( i );
			bw.Write ( 0x000F0000 );

			//--------------------------------------------------------
			//chara 各種データ書出
			SaveBinBehavior ( bw, chara );	//behavior
			SaveBinGarnish ( bw, chara );	//garnish
			SaveBinCommand ( bw, chara );	//Command
			SaveBinBranch ( bw, chara );	//Branch
			SaveBinRoute ( bw, chara );		//Route

			//--------------------------------------------------------
			bw.Flush ();


			//最後にファイルに書出
			FileStream fs = new FileStream ( filepath, FileMode.Create, FileAccess.Write );
			BinaryWriter bwFl = new BinaryWriter( fs );

			//バージョン
			bwFl.Write ( (byte)CONST.VER );

			//サイズ4,294,967,296[byte]まで
			bwFl.Write ( (uint) ms.Length );

			const int SIZE = 4096;	//バッファサイズ
			byte[] buf = new byte [ SIZE ];	//バッファ
			int numBytes = 0;

			ms.Seek ( 0, SeekOrigin.Begin );
			while ( ( numBytes = ms.Read ( buf, 0, SIZE ) ) > 0 )
			{
				bwFl.Write ( buf, 0, numBytes );
			}


			bw.Close ();
			ms.Close ();
		}
	}
}
