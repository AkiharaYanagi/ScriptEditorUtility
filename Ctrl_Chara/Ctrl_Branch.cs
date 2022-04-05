using System;
using System.Windows.Forms;
using System.IO;


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

		//設定ファイル
		private Ctrl_Settings Ctrl_Stgs { get; set; } = new Ctrl_Settings ();

		//コンストラクタ
		public Ctrl_Branch ()
		{
			InitializeComponent ();

			//==============================================================
			//エディットリストボックスの設定
			EL_Branch.Location = new System.Drawing.Point ( 3, 3 );

			//選択移動時のイベント
			EL_Branch.SelectedIndexChanged = ()=>
			{
				Branch br = EL_Branch.Get ();

				//条件
				Cb_Condition.SelectedItem = br.Condition;

				//コマンド
				Cb_Command.Enabled = true;
				Cb_Command.SelectedValue = br.NameCommand;

				//アクション,エフェクト
				SelectSequence ( br.NameSequence );

				//フレーム
				Tbn_Frame.Assosiate ( i=>br.Frame = i, ()=>br.Frame );
			};

			EL_Branch.Func_color_check = (ob) =>
			{
				return ! Chara.behavior.BD_Sequence.ContainsKey ( ((Branch)ob).NameSequence );
			};

			//IO
			EL_Branch.SetIOFunc ( SaveBranch, LoadBranch );
			EL_Branch.Func_SavePath = s=>
			{
				Ctrl_Stgs.File_BranchList = s;
				XML_IO.Save ( Ctrl_Stgs );
			};

			this.Controls.Add ( EL_Branch );

			//==============================================================
			//ブランチコンディション コンボボックス
			Array names = Enum.GetValues ( typeof ( BranchCondition ) );
			Cb_Condition.DataSource = names;
			Cb_Condition.SelectedItem = BranchCondition.DMG;

			Cb_Command.ValueMember = "Name";
			Cb_Action.ValueMember = "Name";
			Cb_Effect.ValueMember = "Name";
			Cb_Effect.Enabled = false;
			//==============================================================
		}

		public void SetEnvironment ( Ctrl_Settings stgs )
		{
			Ctrl_Stgs = stgs;
		}

		//データ設定
		public void SetCharaData ( Chara ch )
		{
			//データ参照の保存
			Chara = ch;
			BD_Branch = ch.BD_Branch;

#if false
			if ( 0 == ch.behavior.BD_Sequence.Count() ) { ch.behavior.BD_Sequence.New (); }
			if ( 0 == ch.BD_Command.Count() ) { ch.BD_Command.New (); }
			if ( 0 == ch.BD_Branch.Count() ) { ch.BD_Branch.New (); }
#endif

			//コントロールに設定
			EL_Branch.SetData ( ch.BD_Branch );

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

		public void LoadData ()
		{
			EL_Branch.LoadData ( Ctrl_Stgs.File_BranchList );
		}

		public void SelectSequence ( string name )
		{
			Sequence sqc = Chara.garnish.BD_Sequence.Get ( name );
			if ( sqc is null )
			{
				OnAction ();
				Cb_Action.SelectedValue = name;
			}
			else
			{
				OnEffect ();
				Cb_Effect.SelectedValue = name;
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
		}

		//アクション
		private void Cb_Action_SelectionChangeCommitted ( object sender, EventArgs e )
		{
			Branch br = EL_Branch.Get ();
			br.NameSequence = (string)Cb_Action.SelectedValue;

			BD_Branch.ResetItems ();
			EL_Branch.Invalidate ();
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


		//-------------------------------------------------------------------------
		//IO

		//単体保存
		public void SaveBranch ( object ob, StreamWriter sw )
		{
			Branch brc = (Branch)ob;
			sw.Write ( brc.Name + "," );
			sw.Write ( brc.Condition + "," );
			sw.Write ( brc.NameCommand + "," );
			sw.Write ( brc.NameSequence + "," );
			sw.Write ( brc.Frame );
		}

		//単体読込
		public void LoadBranch ( StreamReader sr )
		{
			Branch brc = new Branch ();

			string str = sr.ReadLine ();
			string[] str_spl = str.Split(',');
			brc.Name = str_spl[0];
			brc.Condition = ( BranchCondition ) Enum.Parse ( typeof ( BranchCondition ), str_spl[1] );
			brc.NameCommand = str_spl[2];
			brc.NameSequence = str_spl[3];
			brc.Frame = int.Parse ( str_spl[4] );

			EL_Branch.Add ( brc );
		}

	}
}
