using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.IO;


namespace ScriptEditor
{
	using SQC_DRV = Ctrl_SqcList.CTRL_SQC;

	public partial class Ctrl_ImageTable : UserControl
	{
		//データ(シークエンス リストボックス)
		public EditListbox<SequenceData> ELB_Sqc { get; set; } = null;

		//データ編集
		public EditSqcListData EditData { get; set; } = null;

		//アクション指定
		private SQC_DRV flag_sqc_derived = SQC_DRV.ACTION;
		public void SetAction () { flag_sqc_derived = SQC_DRV.ACTION; pB_Sqc1.FlagAction = true; }
		public void SetEffect () { flag_sqc_derived = SQC_DRV.EFFECT; pB_Sqc1.FlagAction = false; }

		//設定ファイル
		public Ctrl_Settings Ctrl_Stgs { get; set; } = new Ctrl_Settings ();


		//コンストラクタ
		public Ctrl_ImageTable()
		{
			InitializeComponent();
		}

		//環境設定
		public void SetEnviroment ( SQC_DRV sqcDrv, EditSqcListData editData, Ctrl_Settings stgs )
		{
			flag_sqc_derived = sqcDrv;
			switch ( flag_sqc_derived )
			{
			case SQC_DRV.ACTION: 
				Tb_ImgDir.Text = stgs.Dir_ImageListAct;
				break;
			case SQC_DRV.EFFECT: 
				Tb_ImgDir.Text = stgs.Dir_ImageListEf;
				break;
			default: break;
			}

			EditData = editData;
			pB_Sqc1.SetEnviroment ( editData );
			pB_Sqc1.Start ( new PB_Sqc.Run () );
			Ctrl_Stgs = stgs;
		}

		//データ設置
		public void SetData ( EditListbox < SequenceData > elb_sd )
		{
			ELB_Sqc = elb_sd;
			pB_Sqc1.ELB_Sqc = elb_sd;
		}

		public void UpdateData ()
		{
			pB_Sqc1.UpdateData ();
		}

		//選択位置にスクロールを移動する
		public void ScrollPos ()
		{
			pB_Sqc1.ScrollPos ( panel1 );
		}

		//ドラッグエンター
		private void Ctrl_ImageTable_DragEnter(object sender, DragEventArgs e)
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

		//ドラッグ＆ドロップ
		private void Ctrl_ImageTable_DragDrop(object sender, DragEventArgs e)
		{
			//ピクチャボックス上の位置
			Point pos = pB_Sqc1.PointToClient ( Cursor.Position );
			int index = pos.Y / ConstSqcListPaint.CH;

			//範囲チェック
			int count = ELB_Sqc.Count ();
			if ( count < 0 ) { return; }
			if ( 0 <= index || index < count )
			{
				//ファイルパスの取得
				string[] filepaths = (string[])e.Data.GetData(DataFormats.FileDrop, false);
				filepaths.OrderBy ( f => f );

				SequenceData sqcDt = ELB_Sqc.GetList () [ index ];
				foreach ( string path in filepaths )
				{
					//データに設定
					//@info リソース使用時にファイル削除ができないのでStreamを用いる
					//Image img = Image.FromFile (path);
					//sqcDt.L_ImgDt.Add ( new ImageData ( sqcDt.Name, img ) );
					FileStream fs = new FileStream ( path, FileMode.Open, FileAccess.Read );
					Image img = Image.FromStream ( fs );

					//サイズ縮小
					Bitmap thumbBmp = new Bitmap ( img );
					Image thumImg = new Bitmap ( thumbBmp, new Size ( 100, 100 ) );

//					sqcDt.BD_ImgDt.Add ( new ImageData ( sqcDt.Name, img ) );
					sqcDt.BD_ImgDt.Add ( new ImageData ( sqcDt.Name, thumbBmp ) );

					fs.Close ();
				}
				
				//選択
				EditData.SelectedSqc = index;
				ELB_Sqc.GetListBox ().SelectedIndex = index;
				//panel1.AutoScrollPosition = new Point(0, index * ConstSqcListPaint.CH );
				ScrollPos ();

				//更新
				EditData.UpdateAll ();
			}
		}

		//パネルの動的スクロール
		private int pos_scrl = 0;
		private void panel1_Scroll(object sender, ScrollEventArgs e)
		{
			//イベントで移動量を保存
			//スクロール移動量を即座に反映する(何もしないとマウスリリース時に反映)

			//縦
			if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
			{
				pos_scrl = e.NewValue;
				int x = panel1.AutoScrollPosition.X;
				panel1.AutoScrollPosition = new Point ( -1*x, pos_scrl );
			}
			//横
			if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
			{
				pos_scrl = e.NewValue;
				int y = panel1.AutoScrollPosition.Y;
				panel1.AutoScrollPosition = new Point ( pos_scrl, -1*y );
			}
		}

		//クリア
		private void Btn_Clear_Click ( object sender, System.EventArgs e )
		{
			EditData.ClearImage ();
		}

		//読込
		private void Btn_Load_Click ( object sender, System.EventArgs e )
		{
			EditData.LoadImageFromDir ( Tb_ImgDir.Text );
		}

		public void LoadImage ()
		{
			EditData.LoadImageFromDir ( Tb_ImgDir.Text );
		}
		public void UpdateImage ()
		{
			EditData.ClearImage ();
			EditData.LoadImageFromDir ( Tb_ImgDir.Text );
		}

		//書出
		private void Btn_Save_Click ( object sender, System.EventArgs e )
		{
			EditData.SaveImageToDir ( Tb_ImgDir.Text );
		}

		//ディレクトリ
		private void textBox1_DragDrop ( object sender, DragEventArgs e )
		{
			string[] filenames = (string[])e.Data.GetData(DataFormats.FileDrop, false);

			//ディレクトリだったら更新
			if ( File.GetAttributes ( filenames[0] ).HasFlag ( FileAttributes.Directory ) )
			{
				Tb_ImgDir.Text = filenames[0];
			}
		}

		private void textBox1_DragEnter ( object sender, DragEventArgs e )
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

		//ボタン：イメージディレクトリ指定
		private void Btn_ImgDir_Click ( object sender, System.EventArgs e )
		{
			OpenFolder_CodePack opF = new OpenFolder_CodePack ();
			if ( opF.OpenFolder () )
			{
				Tb_ImgDir.Text = opF.GetPath ();
				switch ( flag_sqc_derived )
				{
				case SQC_DRV.ACTION: 
					Ctrl_Stgs.Dir_ImageListAct = Tb_ImgDir.Text;
					break;
				case SQC_DRV.EFFECT: 
					Ctrl_Stgs.Dir_ImageListEf = Tb_ImgDir.Text;
					break;
				default: break;
				}
				Ctrl_Stgs.Dir_ImageListAct = Tb_ImgDir.Text;
				XML_IO.Save ( Ctrl_Stgs );
			}
		}

		//ボタン：フォルダ
		private void Btn_Folder_Click ( object sender, System.EventArgs e )
		{
			try
			{
				//ディレクトリだったら更新
				if ( File.GetAttributes ( Tb_ImgDir.Text ).HasFlag ( FileAttributes.Directory ) )
				{
					FormUtility.OpenDir ( Tb_ImgDir.Text );
				}
			}
			catch ( System.Exception exc )
			{
				STS_TXT.Trace ( exc.ToString () );
			}
		}
	}
}
