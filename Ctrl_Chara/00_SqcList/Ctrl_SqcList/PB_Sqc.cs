using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;
using System.ComponentModel;


namespace ScriptEditor
{
	//--------------------------------------------------------------
	//System.TimerとSystem.Windows.Forms.Timerで曖昧なので明示的宣言
	using STimer = System.Timers.Timer;
	//--------------------------------------------------------------

	using EL_SqcDt = EditListbox < SequenceData >;


	//シークエンスデータの画像表示コントロール
	public class PB_Sqc : PictureBox
	{
		//対象データ
//		public EL_SqcDt ELB_Sqc { get; set; } = new EL_SqcDt ();
		public EL_SqcDt ELB_Sqc { get; set; } = null;
		
		//データ編集
		public EditSqcListData EditSLData { get; set; } = new EditSqcListData ();

		//アクション入力フォーム
		public bool FlagAction { get; set; } = false;
//		private Form_Action form_act = new Form_Action();	//参照
		public System.Action < Point > Show_FormAction { get; set; } = pt=>{};

		private ContextMenuStrip contextMenuStrip1;
		private IContainer components;
		private ToolStripMenuItem toolStripMenuItem1;
		private ToolStripMenuItem toolStripMenuItem2;
		private ToolStripMenuItem toolStripMenuItem3;
		private ToolStripMenuItem toolStripMenuItem4;
		private ToolStripMenuItem toolStripMenuItem5;
		private ToolStripMenuItem toolStripMenuItem6;

		//IDE表示
		public class Run {};
		private Run run { get; set; } = new Run ();

		//カーソル判定用別スレッド
		STimer tmr = new STimer( 16 );

		//上位パネル参照
		public Panel Pnl { get; set; } = null;



