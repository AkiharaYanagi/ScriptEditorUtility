using System.Windows.Forms;
using System.Drawing;


namespace ScriptEditor
{
	//エイリアス
	using BD_EfGnrt = BindingDictionary < EffectGenerate >;


	public partial class Ctrl_EfGnrt : UserControl
	{
		//対象
		public BD_EfGnrt BD_EfGnrt { get; set; } = new BD_EfGnrt();

		private EffectGenerate SelectedEfgnrt = new EffectGenerate ();

		//コントロール
		private EditListbox < EffectGenerate > EL_EfGnrt = new EditListbox<EffectGenerate> ();
		private CB_SequenceList CB_L_Effect = new CB_SequenceList ();

		//編集
		public EditCompend EditCompend { get; set; } = new EditCompend ();


		//コンストラクタ
		public Ctrl_EfGnrt ()
		{
			InitializeComponent ();

			//==============================================================
			//エディットリストボックス
			EL_EfGnrt.Location = new Point ( 3, 3 );

			//選択イベント
			EL_EfGnrt.SelectedIndexChanged = ()=>
			{
				SelectedEfgnrt = EL_EfGnrt.Get();
				CB_L_Effect.SelectName ( SelectedEfgnrt.EfName );
				All_Ctrl.Inst.UpdateData ();
			};

			this.Controls.Add ( EL_EfGnrt );
			//==============================================================

			//コンボボックス
			CB_L_Effect.Location = new Point ( 300, 50 );
			this.Controls.Add ( CB_L_Effect );
		}

		//キャラデータ設定
		public void SetCharaData ( Chara ch )
		{
			//エフェクト固定
			CB_L_Effect.SetCompend ( ch.garnish );
		}

		//編集設定
		public void SetEditCompend ( EditCompend ec )
		{
			EditCompend = ec;
		}


		//関連付け
		public void Assosiate ()
		{
			//編集から選択しているスクリプトを取得
			Script scp = EditCompend.SelectedScript;
			BD_EfGnrt = scp.BD_EfGnrt;

			//エディットリストボックスに設定
			EL_EfGnrt.SetData ( BD_EfGnrt );
			if ( BD_EfGnrt.Count() < 1 ) { return; }
			
			SelectedEfgnrt = BD_EfGnrt[0];

			if ( SelectedEfgnrt is null ) { return; }
			
			//各コントロールに設定
			CB_L_Effect.SelectName ( SelectedEfgnrt.EfName );
			CB_L_Effect.SetFunc = ef=>SelectedEfgnrt.EfName = ef.Name;
			Tbn_x.Assosiate ( i=>SelectedEfgnrt.SetPtX(i), ()=>SelectedEfgnrt.Pt.X );
			Tbn_y.Assosiate ( i=>SelectedEfgnrt.SetPtY(i), ()=>SelectedEfgnrt.Pt.Y );
			Tbn_z.Assosiate ( i=>SelectedEfgnrt.Z_PER100F=i, ()=>SelectedEfgnrt.Z_PER100F );

			//更新
			UpdateData ();
		}

		//更新
		public void UpdateData ()
		{
			EL_EfGnrt.UpdateData ();

			
			if ( EL_EfGnrt.Count () > 0 )
			{
				Tbn_x.UpdateData ();
				Tbn_y.UpdateData ();
				Tbn_z.UpdateData ();

				EffectGenerate efgnrt = EL_EfGnrt.Get();
				Cbx_gnrt.Checked = SelectedEfgnrt.Gnrt;
				Cbx_loop.Checked = SelectedEfgnrt.Loop;
				Cbx_sync.Checked = SelectedEfgnrt.Sync;
			}
			else
			{
				Tbn_x.Text = "";
				Tbn_y.Text = "";
				Tbn_z.Text = "";
				Cbx_gnrt.Checked = false;
				Cbx_loop.Checked = false;
				Cbx_sync.Checked = false;
			}
		}

		public void Disp ()
		{
			EffectGenerate efgnrt = EL_EfGnrt.Get();
			if ( efgnrt is null ) { return; }
			
			//各コントロールに設定
			CB_L_Effect.SelectName ( efgnrt.EfName );
			Tbn_x.UpdateData ();
			Tbn_y.UpdateData ();
			Tbn_z.UpdateData ();
		}

		private void Cbx_sync_CheckedChanged ( object sender, System.EventArgs e )
		{
			EffectGenerate efgnrt = EL_EfGnrt.Get();
			if ( efgnrt is null ) { return; }
			efgnrt.Sync = Cbx_sync.Checked;
		}

		private void Cbx_gnrt_CheckedChanged ( object sender, System.EventArgs e )
		{
			EffectGenerate efgnrt = EL_EfGnrt.Get();
			if ( efgnrt is null ) { return; }
			efgnrt.Gnrt = Cbx_gnrt.Checked;
		}

		private void Cbx_loop_CheckedChanged ( object sender, System.EventArgs e )
		{
			EffectGenerate efgnrt = EL_EfGnrt.Get();
			if ( efgnrt is null ) { return; }
			efgnrt.Loop = Cbx_loop.Checked;
		}
	}
}
