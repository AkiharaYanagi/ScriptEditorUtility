using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;


namespace ScriptEditorUtility
{
	public partial class Ctrl_SubFormBtn : UserControl
	{
		public FormBtn FB_Action		{ get; set; } = new FormBtn ( "アクション" );
		public FormBtn FB_ScriptList	{ get; set; } = new FormBtn ( "スクリプト(リスト)" );
		public FormBtn FB_ScriptValue	{ get; set; } = new FormBtn ( "スクリプト(値)" );
		public FormBtn FB_Image			{ get; set; } = new FormBtn ( "イメージ" );
		public FormBtn FB_RectList		{ get; set; } = new FormBtn ( "枠リスト" );
		public FormBtn FB_EfGnrt		{ get; set; } = new FormBtn ( "Ef生成" );
		public FormBtn FB_Route			{ get; set; } = new FormBtn ( "ルート" );
		public FormBtn FB_Preview		{ get; set; } = new FormBtn ( "プレビュー" );

		//----
		private List < FormBtn > L_FmBtn = new List < FormBtn > ();

		public Ctrl_SubFormBtn ()
		{
			InitializeComponent ();
			this.Anchor = AnchorStyles.Top | AnchorStyles.Right;

			//ボタンリストに登録
			L_FmBtn.Add ( FB_Action );
			L_FmBtn.Add ( FB_ScriptList );
			L_FmBtn.Add ( FB_ScriptValue );
			L_FmBtn.Add ( FB_Image );
			L_FmBtn.Add ( FB_RectList );
			L_FmBtn.Add ( FB_EfGnrt );
			L_FmBtn.Add ( FB_Route );
			L_FmBtn.Add ( FB_Preview );

			//ボタンの配置
			int n = 0;
			foreach ( var item in L_FmBtn )
			{
				item.Location = new Point ( 0, n * 40 );
				this.Controls.Add ( item );
				++ n;
			}
		}

#if false
		//--------------------------------------------------------------------
		private void BtnFmAction_Click ( object sender, EventArgs e )
		{
//			FormAction.Inst.Active ();
		}
		private void Btn_HideAction_Click ( object sender, EventArgs e )
		{
//			FormAction.Inst.Hidden ();
		}
		//--------------------------------------------------------------------
		private void BtnScriptList_Click ( object sender, EventArgs e )
		{
			Form_ScriptList.Inst.Active ();
		}
		private void Btn_HideScriptList_Click ( object sender, EventArgs e )
		{
			Form_ScriptList.Inst.Hide ();
		}
		//--------------------------------------------------------------------
		private void BtnScriptValue_Click ( object sender, EventArgs e )
		{
			FormScript.Inst.Active ();
		}
		private void Btn_HideScriptValue_Click ( object sender, EventArgs e )
		{
			FormScript.Inst.Hide ();
		}
		//--------------------------------------------------------------------
		private void BtnImg_Click ( object sender, EventArgs e )
		{
			FormImage.Inst.Active ();
		}
		private void Btn_HideImage_Click ( object sender, EventArgs e )
		{
			FormImage.Inst.Hide ();
		}
		//--------------------------------------------------------------------
		private void Btn_RctList_Click ( object sender, EventArgs e )
		{
			FormRect2.Inst.Active ();
		}
		private void Btn_HideRectList_Click ( object sender, EventArgs e )
		{
			FormRect2.Inst.Hide ();
		}
		//--------------------------------------------------------------------
		private void Btn_EfGnrt_Click ( object sender, EventArgs e )
		{
//			FormEfGnrt.Inst.Active ();
			_FormEfGnrt.Inst.Active ();
		}
		private void Btn_HideEfGnrt_Click ( object sender, EventArgs e )
		{
//			FormEfGnrt.Inst.Hidden ();
			_FormEfGnrt.Inst.Hide ();
		}
		//--------------------------------------------------------------------
		private void Btn_Route_Click ( object sender, EventArgs e )
		{
			FormRoute.Inst.Active ();
		}
		private void Btn_HideRoute_Click ( object sender, EventArgs e )
		{
			FormRoute.Inst.Hide ();
		}
		//--------------------------------------------------------------------
		private void Btn_Preview_Click ( object sender, EventArgs e )
		{
//			FormPreview.Inst.Active ();
		}
		private void Btn_HidePreview_Click ( object sender, EventArgs e )
		{
//			FormPreview.Inst.Hidden ();
		}
		//--------------------------------------------------------------------
#endif

	}
}
