using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.IO;
using System.Drawing.Imaging;



//@todo フォームアクション　監理をCtrl_SqcListに移項



namespace ScriptEditor
{
	using SQC_DRVD = Ctrl_SqcList.CTRL_SQC;

	public partial class Ctrl_ImageTable : UserControl
	{
		//データ(シークエンス リストボックス)
		public EditListbox<SequenceData> ELB_Sqc { get; set; } = new EditListbox<SequenceData>();

		//データ編集
		public EditSqcListData EditData { get; set; } = new EditSqcListData ();

		//アクション/エフェクト指定
		private SQC_DRVD flag_sqc_derived = SQC_DRVD.ACTION;
		public void SetAction () { flag_sqc_derived = SQC_DRVD.ACTION; pB_Sqc1.FlagAction = true; }
		public void SetEffect () { flag_sqc_derived = SQC_DRVD.EFFECT; pB_Sqc1.FlagAction = false; }

		public bool IsAction () { return flag_sqc_derived == SQC_DRVD.ACTION; }
		public bool IsEffect () { return flag_sqc_derived == SQC_DRVD.EFFECT; }

		//アクション設定フォーム
		private readonly Form_Action form_act = new Form_Action();

		//設定ファイル
		public Ctrl_Settings Ctrl_Stgs { get; set; } = new Ctrl_Settings ();


		//コンストラクタ
		public Ctrl_ImageTable()
		{
			InitializeComponent();
		}

		//環境設定
		public void SetEnviroment ( SQC_DRVD sqcDrvd, EditSqcListData editSLData, Ctrl_Settings stgs )
		{
			flag_sqc_derived = sqcDrvd;
			switch ( flag_sqc_derived )
			{
			case SQC_DRVD.ACTION: 
				Tb_ImgDir.Text = stgs.Dir_ImageListAct;
				break;
			case SQC_DRVD.EFFECT: 
				Tb_ImgDir.Text = stgs.Dir_ImageListEf;
				break;
			default: break;
			}

			EditData = editSLData;
			pB_Sqc1.SetEnviroment ( editSLData, pt=>form_act.ShowForm(pt) );
			pB_Sqc1.Start ( new PB_Sqc.Run () );
			Ctrl_Stgs = stgs;

			form_act.SetEnvironment ( editSLData );
		}

		//初期コントロール設置
		public void LoadCtrl ( EditListbox < SequenceData > elb_sd )
		{
			ELB_Sqc = elb_sd;
			pB_Sqc1.ELB_Sqc = elb_sd;
			pB_Sqc1.Pnl = panel1;
		}

		//シークエンスリスト更新
		public void ResetItems ()
		{ 
			form_act.ResetItems ();
		}

		//キャラデータ設置
		public void SetCharaData ( Chara ch )
		{
			form_act.SetCharaData ( ch );
		}

		//コンペンド指定
		public void SetCompend ( Compend cmpd )
		{
			form_act.SetCompend ( cmpd );

			//コンペンドからイメージリストを作成
		}

		public void Assosiate ()
		{
			form_act.Assosiate ();
		}

		//更新
		public void UpdateData ()
		{
			pB_Sqc1.UpdateData ();
			form_act.UpdateData ();
		}

		public void UpdateSize ()
		{
			pB_Sqc1.UpdateSize ();
		}

		public void Disp ()
		{
//			pB_Sqc1.Invalidate ();
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
					//イメージデータに設定

					//保存用パス
					string save_path = EditChara.Inst.Settings.LastFilepath;
					string save_dir = Path.GetDirectoryName ( save_path );
					string chName = Path.GetFileNameWithoutExtension ( save_path );
					string dir_img = save_dir + "\\" + chName + "_img";


					//@info リソース使用時にファイル削除ができないのでStreamを用いる
					//Image img = Image.FromFile (path);
					//sqcDt.L_ImgDt.Add ( new ImageData ( sqcDt.Name, img ) );
					using (FileStream fs = new FileStream ( path, FileMode.Open, FileAccess.Read ))
					{
					Image img = Image.FromStream ( fs );

					//サムネイル サイズ縮小
					Bitmap thumImg = new Bitmap ( img, new Size ( 100, 100 ) );

					//個数で名前を指定
					int n = sqcDt.BD_ImgDt.Count();
					if ( 99 < n ) { n = 0; }	//※100以上は桁数の問題もあり非対応
					string name = sqcDt.Name + "_" + n.ToString ( "00" ) + ".png";


					//イメージディレクトリのファイルに書出
					string img_save_path = dir_img + "\\" + name;
					Directory.CreateDirectory ( dir_img );
					img.Save ( img_save_path, ImageFormat.Png );

					//イメージデータ
//					ImageData imgdt = new ImageData ( name, img );
					ImageData imgdt = new ImageData ( name );	//イメージは仮■で埋め
					imgdt.Path = img_save_path;
					imgdt.Thumbnail = thumImg;
					sqcDt.BD_ImgDt.Add ( imgdt );
					}
					
				}

				//メインに反映
				if ( IsAction () )
				{
					EditData.ApplyData_Action ();
				}
				if ( IsEffect () )
				{
					EditData.ApplyData_Effect ();
				}



				//ピクチャボックスのサイズ変更
				pB_Sqc1.UpdateSize ();
				
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

		//ドラッグエンタ
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
				case SQC_DRVD.ACTION: 
					Ctrl_Stgs.Dir_ImageListAct = Tb_ImgDir.Text;
					break;
				case SQC_DRVD.EFFECT: 
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
