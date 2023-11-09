using System;
using System.Windows.Forms;


namespace ScriptEditor
{
	//コンペンド ( ビヘイビア / ガーニッシュ ) 選択
	//2種類あるためシングルトンではなくオブジェクトを個別に生成する


	//シークエンス詳細
	public partial class Form_Action : Form
	{
		//コントロール
		private Ctrl_Action Ctrl_Action = new Ctrl_Action ();

		//編集
		public EditSqcListData EditData { get; set; } = new EditSqcListData ();

		//コンストラクタ
		public Form_Action()
		{
			//開始位置
			this.StartPosition = FormStartPosition.Manual;

			InitializeComponent();

			//コントロールの追加
			this.Controls.Add ( Ctrl_Action );
		}

		//フォームを閉じる代わりに隠す
		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			e.Cancel = true;
			this.Hide ();
		}

		//環境設定
		public void SetEnvironment ( EditSqcListData editdata )
		{
			EditData = editdata;

			Ctrl_Action.SetEnvironment ();
		}

		//キャラデータ設置
		public void SetCharaData ( Chara ch )
		{
			Ctrl_Action.SetCharaData ( ch );
		}

		//コンペンド指定
		public void SetCompend ( Compend cmpd )
		{
			Ctrl_Action.SetCompend ( cmpd );
		}

		//関連付け
		public void Assosiate ( SequenceData sqcDt )
		{
			Action act = (Action)sqcDt.Sqc;

			Ctrl_Action.Assosiate ( act );

#if false
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
#endif
		}

		private void Btn_OK_Click ( object sender, EventArgs e )
		{
			EditData.UpdateAll ();
			this.Close ();
		}

		private void Btn_Cancel_Click ( object sender, EventArgs e )
		{
			this.Close ();
		}

	}
}
