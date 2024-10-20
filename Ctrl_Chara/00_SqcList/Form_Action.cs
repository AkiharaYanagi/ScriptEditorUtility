using System;
using System.Windows.Forms;
using System.Drawing;


namespace ScriptEditor
{
	//コンペンド ( ビヘイビア / ガーニッシュ ) 選択
	//2種類あるためシングルトンではなくオブジェクトを個別に生成する


	//シークエンス詳細
	public partial class Form_Action : Form
	{
		//コントロール
		private Ctrl_Action Ctrl_Action = new Ctrl_Action ();


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
		public void ShowForm ( Point pt )
		{
			Location = pt;
			Assosiate ();
			Show();
			Focus ();
		}


		//環境設定
		public void SetEnvironment ( EditSqcListData editSLdata )
		{
			Ctrl_Action.SetEnvironment ( this, editSLdata );
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

		public void UpdateData ()
		{
			Ctrl_Action.UpdateData ();
		}

		//関連付け
		public void Assosiate ()
		{
			Ctrl_Action.Assosiate ();
		}

		//再設定
		public void ResetItems ()
		{
			Ctrl_Action.ResetData ();
		}
	}
}
