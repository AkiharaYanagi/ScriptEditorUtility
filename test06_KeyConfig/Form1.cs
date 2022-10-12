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
			Ctrl_KeyConfig keyConfig = new Ctrl_KeyConfig ();
			this.Controls.Add ( keyConfig );
		}


		private void Form1_FormClosed ( object sender, FormClosedEventArgs e )
		{
		}
	}
}
