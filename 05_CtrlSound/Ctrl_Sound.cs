using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;


namespace ScriptEditor
{
	public partial class Ctrl_Sound : UserControl
	{
		//共通SE
		EditListbox < Sound > ELB_Ef_Cmn = new EditListbox < Sound > ();

		//固有SE
		EditListbox < Sound > ELB_Ef_Chr = new EditListbox < Sound > ();

		//固有VC
		EditListbox < Sound > ELB_VC_Chr = new EditListbox < Sound > ();

		public Ctrl_Sound ()
		{
			InitializeComponent ();


			ELB_Ef_Cmn.Location = new Point ( 10, 50 );
			this.Controls.Add ( ELB_Ef_Cmn );

			ELB_Ef_Chr.Location = new Point ( 260, 50 );
			this.Controls.Add ( ELB_Ef_Chr );

			ELB_VC_Chr.Location = new Point ( 510, 50 );
			this.Controls.Add ( ELB_VC_Chr );
		}
	}
}
