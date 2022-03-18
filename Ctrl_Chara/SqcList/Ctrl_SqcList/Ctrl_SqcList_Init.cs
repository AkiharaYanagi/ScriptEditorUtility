using System.Windows.Forms;
using System.IO;


namespace ScriptEditor
{
	public partial class Ctrl_SqcList : UserControl
	{
		public void LoadCtrl ()
		{
			//IDEデザイン時は除く
			if ( run )
			{
				//設定ファイル読込
				settings.Load();

				//ディレクトリ設定
				Directory.SetCurrentDirectory ( FormUtility.UpDir ( settings.LastDirectory ) );
				textBox1.Text = settings.LastDirectory;
			}

			//タイトルバー
			this.Text = "Sequence List Editor";

			//コントロール追加
			ELB_Sqc.Location = new System.Drawing.Point(0, 27);
			ELB_Sqc.SetData(Data.L_Sqc);
			ELB_Sqc.GetListBox().DisplayMember = "Name";
			this.Controls.Add(ELB_Sqc);

			//データ編集
			EditData.Dt = Data;
			EditData.UpdateAll = UpdateAll;

			//データの設定
			ctrl_ImageTable1.SetEnviroment ( EditData );
			ctrl_ImageTable1.SetData ( ELB_Sqc );

			//イベントの設定
			ELB_Sqc.Changed = () =>
			{
				UpdateCtrl();
			};
			ELB_Sqc.SelectedIndexChanged = () =>
			{
				EditData.SelectedSqc = ELB_Sqc.GetListBox().SelectedIndex;
				ctrl_ImageTable1.ScrollPos ();
			};
			ELB_Sqc.Add = () =>
			{
				UpdateCtrl();
			};

#if false
			//前回のデータ読込
			if ( File.Exists ( settings.LastFilename ) )
			{
				LoadSqcListData loadData = new LoadSqcListData ();
				loadData.Run ( Data, settings.LastFilename );
				LoadImage loadImage = new LoadImage ();
				loadImage.Run ( Data, settings.LastDirectory );

				UpdateAll ();
			}
#endif

//			STS_TXT.Tssl = toolStripStatusLabel1;
//			STS_TXT.Trace ( "Init." );
		}
	}
}
