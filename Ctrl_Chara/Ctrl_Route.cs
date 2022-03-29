using System.Windows.Forms;
using System.Drawing;
using System.IO;


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
			EL_Route.Listbox_Add = ()=>
			{
				SetRoute ( EL_Route.Get() ); 
			};
			//選択変更時
			EL_Route.SelectedIndexChanged = ()=>
			{
				SetRoute ( EL_Route.Get() );
			};

			//名前存在チェック
			EL_Route.Func_color_check = (ob)=>
			{
				Route rut = ((Route)ob);
				foreach ( TName tn in rut.BD_BranchName.GetEnumerable () )
				{
					if ( ! BD_Branch.ContainsKey ( tn.Name ) ) return true;
				}
				return false;
			};

			//IO
			EL_Route.SetIOFunc ( SaveRoute, LoadRoute );

			//----------------------------------
			//コントロール(ブランチ)
			EL_Branch.Location = new Point ( 303, 70 );
			this.Controls.Add ( EL_Branch );
			EL_Branch.TbName_Off ();
			EL_Branch.SelectedIndexChanged = ()=>
			{
				SelectBranch ();
			};
			//==============================================================

//			CB_Branch.DisplayMember = "Name";
			CB_Branch.ValueMember = "Name";
		}

		//キャラデータの設定
		public void SetCharaData ( Chara ch )
		{
			//保存
			BD_Branch = ch.BD_Branch;
			CB_Branch.DataSource = BD_Branch.GetBindingList ();

			//リストに登録
			EL_Route.SetData ( ch.BD_Route );

			if ( EL_Route.Count () < 1 ) { return; }

			//データの設定
			SetRoute ( EL_Route.Get() );
		}

		//ルートの設定
		public void SetRoute ( Route rut )
		{
			Tb_Summary.Text = rut.Summary;
			EL_Branch.SetData ( rut.BD_BranchName );

			//ブランチの選択
			SelectBranch ();
		}

		//ブランチの選択
		private void SelectBranch ()
		{
			//ブランチの選択
			if ( 0 == EL_Branch.GetListBox().Items.Count ) { return; }
			TName tn = EL_Branch.Get ();
			CB_Branch.SelectedValue = tn.Name;
		}

		//コンボボックス選択時
		private void CB_Branch_SelectionChangeCommitted ( object sender, System.EventArgs e )
		{
			TName tn = EL_Branch.Get ();
			tn.Name = ((Branch)CB_Branch.SelectedItem).Name;
			EL_Branch.ResetItems ();
		}

		//--------------------------------------
		//IO
		public void SaveRoute ( object ob, StreamWriter sw )
		{
			Route rut = (Route)ob;
			sw.Write ( rut.Name + "," );
			sw.Write ( rut.Summary + "," );
			sw.Write ( ";" );
			foreach ( TName tn in rut.BD_BranchName.GetEnumerable() )
			{
				sw.Write ( tn.Name + "," );
			}
			sw.Write ( ";" );
		}

		public void LoadRoute ( StreamReader sr )
		{
			Route rut = new Route ();
			string str = sr.ReadLine ();
			string[] str_spl_semi = str.Split ( ';' );
			string[] str_pre = str_spl_semi[0].Split(',');
			rut.Name = str_pre[0];
			rut.Summary = str_pre[1];

			string[] str_brcName = str_spl_semi[1].Split(',');
			foreach ( string strName in str_brcName )
			{
				rut.BD_BranchName.Add ( new TName ( strName ) );
			}
			
			EL_Route.Add ( rut );
		}
	}
}
