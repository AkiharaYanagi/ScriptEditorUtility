using System;
using System.Windows.Forms;

namespace ScriptEditor
{
	//========================================
	// testCtrl_Route
	//========================================
	public partial class Form1 :Form
	{
//		private string filename = "testChara.dat";

		public Form1 ()
		{
			Ctrl_Route ctrl_Route1 = new Ctrl_Route ();
			this.Controls.Add ( ctrl_Route1 );
			FormUtility.InitPosition ( this );
			InitializeComponent ();


			//テストデータの作成
			Chara ch_test = new Chara ();

#if false
			TestCharaData tcd = new TestCharaData ();
			tcd.Make ( ch_test );

			//キャラデータのテスト
			TestChara testChara = new TestChara ();
			testChara.Test ( ch_test );

			//ファイルIOテスト
			SaveChara saveChara = new SaveChara ( filename, ch_test );
			Chara ch_load = new Chara ();
			LoadChara loadChara = new LoadChara ( filename, ch_load );
			testChara.TestCopyChara ( ch_test, ch_load );
#endif

			//コントロールに渡してテスト
			ctrl_Route1.SetCharaData  ( ch_test );
		}
	}
}
