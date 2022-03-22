using System.Windows.Forms;
using System.IO;
using Microsoft.WindowsAPICodePack.Dialogs;


namespace ScriptEditor
{
	public partial class Ctrl_SqcList : UserControl
	{
		//リストボックスの更新
		private void UpdateElb()
		{
			foreach (SequenceData sqcDt in ELB_Sqc.GetList())
			{
				sqcDt.Name = sqcDt.Sqc.Name;
			}
			ELB_Sqc.ResetItems();
			ELB_Sqc.Invalidate();
		}

		//ピクチャボックスの更新
		private void UpdateCtrl ()
		{
			ctrl_ImageTable1.UpdateData();
			ctrl_ImageTable1.Invalidate();
		}

		//全体更新
		private void UpdateAll()
		{
			UpdateElb();
			UpdateCtrl ();

			this.Invalidate ();
		}

		//--------------------------------------------
		//ドラッグ＆ドロップ

		//イメージディレクトリ
		private void textBox1_DragEnter(object sender, DragEventArgs e)
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

		private void textBox1_DragDrop(object sender, DragEventArgs e)
		{
			string[] filenames = (string[])e.Data.GetData(DataFormats.FileDrop, false);

			//ディレクトリだったら更新
			if ( File.GetAttributes ( filenames[0] ).HasFlag ( FileAttributes.Directory ) )
			{
				textBox1.Text = filenames[0];
//				settings.LastDirectory = filenames[0];
			}
		}

		//イメージディレクトリの指定
		private void Btn_ImgDir_Click ( object sender, System.EventArgs e )
		{
			//-------------------------------------------------------
			// WindowsAPICodePackを利用したフォルダダイアログ
			//-------------------------------------------------------
			CommonOpenFileDialog cofd = new CommonOpenFileDialog();
			cofd.IsFolderPicker = true;
			if (cofd.ShowDialog() == CommonFileDialogResult.Ok)
			{
				textBox1.Text = cofd.FileName;

//				settings.LastDirectory = cofd.FileName;
			}
#if false
			OpenFolder_CodePack opF = new OpenFolder_CodePack ();
			if ( opF.OpenFolder () )
			{
				textBox1.Text = opF.GetPath ();
			}
#endif
		}

		//イメージの保存
		private void Btn_SaveImage_Click ( object sender, System.EventArgs e )
		{
			SaveImage saveImage = new SaveImage ();
			saveImage.Run ( Data, textBox1.Text );
		}

		//イメージのクリア
		private void Btn_ClearImage_Click ( object sender, System.EventArgs e )
		{
			EditData.ClearImage ();
		}

		//イメージの読込
		private void Btn_LoadImage_Click ( object sender, System.EventArgs e )
		{
			if ( Directory.Exists ( textBox1.Text ) )
			{
				LoadImage li = new LoadImage ();
				li.Run ( Data, textBox1.Text );
				EditData.UpdateAll ();
			}
		}
	}
}
