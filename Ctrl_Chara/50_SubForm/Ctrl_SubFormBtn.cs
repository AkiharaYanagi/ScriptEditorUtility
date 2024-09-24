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
		public FormBtn FB_EditInfo		{ get; set; } = new FormBtn ( "EditInfo" );

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
			L_FmBtn.Add ( FB_EditInfo );

			//ボタンの配置
			int n = 0;
			foreach ( var item in L_FmBtn )
			{
				item.Location = new Point ( 0, n * 40 );
				this.Controls.Add ( item );
				++ n;
			}
		}
	}
}
