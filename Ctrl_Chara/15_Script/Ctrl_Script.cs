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
		private Script script = new Script ();

		//編集
		EditCompend EditCompend = new EditCompend ();

		//コントロール集合
		private List < IScriptParam > ls_ctrl_scpPrm = new List<IScriptParam> ();

		//コンポーネント
		//計算状態セッタ
		private Cmpnt_ClcSt cmpnt_ClcSt = new Cmpnt_ClcSt ();
		//数値
		private Cmpnt_Int cmpnt_px = new Cmpnt_Int();
		private Cmpnt_Int cmpnt_py = new Cmpnt_Int();
		private Cmpnt_Int cmpnt_vx = new Cmpnt_Int();
		private Cmpnt_Int cmpnt_vy = new Cmpnt_Int();
		private Cmpnt_Int cmpnt_ax = new Cmpnt_Int();
		private Cmpnt_Int cmpnt_ay = new Cmpnt_Int();
		private Cmpnt_Int cmpnt_power = new Cmpnt_Int ();
		private Cmpnt_Int cmpnt_warp = new Cmpnt_Int ();
		private Cmpnt_Int cmpnt_recoil_I = new Cmpnt_Int();
		private Cmpnt_Int cmpnt_recoil_E = new Cmpnt_Int();
		private Cmpnt_Int cmpnt_balance_I = new Cmpnt_Int();
		private Cmpnt_Int cmpnt_balance_E = new Cmpnt_Int();
		private Cmpnt_Int cmpnt_rotate = new Cmpnt_Int ();
		private Cmpnt_Int cmpnt_r_center_x = new Cmpnt_Int();
		private Cmpnt_Int cmpnt_r_center_y = new Cmpnt_Int();

		private Cmpnt_Int cmpnt_blackOut = new Cmpnt_Int ();
		private Cmpnt_Int cmpnt_vibration = new Cmpnt_Int ();
		private Cmpnt_Int cmpnt_Stop = new Cmpnt_Int ();
		private Cmpnt_Int cmpnt_AftImg_N = new Cmpnt_Int ();
		private Cmpnt_Int cmpnt_AftImg_Time = new Cmpnt_Int ();
		private Cmpnt_Int cmpnt_AftImg_Pitch = new Cmpnt_Int ();

		private Cmpnt_Int cmpnt_Scaling_x = new Cmpnt_Int ();
		private Cmpnt_Int cmpnt_Scaling_y = new Cmpnt_Int ();
		private Cmpnt_Int cmpnt_SE = new Cmpnt_Int ();

		private CB_Names cb_SE = new CB_Names ();
		private CB_Names cb_VC = new CB_Names ();


		//編集対象を切り替えるラジオボタン
		private RB_ScriptTarget rb_ScpTgt = new RB_ScriptTarget ();


		//位置定数
		private const int BX = 60;
		private const int PX = 50;
		private const int BY = 100;
		private const int PY = 30;

		private const int BX1 = 280;
		
		//----------------------------------------------------
		//コンストラクタ
		public _Ctrl_Script ()
		{
			//編集対象を切り替えるラジオボタン
			rb_ScpTgt.ls_ctrl_scpPrm = ls_ctrl_scpPrm;
			this.Controls.Add ( rb_ScpTgt );

			//コントロール一連に登録
			ls_ctrl_scpPrm.Add ( cmpnt_ClcSt );
			ls_ctrl_scpPrm.Add ( cmpnt_px );
			ls_ctrl_scpPrm.Add ( cmpnt_py );
			ls_ctrl_scpPrm.Add ( cmpnt_vx );
			ls_ctrl_scpPrm.Add ( cmpnt_vy );
			ls_ctrl_scpPrm.Add ( cmpnt_ax );
			ls_ctrl_scpPrm.Add ( cmpnt_ay );
			ls_ctrl_scpPrm.Add ( cmpnt_power );
			ls_ctrl_scpPrm.Add ( cmpnt_warp );
			ls_ctrl_scpPrm.Add ( cmpnt_recoil_I );
			ls_ctrl_scpPrm.Add ( cmpnt_recoil_E );
			ls_ctrl_scpPrm.Add ( cmpnt_balance_I );
			ls_ctrl_scpPrm.Add ( cmpnt_balance_E );
			ls_ctrl_scpPrm.Add ( cmpnt_rotate);
			ls_ctrl_scpPrm.Add ( cmpnt_r_center_x );
			ls_ctrl_scpPrm.Add ( cmpnt_r_center_y );

			ls_ctrl_scpPrm.Add ( cmpnt_blackOut );
			ls_ctrl_scpPrm.Add ( cmpnt_vibration );
			ls_ctrl_scpPrm.Add ( cmpnt_Stop );
			ls_ctrl_scpPrm.Add ( cmpnt_AftImg_N );
			ls_ctrl_scpPrm.Add ( cmpnt_AftImg_Time );
			ls_ctrl_scpPrm.Add ( cmpnt_AftImg_Pitch );
			ls_ctrl_scpPrm.Add ( cmpnt_Scaling_x );
			ls_ctrl_scpPrm.Add ( cmpnt_Scaling_y );
			ls_ctrl_scpPrm.Add ( cmpnt_SE );


			//コンポーネントの追加
			foreach ( Control ctrl in ls_ctrl_scpPrm )
			{
				this.Controls.Add ( ctrl );
			}
			this.Controls.Add ( cb_SE );
			this.Controls.Add ( cb_VC );


			//コンポーネントの各種設定
			cmpnt_ClcSt.SetParam ( new SP_CLT_ST ( (s,c)=>s.BtlPrm.CalcState=c, s=>s.BtlPrm.CalcState ) );
			cmpnt_px.SetParam ( new SP_INT ( (s,i)=>s.SetPosX(i), s=>s.Pos.X ) );
			cmpnt_py.SetParam ( new SP_INT ( (s,i)=>s.SetPosY(i), s=>s.Pos.Y ) );
			cmpnt_vx.SetParam ( new SP_INT ( (s,i)=>s.BtlPrm.SetVelX(i), s=>s.BtlPrm.Vel.X ) );
			cmpnt_vy.SetParam ( new SP_INT ( (s,i)=>s.BtlPrm.SetVelY(i), s=>s.BtlPrm.Vel.Y ) );
			cmpnt_ax.SetParam ( new SP_INT ( (s,i)=>s.BtlPrm.SetAccX(i), s=>s.BtlPrm.Acc.X ) );
			cmpnt_ay.SetParam ( new SP_INT ( (s,i)=>s.BtlPrm.SetAccY(i), s=>s.BtlPrm.Acc.Y ) );
			cmpnt_power.SetParam ( new SP_INT ( (s,i)=>s.BtlPrm.Power=i, s=>s.BtlPrm.Power ) );
			cmpnt_warp.SetParam ( new SP_INT ( (s,i)=>s.BtlPrm.Warp=i, s=>s.BtlPrm.Warp ) );
			cmpnt_recoil_I.SetParam ( new SP_INT ( (s,i)=>s.BtlPrm.Recoil_I=i, s=>s.BtlPrm.Recoil_I ) );
			cmpnt_recoil_E.SetParam ( new SP_INT ( (s,i)=>s.BtlPrm.Recoil_E=i, s=>s.BtlPrm.Recoil_E ) );
			cmpnt_balance_I.SetParam ( new SP_INT ( (s,i)=>s.BtlPrm.Blance_I=i, s=>s.BtlPrm.Blance_I ) );
			cmpnt_balance_E.SetParam ( new SP_INT ( (s,i)=>s.BtlPrm.Blance_E=i, s=>s.BtlPrm.Blance_E ) );
			cmpnt_rotate.SetParam ( new SP_INT ( (s,i)=>s.StgPrm.Rotate=i, s=>s.StgPrm.Rotate ) );
			cmpnt_r_center_x.SetParam ( new SP_INT ( (s,i)=>s.StgPrm.SetRotate_centerX(i), s=>s.StgPrm.Rotate_center.X ) );
			cmpnt_r_center_y.SetParam ( new SP_INT ( (s,i)=>s.StgPrm.SetRotate_centerY(i), s=>s.StgPrm.Rotate_center.Y ) );

			cmpnt_blackOut.SetParam ( new SP_INT ( (s,i)=>s.StgPrm.BlackOut=i, s=>s.StgPrm.BlackOut ) );
			cmpnt_vibration.SetParam ( new SP_INT ( (s,i)=>s.StgPrm.Vibration=i, s=>s.StgPrm.Vibration ) );
			cmpnt_Stop.SetParam ( new SP_INT ( (s,i)=>s.StgPrm.Stop=i, s=>s.StgPrm.Stop ) );
			cmpnt_AftImg_N.SetParam ( new SP_INT ( (s,i)=>s.StgPrm.AfterImage_N=i, s=>s.StgPrm.AfterImage_N ) );
			cmpnt_AftImg_Time.SetParam ( new SP_INT ( (s,i)=>s.StgPrm.AfterImage_time=i, s=>s.StgPrm.AfterImage_time ) );
			cmpnt_AftImg_Pitch.SetParam ( new SP_INT ( (s,i)=>s.StgPrm.AfterImage_pitch=i, s=>s.StgPrm.AfterImage_pitch ) );

			cmpnt_Scaling_x.SetParam ( new SP_INT ( (s,i)=>s.StgPrm.SetScalingX(i), s=>s.StgPrm.Scaling.X ) );
			cmpnt_Scaling_y.SetParam ( new SP_INT ( (s,i)=>s.StgPrm.SetScalingY(i), s=>s.StgPrm.Scaling.Y ) );
			cmpnt_SE.SetParam ( new SP_INT ( (s,i)=>s.StgPrm.SE=i, s=>s.StgPrm.SE ) );

			cb_SE.ScpPrm = new ScriptParam < string > ( (s,str)=>s.StgPrm.SE_name=str, s=>s.StgPrm.SE_name );
			cb_VC.ScpPrm = new ScriptParam < string > ( (s,str)=>s.StgPrm.VC_name=str, s=>s.StgPrm.VC_name );


			//コンポーネントの位置
			cmpnt_px.Location		 = new Point ( BX		, BY + PY * 0 );
			cmpnt_py.Location		 = new Point ( BX + PX	, BY + PY * 0 );
			cmpnt_vx.Location		 = new Point ( BX		, BY + PY * 1 );
			cmpnt_vy.Location		 = new Point ( BX + PX	, BY + PY * 1 );
			cmpnt_ax.Location		 = new Point ( BX		, BY + PY * 2 );
			cmpnt_ay.Location		 = new Point ( BX + PX	, BY + PY * 2 );
			cmpnt_power.Location	 = new Point ( BX		, BY + PY * 3 );
			cmpnt_warp.Location		 = new Point ( BX		, BY + PY * 4 );
			cmpnt_recoil_I.Location	 = new Point ( BX		, BY + PY * 5 );
			cmpnt_recoil_E.Location	 = new Point ( BX + PX	, BY + PY * 5 );
			cmpnt_balance_I.Location = new Point ( BX		, BY + PY * 6 );
			cmpnt_balance_E.Location = new Point ( BX + PX	, BY + PY * 6 );
			cmpnt_rotate.Location	 = new Point ( BX		, BY + PY * 7 );
			cmpnt_r_center_x.Location = new Point ( BX		, BY + PY * 8 );
			cmpnt_r_center_y.Location = new Point ( BX + PX	, BY + PY * 8 );

			cmpnt_ClcSt.Location		= new Point ( BX1		, BY + PY * 0 );
			cmpnt_blackOut.Location		= new Point ( BX1		, BY + PY * 1 );
			cmpnt_vibration.Location	= new Point ( BX1		, BY + PY * 2 );
			cmpnt_Stop.Location			= new Point ( BX1		, BY + PY * 3 );
			cmpnt_AftImg_N.Location		= new Point ( BX1		, BY + PY * 4 );
			cmpnt_AftImg_Time.Location	= new Point ( BX1		, BY + PY * 5 );
			cmpnt_AftImg_Pitch.Location = new Point ( BX1		, BY + PY * 6 );

			cmpnt_Scaling_x.Location	= new Point ( BX1		, BY + PY * 7 );
			cmpnt_Scaling_y.Location	= new Point ( BX1 + PX	, BY + PY * 7 );
			cmpnt_SE.Location			= new Point ( BX1		, BY + PY * 8 );

			cb_SE.Location = new Point ( BX1		, BY + PY * 9 );
			cb_VC.Location = new Point ( BX1		, BY + PY * 10 );


			//初期化
			foreach ( IScriptParam iscp in ls_ctrl_scpPrm )
			{
				iscp.SetTarget_Group ();
			}

			//デフォルトの初期化
			InitializeComponent ();
		}


		public void LoadData ( string filedir )
		{
			cb_SE.LoadData ( filedir + "\\SE_Name.txt" );
			cb_VC.LoadData ( filedir + "\\VC_Name.txt" );
		}


		//環境設置
		//引数：コンペンド編集, 表示
		public void SetEnvironment ( EditCompend ec )
		{
			EditCompend = ec;

			//環境設定
			foreach ( IScriptParam isp in ls_ctrl_scpPrm ) { isp.SetEnvironment ( ec, ()=>{} ); }
		}

		//関連付け
		public void Assosiate ()
		{
			script = EditCompend.SelectedScript;

			Tb_Frame.Text = script.Frame.ToString ();
			Tb_Img.Text = script.ImgName;

			foreach ( IScriptParam isp in ls_ctrl_scpPrm ) { isp.Assosiate ( script ); }

			cb_SE.Assosiate ( script );
			cb_VC.Assosiate ( script );
		}

		//更新
		public void UpdateData ()
		{
			foreach ( IScriptParam isp in ls_ctrl_scpPrm ) { isp.UpdateData (); }

			cb_SE.UpdateData ();
			cb_VC.UpdateData ();
		}

		//表示
		public void Disp ()
		{
			this.Invalidate ();
		}


	}
}
