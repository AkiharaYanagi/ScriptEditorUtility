using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;


namespace ScriptEditor
{
	//------------------------------------------------------------------
	//	シークエンス内のスクリプトを表示し、編集対象を選択する
	//------------------------------------------------------------------
	public class PB_SqcBrd : PictureBox
	{
		//対象シークエンス
		public Sequence Sqc { get; set; } = new Sequence ();

		//編集
		public EditCompend EditCompend = new EditCompend ();


		//親コントロール
//		private System.Action ActDisp = ()=>{};		//全体からの表示
//		private System.Action ActAssosiate = ()=>{};	//全体からの関連付け


		//-----------------------------------------------------------------------------
		//内部使用定数
		private const int W = 20;	//升幅
		private const int H = 12;	//升高
		private const int BX = 100;	//基準X
		private const int BY = 12;	//基準Y
		private const int HH = 10;	//升高半分


		//-----------------------------------------------------------------------------
		//コンストラクタ
		public PB_SqcBrd ()
		{
//			BackColor = Color.Red;
		}

		//環境設定
		public void SetEnviron ( EditCompend ec, _Ctrl_Compend ctrl_cmpd )
		{
			EditCompend = ec;
			//ActDisp = ctrl_cmpd.Disp;
			//ActAssosiate = ctrl_cmpd.Assosiate;
		}

		//関連付け
		public void Assosiate ()
		{
			Sqc = EditCompend.SelectedSequence;
		}


		//親サイズ
		public Size Ctrl_Size { get; set; } = new Size ();

