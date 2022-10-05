using System;
using System.Windows.Forms;
using System.Threading.Tasks;

using SlimDX.DirectInput;


namespace ScriptEditor
{
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			FormUtility.InitPosition ( this );
			InitializeComponent ();

			Ctrl_KeyConfig keyConfig = new Ctrl_KeyConfig ();
			this.Controls.Add ( keyConfig );

			Action act = Thread;
			Task task = Task.Run ( act );
		}


		public static void Thread ()
		{
			Timer timer = new Timer ();
			timer.Tick += new EventHandler ( UpdateData );
			timer.Interval = 16;
			timer.Start ();
		}

		private DirectInput dInput = new DirectInput ();
		public static void UpdateData ( object sender, EventArgs e )
		{

		}
	}
}
