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
		private const string Filename = "charabin.dat";

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

			TestChara.TestIO_Document ( chara );
		}

		//Save
		private void button1_Click ( object sender, EventArgs e )
		{
			SaveCharaBin saveCharaBin = new SaveCharaBin ();
			saveCharaBin.Do ( Filename, chara );
			label1.Text = "Save.";
		}

		//フォルダ
		private void button2_Click ( object sender, EventArgs e )
		{
			FormUtility.OpenCurrentDir();
		}

		//Load
		private void button3_Click ( object sender, EventArgs e )
		{
			LoadCharaBin loadCharaBin = new LoadCharaBin ();
			loadCharaBin.Do ( Filename, chara );
			label1.Text = "Load.";
		}
	}
}
