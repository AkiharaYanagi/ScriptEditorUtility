using System;
using System.Windows.Forms;
using System.ComponentModel;

namespace ScriptEditor
{
	using BD_Brc = BindingDictionary < Branch0 >;
	using BD_Seq = BindingDictionary < Sequence >;
	using BD_Cmd = BindingDictionary < Command >;
	using BL_Seq = BindingList < Sequence >;
	using BL_Cmd = BindingList < Command >;

	public partial class Ctrl_Branch :UserControl
	{
		//対象
		public BD_Brc BD_Branch { get; set; } = new BD_Brc ();
		public BD_Seq BD_Sequence { get; set; } = new BD_Seq ();
		public BD_Cmd BD_Command { get; set; } = new BD_Cmd ();

		public BL_Seq BL_Act { get; set; } = new BL_Seq ();
		public BL_Cmd BL_Cmd { get; set; } = new BL_Cmd ();

		//選択
		public Branch0 SelectedBranch { get; set; } = new Branch0 ();

		//コントロール
		EditListbox < Branch0 > EL_Branch = new EditListbox<Branch0> ();
		private ListBox listBox1 = null;

		//コンストラクタ
		public Ctrl_Branch ()
		{
			InitializeComponent ();

			BD_Branch.Add ( new Branch0 () );
			SelectedBranch = BD_Branch.Get ( 0 );
			Tb_Name.Text = SelectedBranch.Name;
			
			//==============================================================
			//エディットリストボックスの設定
			this.Controls.Add ( EL_Branch );

			EL_Branch.SetData ( BD_Branch );
			EL_Branch.ResetItems ();

			listBox1 = EL_Branch.GetListBox ();
			listBox1.DisplayMember = "Name";

			//選択移動時のイベント
			EL_Branch.SelectedIndexChanged = ()=>
			{
				if ( 0 >= listBox1.SelectedItems.Count ) { return; }
		
				SelectedBranch = EL_Branch.Get ();
				Tb_Name.Text = SelectedBranch.Name;

				Cb_Command.SelectedValue = SelectedBranch.NameCommand;

				listBox1.DisplayMember = "Name";
			};

			//追加時
			EL_Branch.Add = ()=>
			{

			};
		}

		//データ設定
		public void SetCharaData ( Chara ch )
		{
			BD_Branch = ch.BD_Branch;
			EL_Branch.SetData ( BD_Branch );

			BD_Command = ch.BD_Command;
			BD_Sequence = ch.behavior.BD_Sequence;

			//コンボボックスの更新
			BL_Act = ch.behavior.BD_Sequence.GetBindingList ();
			Cb_Action.DataSource = BL_Act;
			BL_Cmd = ch.BD_Command.GetBindingList ();
			Cb_Command.DataSource = BL_Cmd;
			Cb_Command.ValueMember = "Name";
		}

		private void Tb_Name_TextChanged ( object sender, EventArgs e )
		{
			SelectedBranch.Name = Tb_Name.Text;
			EL_Branch.ResetItems ();
		}

		private void Cb_Command_SelectionChangeCommitted ( object sender, EventArgs e )
		{

		}
	}
}
