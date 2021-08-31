using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;


namespace ScriptEditor
{
	//---------------------------------------------------
	//	ルートを編集するコントロール
	//---------------------------------------------------
	using BD_Rot = BindingDictionary < Route >;
	using BD_Brc = BindingDictionary < Branch0 >;

	public partial class Ctrl_Route :UserControl
	{
		//対象
		public BD_Rot BD_Route { get; set; } = new BD_Rot ();
		public BD_Brc BD_Branch { get; set; } = new BD_Brc ();

		//コントロール
		EditListbox < Route > EL_Route = new EditListbox < Route > ();
		EditListbox < Branch0 > EL_Branch = new EditListbox < Branch0 > ();

		//選択
		private Route SelectedRoute = null;

		public Ctrl_Route ()
		{
			InitializeComponent ();

			//==============================================================
			//エディットリストボックスの設定

			//--------------------------------------------------
			//ルート
			EL_Route.Location = new Point ( 3, 70 );
			this.Controls.Add ( EL_Route );

			//追加時
			EL_Route.Add = ()=>
			{
				SelectedRoute = EL_Route.Get();
				SetRoute ( SelectedRoute ); 
			};
			//選択変更時
			EL_Route.SelectedIndexChanged = ()=>
			{
				SelectedRoute = EL_Route.Get();
				SetRoute ( SelectedRoute );
			};
			//名前の変更
			EL_Route.Tb_KeyPress = ()=>
			{
				if ( SelectedRoute is null ) { return; }
				SelectedRoute.Name = EL_Route.GetName ();
				EL_Route.ResetItems ();
			};


			//--------------------------------------------------
			//ブランチ
			EL_Branch.Add = ()=>
			{
				Cb_Branch.Items.Clear ();

				Chara ch = EditChara.Inst.Chara;

				//ブランチ選択コンボボックス
				foreach ( Branch0 br in ch.BD_Branch.GetBindingList () )
				{
					Cb_Branch.Items.Add ( br );
					Cb_Branch.SelectedIndex = 0;
				}

			};

			//--------------------------------------------------
			EL_Branch.Location = new Point ( 300, 70 );
			this.Controls.Add ( EL_Branch );
			Cb_Branch.DisplayMember = "Name";
		}

		public void SetCharaData ( Chara ch )
		{
			//ルートの保存
			BD_Route = ch.BD_Route;
			EL_Route.SetData ( BD_Route );

			//ブランチの保存
			BD_Branch = ch.BD_Branch;
			EL_Branch.SetData ( BD_Branch );
			
			//ブランチ選択コンボボックス
			foreach ( Branch0 br in ch.BD_Branch.GetBindingList () )
			{
				Cb_Branch.Items.Add ( br );
				Cb_Branch.SelectedIndex = 0;
			}

			//コマンド選択コンボボックス
			foreach ( Command cmd in ch.BD_Command.GetBindingList () )
			{
				Cb_Command.Items.Add ( cmd );
				Cb_Command.SelectedIndex = 0;
			}

			//アクション選択コンボボックス
			foreach ( Action act in ch.behavior.BD_Sequence.GetBindingList () )
			{
				Cb_Action.Items.Add ( act );
				Cb_Action.SelectedIndex = 0;
			}

			//要約
			Tb_Summary.Text = BD_Route.GetBindingList () [ 0 ].Summary;
		}

		public void SetRoute ( Route rut )
		{
			BD_Branch = rut.BD_Branch;
			EL_Branch.SetData ( BD_Branch );
			Tb_Summary.Text = rut.Summary;
		}
	}
}
