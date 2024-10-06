using System;
using System.Windows.Forms;

using System.Media;



namespace ScriptEditor
{
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			FormUtility.InitStartDisplayOnMouse ( this );
			InitializeComponent ();

			//Sound snd = new Sound ();
			//snd.Load ();

			//SoundPlayer snd = new SoundPlayer ( "剣撃クロスゾーン1.wav" );
			//snd.Play ();

			Ctrl_Sound ctrl_sound = new Ctrl_Sound ();
			this.Controls.Add ( ctrl_sound );
		}
	}
}
