using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ScriptEditor
{
	public partial class _Ctrl_Script : UserControl
	{
		//現在スクリプト
		private Script script = null;

		//編集
		EditCompend EditCompend = null;

		//コントロール集合
		private List < TB_ScpNumber > ls_tbsn = new List<TB_ScpNumber> ();
		private List < IScriptParam > ls_ctrl_scpPrm = new List<IScriptParam> ();

		//計算状態セッタ
		private ScriptParam < CLC_ST > ScpParam_CLC_ST { get; set; } = null;

		private Ctrl_ClcSt ctrl_ClcSt = new Ctrl_ClcSt ();
		
		//----------------------------------------------------
		//コンストラクタ
		public _Ctrl_Script ()
		{
			InitializeComponent ();

			//コントロール一連に登録
			ls_tbsn.Add ( TbScp_X );
			ls_tbsn.Add ( TbScp_Y );


			//特殊指定コントロールにも ScriptParam 関連のインターフェースを実装する

			ls_ctrl_scpPrm.Add ( ctrl_ClcSt );




			//計算状態 プルダウンメニュー初期化
			foreach ( CLC_ST clcst in Enum.GetValues ( typeof ( CLC_ST ) ) )
			{
				CB_ClcSt.Items.Add ( clcst );
			}
			ScpParam_CLC_ST = new ScriptParam<CLC_ST> ( (s,c)=>s.CalcState=c, s=>s.CalcState );
		}


		public void SetEnvironment ( EditCompend ec, System.Action disp )
		{
			EditCompend = ec;

			//環境設定
			TbScp_X.SetEnvironment ( ec, (s,i)=>s.SetPosX(i), s=>s.Pos.X );
			TbScp_Y.SetEnvironment ( ec, (s,i)=>s.SetPosY(i), s=>s.Pos.Y );

			foreach ( TB_ScpNumber tbsn in ls_tbsn ) { tbsn.Disp = disp; }

			foreach ( IScriptParam isp in ls_ctrl_scpPrm ) { isp.Disp = disp; }
		}

		public void UpdateData ()
		{
			foreach ( TB_ScpNumber tbsn in ls_tbsn ) { tbsn.UpdateData (); }

			foreach ( IScriptParam isp in ls_ctrl_scpPrm ) { isp.UpdateData (); }
		}


		public void Assosiate ( Script s )
		{
			script = s;

			Tb_Frame.Text = script.Frame.ToString ();
			//Cb_ClcSt.SelectedItem = script.CalcState;
			Tb_Img.Text = script.ImgName;

			foreach ( TB_ScpNumber tbsn in ls_tbsn ) { tbsn.Assosiate ( s ); }

			foreach ( IScriptParam isp in ls_ctrl_scpPrm ) { isp.Assosiate ( s ); }
		}


		//ラジオボタンで編集範囲を設定
		private void RB_TRG_ALL_CheckedChanged ( object sender, EventArgs e )
		{
			foreach ( TB_ScpNumber tbsn in ls_tbsn ) { tbsn.SetTarget_All (); }

			foreach ( IScriptParam isp in ls_ctrl_scpPrm ) { isp.SetTarget_All (); }
		}

		private void RB_TRG_GRP_CheckedChanged ( object sender, EventArgs e )
		{
			foreach ( TB_ScpNumber tbsn in ls_tbsn ) { tbsn.SetTarget_Group (); }

			foreach ( IScriptParam isp in ls_ctrl_scpPrm ) { isp.SetTarget_Group (); }
		}

		private void RB_TRG_SGL_CheckedChanged ( object sender, EventArgs e )
		{
			foreach ( TB_ScpNumber tbsn in ls_tbsn ) { tbsn.SetTarget_Single (); }

			foreach ( IScriptParam isp in ls_ctrl_scpPrm ) { isp.SetTarget_Single (); }
		}


		//計算状態プルダウンメニュー
		private void CB_ClcSt_SelectionChangeCommitted_1 ( object sender, EventArgs e )
		{

		}
	}
}
