using System.Windows.Forms;


namespace ScriptEditor
{
	//エイリアス
	using BD_EfGnrt = BindingDictionary < EffectGenerate >;


	public partial class Ctrl_EfGnrt : UserControl
	{
		//対象
		public BD_EfGnrt BD_EfGnrt { get; set; } = new BD_EfGnrt();

		private EffectGenerate efgnrt = new EffectGenerate ();

		//コントロール
		private EditListbox < EffectGenerate > EL_EfGnrt = new EditListbox<EffectGenerate> ();


		//コンストラクタ
		public Ctrl_EfGnrt ()
		{
			InitializeComponent ();

			//==============================================================
			//エディットリストボックス
			EL_EfGnrt.Location = new System.Drawing.Point ( 3, 3 );

			//選択イベント
			EL_EfGnrt.SelectedIndexChanged = ()=>
			{
				efgnrt = EL_EfGnrt.Get();
				UpdateData ();
			};

			this.Controls.Add ( EL_EfGnrt );
			//==============================================================
		}

		//データ設定
		public void SetCharaData ( Chara ch )
		{
		}

		//関連付け
		public void Assosiate ( Script scp )
		{
			EL_EfGnrt.SetData ( scp.BD_EfGnrt );

			Tbn_x.Assosiate ( i=>efgnrt.SetPtX(i), ()=>efgnrt.Pt.X );
			Tbn_y.Assosiate ( i=>efgnrt.SetPtY(i), ()=>efgnrt.Pt.Y );
			Tbn_z.Assosiate ( i=>efgnrt.Z_PER100F=i, ()=>efgnrt.Z_PER100F );
		}

		//更新
		public void UpdateData ()
		{
			efgnrt = EL_EfGnrt.Get();

			Tbn_x.UpdateData ();
			Tbn_y.UpdateData ();
			Tbn_z.UpdateData ();
			
			Cbx_gnrt.Checked = efgnrt.Gnrt;
			Cbx_loop.Checked = efgnrt.Loop;
			Cbx_sync.Checked = efgnrt.Sync;
		}
	}
}
