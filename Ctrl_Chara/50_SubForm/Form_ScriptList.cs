using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace ScriptEditor
{
	using L_Scp = List < Script >;


	//-------------------------------------------------------------
	//	スクリプトリスト操作フォーム
	//-------------------------------------------------------------
	public sealed class Form_ScriptList : SubForm_Compend
	{
		//コントロール
		private Ctrl_ScriptList ctrl_Scpls = new Ctrl_ScriptList ();

#if false
		//---------------------------------------------------------------------
		//シングルトン実体
		public static Form_ScriptList Inst { get; } = new Form_ScriptList ();

		//プライベートコンストラクタ
		private Form_ScriptList ()
		{
			InitializeComponent ();
			LoadObject ();
			this.Controls.Add ( ctrl_Scpls );
		}
		//---------------------------------------------------------------------
#endif

		public Form_ScriptList ()
		{
			InitializeComponent ();
			base.LoadObject ();
			this.Controls.Add ( ctrl_Scpls );
		}

		private void InitializeComponent ()
		{
            this.SuspendLayout();
            // 
            // Form_ScriptList
            // 
            this.ClientSize = new System.Drawing.Size(438, 336);
            this.Name = "Form_ScriptList";
            this.Text = "ScriptList";
            this.ResumeLayout(false);

		}


		//コンペンド編集の切り替え
		public override void SetEditCompend ( EditCompend ec )
		{
			ctrl_Scpls.SetEditCompend ( ec );
			base.SetEditCompend ( ec );
		}

	}
}
