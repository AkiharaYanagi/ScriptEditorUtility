using System;
using System.Windows.Forms;
using System.Collections.Generic;


namespace ScriptEditor
{
	//スクリプト編集コンポーネントリスト において対象を切り替える
	public partial class RB_ScriptTarget : UserControl
	{
		public List < IScriptParam > ls_ctrl_scpPrm { get; set; } = new List<IScriptParam> ();

		public RB_ScriptTarget ()
		{
			InitializeComponent ();

			RB_TRG_ALL.CheckedChanged += RB_TRG_ALL_CheckedChanged;
			RB_TRG_GRP.CheckedChanged += RB_TRG_GRP_CheckedChanged;
			RB_TRG_SGL.CheckedChanged += RB_TRG_SGL_CheckedChanged;

			//初期位置
			RB_TRG_GRP.Checked = true;
		}

		//ラジオボタンで編集範囲を設定
		private void RB_TRG_ALL_CheckedChanged ( object sender, EventArgs e )
		{
			if ( RB_TRG_ALL.Checked )
			{
				foreach ( IScriptParam isp in ls_ctrl_scpPrm ) { isp.SetTarget_All (); }
			}
		}

		private void RB_TRG_GRP_CheckedChanged ( object sender, EventArgs e )
		{
			if ( RB_TRG_GRP.Checked )
			{
				foreach ( IScriptParam isp in ls_ctrl_scpPrm ) { isp.SetTarget_Group (); }
			}
		}

		private void RB_TRG_SGL_CheckedChanged ( object sender, EventArgs e )
		{
			if ( RB_TRG_SGL.Checked )
			{
				foreach ( IScriptParam isp in ls_ctrl_scpPrm ) { isp.SetTarget_Single (); }
			}
		}
	}
}
