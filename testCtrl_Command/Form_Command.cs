using System;
using System.Windows.Forms;

namespace ScriptEditor
{
	//========================================
	// testCtrl_Command
	//========================================
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			this.Controls.Add ( new Ctrl_CmdList () );
			FormUtility.InitPosition ( this );
			InitializeComponent ();
		}
	}
}
