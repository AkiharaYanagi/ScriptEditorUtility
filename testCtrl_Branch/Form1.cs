using System;
using System.Windows.Forms;

namespace ScriptEditor
{
	//========================================
	// testCtrl_Branch
	//========================================
	public partial class Form1 :Form
	{
		private string filename = "testChara.dat";

		public Form1 ()
		{
			FormUtility.InitPosition ( this );
			InitializeComponent ();
			this.Text = "Test Branch";

			//キャラデータの設定
			Chara ch_test = new Chara ();
			TestChara testChara = new TestChara ();
			testChara.Test ( ch_test );

			SaveChara saveChara = new SaveChara ( filename, ch_test );
			Chara ch_load = new Chara ();
			LoadChara loadChara = new LoadChara ( filename, ch_load );

			//セーブ・ロード後に等しいか比較
			testChara.Equal ( ch_test, ch_load );

			//エディットに登録
			EditChara.Inst.SetCharaDara ( ch_test );

			//コントロールに渡してテスト
			ctrl_Branch1.SetCharaData  ( ch_test );
		}
	}
}
