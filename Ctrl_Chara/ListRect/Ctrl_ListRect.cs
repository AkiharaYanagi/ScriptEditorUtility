using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;


namespace ScriptEditor
{
	using LsRect = List < Rectangle >;

	//枠リストの１種類を扱うコントロール
	//※枠リストの複数スクリプト編集はコピーペースト方式
	//	(リスト個数が異なる可能性があるため)
	public partial class Ctrl_ListRect : UserControl
	{
		//対象データ
		public LsRect LsRect { get; set; } = new LsRect ();

		//選択位置
		public int SelectedIndex { get; set; } = -1;

		//コンストラクタ
		public Ctrl_ListRect ()
		{
			InitializeComponent ();

			this.Paint += Ctrl_ListRect_Paint;
			
			Assosiate ( LsRect );
		}

		private void Ctrl_ListRect_Paint ( object sender, PaintEventArgs e )
		{
			Pb_Num.Invalidate ();
		}

		public void SetCharaData ( )
		{

		}

		public void Assosiate ( LsRect lsr )
		{
			LsRect = lsr;
			Tbn_X.SetFunc = i => SetRect_X(i);
			Tbn_Y.SetFunc = i => SetRect_Y(i);
			Tbn_W.SetFunc = i => SetRect_W(i);
			Tbn_H.SetFunc = i => SetRect_H(i);
			UpdateData ();
		}

		public void UpdateData ()
		{
			Rectangle r = GetRect ( SelectedIndex );
			Tbn_X.Text = r.X.ToString ();
			Tbn_Y.Text = r.Y.ToString ();
			Tbn_W.Text = r.Width.ToString ();
			Tbn_H.Text = r.Height.ToString ();

			Pb_Num.Invalidate ();
			this.Invalidate ();
		}

		public Rectangle GetRect ( int index )
		{
			if ( index < 0 || LsRect.Count <= index ) { return new Rectangle (); }
			return LsRect [ index ];
		}

		public void SetRect ( Rectangle r )
		{
			LsRect [ SelectedIndex ] = r;
		}

		public void SetRect_X ( int x )
		{
			Rectangle r = LsRect [ SelectedIndex ];
			LsRect [ SelectedIndex ] = new Rectangle ( x, r.Y, r.Width, r.Height ); 
		}
		public void SetRect_Y ( int y )
		{
			Rectangle r = LsRect [ SelectedIndex ];
			LsRect [ SelectedIndex ] = new Rectangle ( r.X, y, r.Width, r.Height ); 
		}
		public void SetRect_W ( int w )
		{
			Rectangle r = LsRect [ SelectedIndex ];
			LsRect [ SelectedIndex ] = new Rectangle ( r.X, r.Y, w, r.Height ); 
		}
		public void SetRect_H ( int h )
		{
			Rectangle r = LsRect [ SelectedIndex ];
			LsRect [ SelectedIndex ] = new Rectangle ( r.X, r.Y, r.Width, h ); 
		}

		public void SetName ( string name )
		{
			this.Lbl_RectName.Text = name;
		}
		public string GetName ()
		{
			return Lbl_RectName.Text;
		}

		private void Pb_Num_Paint ( object sender, PaintEventArgs e )
		{
			Graphics g = e.Graphics;
			const int Num_Rect = 8;
			const int BX = 20;
			const int DX = 20;
			const int W = 20;
			const int H = 20;

			using ( Pen PEN0 = new Pen ( Brushes.Gray ) )
			{
				//背景
				for ( int i = 0; i < LsRect.Count; ++ i )
				{
					int x = i * DX;
					g.FillRectangle ( Brushes.White, x, 0, W, H );
				}

				//選択
				if( SelectedIndex != -1 )
				{
					g.FillRectangle ( Brushes.Red, SelectedIndex * 20, 0, W, H );
				}
			
				//罫線
				for ( int i = 0; i < Num_Rect - 1; ++ i )
				{
					int x = i * DX + BX;
					g.DrawLine ( PEN0, x, 0, x, W );
				}
			}

		}

		//表示部クリック
		private void Pb_Num_MouseClick ( object sender, MouseEventArgs e )
		{
			Point pos = Pb_Num.PointToClient ( Cursor.Position );
			const int W = 20;
			int pt_x = pos.X / W;
			if( pt_x < LsRect.Count )
			{
				SelectedIndex = pt_x;
			}
			UpdateData ();
			Pb_Num.Invalidate ();
		}

		//追加ボタン
		private void Btn_Add_Click ( object sender, System.EventArgs e )
		{
			if ( LsRect.Count < ConstChara.NumRect )
			{
				LsRect.Add ( new Rectangle () );
				Pb_Num.Invalidate ();
			}
		}

		//削除ボタン
		private void Btn_Del_Click ( object sender, System.EventArgs e )
		{
			if ( LsRect.Count == 0 ) { return; }

			LsRect.RemoveAt ( LsRect.Count - 1 );

			if ( SelectedIndex > LsRect.Count - 1 ) { SelectedIndex = -1; }

			Pb_Num.Invalidate ();
		}

		//====================================================================
		//背景クリック
		public System.Action < Ctrl_ListRect > Selected { get; set; } = s=>{};
		private void Ctrl_ListRect_Click ( object sender, System.EventArgs e )
		{
#if false
			Selected?.Invoke ( this );
			this.BackColor = Color.AliceBlue;
#endif
		}

		public void UnSelected ()
		{
			this.BackColor = Color.FromName ("Control");
		}
	}
}
