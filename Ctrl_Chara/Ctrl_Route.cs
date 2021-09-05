using System.Windows.Forms;
using System.Drawing;

namespace ScriptEditor
{
	//---------------------------------------------------
	//	ルートを編集するコントロール
	//---------------------------------------------------
	using BD_Rot = BindingDictionary < Route >;
	using BD_Brc = BindingDictionary < Branch >;

	public partial class Ctrl_Route : UserControl
	{
		//コントロール(ルート)
		EditListbox < Route > EL_Route = new EditListbox < Route > ();

		//コントロール(ブランチ)
		EditListbox < TName > EL_Branch = new EditListbox < TName > ();

		//ブランチ参照
		BD_Brc BD_Branch = new BD_Brc ();


		//コンストラクタ
		public Ctrl_Route ()
		{
			InitializeComponent ();

			//==============================================================
			//エディットリストボックスの設定

			//----------------------------------
			//コントロール(ルート)
			EL_Route.Location = new Point ( 3, 70 );
			this.Controls.Add ( EL_Route );

			//追加時
			EL_Route.Add = ()=>
			{
				SetRoute ( EL_Route.Get() ); 
			};
			//選択変更時
			EL_Route.SelectedIndexChanged = ()=>
			{
				SetRoute ( EL_Route.Get() );
			};

			//----------------------------------
			//コントロール(ブランチ)
			EL_Branch.Location = new Point ( 303, 70 );
			this.Controls.Add ( EL_Branch );

		}

		public void SetCharaData ( Chara ch )
		{
			//保存
			BD_Branch = ch.BD_Branch;

			//リストに登録
			EL_Route.SetData ( ch.BD_Route );

			//データの設定
			SetRoute ( EL_Route.Get() );
		}

		public void SetRoute ( Route rut )
		{
			Tb_Summary.Text = rut.Summary;
			EL_Branch.SetData ( rut.BL_BranchName );
		}
	}
}
