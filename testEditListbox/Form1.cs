using System.Windows.Forms;

namespace ScriptEditor
{
	public partial class Form1 :Form
	{
		private int i = 0;

		public Form1 ()
		{
			FormUtility.InitPosition ( this );
			InitializeComponent ();

			EditListbox < int > EL_i = new EditListbox<int> ();
			Controls.Add ( EL_i );

			editListbox_01.Make = Make_Fc;
		}

		public object Make_Fc ()
		{
			return new Action ( (i ++).ToString () );
		}
	}
}
