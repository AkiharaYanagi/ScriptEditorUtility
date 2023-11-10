using System;
using System.Windows.Forms;

namespace ScriptEditor
{
	//========================================
	// testCtrl_Command
	//========================================
	public partial class Form1 : Form
	{
		//設定ファイル
		private Ctrl_Settings ctrl_stg = new Ctrl_Settings ();

		public Form1 ()
		{
			ctrl_stg = (Ctrl_Settings)XML_IO.Load ( typeof ( Ctrl_Settings ) );

			Ctrl_CmdList ctrl_cmdlst = new Ctrl_CmdList ();
			this.Controls.Add ( ctrl_cmdlst );

			FormUtility.InitPosition ( this );
			InitializeComponent ();

			//------------------------------------
			//test
			Chara ch = new Chara ();
			TestCharaData testCharaData = new TestCharaData ();
			testCharaData.Make ( ch );
			TestChara testChara = new TestChara ();
//			testChara.Test ( ch );
			//------------------------------------

			ctrl_cmdlst.SetEnvironment ( ctrl_stg );
			ctrl_cmdlst.LoadPreData ();
		}
	}
}
