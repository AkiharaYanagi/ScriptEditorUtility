using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;


namespace ScriptEditor
{
	delegate void EventFunc ( object sender, EventArgs args );

	public partial class Ctrl_AllRect : UserControl
	{
		//各 枠リスト
		private List < Ctrl_ListRect > Ls_LsRect = new List<Ctrl_ListRect> ();
		private const int nLsRect = 4;

		//仕切線
		private List < Label > labels = new List<Label> ();
		private const int nLabel = 5;

		private const int PH = 100;

		//選択ボタン
		private List < Button > LsButton = new List<Button> ();

		//編集
		public EditCompend EditCompend { get; set; } = null;

		//-----------------------------------------------------------------------
		//コンストラクタ
		public Ctrl_AllRect ()
		{
			//仕切り線
			for ( int i = 0; i < nLabel; ++ i )
			{
				Label lbl = new Label ();					 
				SetLabel ( lbl, 10, 5 + i * PH );
				labels.Add  ( lbl );
			}

			//枠リスト コントロール
			string[] names = { "CRect", "HRect", "ARect", "ORect" };
			for ( int i = 0; i < nLsRect; ++ i )
			{
				Ctrl_ListRect ctrl_ListRect = new Ctrl_ListRect ();
				ctrl_ListRect.SetName ( names[i] );
				ctrl_ListRect.Location = new Point ( 10, 10 + i * PH );
				ctrl_ListRect.Selected = t => SelectListRect ( t );
				Ls_LsRect.Add ( ctrl_ListRect );
				this.Controls.Add ( ctrl_ListRect );
			}

			//選択ボタン
			EventFunc[] funcs = { Select_C_Rect, Select_H_Rect, Select_A_Rect, Select_O_Rect };
			for ( int i = 0; i < nLsRect; ++ i )
			{
				Button btn = new Button ();
				btn.Text = "";
				btn.BackColor = Color.LightGray;
				btn.Size = new Size ( 20, 60 );
				btn.Location = new Point ( 370, 10 + i * PH );
				btn.Click += new EventHandler ( funcs [ i ] );
				LsButton.Add ( btn );
				this.Controls.Add ( btn );
			}

			//初期選択
			SelectRect ( 0 );

			InitializeComponent ();
		}

		public void SetEnvironment ( EditCompend ec, System.Action UpdateFunc )
		{
			EditCompend = ec;
			SetUpdateFunc ( UpdateFunc );
		}

		public void Assosiate ( Script scp )
		{
			Ls_LsRect [0].Assosiate ( scp.ListCRect );
			Ls_LsRect [1].Assosiate ( scp.ListHRect );
			Ls_LsRect [2].Assosiate ( scp.ListARect );
			Ls_LsRect [3].Assosiate ( scp.ListORect );
		}

		public void UpdateData ()
		{

		}

		public void SetUpdateFunc ( System.Action UpdateFunc )
		{
			Ls_LsRect [0].UpdateAll = UpdateFunc;
			Ls_LsRect [1].UpdateAll = UpdateFunc;
			Ls_LsRect [2].UpdateAll = UpdateFunc;
			Ls_LsRect [3].UpdateAll = UpdateFunc;
		}

		//各コントロール取得
		public Ctrl_ListRect GetCtrlListCRect () { return Ls_LsRect [0]; }
		public Ctrl_ListRect GetCtrlListHRect () { return Ls_LsRect [1]; }
		public Ctrl_ListRect GetCtrlListARect () { return Ls_LsRect [2]; }
		public Ctrl_ListRect GetCtrlListORect () { return Ls_LsRect [3]; }

		//仕切線
		private void SetLabel ( Label lbl, int x, int y )
		{
			lbl.Text = "";
			lbl.BorderStyle = BorderStyle.Fixed3D;
			lbl.Size = new Size ( 350, 2 );
			lbl.Location = new Point ( x, y );
			Controls.Add ( lbl );
		}

		//描画
		private void Ctrl_AllRect_Paint ( object sender, PaintEventArgs e )
		{
			foreach ( Ctrl_ListRect lr in Ls_LsRect )
			{
				lr.BackColor = Color.FromName ( "Control" );
			}
			SelectedLsRect.BackColor = Color.Bisque;
		}

		//=====================================================================
		//コピー

		//コピー保存
		private List<Rectangle> CopyListRect = new List<Rectangle> ();

		//選択
		private Ctrl_ListRect SelectedLsRect = null;
		private void SelectListRect ( Ctrl_ListRect cls )
		{
			UnSelected ();
			SelectedLsRect = cls;
		}

		private void Select_C_Rect ( object s, EventArgs e ) { SelectRect ( 0 ); }
		private void Select_H_Rect ( object s, EventArgs e ) { SelectRect ( 1 ); }
		private void Select_A_Rect ( object s, EventArgs e ) { SelectRect ( 2 ); }
		private void Select_O_Rect ( object s, EventArgs e ) { SelectRect ( 3 ); }

		private void SelectRect ( int index )
		{
			foreach ( Button b in LsButton ) { b.BackColor = Color.FromName("Control"); }
			LsButton [ index ].BackColor = Color.Bisque;
			SelectedLsRect = Ls_LsRect [ index ];
			Invalidate ();
		}

		//全選択解除
		private void UnSelected () 
		{
			foreach ( Ctrl_ListRect lr in Ls_LsRect )
			{
				lr.UnSelected ();
			}
		}

		//コピーボタン
		private void Btn_Copy_Click ( object sender, EventArgs e )
		{
			if ( SelectedLsRect is null ) { return; }

			List<Rectangle> lr = SelectedLsRect.LsRect;

			//テキストに表示
			TB_CopyName.Text = SelectedLsRect.GetName () + " [ " + lr.Count.ToString() + " ]";
			
			//リストの保存
			CopyListRect = new List<Rectangle> ( lr );
		}

		//クリア
		private void Btn_Clear_Click ( object sender, EventArgs e )
		{
			TB_CopyName.Text = "";
			CopyListRect = new List<Rectangle> ();
		}


		//ペースト：シングル
		private void Btn_PasteSingle_Click ( object sender, EventArgs e )
		{
			SelectedLsRect.LsRect = new List<Rectangle> ( CopyListRect );
			SelectedLsRect.Invalidate ();
		}

		//ペースト：グループ
		private void Btn_PasteGroup_Click ( object sender, EventArgs e )
		{
			EditScript es = EditCompend?.EditScript;
			es?.DoGroup ( s=>s.ListCRect = new List<Rectangle>(CopyListRect) );
		}

		//ペースト：シークエンス
		private void Btn_PasteSequence_Click ( object sender, EventArgs e )
		{

		}
	}
}
