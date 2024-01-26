using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;


namespace ScriptEditor
{
	using LRect = List < Rectangle >;
	delegate void EventFunc ( object sender, EventArgs args );

	public partial class Ctrl_AllRect : UserControl
	{
		//各種類 枠リスト
		private List < Ctrl_ListRect > Ls_LsRect = new List<Ctrl_ListRect> ();
		private const int nLsRect = 4;

		enum KindRect
		{
			CRect = 0,	//接触枠
			HRect = 1,	//当り枠
			ARect = 2,	//攻撃枠
			ORect = 3,	//相殺枠
		};

		//仕切線
		private List < Label > labels = new List<Label> ();
		private const int nLabel = 5;

		private const int PH = 100;

		//選択ボタン
		private List < Button > LsButton = new List<Button> ();

		//選択枠リスト種類
		private KindRect SelectedIndexRect = KindRect.CRect;

		//編集
		public EditCompend EditCompend { get; set; } = new EditCompend ();

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

		//コンペンド選択 ( ビヘイビア / ガーニッシュ )
		public void SetEditCompend ( EditCompend ec )
		{
			EditCompend = ec;
			Ls_LsRect [0].SetEnviron ( ec.EditScript.EditAllRect.L_CRect );
			Ls_LsRect [1].SetEnviron ( ec.EditScript.EditAllRect.L_HRect );
			Ls_LsRect [2].SetEnviron ( ec.EditScript.EditAllRect.L_ARect );
			Ls_LsRect [3].SetEnviron ( ec.EditScript.EditAllRect.L_ORect );
		}

		//関連付け
		public void Assosiate ()
		{
			Script scp = EditCompend.SelectedScript;
			Ls_LsRect [0].Assosiate ( scp.ListCRect );
			Ls_LsRect [1].Assosiate ( scp.ListHRect );
			Ls_LsRect [2].Assosiate ( scp.ListARect );
			Ls_LsRect [3].Assosiate ( scp.ListORect );
		}

		public void UpdateData ()
		{
		}

		public void Disp ()
		{
			this.Invalidate ();
		}

#if false
		public void SetFnDispAll ( System.Action FnDispAll )
		{
			Ls_LsRect [0].DispAll = FnDispAll;
			Ls_LsRect [1].DispAll = FnDispAll;
			Ls_LsRect [2].DispAll = FnDispAll;
			Ls_LsRect [3].DispAll = FnDispAll;
		}
#endif

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
		private LRect CopyListRect = new LRect ();

		//選択
		private Ctrl_ListRect SelectedLsRect = null;
		private void SelectListRect ( Ctrl_ListRect cls )
		{
			UnSelected ();
			SelectedLsRect = cls;
		}

		private void Select_C_Rect ( object s, EventArgs e ) { SelectRect ( KindRect.CRect ); }
		private void Select_H_Rect ( object s, EventArgs e ) { SelectRect ( KindRect.HRect ); }
		private void Select_A_Rect ( object s, EventArgs e ) { SelectRect ( KindRect.ARect ); }
		private void Select_O_Rect ( object s, EventArgs e ) { SelectRect ( KindRect.ORect ); }

		private void SelectRect ( KindRect kindRect )
		{
			//ボタン表示
			foreach ( Button b in LsButton ) { b.BackColor = Color.FromName("Control"); }
			LsButton [ (int)kindRect ].BackColor = Color.Bisque;

			//選択を保存
			SelectedLsRect = Ls_LsRect [ (int)kindRect ];
			SelectedIndexRect = kindRect;

			//再描画
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

			All_Ctrl.Inst.UpdateData ();	//表示の更新
		}

		//ペースト：グループ
		private void Btn_PasteGroup_Click ( object sender, EventArgs e )
		{
			EditScript es = EditCompend?.EditScript;
			switch ( SelectedIndexRect )
			{
			case KindRect.CRect: es?.DoGroup ( s=>s.ListCRect = new LRect(CopyListRect) ); break;
			case KindRect.HRect: es?.DoGroup ( s=>s.ListHRect = new LRect(CopyListRect) ); break;
			case KindRect.ARect: es?.DoGroup ( s=>s.ListARect = new LRect(CopyListRect) ); break;
			case KindRect.ORect: es?.DoGroup ( s=>s.ListORect = new LRect(CopyListRect) ); break;
			}

			All_Ctrl.Inst.UpdateData ();	//表示の更新
		}

		//ペースト：シークエンス
		private void Btn_PasteSequence_Click ( object sender, EventArgs e )
		{
			Action < Action < Script > > F = EditCompend.EditScriptInSequence;

			switch ( SelectedIndexRect )
			{
			case KindRect.CRect: F ( s=>{ s.ListCRect = new LRect ( CopyListRect ); } ); break;
			case KindRect.HRect: F ( s=>{ s.ListHRect = new LRect ( CopyListRect ); } ); break;
			case KindRect.ARect: F ( s=>{ s.ListARect = new LRect ( CopyListRect ); } ); break;
			case KindRect.ORect: F ( s=>{ s.ListORect = new LRect ( CopyListRect ); } ); break;
			}

			All_Ctrl.Inst.UpdateData ();	//表示の更新
		}

		//内部関数：New List
		private LRect NewLRect ( LRect src )
		{
			return new LRect ( src );
		}
	}
}
