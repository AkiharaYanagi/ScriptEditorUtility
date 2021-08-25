using System;
using System.Windows.Forms;
using System.ComponentModel;

namespace ScriptEditor
{
	using BD_Brc = BindingDictionary < Branch0 >;
	using BL_Act = BindingList < Action >;
	using BL_Cmd = BindingList < Command >;

	public partial class Ctrl_Branch :UserControl
	{
		//対象
		public BD_Brc BD_Branch { get; set; } = new BD_Brc ();

		public BL_Act BL_Act { get; set; } = new BL_Act ();
		public BL_Cmd BL_Cmd { get; set; } = new BL_Cmd ();

		//選択
		public Branch0 SelectedBranch { get; set; } = new Branch0 ();

		EditListbox < Branch0 > EL_Branch = new EditListbox<Branch0> ();
		private ListBox listBox1 = null;

		//コンストラクタ
		public Ctrl_Branch ()
		{
			InitializeComponent ();

			BD_Branch.Add ( new Branch0 () );


			SelectedBranch = BD_Branch.GetBindingList () [ 0 ];

			Tb_Name.Text = SelectedBranch.Name;
			Cb_Action.DataSource = BL_Act;
			Cb_Command.DataSource = BL_Cmd;


			SetData ( BD_Branch );
			EL_Branch.ResetItems ();

			//==============================================================
			//エディットリストボックスの設定

			this.Controls.Add ( EL_Branch );

			listBox1 = EL_Branch.GetListBox ();
			listBox1.DisplayMember = "Name";

			//選択移動時のイベント
			EL_Branch.SelectedIndexChanged = ()=>
			{
				if ( 0 >= listBox1.SelectedItems.Count ) { return; }
		
				SelectedBranch = EL_Branch.Get ();
				Tb_Name.Text = SelectedBranch.Name;

				listBox1.DisplayMember = "Name";
			};

			//==============================================================
#if false

			// BindingList < object >

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
#endif

		}

		//データ設定
		public void SetData ( BD_Brc bd_brc )
		{
			BD_Branch = bd_brc;
			EL_Branch.SetData ( BD_Branch.GetBindingList () );
		}

		public void SetCharaData ( Chara ch )
		{
			BD_Branch = ch.BD_Branch;
			EL_Branch.SetData ( BD_Branch.GetBindingList () );
		}

		private void Tb_Name_TextChanged ( object sender, EventArgs e )
		{
			SelectedBranch.Name = Tb_Name.Text;
			EL_Branch.ResetItems ();
		}
	}
}
