using System;
using System.Windows.Forms;
using System.Drawing;

namespace ScriptEditor
{
	//アクションのみフォームで指定
	public partial class Form_Action : Form
	{
		//全体更新
//		public System.Action UpdateAll { get; set; } = null;

		//編集
		public EditSqcListData EditData { get; set; } = null;

		//コンストラクタ
		public Form_Action()
		{
			InitializeComponent();

			//コンボボックスの初期化
			//次アクション

			
			//カテゴリ
			foreach ( string name in Enum.GetNames ( typeof ( ActionCategory ) ) )
			{
				Cb_Category.Items.Add ( name );
			}
			Cb_Category.SelectedIndex = 0;
			Cb_Category.Size = new Size ( 220, 40 );
		}

		//フォームを閉じない
		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			e.Cancel = true;
//			UpdateAll?.Invoke ();
			EditData.UpdateAll ();
			this.Hide ();
		}

		//環境設定
		public void SetEnvironment ( EditSqcListData editdata )
		{
			EditData = editdata;
		}

		//関連付け
		public void Assosiate ( SequenceData sqcDt )
		{
			Action act = (Action)sqcDt.Sqc;

			tB_Setter1.Assosiate ( s=>sqcDt.SetName(s), ()=>{return sqcDt.Sqc.Name;} );
			tB_Number1.Assosiate ( i=>sqcDt.nScript = i, ()=>{ return sqcDt.nScript; } );

			//コンボボックス
			Cb_Next.Items.Clear ();
			foreach ( SequenceData sd in EditData.Dt.L_Sqc.GetEnumerable () )
			{
				Cb_Next.Items.Add ( sd.Name );
			}
			Cb_Next.SelectedItem = act.NextActionName;
			SetNext = ( s ) =>
			{
				act.NextActionName = s;
			};

			SetCategory = ( s ) =>
			{
				act.Category = (ActionCategory)Enum.Parse ( typeof (ActionCategory), s ); 
			};
			Cb_Category.SelectedItem = act.Category.ToString();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			this.Close ();
		}

		//次アクション選択
		public System.Action < string > SetNext = ( s ) => { };
		private void Cb_Next_SelectionChangeCommitted ( object sender, EventArgs e )
		{
			SetNext ( (string)Cb_Next.SelectedItem );
		}

		//カテゴリ選択
		public System.Action < string > SetCategory = (s)=>{};
		private void comboBox1_SelectionChangeCommitted ( object sender, EventArgs e )
		{
			SetCategory ( (string)Cb_Category.SelectedItem );
		}

	}
}
