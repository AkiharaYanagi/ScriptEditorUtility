using System;
using System.Windows.Forms;
using System.Collections.Generic;


namespace ScriptEditor
{
	//スクリプト編集コンポーネントリスト において対象を切り替える
	public partial class RB_ScriptTarget : UserControl
	{
		//対象指定 (初期値：SELECT)
		public EditTargetScript EditTarget { get; set; } = EditTargetScript.SELECT;
	
		public List < IScriptParam > ls_ctrl_scpPrm { get; set; } = new List<IScriptParam> ();

		public RB_ScriptTarget ()
		{
			InitializeComponent ();

			RB_TRG_SGL.CheckedChanged += RB_TRG_SGL_CheckedChanged;
			RB_TRG_GRP.CheckedChanged += RB_TRG_GRP_CheckedChanged;
			RB_TRG_SLC.CheckedChanged += RB_TRG_SLC_CheckedChanged;
			RB_TRG_ALL.CheckedChanged += RB_TRG_ALL_CheckedChanged;

			//初期位置
			RB_TRG_SLC.Checked = true;
			EditTarget = EditTargetScript.SELECT;
		}

		//コントロール設定
		public void SetCtrl ( List < IScriptParam > l_ctrl )
		{
			ls_ctrl_scpPrm = l_ctrl;
			foreach ( IScriptParam isp in ls_ctrl_scpPrm ) { isp.SetTarget_Select (); }
			EditTarget = EditTargetScript.SELECT;
		}

		//ラジオボタンで編集範囲を設定

		private void RB_TRG_SGL_CheckedChanged ( object sender, EventArgs e )
		{
			if ( RB_TRG_SGL.Checked )
			{
				foreach ( IScriptParam isp in ls_ctrl_scpPrm ) { isp.SetTarget_Single (); }
				EditTarget = EditTargetScript.SINGLE;
			}
		}

		private void RB_TRG_GRP_CheckedChanged ( object sender, EventArgs e )
		{
			if ( RB_TRG_GRP.Checked )
			{
				foreach ( IScriptParam isp in ls_ctrl_scpPrm ) { isp.SetTarget_Group (); }
				EditTarget = EditTargetScript.GROUP;
			}
		}

		private void RB_TRG_SLC_CheckedChanged ( object sender, EventArgs e )
		{
			if ( RB_TRG_SLC.Checked )
			{
				foreach ( IScriptParam isp in ls_ctrl_scpPrm ) { isp.SetTarget_Select (); }
				EditTarget = EditTargetScript.SELECT;
			}
		}

		private void RB_TRG_ALL_CheckedChanged ( object sender, EventArgs e )
		{
			if ( RB_TRG_ALL.Checked )
			{
				foreach ( IScriptParam isp in ls_ctrl_scpPrm ) { isp.SetTarget_All (); }
				EditTarget = EditTargetScript.ALL;
			}
		}

	}
}
