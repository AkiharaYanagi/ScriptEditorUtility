using System;
using System.Windows.Forms;
using System.ComponentModel;

namespace ScriptEditor
{
	using BL_Ob = BindingList < object >;
	using BD_Brc = BindingDictionary < Branch >;

	public partial class Ctrl_Branch :UserControl
	{
		//対象
		public BD_Brc BD_Branch { get; set; } = new BD_Brc ();

		//選択
		public Branch SelectedBranch { get; set; } = new Branch ();

		private ListBox listBox1 = null;

		//コンストラクタ
		public Ctrl_Branch ()
		{
			InitializeComponent ();

			EditListbox < Branch > EL_Branch = new EditListbox<Branch> ();
			this.Controls.Add ( EL_Branch );

			//==============================================================

//			editListbox_01.BL_T = (BL_Ob)BD_Branch.GetBindingList ();
			listBox1 = editListbox_01.GetListBox ();

			//-------------------------------------------------
			//デリゲートの設定

			//生成
			editListbox_01.Make = ()=> { return new Branch (); };

			//選択移動時
			editListbox_01.SelectedIndexChanged = ()=>
			{
				if ( 0 >= listBox1.SelectedItems.Count ) { return; }
		
				SelectedBranch = (Branch) editListbox_01.GetObject ();
				Tb_Name.Text = SelectedBranch.Name;

				listBox1.DisplayMember = "Name";
			};
			//-------------------------------------------------

		}

		//データ設定
		public void SetData ( BD_Brc bd_brc )
		{
			BD_Branch = bd_brc;
		}

		private void Tb_Name_TextChanged ( object sender, EventArgs e )
		{
			SelectedBranch.Name = Tb_Name.Text;
			editListbox_01.ResetItems ();
		}
	}
}
