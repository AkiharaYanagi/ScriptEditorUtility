using System;
using System.Windows.Forms;


namespace ScriptEditor
{
	//アクション設定用だが、エフェクトも流用する

	public class Ctrl_Action : UserControl
	{
		private Label Lbl_Name;
		public TextBox TB_Name;
		private Label Lbl_Next;
		public CB_SequenceList CBSL_Next;
		private Label Lbl_Posture;
		public ComboBox CB_Posture;
		private Label Lbl_Category;
		public ComboBox CB_Category;
		private Label Lbl_HitNum;
		private TB_Number TBN_HitNum;
		private Label Lbl_HitPitch;
		private TB_Number Tbn_HitPitch;
		private Label lbl_Balance;
		private TB_Number Tbn_Mana;
		private Label Lbl_Mana;
		private TB_Number Tbn_Accel;
		private Label Lbl_Accel;
		private Button Btn_Cancel;
		private Button Btn_OK;
		private TB_Number Tbn_Balance;


		private void InitializeComponent ()
		{
			this.lbl_Balance = new System.Windows.Forms.Label();
			this.Tbn_Balance = new ScriptEditor.TB_Number();
			this.Tbn_HitPitch = new ScriptEditor.TB_Number();
			this.TBN_HitNum = new ScriptEditor.TB_Number();
			this.CBSL_Next = new ScriptEditor.CB_SequenceList();
			this.CB_Posture = new System.Windows.Forms.ComboBox();
			this.CB_Category = new System.Windows.Forms.ComboBox();
			this.TB_Name = new System.Windows.Forms.TextBox();
			this.Lbl_Posture = new System.Windows.Forms.Label();
			this.Lbl_HitPitch = new System.Windows.Forms.Label();
			this.Lbl_HitNum = new System.Windows.Forms.Label();
			this.Lbl_Category = new System.Windows.Forms.Label();
			this.Lbl_Next = new System.Windows.Forms.Label();
			this.Lbl_Name = new System.Windows.Forms.Label();
			this.Tbn_Mana = new ScriptEditor.TB_Number();
			this.Lbl_Mana = new System.Windows.Forms.Label();
			this.Tbn_Accel = new ScriptEditor.TB_Number();
			this.Lbl_Accel = new System.Windows.Forms.Label();
			this.Btn_Cancel = new System.Windows.Forms.Button();
			this.Btn_OK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lbl_Balance
			// 
			this.lbl_Balance.AutoSize = true;
			this.lbl_Balance.Location = new System.Drawing.Point(13, 231);
			this.lbl_Balance.Name = "lbl_Balance";
			this.lbl_Balance.Size = new System.Drawing.Size(41, 12);
			this.lbl_Balance.TabIndex = 26;
			this.lbl_Balance.Text = "バランス";
			// 
			// Tbn_Balance
			// 
			this.Tbn_Balance.GetFunc = null;
			this.Tbn_Balance.Location = new System.Drawing.Point(70, 228);
			this.Tbn_Balance.Name = "Tbn_Balance";
			this.Tbn_Balance.SetFunc = null;
			this.Tbn_Balance.Size = new System.Drawing.Size(100, 19);
			this.Tbn_Balance.TabIndex = 23;
			this.Tbn_Balance.Text = "0";
			// 
			// Tbn_HitPitch
			// 
			this.Tbn_HitPitch.GetFunc = null;
			this.Tbn_HitPitch.Location = new System.Drawing.Point(70, 193);
			this.Tbn_HitPitch.Name = "Tbn_HitPitch";
			this.Tbn_HitPitch.SetFunc = null;
			this.Tbn_HitPitch.Size = new System.Drawing.Size(100, 19);
			this.Tbn_HitPitch.TabIndex = 24;
			this.Tbn_HitPitch.Text = "0";
			// 
			// TBN_HitNum
			// 
			this.TBN_HitNum.GetFunc = null;
			this.TBN_HitNum.Location = new System.Drawing.Point(70, 156);
			this.TBN_HitNum.Name = "TBN_HitNum";
			this.TBN_HitNum.SetFunc = null;
			this.TBN_HitNum.Size = new System.Drawing.Size(100, 19);
			this.TBN_HitNum.TabIndex = 25;
			this.TBN_HitNum.Text = "0";
			// 
			// CBSL_Next
			// 
			this.CBSL_Next.FormattingEnabled = true;
			this.CBSL_Next.Location = new System.Drawing.Point(70, 43);
			this.CBSL_Next.Name = "CBSL_Next";
			this.CBSL_Next.SetFunc = null;
			this.CBSL_Next.Size = new System.Drawing.Size(218, 20);
			this.CBSL_Next.TabIndex = 22;
			// 
			// CB_Posture
			// 
			this.CB_Posture.FormattingEnabled = true;
			this.CB_Posture.Location = new System.Drawing.Point(70, 119);
			this.CB_Posture.Name = "CB_Posture";
			this.CB_Posture.Size = new System.Drawing.Size(219, 20);
			this.CB_Posture.TabIndex = 21;
			// 
			// CB_Category
			// 
			this.CB_Category.FormattingEnabled = true;
			this.CB_Category.Location = new System.Drawing.Point(70, 82);
			this.CB_Category.Name = "CB_Category";
			this.CB_Category.Size = new System.Drawing.Size(220, 20);
			this.CB_Category.TabIndex = 20;
			// 
			// TB_Name
			// 
			this.TB_Name.Location = new System.Drawing.Point(70, 8);
			this.TB_Name.Name = "TB_Name";
			this.TB_Name.Size = new System.Drawing.Size(219, 19);
			this.TB_Name.TabIndex = 19;
			// 
			// Lbl_Posture
			// 
			this.Lbl_Posture.AutoSize = true;
			this.Lbl_Posture.Location = new System.Drawing.Point(13, 122);
			this.Lbl_Posture.Name = "Lbl_Posture";
			this.Lbl_Posture.Size = new System.Drawing.Size(29, 12);
			this.Lbl_Posture.TabIndex = 18;
			this.Lbl_Posture.Text = "体勢";
			// 
			// Lbl_HitPitch
			// 
			this.Lbl_HitPitch.AutoSize = true;
			this.Lbl_HitPitch.Location = new System.Drawing.Point(13, 196);
			this.Lbl_HitPitch.Name = "Lbl_HitPitch";
			this.Lbl_HitPitch.Size = new System.Drawing.Size(52, 12);
			this.Lbl_HitPitch.TabIndex = 16;
			this.Lbl_HitPitch.Text = "ヒット間隔";
			// 
			// Lbl_HitNum
			// 
			this.Lbl_HitNum.AutoSize = true;
			this.Lbl_HitNum.Location = new System.Drawing.Point(13, 159);
			this.Lbl_HitNum.Name = "Lbl_HitNum";
			this.Lbl_HitNum.Size = new System.Drawing.Size(40, 12);
			this.Lbl_HitNum.TabIndex = 17;
			this.Lbl_HitNum.Text = "ヒット数";
			// 
			// Lbl_Category
			// 
			this.Lbl_Category.AutoSize = true;
			this.Lbl_Category.Location = new System.Drawing.Point(13, 85);
			this.Lbl_Category.Name = "Lbl_Category";
			this.Lbl_Category.Size = new System.Drawing.Size(39, 12);
			this.Lbl_Category.TabIndex = 15;
			this.Lbl_Category.Text = "カテゴリ";
			// 
			// Lbl_Next
			// 
			this.Lbl_Next.AutoSize = true;
			this.Lbl_Next.Location = new System.Drawing.Point(13, 48);
			this.Lbl_Next.Name = "Lbl_Next";
			this.Lbl_Next.Size = new System.Drawing.Size(17, 12);
			this.Lbl_Next.TabIndex = 14;
			this.Lbl_Next.Text = "次";
			// 
			// Lbl_Name
			// 
			this.Lbl_Name.AutoSize = true;
			this.Lbl_Name.Location = new System.Drawing.Point(13, 11);
			this.Lbl_Name.Name = "Lbl_Name";
			this.Lbl_Name.Size = new System.Drawing.Size(17, 12);
			this.Lbl_Name.TabIndex = 13;
			this.Lbl_Name.Text = "名";
			// 
			// Tbn_Mana
			// 
			this.Tbn_Mana.GetFunc = null;
			this.Tbn_Mana.Location = new System.Drawing.Point(70, 253);
			this.Tbn_Mana.Name = "Tbn_Mana";
			this.Tbn_Mana.SetFunc = null;
			this.Tbn_Mana.Size = new System.Drawing.Size(100, 19);
			this.Tbn_Mana.TabIndex = 23;
			this.Tbn_Mana.Text = "0";
			// 
			// Lbl_Mana
			// 
			this.Lbl_Mana.AutoSize = true;
			this.Lbl_Mana.Location = new System.Drawing.Point(13, 256);
			this.Lbl_Mana.Name = "Lbl_Mana";
			this.Lbl_Mana.Size = new System.Drawing.Size(24, 12);
			this.Lbl_Mana.TabIndex = 26;
			this.Lbl_Mana.Text = "マナ";
			// 
			// Tbn_Accel
			// 
			this.Tbn_Accel.GetFunc = null;
			this.Tbn_Accel.Location = new System.Drawing.Point(70, 281);
			this.Tbn_Accel.Name = "Tbn_Accel";
			this.Tbn_Accel.SetFunc = null;
			this.Tbn_Accel.Size = new System.Drawing.Size(100, 19);
			this.Tbn_Accel.TabIndex = 23;
			this.Tbn_Accel.Text = "0";
			// 
			// Lbl_Accel
			// 
			this.Lbl_Accel.AutoSize = true;
			this.Lbl_Accel.Location = new System.Drawing.Point(13, 284);
			this.Lbl_Accel.Name = "Lbl_Accel";
			this.Lbl_Accel.Size = new System.Drawing.Size(42, 12);
			this.Lbl_Accel.TabIndex = 26;
			this.Lbl_Accel.Text = "アクセル";
			// 
			// Btn_Cancel
			// 
			this.Btn_Cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.Btn_Cancel.Location = new System.Drawing.Point(211, 319);
			this.Btn_Cancel.Name = "Btn_Cancel";
			this.Btn_Cancel.Size = new System.Drawing.Size(88, 25);
			this.Btn_Cancel.TabIndex = 28;
			this.Btn_Cancel.Text = "Cancel";
			this.Btn_Cancel.UseVisualStyleBackColor = false;
			this.Btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
			// 
			// Btn_OK
			// 
			this.Btn_OK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.Btn_OK.Location = new System.Drawing.Point(315, 294);
			this.Btn_OK.Name = "Btn_OK";
			this.Btn_OK.Size = new System.Drawing.Size(87, 50);
			this.Btn_OK.TabIndex = 27;
			this.Btn_OK.Text = "OK";
			this.Btn_OK.UseVisualStyleBackColor = false;
			this.Btn_OK.Click += new System.EventHandler(this.Btn_OK_Click);
			// 
			// Ctrl_Action
			// 
			this.Controls.Add(this.Btn_Cancel);
			this.Controls.Add(this.Btn_OK);
			this.Controls.Add(this.Lbl_Accel);
			this.Controls.Add(this.Tbn_Accel);
			this.Controls.Add(this.Lbl_Mana);
			this.Controls.Add(this.Tbn_Mana);
			this.Controls.Add(this.lbl_Balance);
			this.Controls.Add(this.Tbn_Balance);
			this.Controls.Add(this.Tbn_HitPitch);
			this.Controls.Add(this.TBN_HitNum);
			this.Controls.Add(this.CBSL_Next);
			this.Controls.Add(this.CB_Posture);
			this.Controls.Add(this.CB_Category);
			this.Controls.Add(this.TB_Name);
			this.Controls.Add(this.Lbl_Posture);
			this.Controls.Add(this.Lbl_HitPitch);
			this.Controls.Add(this.Lbl_HitNum);
			this.Controls.Add(this.Lbl_Category);
			this.Controls.Add(this.Lbl_Next);
			this.Controls.Add(this.Lbl_Name);
			this.Name = "Ctrl_Action";
			this.Size = new System.Drawing.Size(405, 347);
			this.ResumeLayout(false);
			this.PerformLayout();

		}


