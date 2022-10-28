using System;
using System.Windows.Forms;

namespace ScriptEditor
{
	public partial class Form1 : Form
	{
		private Chara chara = new Chara ();

		public Form1 ()
		{
			FormUtility.InitPosition ( this );
			InitializeComponent ();

			LoadChara loadChara = new LoadChara ();
			loadChara.Do ( "chara.dat", chara );

			label1.Text = "Load.";
		}

		private void button1_Click ( object sender, EventArgs e )
		{
			SaveCharaBin saveCharaBin = new SaveCharaBin ();
			saveCharaBin.Do ( "charabin.dat", chara );
			label1.Text = "Save.";
		}

		private void button2_Click ( object sender, EventArgs e )
		{
			FormUtility.OpenCurrentDir();
		}
	}
}
