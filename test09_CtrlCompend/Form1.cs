using System;
using System.Windows.Forms;


namespace ScriptEditor
{
	public partial class Form1 : Form
	{
		private _Ctrl_Image ctrl_image = new _Ctrl_Image ();

		public Form1 ()
		{
			FormUtility.InitPosition ( this );
			InitializeComponent ();

			Controls.Add ( ctrl_image );
		}
	}
}
