using System;
using System.Windows.Forms;

namespace ScriptEditor
{
	//========================================
	// testCtrl_Route
	//========================================
	public partial class Form1 :Form
	{
		private string filename = "testChara.dat";

		public Form1 ()
		{
			FormUtility.InitPosition ( this );
			InitializeComponent ();

			//キャラデータの設定
			Chara ch_test = new Chara ();
			TestCharaData tcd = new TestCharaData ();
			tcd.Make ( ch_test );

			TestChara testChara = new TestChara ();
			testChara.Test ( ch_test );

			//ファイルIOテスト
			SaveChara saveChara = new SaveChara ( filename, ch_test );
			Chara ch_load = new Chara ();
			LoadChara loadChara = new LoadChara ( filename, ch_load );
			testChara.TestCopyChara ( ch_test, ch_load );

			//Editからの操作
			EditChara.Inst.SetCharaDara ( ch_test );
			EditChara.Inst.AddBranch ();
			EditChara.Inst.AddRoute ( "地上通常技", "立ち状態から出る通常技全般" );
			Route rut = EditChara.Inst.Chara.BD_Route.Get ( 0 );
			Branch0 br = EditChara.Inst.Chara.BD_Branch.Get ( 0 );
			rut.BD_Branch.Add ( br );

			//コントロールに渡してテスト
			ctrl_Route1.SetCharaData  ( ch_test );
		}
	}
}
