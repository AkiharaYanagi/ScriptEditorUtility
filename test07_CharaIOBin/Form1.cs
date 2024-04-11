using System;
using System.Windows.Forms;
using System.Diagnostics;


namespace ScriptEditor
{
	public partial class Form1 : Form
	{
		//対象データ
		private Chara chara = new Chara ();

		//対象ファイル名
		private const string FilenameBin = "charabin.dat";
		private const string Filename_doc = "chara_doc.dat";

		public Form1 ()
		{
			Debug.Write ( "debug" );

			FormUtility.InitPosition ( this );
			InitializeComponent ();

			//テストデータの作成
			TestCharaData testCh = new TestCharaData ();
			testCh.Make ( chara );

			//テスト
			TestChara TestChara = new TestChara ();
			TestChara.Test ( chara );

			LBL_MSG.Text = "Init.";
		}

		//フォルダ
		private void button2_Click ( object sender, EventArgs e )
		{
			FormUtility.OpenCurrentDir();
		}

		//Save
		private void button1_Click ( object sender, EventArgs e )
		{
			SaveCharaBin saveCharaBin = new SaveCharaBin ();
			saveCharaBin.Do ( FilenameBin, chara );
			LBL_MSG.Text = "Save.";
		}

		//Load
		private void button3_Click ( object sender, EventArgs e )
		{
			LoadCharaBin loadCharaBin = new LoadCharaBin ();
			loadCharaBin.Do ( FilenameBin, chara );
			LBL_MSG.Text = loadCharaBin.ErrMsg;
		}

		private void BTN_LOAD_DOC_Click ( object sender, EventArgs e )
		{
			LoadChara loadChara = new LoadChara ();
			loadChara.Do ( Filename_doc, chara );
			LBL_MSG.Text = loadChara.ErrMsg;
		}

		private void BTN_SAVE_DOC_Click ( object sender, EventArgs e )
		{
			SaveChara saveChara = new SaveChara ();
			saveChara.Do ( Filename_doc, chara );
			LBL_MSG.Text = saveChara.ErrMsg;
		}
	}
}
