using System;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace ScriptEditor
{
	//========================================
	// testCtrl_SqcList
	//========================================
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			Ctrl_SqcList ctrl_SqcList1 = new Ctrl_SqcList ();
			this.Controls.Add ( ctrl_SqcList1 );
			FormUtility.InitPosition ( this );
			InitializeComponent ();

			this.Text = "test_Ctrl_SqcList";
			ctrl_SqcList1.run = true;
			ctrl_SqcList1.LoadCtrl();
		}
	}
}
