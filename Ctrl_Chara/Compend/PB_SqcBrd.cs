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
		public EditCompend EditCompend = null;


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
		public void SetEnviron ( EditCompend ec )
		{
			EditCompend = ec;
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

			//大きさ
			int minWidth = 400;
			int sqcWidth = BX + ( N + SURPLUS ) * W;
			this.Width = ( minWidth < sqcWidth ) ? sqcWidth: minWidth;
//			this.Width = 1000;
//			this.Height = 130 + BY + ( 2 * H );
			this.Height = 100;

			//大きさの一時保存
			int TW = Ctrl_Size.Width;
			int TH = Ctrl_Size.Height;
			int PW = this.Width;

			
			//---------------------------------------------------------
			//背景(存在しない部分を一括で描画)
			g.FillRectangle ( Brushes.LightGray, BX, 0, PW - BX, TH );

			//---------------------------------------------------------
			//キャラデータの表示
			g.FillRectangle ( Brushes.AliceBlue, BX, H, W * N, H );


			//---------------------------------------------------------
			//内部使用リソース
			using ( Pen PEN_BAR = new Pen ( Color.FromArgb ( 0x80, 0x80, 0xff ), 1 ) )
			using ( Pen PEN_BASE_BAR = new Pen ( Color.FromArgb ( 0xc0, 0xc0, 0xc0 ), 0.5f ) )
			using ( Pen PEN_SELECTED = new Pen ( Color.FromArgb ( 0x00, 0x00, 0xff ), 2.0f ) )
			using ( SolidBrush BRUSH_0 = new SolidBrush ( Color.FromArgb ( 0xff, 0xd0, 0xff, 0xd0 ) ) )
			using ( StringFormat STR_FMT = new StringFormat () )
			{
			//文字列フォーマット
			STR_FMT.Alignment = StringAlignment.Center;	//文字列の中央揃え(水平)
			STR_FMT.LineAlignment = StringAlignment.Center;	//文字列の中央揃え(垂直)

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
			//基準線
			const int PH = H + H * 6;

			//縦線	
#if false
			g.DrawLine ( PEN_BASE_BAR, 0, 0, 0, PH );
#endif
			for ( int i = 0; i < N + SURPLUS + 1; ++ i )
			{
				int bx0 = BX + W * i;
				g.DrawLine ( PEN_BASE_BAR, bx0, 0, bx0, PH );
			}

			//横線
//			g.DrawLine ( PEN_BASE_BAR, 0, 0, PW, 0 );


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
	}
}
