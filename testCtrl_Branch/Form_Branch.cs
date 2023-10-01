using System;
using System.Windows.Forms;

namespace ScriptEditor
{
	//========================================
	// testCtrl_Branch
	//========================================
	public partial class Form1 :Form
	{
		//設定ファイル
		private Ctrl_Settings ctrl_stg = new Ctrl_Settings ();

		public Form1 ()
		{
			ctrl_stg = (Ctrl_Settings)XML_IO.Load ( typeof ( Ctrl_Settings ) );

			Ctrl_Branch ctrl_Branch1 = new Ctrl_Branch ();
			this.Controls.Add ( ctrl_Branch1 );
			FormUtility.InitPosition ( this );
			InitializeComponent ();
			this.Text = "Test Branch";

			//キャラデータの設定
			Chara ch_test = new Chara ();
			ch_test.BD_Command.New ();
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

			//セーブ・ロード後に等しいか比較
			testChara.Equal ( ch_test, ch_load );

			//エディットに登録
			EditChara.Inst.SetCharaDara ( ch_test );

#endif
			//テストデータ
			foreach ( string name in Enum.GetNames ( typeof ( ENM_ACT ) ) )
			{
				ch_test.behavior.BD_Sequence.Add ( new Action ( name ) );
			}
			foreach ( string name in Enum.GetNames ( typeof ( ENM_CMD ) ) )
			{
				ch_test.BD_Command.Add ( new Command ( name ) );
			}

			//コントロールに渡してテスト
			ctrl_Branch1.SetEnvironment ( ctrl_stg );
			ctrl_Branch1.SetCharaData  ( ch_test );
			ctrl_Branch1.LoadPreData ();
		}
	}
}
