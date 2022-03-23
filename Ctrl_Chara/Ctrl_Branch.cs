using System;
using System.Windows.Forms;
using System.Drawing;


namespace ScriptEditor
{
	using BD_Brc = BindingDictionary < Branch >;
	using BD_Seq = BindingDictionary < Sequence >;
	using BD_Cmd = BindingDictionary < Command >;

	public partial class Ctrl_Branch :UserControl
	{
		//対象
		private Chara Chara = null;
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

				Cb_Condition.SelectedItem = br.Condition;

				Cb_Command.Enabled = true;
				Cb_Command.SelectedValue = br.NameCommand;

				//アクション,エフェクト
				SelectSequence ( br.NameSequence );
			};

			EL_Branch.Func_check = (ob) =>
			{
				return ! Chara.behavior.BD_Sequence.ContainsKey ( ((Branch)ob).NameSequence );
			};

			//==============================================================
			Array names = Enum.GetValues ( typeof ( BranchCondition ) );
			Cb_Condition.DataSource = names;
			Cb_Condition.SelectedItem = BranchCondition.DMG;

			Cb_Command.ValueMember = "Name";
			Cb_Action.ValueMember = "Name";
			Cb_Effect.ValueMember = "Name";
			Cb_Effect.Enabled = false;
			//==============================================================
		}

		//データ設定
		public void SetCharaData ( Chara ch )
		{
			//データ参照の保存
			Chara = ch;
			BD_Branch = ch.BD_Branch;

			if ( 0 == ch.behavior.BD_Sequence.Count() ) { ch.behavior.BD_Sequence.New (); }
			if ( 0 == ch.BD_Command.Count() ) { ch.BD_Command.New (); }
			if ( 0 == ch.BD_Branch.Count() ) { ch.BD_Branch.New (); }

			//コントロールに設定
			EL_Branch.SetData ( ch.BD_Branch );

			
			//test リストボックス
			listBox1.DrawMode = DrawMode.OwnerDrawFixed;
			listBox1.DataSource = ch.BD_Branch.GetBindingList();
			listBox1.DisplayMember = "Name";


			//コンボボックスの更新
			Cb_Command.DataSource = ch.BD_Command.GetBindingList ();
			Cb_Action.DataSource = ch.behavior.BD_Sequence.GetBindingList ();
			Cb_Effect.DataSource = ch.garnish.BD_Sequence.GetBindingList ();

			//１つ以上存在したら選択
			if ( EL_Branch.Count () > 0 )
			{
				EL_Branch.GetListBox ().SelectedIndex = 0;
				Branch br = EL_Branch.Get ();
				Cb_Command.Enabled = true;
				Cb_Command.SelectedValue = br.NameCommand;
				SelectSequence ( br.NameSequence );
			}
		}

		public void SelectSequence ( string name )
		{
			Sequence sqc = Chara.behavior.BD_Sequence.Get ( name );
			if ( sqc is null )
			{
				OnEffect ();
				Cb_Effect.SelectedValue = name;
			}
			else
			{
				OnAction ();
				Cb_Action.SelectedValue = name;
			}
		}

		//コンボボックスユーザ選択時

		//条件
		private void Cb_Condition_SelectionChangeCommitted ( object sender, EventArgs e )
		{
			EL_Branch.Get ().Condition = (BranchCondition)Cb_Condition.SelectedItem;
		}

		//コマンド
		private void Cb_Command_SelectionChangeCommitted ( object sender, EventArgs e )
		{
			Branch br = EL_Branch.Get ();
			br.NameCommand = (string)Cb_Command.SelectedValue;

			BD_Branch.ResetItems ();
			EL_Branch.Invalidate ();
			listBox1.Invalidate ();
		}

		//アクション
		private void Cb_Action_SelectionChangeCommitted ( object sender, EventArgs e )
		{
			Branch br = EL_Branch.Get ();
			br.NameSequence = (string)Cb_Action.SelectedValue;

			BD_Branch.ResetItems ();
			EL_Branch.Invalidate ();
			listBox1.Invalidate ();
		}

		//エフェクト
		private void Cb_Effect_SelectionChangeCommitted ( object sender, EventArgs e )
		{
			Branch br = EL_Branch.Get ();
			br.NameSequence = (string)Cb_Effect.SelectedValue;
		}


		//--------------------------------------------------------------------
		//ラジオボタン連動
		private void RB_Effect_CheckedChanged ( object sender, EventArgs e )
		{
			Cb_Action.SelectedIndex = -1;
			Cb_Action.Enabled = false;
			Cb_Effect.Enabled = RB_Effect.Checked;
		}

		private void RB_Action_CheckedChanged ( object sender, EventArgs e )
		{
			Cb_Effect.SelectedIndex = -1;
			Cb_Effect.Enabled = false;
			Cb_Action.Enabled = RB_Action.Checked;
		}

		private void OnAction ()
		{
			Cb_Effect.SelectedIndex = -1;
			Cb_Effect.Enabled = false;
			RB_Effect.Checked = false;

			Cb_Action.Enabled = true;
			RB_Action.Checked = true;
		}
		private void OnEffect ()
		{
			Cb_Action.SelectedIndex = -1;
			Cb_Action.Enabled = false;
			RB_Action.Checked = false;

			Cb_Effect.Enabled = true;
			RB_Effect.Checked = true;
		}


		//test 
		private void listBox1_DrawItem ( object sender, DrawItemEventArgs e )
		{
			e.DrawBackground ();

			Branch brc = (Branch)listBox1.Items[e.Index];
			string name = brc.NameSequence;
			Brush Brs = Brushes.Black;

			//名前指定チェック
			if ( ! Chara.behavior.BD_Sequence.ContainsKey ( brc.NameSequence ) )
			{
				Brs = Brushes.Red;
			}

			e.Graphics.DrawString ( 
				name, e.Font, Brs, e.Bounds, StringFormat.GenericDefault );
			e.DrawFocusRectangle ();
		}


	}
}
