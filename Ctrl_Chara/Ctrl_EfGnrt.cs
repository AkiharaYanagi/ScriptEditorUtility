using System.Windows.Forms;
using System.Drawing;


namespace ScriptEditor
{
	//エイリアス
	using BD_EfGnrt = BindingDictionary < EffectGenerate >;


	public partial class _Ctrl_EfGnrt : UserControl
	{
		//対象
		public BD_EfGnrt BD_EfGnrt { get; set; } = new BD_EfGnrt();

		private EffectGenerate efgnrt = new EffectGenerate ();

		//コントロール
		private EditListbox < EffectGenerate > EL_EfGnrt = new EditListbox<EffectGenerate> ();
		private CB_SequenceList CB_L_Effect = new CB_SequenceList ();

		//編集
		public EditCompend EditCompend { get; set; } = new EditCompend ();


		//コンストラクタ
		public _Ctrl_EfGnrt ()
		{
			InitializeComponent ();

			//==============================================================
			//エディットリストボックス
			EL_EfGnrt.Location = new Point ( 3, 3 );

			//選択イベント
			EL_EfGnrt.SelectedIndexChanged = ()=>
			{
				efgnrt = EL_EfGnrt.Get();
				UpdateData ();
			};

			this.Controls.Add ( EL_EfGnrt );
			//==============================================================

			CB_L_Effect.Location = new Point ( 300, 50 );
			this.Controls.Add ( CB_L_Effect );
		}

		//キャラデータ設定
		public void SetCharaData ( Chara ch )
		{
			CB_L_Effect.DataSource = ch.garnish.BD_Sequence.GetBindingList();
		}

		//編集設定
		public void SetEditCompend ( EditCompend ec )
		{
			EditCompend = ec;
		}


		//関連付け
#if false
		public void Assosiate ( Script scp )
		{
			EL_EfGnrt.SetData ( scp.BD_EfGnrt );

			Tbn_x.Assosiate ( i=>efgnrt.SetPtX(i), ()=>efgnrt.Pt.X );
			Tbn_y.Assosiate ( i=>efgnrt.SetPtY(i), ()=>efgnrt.Pt.Y );
			Tbn_z.Assosiate ( i=>efgnrt.Z_PER100F=i, ()=>efgnrt.Z_PER100F );
		}
#endif

		public void Assosiate ()
		{
			Script scp = EditCompend.SelectedScript;
			EL_EfGnrt.SetData ( scp.BD_EfGnrt );
			
			Tbn_x.Assosiate ( i=>efgnrt.SetPtX(i), ()=>efgnrt.Pt.X );
			Tbn_y.Assosiate ( i=>efgnrt.SetPtY(i), ()=>efgnrt.Pt.Y );
			Tbn_z.Assosiate ( i=>efgnrt.Z_PER100F=i, ()=>efgnrt.Z_PER100F );
		}

		//更新
		public void UpdateData ()
		{
			//efgnrt = EL_EfGnrt.Get();

			Tbn_x.UpdateData ();
			Tbn_y.UpdateData ();
			Tbn_z.UpdateData ();
			
			Cbx_gnrt.Checked = efgnrt.Gnrt;
			Cbx_loop.Checked = efgnrt.Loop;
			Cbx_sync.Checked = efgnrt.Sync;
		}

		public void Disp ()
		{

		}
	}
}
