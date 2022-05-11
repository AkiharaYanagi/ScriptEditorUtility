using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace ScriptEditor
{
	using SP_CLT_ST = ScriptParam<CLC_ST>;
	using SP_INT = ScriptParam<int>;

	public partial class _Ctrl_Script : UserControl
	{
		//現在スクリプト
		private Script script = null;

		//編集
		EditCompend EditCompend = null;

		//コントロール集合
		private List < TB_ScpNumber > ls_tbsn = new List<TB_ScpNumber> ();
		private List < IScriptParam > ls_ctrl_scpPrm = new List<IScriptParam> ();

		//コンポーネント
		//計算状態セッタ
		private Cmpnt_ClcSt cmpnt_ClcSt = new Cmpnt_ClcSt ();
		//数値
		private Cmpnt_Int cmpnt_Int_px = new Cmpnt_Int();
		private Cmpnt_Int cmpnt_Int_py = new Cmpnt_Int();

		private Cmpnt_Int cmpnt_Int_vx = new Cmpnt_Int();
		private Cmpnt_Int cmpnt_Int_vy = new Cmpnt_Int();

		//位置定数
		private const int BX = 60;
		private const int PX = 50;
		private const int BY = 100;
		private const int PY = 30;
		
		//----------------------------------------------------
		//コンストラクタ
		public _Ctrl_Script ()
		{
			//コンポーネントの手動追加
			//計算状態
			cmpnt_ClcSt.SetParam ( new SP_CLT_ST ( (s,c)=>s.CalcState=c, s=>s.CalcState ) );
			cmpnt_ClcSt.Location = new Point ( 220, BY );
			this.Controls.Add ( cmpnt_ClcSt );

			cmpnt_Int_px.SetParam ( new SP_INT ( (s,i)=>s.SetPosX(i), s=>s.Pos.X ) );
			cmpnt_Int_px.Location = new Point ( BX, BY );
			this.Controls.Add ( cmpnt_Int_px );

			cmpnt_Int_py.SetParam ( new SP_INT ( (s,i)=>s.SetPosY(i), s=>s.Pos.Y ) );
			cmpnt_Int_py.Location = new Point ( BX + PX, BY );
			this.Controls.Add ( cmpnt_Int_py );

			cmpnt_Int_vx.SetParam ( new SP_INT ( (s,i)=>s.Param_Btl.SetVelX(i), s=>s.Param_Btl.Vel.X ) );
			cmpnt_Int_vx.Location = new Point ( BX, BY + PY );
			this.Controls.Add ( cmpnt_Int_vx );

			cmpnt_Int_vy.SetParam ( new SP_INT ( (s,i)=>s.Param_Btl.SetVelY(i), s=>s.Param_Btl.Vel.Y ) );
			cmpnt_Int_vy.Location = new Point ( BX + PX, BY + PY );
			this.Controls.Add ( cmpnt_Int_vy );

			//デフォルトの初期化
			InitializeComponent ();

			//コントロール一連に登録
			ls_ctrl_scpPrm.Add ( cmpnt_Int_px );
			ls_ctrl_scpPrm.Add ( cmpnt_Int_py );
			ls_ctrl_scpPrm.Add ( cmpnt_Int_vx );
			ls_ctrl_scpPrm.Add ( cmpnt_Int_vy );
			ls_ctrl_scpPrm.Add ( cmpnt_ClcSt );
		}

		//環境設置
		public void SetEnvironment ( EditCompend ec, System.Action disp )
		{
			EditCompend = ec;

			//環境設定
			foreach ( IScriptParam isp in ls_ctrl_scpPrm ) { isp.SetEnvironment ( ec, disp ); }
		}

		//更新
		public void UpdateData ()
		{
			foreach ( IScriptParam isp in ls_ctrl_scpPrm ) { isp.UpdateData (); }
		}

		//関連付け
		public void Assosiate ( Script s )
		{
			script = s;

			Tb_Frame.Text = script.Frame.ToString ();
			Tb_Img.Text = script.ImgName;

			foreach ( IScriptParam isp in ls_ctrl_scpPrm ) { isp.Assosiate ( s ); }
		}

		//ラジオボタンで編集範囲を設定
		private void RB_TRG_ALL_CheckedChanged ( object sender, EventArgs e )
		{
			foreach ( IScriptParam isp in ls_ctrl_scpPrm ) { isp.SetTarget_All (); }
		}

		private void RB_TRG_GRP_CheckedChanged ( object sender, EventArgs e )
		{
			foreach ( IScriptParam isp in ls_ctrl_scpPrm ) { isp.SetTarget_Group (); }
		}

		private void RB_TRG_SGL_CheckedChanged ( object sender, EventArgs e )
		{
			foreach ( IScriptParam isp in ls_ctrl_scpPrm ) { isp.SetTarget_Single (); }
		}


	}
}