		//------------------------------------------------------

		//対象データ
		public Action Act { get; set; } = new Action ();

		//編集機能参照
		public EditBehavior EditBehavior { get; set; } = new EditBehavior ();

		//編集
		public EditSqcListData EditSLData { get; set; } = new EditSqcListData ();

		//親フォーム
		public Form_Action Form_Action { get; set; } = null;



		//コンストラクタ
		public Ctrl_Action ()
		{
			//コンポーネント初期化
			InitializeComponent ();

			//名前設定用
			TB_Name.TextChanged += new EventHandler ( SetName );

			//定数：アクション部門(カテゴリ) コンボボックス
			foreach ( ActionCategory ac in Enum.GetValues ( typeof ( ActionCategory ) ) )
			{
				CB_Category.Items.Add ( ac );
			}
			CB_Category.SelectionChangeCommitted += new EventHandler ( SetCategory );

			//定数：アクション態勢コンボボックス
			foreach ( ActionPosture ac in Enum.GetValues ( typeof ( ActionPosture ) ) )
			{
				CB_Posture.Items.Add ( ac );
			}
			CB_Posture.SelectionChangeCommitted += new EventHandler ( SetPosture );
		}

		//環境設定
		public void SetEnvironment ( Form_Action form, EditSqcListData editSLdata )
		{
			Form_Action = form;
			EditBehavior = EditChara.Inst.EditBehavior;
			EditSLData = editSLdata;
		}

