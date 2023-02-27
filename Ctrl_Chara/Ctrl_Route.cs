using System.Windows.Forms;
using System.Drawing;
using System.IO;


namespace ScriptEditor
{
	//---------------------------------------------------
	//	ルートを編集するコントロール
	//---------------------------------------------------
	using BD_Brc = BindingDictionary < Branch >;

	public partial class Ctrl_Route : UserControl
	{
		//コントロール(ルート)
		EditListbox < Route > EL_Route = new EditListbox < Route > ();

		//コントロール(ブランチネーム)
		EditListbox < TName > EL_Branch = new EditListbox < TName > ();

		//キャラのブランチデータ参照
		BD_Brc BD_Branch = new BD_Brc ();

		//設定ファイル
		private Ctrl_Settings Ctrl_Stgs { get; set; } = new Ctrl_Settings ();


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
			EL_Route.Listbox_Add = ()=>SetRoute ( EL_Route.Get() );
			//ルート選択変更時
			//ブランチも表示を更新する
			EL_Route.SelectedIndexChanged = ()=>SetRoute ( EL_Route.Get() );

			//名前存在チェック
			EL_Route.Func_color_check = (ob)=>
			{
				//各ルートが保持するブランチ名が存在するかどうか
				Route rut = (Route)ob;
				foreach ( TName tn in rut.BD_BranchName.GetEnumerable () )
				{
					if ( ! BD_Branch.ContainsKey ( tn.Name ) ) { return true; }
				}
				return false;
			};

			//IO
			EL_Route.SetIOFunc ( SaveRoute, LoadRoute );
			EL_Route.Func_SavePath = s=>
			{
				Ctrl_Stgs.File_RouteList = s;
				XML_IO.Save ( Ctrl_Stgs );
			};

			//----------------------------------
			//コントロール(ブランチ)
			EL_Branch.Location = new Point ( 303, 70 );
			this.Controls.Add ( EL_Branch );
			EL_Branch.TbName_Off ();
			EL_Branch.SelectedIndexChanged = ()=>
			{
				SelectBranch ();
			};
			EL_Branch.Func_color_check = (ob)=>
			{
				return ! BD_Branch.ContainsKey ( ((TName)ob).Name );
			};
			//==============================================================

//			CB_Branch.DisplayMember = "Name";
			CB_Branch.ValueMember = "Name";
		}

		//キャラデータの設定
		public void SetCharaData ( Chara ch )
		{
			//ブランチの元データを保存
			BD_Branch = ch.BD_Branch;

			//ブランチをコントロールに設定
			CB_Branch.DataSource = ch.BD_Branch.GetBindingList ();

			//ルートをリストに登録
			EL_Route.SetData ( ch.BD_Route );

			//データの設定
			if ( EL_Route.Count () < 1 ) { return; }
			SetRoute ( EL_Route.Get() );
		}

		public void SetEnvironment ( Ctrl_Settings stgs )
		{
			Ctrl_Stgs = stgs;
		}

		public void LoadData ()
		{
			EL_Route.LoadData ( Ctrl_Stgs.File_RouteList );
		}

		public void UpdateData ()
		{
			BD_Branch.ResetItems ();
		}


		//==============================================================
		//ルートの設定
		public void SetRoute ( Route rut )
		{
			//名前
			Tb_Summary.Text = rut.Summary;

			//ブランチネーム
			if ( rut.BD_BranchName.Count () < 1 ) 
			{
				//0のときも空欄表示
				EL_Branch.SetData ( rut.BD_BranchName );

				return; 
			}
			EL_Branch.SetData ( rut.BD_BranchName );

			//ブランチの選択
			SelectBranch ();
		}

		//ルートの選択
		private void SelectRoute ()
		{
			Route rut = EL_Route.Get();
			EL_Branch.SetData ( rut.BD_BranchName );

			//ブランチの選択
			SelectBranch ();
		}

		//ブランチの選択
		private void SelectBranch ()
		{
			//ブランチの選択
			if ( 0 == EL_Branch.GetListBox().Items.Count )
			{
				CB_Branch.SelectedIndex = -1;
				CB_Branch.Enabled = false;
				return; 
			}
			else
			{
				CB_Branch.Enabled = true;
			}

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
			sw.Write ( rut.Summary + ";" );	//前半区切り";"セミコロン
			int i = 0;
			int count = rut.BD_BranchName.Count ();
			foreach ( TName tn in rut.BD_BranchName.GetEnumerable() )
			{
				sw.Write ( tn.Name );
				if ( count == ++ i ) { break; }	//リストの最後には","カンマを追加しない
				sw.Write ( "," );
			}
			sw.Write ( ";" );
		}

		public void LoadRoute ( StreamReader sr )
		{
			Route rut = new Route ();
			string str = sr.ReadLine ();
			string[] str_spl_semi = str.Split ( ';' ); //	"前半";"後半";
			
			//前半	"名前","要約";
			string[] str_pre = str_spl_semi[0].Split(',');
			rut.Name = str_pre[0];
			rut.Summary = str_pre[1];

			//後半	"ブランチ名",……,"ブランチ名";
			string[] str_brcName = str_spl_semi[1].Split(',');
			foreach ( string strName in str_brcName )
			{
				rut.BD_BranchName.Add ( new TName ( strName ) );
			}
			
			EL_Route.Add ( rut );
		}
	}
}
