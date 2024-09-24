using System;
using System.Drawing;
using System.Windows.Forms;


namespace ScriptEditor
{
	public partial class SubForm_CmpdInfo : SubForm_Compend
	{
		//コントロール
		private static Ctrl_EditCompend Ctrl_EditCompend = new Ctrl_EditCompend (); //仮オブジェクト

#if false
		//---------------------------------------------------------------------
		//シングルトン実体
		public static SubForm_CmpdInfo Inst { get; } = new SubForm_CmpdInfo ();

		//プライベートコンストラクタ
		public SubForm_CmpdInfo ()
		{
			InitializeComponent ();
			base.InitPt = new Point ( 0, 0 );
			base.LoadObject ();

			this.Controls.Add ( Ctrl_EditCompend );
		}
		//---------------------------------------------------------------------
#endif
		public SubForm_CmpdInfo ()
		{
			InitializeComponent ();
			base.LoadObject ();
			this.Controls.Add ( Ctrl_EditCompend );
		}

		//コンペンド編集の切り替え
		public override void SetEditCompend ( EditCompend ec )
		{
			Ctrl_EditCompend.SetEnvironment ( ec );
			base.SetEditCompend ( ec );
		}
	}
}