		//描画
		protected override void OnPaint ( PaintEventArgs pe )
		{
			Graphics g = pe.Graphics;
			List<Script> ls = Sqc.ListScript;
			int N = ls.Count;		//スクリプト個数
			const int SURPLUS = 2;	//背景の余白個数
			const int PH = H + H * 6;			//基準線

			//大きさ
			int minWidth = 400;
			int sqcWidth = BX + ( N + SURPLUS ) * W;
			this.Width = ( minWidth < sqcWidth ) ? sqcWidth: minWidth;
//			this.Width = 1000;
//			this.Height = 130 + BY + ( 2 * H );
			this.Height = 90;

			//大きさの一時保存
			int TW = Ctrl_Size.Width;
			int TH = Ctrl_Size.Height;
			int PW = this.Width;

			//選択
			int selectedScript = EditCompend.SelectedScript.Frame;
			int selectedSpanStart = EditCompend.SelectedSpanStart;
			int selectedSpanEnd = EditCompend.SelectedSpanEnd;

			
			//---------------------------------------------------------
			//背景(存在しない部分を一括で描画)
			g.FillRectangle ( Brushes.DarkGray, BX, 0, PW - BX, TH );

			//---------------------------------------------------------
			//キャラデータの表示
			g.FillRectangle ( Brushes.AliceBlue, BX, H, W * N, H );


			//---------------------------------------------------------
			//内部使用リソース
			using ( Pen PEN_BAR = new Pen ( Color.FromArgb ( 0x80, 0x80, 0xff ), 1 ) )
			using ( Pen PEN_BASE_BAR = new Pen ( Color.FromArgb ( 0xc0, 0xc0, 0xc0 ), 0.5f ) )
			using ( Pen PEN_SELECTED = new Pen ( Color.FromArgb ( 0x00, 0x00, 0xff ), 2.0f ) )
			using ( SolidBrush BRUSH_0 = new SolidBrush ( Color.FromArgb ( 0xff, 0xa0, 0xe0, 0xa0 ) ) )
			using ( StringFormat STR_FMT = new StringFormat () )
			{
			//文字列フォーマット
			STR_FMT.Alignment = StringAlignment.Center;	//文字列の中央揃え(水平)
			STR_FMT.LineAlignment = StringAlignment.Center;	//文字列の中央揃え(垂直)


			//---------------------------------------------------------
			//選択スパン表示
			int wSpan = W * ( 1 + selectedSpanEnd - selectedSpanStart );
			int xSpan = BX + W * selectedSpanStart;
			Rectangle rectSpan = new Rectangle ( xSpan, BY, wSpan, H );
			g.FillRectangle ( BRUSH_0, rectSpan );

			//---------------------------------------------------------
			//スクリプト内容
			int frame = 0;
			foreach ( Script s in Sqc.ListScript )
			{
				DrawScp ( g, frame, 0, DefineColor.Get ( s.Group ) );	//グループ表示	
				DrawScp ( g, frame, 2, GetScpCntClr ( s.ListCRect, ScpCntColor [ 1 ] ) );	//接触枠
				DrawScp ( g, frame, 3, GetScpCntClr ( s.ListHRect, ScpCntColor [ 2 ] ) );	//防御枠
				DrawScp ( g, frame, 4, GetScpCntClr ( s.ListARect, ScpCntColor [ 3 ] ) );	//攻撃枠
				DrawScp ( g, frame, 5, GetScpCntClr ( s.ListORect, ScpCntColor [ 4 ] ) );	//相殺枠
				DrawScp ( g, frame, 6, GetScpEfGnClr ( s.BD_EfGnrt, ScpCntColor [ 5 ] ) );	//Ef生成
				++ frame;
			}

			//---------------------------------------------------------
			//フレーム数
			for ( int i = 0; i < N + SURPLUS; ++i )
			{
				Rectangle r = new Rectangle ( BX + i * W - 10, H + 2, W + 20, H );
				g.DrawString ( i.ToString (), Font, Brushes.Gray, r, STR_FMT );
			}

			//文字
			DrawStr_0 ( g, "Group", 0, STR_FMT );
			DrawStr_0 ( g, "[Frame]", 1, STR_FMT );
			DrawStr_0 ( g, "■ CRect", 2, STR_FMT );
			DrawStr_0 ( g, "■ HRect", 3, STR_FMT );
			DrawStr_0 ( g, "■ ARect", 4, STR_FMT );
			DrawStr_0 ( g, "■ ORect", 5, STR_FMT );
			DrawStr_0 ( g, "■ EfGnrt", 6, STR_FMT );

			//---------------------------------------------------------
			//縦線	
			for ( int i = 0; i < N + SURPLUS + 1; ++ i )
			{
				int bx0 = BX + W * i;
				g.DrawLine ( PEN_BASE_BAR, bx0, 0, bx0, PH );
			}

			//---------------------------------------------------------
			//選択位置表示
			int sx = EditCompend.SelectedScript.Frame;
			g.DrawRectangle ( Pens.Green, xSpan, 0, wSpan, PH - 1 );	//複数選択の範囲表示
			g.FillRectangle ( Brushes.Firebrick, BX + sx * W, H, W, H );	//フレーム選択
			Rectangle r_n = new Rectangle ( BX + sx * W - 10, H + 2, W + 20, H );
			g.DrawString ( sx.ToString (), Font, Brushes.White, r_n, STR_FMT );
			g.DrawRectangle ( Pens.Firebrick, BX + sx * W, 0, W, PH - 1 );	//フレーム選択
			//---------------------------------------------------------

			}	//using


			
			base.OnPaint ( pe );
		}


		//==========================================================
		//描画用 内部関数
		private readonly Brush[] StrColorBrushes =
		{
			Brushes.Gray, 
			Brushes.Gray, 
			new SolidBrush ( ScpCntColor [ 1 ] ),
			Brushes.Teal, 
			Brushes.LightCoral, 
			Brushes.Goldenrod, 
			Brushes.Turquoise, 
			Brushes.LimeGreen, 
		};

		private void DrawStr_0 ( Graphics g, string str, int i, StringFormat sf )
		{
			Rectangle r = new Rectangle ( 0, H * i + 2, BX, H );
			using ( Font FONT0 = new Font ( "Meiryo", 8.0f ) )
			{
				g.DrawString ( str, FONT0, StrColorBrushes [ i ], r, sf );
			}
		}

