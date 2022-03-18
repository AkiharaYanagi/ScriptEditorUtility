using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;

namespace ScriptEditor
{
	public partial class Ctrl_SqcList : UserControl
	{
		private void フォルダToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			FormUtility.OpenCurrentDir();
		}

		private void 上書保存ToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			//イメージ保存ディレクトリと、１つ上のデータ保存ディレクトリ
//			string ImageDir = settings.LastDirectory;
			string ImageDir = "ImageDir";
			string DataDir = ImageDir.Substring ( 0, ImageDir.LastIndexOf ( "\\" ) + 1 );

			//リスト
			SaveSqcListData saveData = new SaveSqcListData();
			saveData.Run ( Data, DataDir + "\\data.txt" );

			//イメージ
			SaveImage saveImage = new SaveImage ();
			saveImage.Run ( Data, ImageDir );

			//設定(ディレクトリ)
			Directory.SetCurrentDirectory ( DataDir );
//			settings.Save ();
		}

		private void 別名保存ToolStripMenuItem_Click ( object sender, System.EventArgs e )
		{

		}


		private void 読込ToolStripMenuItem_Click ( object sender, System.EventArgs e )
		{
#if false
			//-------------------------------------------------------
			// WindowsAPICodePackを利用したフォルダダイアログ
			//-------------------------------------------------------
			CommonOpenFileDialog cofd = new CommonOpenFileDialog ();
			cofd.IsFolderPicker = true;
			if ( cofd.ShowDialog () == CommonFileDialogResult.Ok )
			{

			}
#endif
			OpenFileDialog opfDlg = new OpenFileDialog();
			if (opfDlg.ShowDialog() == DialogResult.OK)
			{
				Data.Clear();
				LoadSqcListData ld = new LoadSqcListData();
				ld.Run(Data, opfDlg.FileName);
				UpdateAll();
			}
		}

	}
}
