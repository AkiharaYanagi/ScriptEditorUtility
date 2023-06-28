using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;


namespace ScriptEditor
{
	public partial class _Ctrl_Compend : UserControl
	{
		private _Ctrl_Image ctrl_image = new _Ctrl_Image ();

		public _Ctrl_Compend ()
		{
			InitializeComponent ();


			Controls.Add ( ctrl_image );
			ctrl_image.Location = new Point ( 200, 300 );
		}

		public void SetEnviron ( EditCompend ec )
		{
			this.BackColor = Color.AliceBlue;
			this.Size = new Size ( 1000, 800 );
			ctrl_image.SetEnviron ( ec );
		}
	}
}