		//キャラデータ設置
		public void SetCharaData ( Chara ch )
		{
			CBSL_Next.ResetItems ();
		}

		public void UpdateData ()
		{
			//behaviorからコンボボックスを再設定する
			CBSL_Next.SetCompend ( EditBehavior.Compend );

			TBN_HitNum.UpdateData ();
			Tbn_HitPitch.UpdateData ();
			Tbn_Balance.UpdateData ();
			Tbn_Mana.UpdateData ();
			Tbn_Accel.UpdateData ();
		}

		public void ResetData ()
		{
			CBSL_Next.ResetItems ();
		}


		//コンペンド指定
		public void SetCompend ( Compend cmpd )
		{
			//次シークエンス指定
			CBSL_Next.SetCompend ( cmpd );
		}

		//関連付け
		public void Assosiate ()
		{
			//対象データを更新
			Act = (Action)EditSLData.GetSequenceData().Sqc;

			//表示部
			TB_Name.Text = Act.Name;
			CBSL_Next.SelectName ( Act.NextActionName );
			CB_Category.SelectedItem = Act.Category;
			CB_Posture.SelectedItem = Act.Posture;


			//各コントロールに設定用のデリゲートを渡す

			//次シークエンス指定
			CBSL_Next.SetFunc = a=>Act.NextActionName = a.Name;

			// CB_Category カテゴリ -> イベントハンドラで指定
			// CB_Posture 体勢

			//int設定
			TBN_HitNum.SetFunc = i=>Act.HitNum = i;
			Tbn_HitPitch.SetFunc = i=>Act.HitPitch = i;
			Tbn_Balance.SetFunc = i=>Act.Balance = i;
			Tbn_Mana.SetFunc = i=>Act.Mana = i;
			Tbn_Accel.SetFunc = i=>Act.Accel = i;

			TBN_HitNum.GetFunc = ()=>Act.HitNum;
			Tbn_HitPitch.GetFunc = ()=>Act.HitPitch;
			Tbn_Balance.GetFunc = ()=>Act.Balance;
			Tbn_Mana.GetFunc = ()=>Act.Mana;
			Tbn_Accel.GetFunc = ()=>Act.Accel;

			//更新
			UpdateData ();
		}