		//=================================================================-
		//コンストラクタ
		public PB_Sqc ()
		{
			InitializeComponent ();

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
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripMenuItem6});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(111, 136);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(110, 22);
			this.toolStripMenuItem1.Text = "削除";
			this.toolStripMenuItem1.Click += new System.EventHandler(this.削除toolStripMenuItem1);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(110, 22);
			this.toolStripMenuItem2.Text = "前";
			this.toolStripMenuItem2.Click += new System.EventHandler(this.前toolStripMenuItem2);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(110, 22);
			this.toolStripMenuItem3.Text = "次";
			this.toolStripMenuItem3.Click += new System.EventHandler(this.次toolStripMenuItem3);
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(110, 22);
			this.toolStripMenuItem4.Text = "先頭";
			this.toolStripMenuItem4.Click += new System.EventHandler(this.先頭toolStripMenuItem4);
			// 
			// toolStripMenuItem5
			// 
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			this.toolStripMenuItem5.Size = new System.Drawing.Size(110, 22);
			this.toolStripMenuItem5.Text = "末尾";
			this.toolStripMenuItem5.Click += new System.EventHandler(this.末尾toolStripMenuItem5);
			// 
			// toolStripMenuItem6
			// 
			this.toolStripMenuItem6.Name = "toolStripMenuItem6";
			this.toolStripMenuItem6.Size = new System.Drawing.Size(110, 22);
			this.toolStripMenuItem6.Text = "全消去";
			this.toolStripMenuItem6.Click += new System.EventHandler(this.全消去toolStripMenuItem6);
			// 
			// PB_Sqc
			// 
			this.ContextMenuStrip = this.contextMenuStrip1;
			this.contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

		}

		//環境
		public void SetEnviroment ( EditSqcListData editData, System.Action < Point > Show_Form )
		{
			EditSLData = editData;
			Show_FormAction = Show_Form;
		}

		//更新
		public void UpdateData ()
		{
#if false
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

#endif
#if false
			//イメージ名
			//ファイル書出時のみ[通し番号000]_を用い、それ以外は
			//"[シークエンス名]_[シークエンス内番号00].png" で扱う
			//名前の更新
			EditData.UpdateName ();
#endif
		}

		//サイズのみ更新
		public void UpdateSize ()
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

		}

		//スクロール移動
		private int scrollPos = 0;
		public void ScrollPos ( Panel pnl )
		{
			int y = EditSLData.SelectedSqc * ConstSqcListPaint.CH;
//			int py = - 1 * pnl.AutoScrollPosition.Y;
			scrollPos = y;

			//範囲外なら
//			if ( py < y - 400 || y  < py )
			{
				//@info AutoScrollは設定時に正の値を指定する
				//		(取得時は負の値)
				pnl.AutoScrollPosition = new Point ( 0, y );
//				pnl.AutoScrollPosition = new Point ( 0, -y );
			}
#if false

#endif
		}

		//描画
		protected override void OnPaint ( PaintEventArgs pe )
		{
			if ( ELB_Sqc is null ) { return; }
			if ( EditSLData is null ) { return; }

			Graphics g = pe.Graphics;

			int N = ELB_Sqc.Count();
			int W = this.Width;
			int H = this.Height;
			const int CW = ConstSqcListPaint.CW;
			const int CH = ConstSqcListPaint.CH;
			const int BX = 100;	//基準位置
			int slctSqc = EditSLData.SelectedSqc;
			int slctImg = EditSLData.SelectedImage;

			using ( Font FONT0 = new Font ( "Meiryo", 12.0f ) )
			using ( Font FONT1 = new Font ( "Meiryo", 10.0f ) )
			{
				//選択位置
				int selected_y = EditSLData.SelectedSqc * ConstSqcListPaint.CH;
				int yn = EditSLData.SelectedSqc;
				int pnl_y = -1 * Pnl.AutoScrollPosition.Y;
				int pnl_yn = -1 * Pnl.AutoScrollPosition.Y / ConstSqcListPaint.CH;
//				STS_TXT.Trace ( pnl_yn.ToString () );


				//選択
				g.FillRectangle ( Brushes.LightBlue, 0, slctSqc * CH, W, CH );


				//罫線(縦)
				g.DrawLine ( Pens.Black, new Point ( 0, 0 ), new Point ( 0, H ) );
//				g.DrawLine ( Pens.Black, new Point ( 0, selected_y ), new Point ( 0, selected_y + 1000 ) );
				g.DrawLine ( Pens.Black, new Point ( BX, 0 ), new Point ( BX, H ) );

				//罫線(縦)(仕切)
				int nHorizon = W / CW;
				for ( int i = 0; i < nHorizon; ++ i )
				{
					int x = BX + CW + i * CW;
					g.DrawLine ( Pens.Gainsboro, new Point ( x, 0 ), new Point ( x, H ) );
//					g.DrawLine ( Pens.Gainsboro, new Point ( x, selected_y ), new Point ( x, selected_y + 1000 ) );
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
					//描画のため個数制限
//					if ( ns < pnl_yn ) { ++ ns; continue; }
//					if ( pnl_yn + 3 < ns ) { break; }


					int y = ns * CH;

					//シークエンスデータ
					DrawSequence(g, sqcDt.Sqc.Name, FONT0, 0 + y);
					DrawSequence(g, "[" + sqcDt.nScript.ToString() + "]", FONT0, 20 + y);
					if ( FlagAction )
					{
						Action act = (Action)sqcDt.Sqc;
						DrawSequence(g, "(" + act.Category.ToString() + ")", FONT1, 40 + y);
						DrawSequence(g, "( -> " + act.NextActionName + ")", FONT1, 60 + y);
					}

#if false
#endif
					//画像
					int nI = 0;
					foreach ( ImageData imgDt in sqcDt.BD_ImgDt.GetEnumerable() )
					{
#if false
						Bitmap bmp = new Bitmap ( 100, 100 );
						Graphics gBmp = Graphics.FromImage ( bmp );
						gBmp.FillRectangle ( Brushes.AliceBlue, gBmp.VisibleClipBounds );
						gBmp.Dispose ();

#endif

						//g.DrawImage ( imgDt.Img, new Rectangle (CW + nI++ * CW, y, CW, CH ) );
						g.DrawImage ( imgDt.Thumbnail, new Rectangle (CW + nI++ * CW, y, CW, CH ) );
						//g.DrawImage ( bmp, new Rectangle (CW + nI++ * CW, y, CW, CH ) );
						//g.FillRectangle ( Brushes.AliceBlue, new Rectangle (CW + nI++ * CW, y, CW, CH ) );
					}

					++ ns;
				}


				//イメージ選択位置
				int Img_x = CW + CW * slctImg;
				int Img_y = CH * slctSqc;
				if ( Img_y < EditSLData.GetSequenceData()?.BD_ImgDt.Count () )
				{
					g.DrawRectangle ( Pens.Red, new Rectangle ( Img_x, Img_y, CW, CH ) );
				}
				
				//動的カーソル位置
				dpt = PointToClient(new Point(0, 0));
				g.FillRectangle(Brushes.Red, CursorPt.X, CursorPt.Y, 10, 10);


				//スクロール位置
				g.DrawString ( pnl_y.ToString(), FONT1, Brushes.Black, 100, pnl_y );
//				g.DrawString ( "test", new Font ("MSゴシック", 12), new SolidBrush ( Color.Blue ), 100, 100 * scrollPos );
			}

			base.OnPaint ( pe );
		}

		private void DrawSequence ( Graphics g, string str, Font f, float y )
		{
			RectangleF rectf = new RectangleF(new PointF(0, y), new SizeF(200, 20));
			g.DrawString( str, f, Brushes.Black, rectf );
		}


		//マウスクリック
		protected override void OnMouseClick ( MouseEventArgs e )
		{
			//左
			if ( e.Button == MouseButtons.Left )
			{
				Point pos = PointToClient ( Cursor.Position );
				int pt_x = ( pos.X - ConstSqcListPaint.CW ) / ConstSqcListPaint.CW;
				int pt_y = pos.Y / ConstSqcListPaint.CH;
				int n = ELB_Sqc.Count();
				int selectedSqcIndex = EditSLData.SelectedSqc;

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

							//選択中の編集データを指定する
							//SequenceData sqcDt = ELB_Sqc.Get ();
							SequenceData sqcDt = EditSLData.GetSequenceData ();
#if false
							form_act.Location = PointUt.PtAdd ( Cursor.Position, new Point(20, 20) );
							form_act.Assosiate ();
							form_act.Show();
							form_act.Focus ();
#endif
							Point pt = PointUt.PtAdd ( Cursor.Position, new Point(20, 20) );
							Show_FormAction ( pt );
						}
					}
					else
					{
						//選択
						EditSLData.SelectedSqc = pt_y;
						EditSLData.SelectedImage = pt_x;
						ELB_Sqc.GetListBox ().SelectedIndex = pt_y;
					}
				}
				this.Invalidate ();
			}

			base.OnMouseClick ( e );
		}

		//マウス押下イベント
		//@info コンテキストメニューより先に発生
		protected override void OnMouseDown ( MouseEventArgs e )
		{
			//右
			if ( e.Button == MouseButtons.Right )
			{
				EditSLData.SetPt ( PosToPt ( Cursor.Position ) );
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
			EditSLData.Remove ();
			EditSLData.UpdateAll ();
		}

		private void 前toolStripMenuItem2 ( object sender, EventArgs e )
		{
			EditSLData.Prev ();
			EditSLData.UpdateAll ();
		}

		private void 次toolStripMenuItem3 ( object sender, EventArgs e )
		{
			EditSLData.Next ();
			EditSLData.UpdateAll ();
		}

		private void 先頭toolStripMenuItem4 ( object sender, EventArgs e )
		{
			EditSLData.Head ();
			EditSLData.UpdateAll ();
		}

		private void 末尾toolStripMenuItem5 ( object sender, EventArgs e )
		{
			EditSLData.Tail ();
			EditSLData.UpdateAll ();
		}

		private void 全消去toolStripMenuItem6 ( object sender, EventArgs e )
		{
			EditSLData.EraseImage ();
			EditSLData.UpdateAll ();
		}
	}

}
