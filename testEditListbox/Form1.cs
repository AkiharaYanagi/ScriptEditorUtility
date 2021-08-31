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
		}

		public object Make_Fc ()
		{
			return new Action ( (i ++).ToString () );
		}
	}
}
