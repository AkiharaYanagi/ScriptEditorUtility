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
	public class SaveCharaBin
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
			//データが無かったら何もしない
			if ( chara == null ) { return; }

			//一時メモリストリーム
			MemoryStream ms = new MemoryStream ();
			BinaryWriter bw = new BinaryWriter ( ms, Encoding.ASCII );

			//Command
			
			//個数 [1byte]
			byte nCommand = (byte)chara.BD_Command.Count ();
			bw.Write ( nCommand );

			//実データ [sizeof ( Command ) * n]
			//コマンド
			foreach ( Command cmd in chara.BD_Command.GetEnumerable () )
			{
				bw.Write ( cmd.Name );
				bw.Write ( (byte)cmd.LimitTime );

				//ゲームキー
				byte n = (byte)cmd.ListGameKeyCommand.Count;
				bw.Write ( n );
				foreach ( GameKeyCommand gkc in cmd.ListGameKeyCommand )
				{
					//否定
					bw.Write ( gkc.Not );
					//レバー
					//enum なので 個数は固定 GameKeyData.Lever.LVR_N = 8;
					foreach ( GameKeyData.Lever lvr in gkc.DctLvrSt.Keys )
					{
						bw.Write ( (byte)gkc.DctLvrSt [ lvr ] );
					}
					//ボタン
					//enum なので 個数は固定 GameKeyData.Button.BTN_N = 8;
					foreach ( GameKeyData.Button btn in gkc.DctBtnSt.Keys )
					{
						bw.Write ( (byte)gkc.DctBtnSt [ btn ] ); 
					}
				}
			}

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
		}
	}
}
