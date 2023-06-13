using System.Windows.Forms;
using System.IO;


namespace ScriptEditor
{
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			FormUtility.InitPosition ( this );
			InitializeComponent ();
		}

		private void Form1_DragEnter ( object sender, DragEventArgs e )
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effect = DragDropEffects.Copy;
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}
		}

		private void Form1_DragDrop ( object sender, DragEventArgs e )
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				string[] filepaths = (string[])e.Data.GetData(DataFormats.FileDrop, false);
				string path = filepaths [ 0 ];
				string filename = Path.GetFileName ( path );
				string dirname = Path.GetDirectoryName ( path );
				string new_path = dirname + "\\new_" + filename;

				Chara ch = new Chara ();

				LoadChara_old loadChara_old = new LoadChara_old ();
				loadChara_old.Do ( filepaths[0], ch );

				SaveChara saveChara = new SaveChara ();
				saveChara.Do ( new_path, ch );

#if false
				LoadCharaBin_old loadCharaBin_old = new LoadCharaBin_old ();
				loadCharaBin_old.Do ( filepaths[0], ch );

				SaveCharaBin saveCharaBin = new SaveCharaBin ();
				saveCharaBin.Do ( new_path, ch );

				LoadCharaBin loadCharaBin = new LoadCharaBin ();
				loadCharaBin.Do ( new_path, ch );
#endif
			}
		}

	}
}
