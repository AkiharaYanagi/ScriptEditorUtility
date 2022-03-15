﻿using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace ScriptEditor
{
	public partial class Ctrl_ImageTable : UserControl
	{
		//データ(リストボックス)
		public EditListbox<SequenceData> ELB_Sqc { get; set; } = null;

		//データ編集
		public EditSqcListData EditData { get; set; } = null;

		//コンストラクタ
		public Ctrl_ImageTable()
		{
			InitializeComponent();
		}

		public void SetEnviroment ( EditSqcListData editData )
		{
			EditData = editData;
			pB_Sqc1.SetEnviroment ( editData );
			pB_Sqc1.Start ( new PB_Sqc.Run () );
		}

		public void SetData ( EditListbox < SequenceData > elb_sd )
		{
			ELB_Sqc = elb_sd;
			pB_Sqc1.ELB_Sqc = elb_sd;
		}

		public void UpdateData ()
		{
			pB_Sqc1.UpdateData ();
		}

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
					Image img = Image.FromFile (path);
					sqcDt.L_ImgDt.Add ( new ImageData ( sqcDt.Name, img ) );
				}

				//更新
				panel1.AutoScrollPosition = new Point(0, index * ConstSqcListPaint.CH );

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
	}
}