		//名前の設定用イベントハンドラ
		public void SetName ( object sender, EventArgs e )
		{
			//@todo 各種BindingDictionaryにおいて、型Tの名前の変更時
			//	Dictionary側の更新が必要になるかどうか

			TextBox tb = (TextBox)sender;
			//Action.Name = tb.Text;
			//変更なし
			if ( tb.Text == Act.Name ) { return; }

			//BindingDictionary < T > において名前の変更は専用の関数を用いる
			EditBehavior.Compend.BD_Sequence.ChangeName ( Act.Name, tb.Text );

			//シークエンスコンボボックスの更新
			CBSL_Next.ResetItems ();
		}

		//カテゴリコンボボックスの設定用イベントハンドラ
		public void SetCategory ( object sender, EventArgs e )
		{
			ComboBox cb = (ComboBox)sender;
			Act.Category = (ActionCategory)cb.SelectedIndex;
		}

		//体勢コンボボックスの設定用イベントハンドラ
		public void SetPosture ( object sender, EventArgs e )
		{
			ComboBox cb = (ComboBox)sender;
			Act.Posture = (ActionPosture)cb.SelectedIndex;
		}

		//OKボタン
		private void Btn_OK_Click ( object sender, EventArgs e )
		{
			EditSLData.UpdateAll ();
			Form_Action.Close ();
		}

		//Cancelボタン
		private void Btn_Cancel_Click ( object sender, EventArgs e )
		{
			Form_Action.Close ();
		}
	}
}
