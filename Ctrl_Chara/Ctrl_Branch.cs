using System;
using System.Windows.Forms;
using System.ComponentModel;

namespace ScriptEditor
{
	using BD_Brc = BindingDictionary < Branch >;
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

		//コントロール
		EditListbox < Branch > EL_Branch = new EditListbox<Branch> ();

		//コンストラクタ
		public Ctrl_Branch ()
		{
			InitializeComponent ();

			//==============================================================
			//エディットリストボックスの設定
			this.Controls.Add ( EL_Branch );

			//選択移動時のイベント
			EL_Branch.SelectedIndexChanged = ()=>
			{
				Branch br = EL_Branch.Get ();
				Cb_Command.Enabled = true;
				Cb_Action.Enabled = true;
				Cb_Command.SelectedValue = br.NameCommand;
				Cb_Action.SelectedValue = br.NameAction;
			};

			Cb_Action.ValueMember = "Name";
			Cb_Command.ValueMember = "Name";
		}

		//データ設定
		public void SetCharaData ( Chara ch )
		{
			//データの保存
			BD_Branch = ch.BD_Branch;

			//コントロールに設定
			EL_Branch.SetData ( ch.BD_Branch );

			//コンボボックスの更新
			Cb_Action.DataSource = ch.behavior.BD_Sequence.GetBindingList ();
			Cb_Command.DataSource = ch.BD_Command.GetBindingList ();
		}

		public void SetRoute ( Route rut )
		{
			//ルートからBD_Brcを作成
			BD_Brc bd_Brc = new BD_Brc ();
			foreach ( string s in rut.BL_BranchName )
			{
				bd_Brc.Add ( BD_Branch.Get ( s ) );
			}
			//コントロールに設定
			EL_Branch.SetData ( bd_Brc );
			EL_Branch.ResetItems ();

			//選択
			Branch br = EL_Branch.Get ();
			if ( br is null )
			{
				Cb_Command.Enabled = false;
				Cb_Action.Enabled = false;
			}
			else
			{
				Cb_Command.Enabled = true;
				Cb_Action.Enabled = true;
				Cb_Command.SelectedValue = br.NameCommand;
				Cb_Action.SelectedValue = br.NameAction;
			}
		}


		//コンボボックスユーザ選択時
		private void Cb_Command_SelectionChangeCommitted ( object sender, EventArgs e )
		{
			Branch br = EL_Branch.Get ();
			br.NameCommand = (string)Cb_Command.SelectedValue;
		}

		private void Cb_Action_SelectionChangeCommitted ( object sender, EventArgs e )
		{
			Branch br = EL_Branch.Get ();
			br.NameAction = (string)Cb_Action.SelectedValue;
		}
	}
}
