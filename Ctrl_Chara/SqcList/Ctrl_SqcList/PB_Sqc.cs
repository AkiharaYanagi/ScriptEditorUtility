﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;

namespace ScriptEditor
{
	//System.TierとSystem.Windows.Forms.Timerで曖昧なので明示的宣言
	using STimer = System.Timers.Timer;

	//シークエンスデータの画像表示コントロール
	public class PB_Sqc : PictureBox
	{
		//対象データ
		public EditListbox < SequenceData > ELB_Sqc { get; set; } = null;
		
		//データ編集
		public EditSqcListData EditData { get; set; } = null;

		//入力フォーム
		public bool FlagAction { get; set; } = false;
		private Form_Action form_act = new Form_Action();

		private ContextMenuStrip contextMenuStrip1;
		private System.ComponentModel.IContainer components;
		private ToolStripMenuItem toolStripMenuItem1;
		private ToolStripMenuItem toolStripMenuItem2;
		private ToolStripMenuItem toolStripMenuItem3;

		//IDE表示
		public class Run {};
		private Run run { get; set; } = null;

		//カーソル判定用別スレッド
		STimer tmr = new STimer( 16 );

		//コンストラクタ
		public PB_Sqc ()
		{
			InitializeComponent ();

			//入力フォーム
			form_act.StartPosition = FormStartPosition.Manual;

			//カーソル判定用別スレッド
			tmr.Elapsed += DDMouseMove;
		}

		public void Start ( Run r )
		{
			run = r;
			if ( run is null ) { return; }

			tmr.Start ();
		}

		//コンポーネント初期化
		private void InitializeComponent ()
		{
			this.components = new System.ComponentModel.Container();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(99, 70);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(98, 22);
			this.toolStripMenuItem1.Text = "削除";
			this.toolStripMenuItem1.Click += new System.EventHandler(this.削除toolStripMenuItem1);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(98, 22);
			this.toolStripMenuItem2.Text = "前";
			this.toolStripMenuItem2.Click += new System.EventHandler(this.前toolStripMenuItem2);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(98, 22);
			this.toolStripMenuItem3.Text = "次";
			this.toolStripMenuItem3.Click += new System.EventHandler(this.次toolStripMenuItem3);
			// 
			// PB_Sqc
			// 
			this.ContextMenuStrip = this.contextMenuStrip1;
			this.contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

		}

		//環境
		public void SetEnviroment ( EditSqcListData editData )
		{
			EditData = editData;
			form_act.SetEnvironment ( editData );
		}

		//更新
		public void UpdateData ()
		{
			//描画サイズ・縦
			const int CH = ConstSqcListPaint.CH;
			int n = ELB_Sqc.Count();
			this.Height = n < 1 ? 200 : 201 + (n * CH);

			//描画サイズ・横
			const int CW = ConstSqcListPaint.CW;
			int max = 0;
			foreach ( SequenceData sd in ELB_Sqc.GetList ())
			{
				if ( max < sd.BD_ImgDt.Count() ) { max = sd.BD_ImgDt.Count(); }
			}
			this.Width = max < 1 ? 200 : CW + (max * CW);

			//名前の更新
			EditData.UpdateName ();
		}

		//関連付け
		public void Assosiate ( SequenceData sqcDt )
		{
			form_act.tB_Setter1.SetFunc = (s)=>{sqcDt.Name = s;};
		}

		//スクロール移動
		public void ScrollPos ( Panel pnl )
		{
			int y = EditData.SelectedSqc * ConstSqcListPaint.CH;
			int py = - 1 * pnl.AutoScrollPosition.Y;

			//範囲外なら
			if ( py < y - 400 || y  < py )
			{
				pnl.AutoScrollPosition = new Point ( 0, y );
			}
		}

