using System;
using System.Drawing;


namespace ScriptEditor
{
	//---------------------------------------------------------------------
	//	スクリプトの詳細を設定するフォーム
	//---------------------------------------------------------------------
	public sealed partial class FormScript : SubForm_Compend
	{
#if false
		//---------------------------------------------------------------------
		//シングルトン実体
		public static FormScript Inst { get; set; } = new FormScript ();
		
		//プライベートコンストラクタ
		private FormScript ()
		{
//			ctrl_Script1 = new _Ctrl_Script ();
//			this.Controls.Add ( ctrl_Script1 );

			base.InitPt = new Point ( 0, 230 );
			InitializeComponent ();
			LoadObject ();
		}
		//---------------------------------------------------------------------
#endif
		public FormScript ()
		{
			InitializeComponent ();
			base.LoadObject ();
		}

		//コントロール
		private _Ctrl_Script ctrl_Script1 = new _Ctrl_Script ();	//仮オブジェクト

		//FormMainで実体を確保し、設置する
		public void SetCtrl ( _Ctrl_Script ctrl )
		{
			ctrl_Script1 = ctrl;
			this.Controls.Add ( ctrl_Script1 );
		}

		//コンペンド編集の切り替え
		public override void SetEditCompend ( EditCompend ec )
		{
			ctrl_Script1.SetEnvironment ( ec );
			base.SetEditCompend ( ec );
		}
	}
}
