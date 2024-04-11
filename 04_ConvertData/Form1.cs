using System.Windows.Forms;
using System.IO;
using System.Drawing;


using ScriptEditor;
using ScriptEditor_old;


namespace ConvertData
{
	//
	//	スクリプトデータ変換
	//
	//	旧 読み込み関連を_oldとして本プロジェクトに残し、
	//	新 書き出し機能で保存する

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

				//開始
				label1.Text = "Start.";

				Chara ch = new Chara ();

				//@info
				//読み込み時は、Charaデータの対応部分に値を指定するだけなので
				//新しい構造で旧データを保持している状態になる
				ScriptEditor_old.LoadChara loadChara_old = new ScriptEditor_old.LoadChara ();
				loadChara_old.Do ( filepaths[0], ch );

				//新規構造で保存
				SaveChara saveChara = new SaveChara ();
				saveChara.Do ( new_path, ch );

				//テスト
				TestChara testChara = new TestChara ();
				testChara.Test ( ch );

				//終了
				label1.Text = "OK.";
			}
		}

	}
}