		//描画
		protected override void OnPaint ( PaintEventArgs pe )
		{
			if ( ELB_Sqc is null ) { return; }
			if ( EditData is null ) { return; }

			Graphics g = pe.Graphics;

			int n = ELB_Sqc.Count();
			int W = this.Width;
			int H = this.Height;
			const int CW = ConstSqcListPaint.CW;
			const int CH = ConstSqcListPaint.CH;
			const int BX = 100;	//基準位置
			int slctSqc = EditData.SelectedSqc;
			int slctImg = EditData.SelectedImage;

			using ( Font FONT0 = new Font ( "Meiryo", 12.0f ) )
			using ( Font FONT1 = new Font ( "Meiryo", 10.0f ) )
			{
				//選択
				g.FillRectangle ( Brushes.LightBlue, 0, slctSqc * CH, W, CH );

				//罫線(縦)
				g.DrawLine ( Pens.Black, new Point ( 0, 0 ), new Point ( 0, H ) );
				g.DrawLine ( Pens.Black, new Point ( BX, 0 ), new Point ( BX, H ) );

				//罫線(縦)(仕切)
				int nHorizon = W / CW;
				for ( int i = 0; i < nHorizon; ++ i )
				{
					int x = BX + CW + i * CW;
					g.DrawLine ( Pens.Gainsboro, new Point ( x, 0 ), new Point ( x, H ) );
				}

				//罫線(横)
				int NLine = 2 + ELB_Sqc.Count ();
				for ( int i = 0; i < NLine; ++ i )
				{
					g.DrawLine ( Pens.Black, new Point ( 0, i * CH ), new Point ( W, i * CH ) );
				}

				//データ
				int ns = 0;
				foreach ( SequenceData sqcDt in ELB_Sqc.GetList () )
				{
					int y = ns * CH;

					//シークエンスデータ
					DrawSequence(g, sqcDt.Sqc.Name, FONT0, 0 + y);
					DrawSequence(g, "[" + sqcDt.nScript.ToString() + "]", FONT0, 20 + y);
					if ( FlagAction )
					{
						Action act = (Action)sqcDt.Sqc;
						DrawSequence(g, "(" + act.Category.ToString() + ")", FONT1, 40 + y);
					}

					//画像
					int nI = 0;
					foreach ( ImageData imgDt in sqcDt.BD_ImgDt.GetEnumerable() )
					{
						g.DrawImage ( imgDt.Img, new Rectangle (CW + nI++ * CW, y, CW, CH ) );
					}

					++ ns;
				}

				//イメージ選択位置
				int Img_x = CW + CW * slctImg;
				int Img_y = CH * slctSqc;
				if ( Img_y < EditData.GetSequenceData()?.BD_ImgDt.Count () )
				{
					g.DrawRectangle ( Pens.Red, new Rectangle ( Img_x, Img_y, CW, CH ) );
				}
				
				//動的カーソル位置
				dpt = PointToClient(new Point(0, 0));
				g.FillRectangle(Brushes.Red, CursorPt.X, CursorPt.Y, 10, 10);
			}

			base.OnPaint ( pe );
		}

		private void DrawSequence ( Graphics g, string str, Font f, float y )
		{
			RectangleF rectf = new RectangleF(new PointF(0, y), new SizeF(200, 20));
			g.DrawString( str, f, Brushes.Black, rectf );
		}


		//マウス押下
		protected override void OnMouseClick ( MouseEventArgs e )
		{
			//左
			if ( e.Button == MouseButtons.Left )
			{
				Point pos = PointToClient ( Cursor.Position );
				int pt_x = ( pos.X - ConstSqcListPaint.CW ) / ConstSqcListPaint.CW;
				int pt_y = pos.Y / ConstSqcListPaint.CH;
				int n = ELB_Sqc.Count();
				int selectedSqcIndex = EditData.SelectedSqc;

				//データ範囲内 かつ シークエンス列
				if ( pt_y < n )
				{
					//選択済み
					if ( selectedSqcIndex == pt_y && pos.X < ConstSqcListPaint.CW )
					{
						//アクションのみ
						if ( FlagAction )
						{
							//入力フォーム
							SequenceData sqcDt = ELB_Sqc.Get ();
							form_act.Location = PointUt.PtAdd ( Cursor.Position, new Point(20, 20) );
							form_act.Assosiate ( sqcDt );
							form_act.Show();
						}
					}
					else
					{
						//選択
						EditData.SelectedSqc = pt_y;
						EditData.SelectedImage = pt_x;
						ELB_Sqc.GetListBox ().SelectedIndex = pt_y;
					}
				}
				this.Invalidate ();
			}

			base.OnMouseClick ( e );
		}

		//マウス押込イベント
		//@info コンテキストメニューより先に発生
		protected override void OnMouseDown ( MouseEventArgs e )
		{
			//右
			if ( e.Button == MouseButtons.Right )
			{
				EditData.SetPt ( PosToPt ( Cursor.Position ) );
				contextMenuStrip1.Show ( Cursor.Position );
			}
			base.OnMouseDown ( e );
		}

		private Point PosToPt ( Point pos )
		{
			Point p0 = PointToClient ( new Point (0, 0) );
			Point p = PointToClient ( Cursor.Position );
//			STS_TXT.Trace ( p.ToString () );
			int x = (p.X - 100)/100;
			int y = p.Y / 100;
			return new Point ( x, y );
		}
		
		private Point CursorPt = new Point ();
		private Point dpt = new Point ();

		public void DDMouseMove ( object sender, ElapsedEventArgs e )
		{
			Point cp = Cursor.Position;
			CursorPt = PointUt.PtAdd ( cp, dpt );

			Invalidate ();
		}

		//イベント
		private void 削除toolStripMenuItem1 ( object sender, EventArgs e )
		{
			EditData.Remove ();
			EditData.UpdateAll ();
		}

		private void 前toolStripMenuItem2 ( object sender, EventArgs e )
		{
			EditData.Prev ();
			EditData.UpdateAll ();
		}

		private void 次toolStripMenuItem3 ( object sender, EventArgs e )
		{
			EditData.Next ();
			EditData.UpdateAll ();
		}

	}

}