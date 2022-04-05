using System;
using System.Windows.Forms;

namespace ScriptEditor
{
	//========================================
	// testCtrl_Route
	//========================================
	public partial class Form1 :Form
	{
		//設定ファイル
		private Ctrl_Settings ctrl_stg = new Ctrl_Settings ();

		public Form1 ()
		{
			//設定ファイル
			ctrl_stg = ( Ctrl_Settings )XML_IO.Load ( typeof ( Ctrl_Settings ) );

			//コントロール初期化
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
			//テストデータ
			foreach ( string name in Enum.GetNames ( typeof ( ENM_BRC ) ) )
			{
				ch_test.BD_Branch.Add ( new Branch ( name ) );
			}

			//コントロールに渡してテスト
			ctrl_Route1.SetEnvironment ( ctrl_stg );
			ctrl_Route1.SetCharaData  ( ch_test );

			ctrl_Route1.LoadData ();
		}
	}
}