		//スクリプト描画
		private void DrawScp ( Graphics g, int frame, int i, Color clr )
		{
			Rectangle r = new Rectangle ( BX + (W * frame), H * i, W, H );
			g.FillRectangle ( new SolidBrush ( clr ), r );
		}

		//スクリプト内の表示色
		private static readonly Color[] ScpCntColor =
		{
			Color.White,
			Color.FromArgb ( 0x80, 0x80, 0xff ),
			Color.FromArgb ( 0xc0, 0xff, 0xc0 ),
			Color.FromArgb ( 0xff, 0xc0, 0x80 ),
			Color.FromArgb ( 0xff, 0xff, 0xb0 ),
			Color.FromArgb ( 0xc0, 0xff, 0xff ), 
		};
		private Color GetScpCntClr ( List<Rectangle> L_Rect, Color clr )
		{
			Color ret_clr = ( 0 < L_Rect.Count ) ? clr : Color.White;
			return ret_clr;
		}
		private Color GetScpEfGnClr ( BindingDictionary < EffectGenerate > BD_EfGn, Color clr )
		{
			Color ret_clr = ( 0 < BD_EfGn.Count () ) ? clr : Color.White;
			return ret_clr;
		}


		//==========================================================
		//マウス イベント
		
		private bool bDrag = false;	//ドラッグ中
		private int startX = 0;		//ドラッグ開始位置

		//マウス押下
		protected override void OnMouseDown ( MouseEventArgs e )
		{
			startX = GetCell ().X;
			SelectFromPos ();
			base.OnMouseDown ( e );
		}

		//マウス移動（ドラッグ）
		protected override void OnMouseMove ( MouseEventArgs e )
		{
			if ( bDrag )
			{
				int x = GetCell ().X;

				//前方選択
				if ( x < startX )
				{
					EditCompend.SelectedSpanStart = x;
					EditCompend.SelectedSpanEnd = startX;
				}
				else
				{
					EditCompend.SelectedSpanStart = startX;
					EditCompend.SelectedSpanEnd = x;
				}
				Invalidate ();
			}
			base.OnMouseMove ( e );
		}

		//マウス離上
		protected override void OnMouseUp ( MouseEventArgs e )
		{
			bDrag = false;
			Invalidate ();
			base.OnMouseUp ( e );
		}

		//------
		//内部関数

		//マウス位置からフレーム表の升目位置を取得する
		private Point GetCell () 
		{
			//マウス位置をコントロールのクライアント位置に直す
			Point pt = this.PointToClient ( Cursor.Position );
			if ( pt.X < BX || pt.Y < BY ) { return new Point ( 0, 0 ); }

			//升目に合わせる
			int pos_x = ( pt.X - BX ) / W;
			int pos_y = ( pt.Y - BY ) / H;
			return new Point ( pos_x, pos_y );
		}

		//位置から対象を選択
		private void SelectFromPos ()
		{
			EditCompend ec = EditCompend;
			Point pos = GetCell ();

			//範囲チェック
			int n_scp = ec.SelectedSequence.ListScript.Count;
			if ( pos.X < 0 || n_scp <= pos.X ) { return; }


			ec.SelectFrame ( pos.X );
			bDrag = true;

			//グループ
			EditScript es = ec.EditScript;
			es.SelectGroup ( ec.SelectedScript.Group );

			//選択位置をステータスに表示
			Script s = EditCompend.SelectedScript;
			STS_TXT.Trace ( "Frame:" + s.Frame.ToString () + ", Group[" + s.Group + "]" );

			//更新
			//ActAssosiate ();
			//ActDisp ();
			//All_Ctrl.Inst.Assosiate ();
			All_Ctrl.Inst.Assosiate_scp ();
		}

	}
}
