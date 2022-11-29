using System;
using System.Windows.Forms;
using System.Diagnostics;


namespace ScriptEditor
{
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			FormUtility.InitPosition ( this );
			InitializeComponent ();

			Debug.WriteLine ( "Form1();\n" );
			this.Controls.Add ( new Ctrl_KeyConfig () );
		}
	}
}
