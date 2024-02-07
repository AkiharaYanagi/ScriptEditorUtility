using System;
using System.Drawing;
using System.Windows.Forms;


namespace ScriptEditor
{
	//スクリプト内のルートリストを編集するコントロール
	//コンペンド編集からサブフォームで呼び出す
	public class Ctrl_Scp_Route : UserControl
	{
		//対象データ(ルート[名前] リスト)
		//エディットリストボックス
		EditListbox < TName > EL_Route = new EditListbox < TName > ();

		//元データとなるキャラ内のルートリスト
		private BindingDictionary < Route > BD_Route = new BindingDictionary<Route> ();

		//確認・選択
		private ComboBox Cb_Route = new ComboBox ();

		//コピー用
		private ListBox Lb_Copy = new ListBox ();
		private Button Btn_Copy;
		private Button Btn_PasteAll;
		private Button Btn_PasteGroup;

		//全体編集
		private EditCompend EditCompend = new EditCompend ();


		//コンストラクタ
		public Ctrl_Scp_Route ()
		{
			//コンポーネント初期化は手動で追加する
			InitializeComponent ();


			this.Size = new Size ( 400, 600 );

			//==============================================================
			//エディットリストボックスの設定

			EL_Route.Location = new Point ( 3, -70 );
			this.Controls.Add ( EL_Route );
			EL_Route.IO_Visible ( false );
			EL_Route.TbName_Off ();

			//追加時
			EL_Route.Listbox_Add = ()=>
			{
#if false
				//対象の先頭を指定
				if ( 0 < BD_Route.Count() )
				{
					Route rut = BD_Route.Get ( 0 );
					//追加しているので必ず１個以上存在する
					EL_Route.BD_T.Get(0).Name = rut.Name;
					EL_Route.ResetItems ();
				}
#endif
			};
			//選択変更時
			EL_Route.SelectedIndexChanged = ()=>
			{
				if ( BD_Route.ContainsKey ( EL_Route.Get ().Name ) )
				{
					Cb_Route.SelectedValue = EL_Route.Get ().Name;
				}
				else
				{
					Cb_Route.SelectedIndex = -1;
				}
			};
			EL_Route.Func_color_check = (ob)=>
			{
				//ルート名が存在するかどうか
				return ! BD_Route.ContainsKey ( ((TName)ob).Name );
			};
			//==============================================================

			Cb_Route.Location = new Point ( 250, 20 );
			Cb_Route.SelectionChangeCommitted += new EventHandler ( Cb_Route_SelectionChangeCommitted );
			this.Controls.Add ( Cb_Route );
			Cb_Route.DisplayMember = "Name";

			Lb_Copy.Location = new Point ( 250, 120 );
			this.Controls.Add ( Lb_Copy );
			Lb_Copy.DisplayMember = "Name";

		}

		public void SetEnvironment ( EditCompend ec )
		{
			EditCompend = ec;
		}


		private void InitializeComponent ()
		{
			this.Btn_Copy = new System.Windows.Forms.Button();
			this.Btn_PasteAll = new System.Windows.Forms.Button();
			this.Btn_PasteGroup = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// Btn_Copy
			// 
			this.Btn_Copy.Location = new System.Drawing.Point(242, 57);
			this.Btn_Copy.Name = "Btn_Copy";
			this.Btn_Copy.Size = new System.Drawing.Size(111, 41);
			this.Btn_Copy.TabIndex = 0;
			this.Btn_Copy.Text = "コピー";
			this.Btn_Copy.UseVisualStyleBackColor = true;
			this.Btn_Copy.Click += new System.EventHandler(this.Btn_Copy_Click);
			// 
			// Btn_PasteAll
			// 
			this.Btn_PasteAll.Location = new System.Drawing.Point(253, 277);
			this.Btn_PasteAll.Name = "Btn_PasteAll";
			this.Btn_PasteAll.Size = new System.Drawing.Size(100, 41);
			this.Btn_PasteAll.TabIndex = 1;
			this.Btn_PasteAll.Text = "全体にペースト";
			this.Btn_PasteAll.UseVisualStyleBackColor = true;
			this.Btn_PasteAll.Click += new System.EventHandler(this.Btn_PastAll_Click);
			// 
			// Btn_PasteGroup
			// 
			this.Btn_PasteGroup.Location = new System.Drawing.Point(253, 337);
			this.Btn_PasteGroup.Name = "Btn_PasteGroup";
			this.Btn_PasteGroup.Size = new System.Drawing.Size(100, 41);
			this.Btn_PasteGroup.TabIndex = 1;
			this.Btn_PasteGroup.Text = "グループにペースト";
			this.Btn_PasteGroup.UseVisualStyleBackColor = true;
			this.Btn_PasteGroup.Click += new System.EventHandler(this.Btn_PasteGroup_Click);
			// 
			// Ctrl_Scp_Route
			// 
			this.Controls.Add(this.Btn_PasteGroup);
			this.Controls.Add(this.Btn_PasteAll);
			this.Controls.Add(this.Btn_Copy);
			this.Name = "Ctrl_Scp_Route";
			this.Size = new System.Drawing.Size(384, 444);
			this.ResumeLayout(false);

		}
	

		//キャラデータ設置
		public void SetCharaData ( Chara ch )
		{
			Cb_Route.DataSource = ch.BD_Route.GetBindingList ();
			Cb_Route.DisplayMember = "Name";
			Cb_Route.ValueMember = "Name";

			BD_Route = ch.BD_Route;
		}

		//関連付け
		public void Assosiate ()
		{
			Script scp = EditCompend.SelectedScript;
			EL_Route.SetData ( scp.BD_RutName );
		}

		//データ更新
		public void UpdateData ()
		{
			EL_Route._UpdateData ();
		}

		
		//表示
		public void Disp ()
		{
			EL_Route.Invalidate ();
		}


		//コンボボックスで選択
		private void Cb_Route_SelectionChangeCommitted ( object sender, EventArgs e )
		{
			Route rut = (Route)Cb_Route.SelectedItem;
			EL_Route.Get ().Name = rut.Name;
			EL_Route.ResetItems ();
		}

		//--------------------------------------------------------------------
		//一時コピー
		private void Btn_Copy_Click ( object sender, EventArgs e )
		{
			ListBox lb = EL_Route.GetListBox ();
			Lb_Copy.Items.Clear ();
			foreach ( object ob in lb.Items )
			{
				Lb_Copy.Items.Add ( (TName)ob );
			}
		}

		
		//スクリプトにコピーしているルートリストを設定する全体編集用関数
		private void SetBD_Route ( Script scp )
		{
			scp.BD_RutName.Clear ();
			foreach ( TName tn in Lb_Copy.Items )
			{
				scp.BD_RutName.Add ( tn );
			}
		}

		//グループにペースト
		private void Btn_PasteGroup_Click ( object sender, EventArgs e )
		{
			EditCompend.EditScript.DoGroup ( scp => SetBD_Route ( scp ) );
		}

		//全体にペースト
		private void Btn_PastAll_Click ( object sender, EventArgs e )
		{
			EditCompend.DoAllScript ( scp => SetBD_Route ( scp ) );
		}
	}
}
