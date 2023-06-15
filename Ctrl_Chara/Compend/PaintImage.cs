using System;
using System.Drawing;
using System.Windows.Forms;


namespace ScriptEditor
{
	using BD_IMGD = BindingDictionary < ImageData >;

	public class _PaintImage : PictureBox
	{
		//現在選択スクリプト
		private Script Script = new Script ();

		//対象イメージデータ
		private BD_IMGD Bd_Imgd = new BD_IMGD ();

		//エフェクトデータはアクションでもエフェクトでも持つ
		private BD_IMGD Bd_Ef_Imgd = new BD_IMGD ();
		
		//イメージ表示基準位置 ( X, Y )
		public Point PtPbImageBase { get; set; } = new Point ( 250, 480 );

		//-----------------------------------------------------
		//ドラッグ＆ドロップ
		private Point prePt = new Point ( 0, 0 );
		private Point startPt = new Point ( 0, 0 );
		//-----------------------------------------------------


		//コンストラクタ
		public _PaintImage ()
		{
			BackColor = Color.DarkGray;
			Dock = DockStyle.Fill;
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
			Script = scp;
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
			ImageData id = Bd_Imgd.Get ( Script.ImgName );
			Image img;
			if( id == null )
			{
				img = MakeDummy ( Script.ImgName );
			}
			else if( id.Img == null )
			{
				img = MakeDummy ( Script.ImgName );
			}
			else { img = id.Img; }

			int x = PtPbImageBase.X + Script.Pos.X;
			int y = PtPbImageBase.Y + Script.Pos.Y;
			g.DrawImage ( img, x, y, img.Width, img.Height );


			base.OnPaint ( pe );
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
	}
}
