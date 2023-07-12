using System;
using System.Drawing;
using System.Windows.Forms;


namespace ScriptEditor
{
	using BD_IMGD = BindingDictionary < ImageData >;

	public class _PaintImage : PictureBox
	{
		//現在選択スクリプト
		EditCompend EditCompend = new EditCompend ();
		
		//対象イメージデータ
		private BD_IMGD Bd_Imgd = new BD_IMGD ();

		//エフェクトデータはアクションでもエフェクトでも持つ
		private BD_IMGD Bd_Ef_Imgd = new BD_IMGD ();
		
		//イメージ表示基準位置 ( X, Y )
		public Point PtPbImageBase { get; set; } = new Point ( 250, 480 );

		//クライアント位置
		public Point PtClient { get; set; } = new Point ();


		//枠描画
		private readonly DispRects dispRects = new DispRects ();

		//-----------------------------------------------------
		//ドラッグ＆ドロップ
		private Point prePt = new Point ( 0, 0 );
		private Point startPt = new Point ( 0, 0 );
		//-----------------------------------------------------


		//コンストラクタ
		public _PaintImage ()
		{
			BackColor = Color.DarkGray;
		}

		//環境設定
		public void SetEnviron ( EditCompend ec )
		{
			EditCompend = ec;
		}

		//キャラデータ設定
		public void SetCharaData ( Chara ch, BD_IMGD bd_imgd )
		{
			Bd_Imgd = bd_imgd;
			Bd_Ef_Imgd = ch.garnish.BD_Image;
		}

		//関連付け
		public void Assosiate ( Script scp )
		{
//			Script = scp;
		}

		//描画
		protected override void OnPaint ( PaintEventArgs pe )
		{
			Graphics g = pe.Graphics;

			using ( Pen PenWhite = new Pen ( Color.White, 4 ) )
			{
			//基準線
			g.DrawLine ( PenWhite, new Point ( PtPbImageBase.X, 0 ), new Point ( PtPbImageBase.X, this.Height ) );
			g.DrawLine ( PenWhite, new Point ( 0, PtPbImageBase.Y ), new Point ( this.Width, PtPbImageBase.Y ) );
			}	//using

			//スクリプト指定イメージ
			Script scp = EditCompend.SelectedScript;
			ImageData imgd = Bd_Imgd.Get ( scp.ImgName );
			DrawImageData ( g, imgd, scp.ImgName, scp.Pos );

			//エフェクト
			foreach ( EffectGenerate efGnrt in scp.BD_EfGnrt.GetEnumerable () )
			{
				ImageData idEf = Bd_Ef_Imgd.Get ( efGnrt.EfName );
				DrawImageData ( g, idEf, efGnrt.EfName, efGnrt.Pt );
			}

			//枠リスト
			dispRects.Disp ( g, scp, PtPbImageBase );

			base.OnPaint ( pe );
		}

		//イメージデータ描画（データが無いときダミーで文字を描画）
		private void DrawImageData ( Graphics g, ImageData imgd, string imgname, Point pos )
		{
			Image img;
			if( imgd == null )
			{
				img = MakeDummy ( imgname );
			}
			else if( imgd.Img == null )
			{
				img = MakeDummy ( imgname );
			}
			else { img = imgd.Img; }

			int x = PtPbImageBase.X + pos.X;
			int y = PtPbImageBase.Y + pos.Y;
			g.DrawImage ( img, x, y, img.Width, img.Height );
		}

		private Image MakeDummy ( string imgname )
		{
			Bitmap bmp = new Bitmap ( 512, 512 );
			using ( Font font = new Font ( "メイリオ", 20 ) )
			{
				Graphics g = Graphics.FromImage ( bmp );
				g.DrawString ( "\"" + imgname + "\"", font, Brushes.Orange, 64, 128 );
				g.DrawString ( "is not exist.", font, Brushes.Orange, 64, 160 );
			}	//using

			return bmp;
		}


		//--------------------------------------------------------------------
		//マウス
		protected override void OnMouseDown ( MouseEventArgs e )
		{
			if ( MouseButtons.Right == e.Button )
			{
				prePt = PtPbImageBase;
				startPt = Cursor.Position;
			}

			base.OnMouseDown ( e ); 
		}

		protected override void OnMouseMove ( MouseEventArgs e )
		{
			if ( MouseButtons.Right == e.Button )
			{
				Point dragPt = PointUt.PtSub ( Cursor.Position, startPt );
				PtPbImageBase = PointUt.PtAdd ( prePt, dragPt );

				Invalidate ();
			}

			base.OnMouseMove ( e );
		}

		//クライアント位置更新
		public void UpdatePtClient ()
		{
			PtClient = PointToClient ( Cursor.Position );
		}
	}
}
