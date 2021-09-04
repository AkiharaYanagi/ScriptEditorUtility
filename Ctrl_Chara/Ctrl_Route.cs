using System.Windows.Forms;
using System.Drawing;

namespace ScriptEditor
{
	//---------------------------------------------------
	//	ルートを編集するコントロール
	//---------------------------------------------------
	using BD_Rot = BindingDictionary < Route >;

	public partial class Ctrl_Route : UserControl
	{
		//コントロール
		EditListbox < Route > EL_Route = new EditListbox < Route > ();

		public Ctrl_Route ()
		{
			InitializeComponent ();

			//==============================================================
			//エディットリストボックスの設定

			//コントロール
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
		}

		public void SetCharaData ( Chara ch )
		{
			//キャラデータの保存
			ctrl_Branch1.SetCharaData ( ch );

			//リストに登録
			EL_Route.SetData ( ch.BD_Route );

			//データの設定
			SetRoute ( EL_Route.Get() );
		}

		public void SetRoute ( Route rut )
		{
			Tb_Summary.Text = rut.Summary;
			ctrl_Branch1.SetRoute ( rut );
		}
	}
}
