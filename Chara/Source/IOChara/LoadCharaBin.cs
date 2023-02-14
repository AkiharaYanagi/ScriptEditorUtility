using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Text;


namespace ScriptEditor
{
	public partial class LoadCharaBin
	{
		//-------------------------------------------------------------
		//	コンストラクタ
		//-------------------------------------------------------------
		public LoadCharaBin ()
		{
		}

		//-------------------------------------------------------------
		//	実行
		//-------------------------------------------------------------
		public void Do ( string filepath, Chara chara )
		{
			try
			{
				_Load ( filepath, chara );
			}
			catch ( ArgumentException e )
			{
				//仮データ
				TestChara testChara = new TestChara ();
				testChara.Test ( chara );

				MessageBox.Show ( "LoadChara : 読込データが不適正です\n" + e.Message + "\n" + e.StackTrace );
			}
		}


		//対象ファイルを読み込みキャラデータをバイナリから変換
		private void _Load ( string filepath, Chara chara )
		{
			//ファイルが存在しないとき何もしない
			if ( ! File.Exists ( filepath ) )
			{
				MessageBox.Show ( filepath + "が見つかりません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error );
				throw new ArgumentException ( "ファイルが存在しませんでした。" );
			}

			//拡張子確認
			if ( Path.GetExtension ( filepath ).CompareTo ( ".dat" ) != 0 )
			{
				MessageBox.Show ( filepath + "は拡張子が.datと異なります。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error );
				throw new ArgumentException ( "拡張子が.datと異なります。" );
			}

			//初期化
			chara.Clear ();

			//ファイルストリーム開始
			using ( FileStream fstrm = new FileStream ( filepath, FileMode.Open, FileAccess.Read ) )
			using ( BinaryReader biReaderFile = new BinaryReader ( fstrm, Encoding.UTF8 ) )
			{
				Debug.WriteLine ( "fstrm.Length = " + fstrm.Length );

				//バージョン(uint)
				uint ver = biReaderFile.ReadUInt32 ();

				//サイズ(uint)
				uint size = biReaderFile.ReadUInt32 ();

				//キャラデータ
				LoadBinBehavior ( biReaderFile, chara );
				LoadBinGarnish ( biReaderFile, chara );
				LoadBinCommand ( biReaderFile, chara );
				LoadBinBranch ( biReaderFile, chara );
				LoadBinRoute ( biReaderFile, chara );
			}


		}
	}
}
