using System;
using System.Windows.Forms;

namespace ScriptEditor
{
	//========================================
	// testCtrl_SqcList
	//========================================
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			FormUtility.InitPosition ( this );
			InitializeComponent ();

			ctrl_SqcList1.run = true;
			ctrl_SqcList1.Init();
		}
	}
}
