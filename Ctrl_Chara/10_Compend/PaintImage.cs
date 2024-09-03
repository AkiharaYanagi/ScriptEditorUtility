using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;


namespace ScriptEditor
{
	using BD_SQC = BindingDictionary < Sequence >;
	using BD_IMGD = BindingDictionary < ImageData >;

	public class _PaintImage : PictureBox
	{
		//現在選択スクリプト
		EditCompend EditCompend = new EditCompend ();
		
		//対象イメージデータ
		private BD_IMGD Bd_Imgd = new BD_IMGD ();

		//エフェクトデータはアクションでもエフェクトでも持つ
		private BD_SQC Bd_Ef = new BD_SQC ();
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
			BackColor = Color.DimGray;
		}

		//環境設定
		public void SetEnviron ( EditCompend ec )
		{
			EditCompend = ec;
		}

		//キャラデータ設定
		public void SetCharaData ( Chara ch, BD_IMGD bd_imgd )
		{
			Bd_Imgd = bd_imgd;	//ビヘイビア、またはガーニッシュのイメージリスト

			Bd_Ef = ch.garnish.BD_Sequence;		//共通エフェクト
			Bd_Ef_Imgd = ch.garnish.BD_Image;	//共通エフェクトイメージ
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

			using ( Font FONT0 = new Font ( "Meiryo", 12.0f ) )
			using ( Pen PenWhite = new Pen ( Color.White, 4 ) )
			{

			//基準線
			g.DrawLine ( PenWhite, new Point ( PtPbImageBase.X, 0 ), new Point ( PtPbImageBase.X, this.Height ) );
			g.DrawLine ( PenWhite, new Point ( 0, PtPbImageBase.Y ), new Point ( this.Width, PtPbImageBase.Y ) );


			//一つ前のメインイメージをゴースト表示
			int index = EditCompend.SelectedScript.Frame;
			List < Script > L_Scp =	EditCompend.SelectedSequence.ListScript;
			//０のときは表示しない
			if ( index > 0 && index - 1 < L_Scp.Count )

			{
				Script preScp = L_Scp [ index - 1 ];
				ImageData preImgDt = Bd_Imgd.Get ( preScp.ImgName );
				DrawImageData_alpha ( g, preImgDt, preScp.ImgName, preScp.Pos, 0.5F );
			}


			//スクリプト指定 メインイメージ
			Script scp = EditCompend.SelectedScript;
			ImageData imgd = Bd_Imgd.Get ( scp.ImgName );
			DrawImageData ( g, imgd, scp.ImgName, scp.Pos );
			g.DrawString ( scp.ImgName, FONT0, Brushes.Wheat, new Point ( 34, 2 ) );

			//エフェクト生成
			foreach ( EffectGenerate efGnrt in scp.BD_EfGnrt.GetEnumerable () )
			{
				//エフェクトの取得
				Effect ef = (Effect)Bd_Ef.Get ( efGnrt.EfName );
				if ( ef is null ) { continue; }
				if ( ef.ListScript.Count <= 0 ) { continue; }
				Script efSc = ef.ListScript[ 0 ];
				
				//イメージの取得
				ImageData idEf = Bd_Ef_Imgd.Get ( efSc.ImgName );

				//エフェクトのスクリプトから位置を取得
				Point efPt = PointUt.PtAdd ( efSc.Pos, efGnrt.Pt );	

				//描画
				DrawImageData ( g, idEf, efGnrt.EfName, efPt );
			}

			//枠リスト
			dispRects.Disp ( g, scp, PtPbImageBase );

			}	//using

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

		//α値指定描画
		private void DrawImageData_alpha ( Graphics g, ImageData imgd, string imgname, Point pos, float alpha )
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
			int w = img.Width;
			int h = img.Height;

			//カラーマトリクスの指定
			ColorMatrix clrMtx = new ColorMatrix ();
			clrMtx.Matrix00 = 1;
			clrMtx.Matrix11 = 1;
			clrMtx.Matrix22 = 1;
			clrMtx.Matrix33 = alpha;
			clrMtx.Matrix44 = 1;

			//イメージアトリビュートの指定
			ImageAttributes imgAtrb = new ImageAttributes ();
			imgAtrb.SetColorMatrix ( clrMtx );

			Rectangle rect = new Rectangle ( x, y, w, h );
			g.DrawImage ( img, rect, 0, 0, w, h, GraphicsUnit.Pixel, imgAtrb );
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
