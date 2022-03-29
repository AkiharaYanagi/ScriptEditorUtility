using System.Windows.Forms;
using System.IO;


namespace ScriptEditor
{
	public partial class Ctrl_SqcList : UserControl
	{
		//表示編集用コントロール
		private Ctrl_ImageTable ctrl_ImageTable1 = new Ctrl_ImageTable ();
		private EditListbox < SequenceData > ELB_Sqc = new EditListbox<SequenceData> ();
		
		//起動時１回のみの初期化
		public void LoadCtrl ()
		{
			//IDEデザイン時は除く
			if ( run )
			{
				//設定ファイル読込
				settings.Load();

				//ディレクトリ設定
				Directory.SetCurrentDirectory ( FormUtility.UpDir ( settings.LastDirectory ) );
			}

			//タイトルバー
			this.Text = "Sequence List Editor";

			//----------------------------------------------------
			//コントロール追加

			//イメージ
			this.Controls.Add ( ctrl_ImageTable1 );
			ctrl_ImageTable1.Location = new System.Drawing.Point ( 230, 30 );
			ctrl_ImageTable1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

			//エディットリストボックス
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
			ELB_Sqc.Listbox_Changed = () =>
			{
				UpdateCtrl();
			};
			ELB_Sqc.SelectedIndexChanged = () =>
			{
				EditData.SelectedSqc = ELB_Sqc.GetListBox().SelectedIndex;
				ctrl_ImageTable1.ScrollPos ();
			};
			ELB_Sqc.Listbox_Add = () =>
			{
				UpdateCtrl();
			};

			//IO
			ELB_Sqc.SetIOFunc ( SaveSqcDt, LoadSqcDt );

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


		public void SaveSqcDt ( object ob, StreamWriter sw )
		{
			SequenceData sqcDt = (SequenceData)ob;
			sw.Write ( sqcDt.Name + "," );
			sw.Write ( sqcDt.nScript.ToString () + "," );
		}

		public void LoadSqcDt ( StreamReader sr )
		{
			SequenceData sqcDt = new SequenceData ();
			
			string[] str_spl = sr.ReadLine ().Split ( ',' );
			sqcDt.Name = str_spl[0];
			sqcDt.nScript = int.Parse ( str_spl[1] );

			ELB_Sqc.Add ( sqcDt );
		}
	}
}
