using System;
using System.Windows.Forms;

namespace ScriptEditor
{
	public partial class Form1 :Form
	{
		private string filename = "testChara.dat";

		public Form1 ()
		{
			FormUtility.InitPosition ( this );
			InitializeComponent ();

			Chara ch_test = new Chara ();
			TestChara testChara = new TestChara ();
			testChara.Test ( ch_test );

			SaveChara saveChara = new SaveChara ( filename, ch_test );
			Chara ch_load = new Chara ();
			LoadChara loadChara = new LoadChara ( filename, ch_load );
			testChara.TestCopyChara ( ch_test, ch_load );

			//@todo テストデータでブランチの値を設定する

			ctrl_Branch1.SetCharaData  ( ch_test );
		}
	}
}
