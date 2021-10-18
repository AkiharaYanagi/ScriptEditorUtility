using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;

namespace ScriptEditor
{
	using BD_Seq = BindingDictionary < Sequence >;
	using BL_Scp = List < Script >;

	public partial class TestCharaData
	{

		//----------------------------------------------------------------------
		//Stand
		private void MakeAction_Stand ( BD_Seq bd_act )
		{
			MakeScript ( (int)ENM_ACTION.Stand, 288 );

			BL_Scp bl_scp = bd_act.Get ( (int)ENM_ACTION.Stand ).ListScript;

			_MakeAction_Part ( bl_scp, 0  , 128, 1, "Stand_00.png" );
			_MakeAction_Part ( bl_scp, 128, 144, 2, "Stand_01.png" );
			_MakeAction_Part ( bl_scp, 144, 272, 3, "Stand_02.png" );
			_MakeAction_Part ( bl_scp, 272, 288, 4, "Stand_01.png" );
		}

		private void _MakeAction_Part ( BL_Scp bl_s, int start, int end, int group, string imgname )
		{
			for ( int i = start; i < end; ++ i )
			{
				Script s = bl_s [ i ];
				s.Group = group;
				s.ImgName = imgname;
				s.CalcState = CLC_ST.CLC_SUBSTITUDE;

				s.Pos = new Point ( -250, -450 );

				s.BL_RutName.Add ( new TName ( ENM_RUT.地上通常技.ToString() ) );
				s.BL_RutName.Add ( new TName ( ENM_RUT.地上移動.ToString() ) );
			}
		}

		//----------------------------------------------------------------------
		//FrontMove
		private void MakeAction_Move ( BD_Seq bd_act )
		{
			const int i_FrontMove = (int)ENM_ACTION.FrontMove;
			Action act = (Action)bd_act.Get ( i_FrontMove );
			act.NextActionName = ENM_ACTION.FrontMove.ToString ();

			MakeScript ( i_FrontMove, 16 );
			foreach ( Script s in act.ListScript )
			{
				s.Group = 1; 
				s.ImgName = "FrontMove_00.png";
				s.CalcState = CLC_ST.CLC_SUBSTITUDE;
				s.SetVelX ( 15 );
				s.BL_RutName.Add ( new TName ( ENM_RUT.前持続停止 ) );
				s.BL_RutName.Add ( new TName ( ENM_RUT.ジャンプ ) );
				s.BL_RutName.Add ( new TName ( ENM_RUT.特殊 ) );
				s.BL_RutName.Add ( new TName ( ENM_RUT.地上通常技 ) );
				s.BL_RutName.Add ( new TName ( ENM_RUT.地上必殺技 ) );
			}
		}

		//BackMove
		private void MakeAction_BackMove ( BD_Seq bd_act )
		{
			const int i_BackMove = (int)ENM_ACTION.BackMove;
			Action act = (Action)bd_act.Get ( i_BackMove );
			act.NextActionName = ENM_ACTION.BackMove.ToString ();

			MakeScript ( i_BackMove, 16 );
			foreach ( Script s in act.ListScript )
			{
				s.Group = 1; 
				s.ImgName = "BacktMove_00.png";
				s.CalcState = CLC_ST.CLC_SUBSTITUDE;
				s.SetVelX ( -15 );
				s.BL_RutName.Add ( new TName ( ENM_RUT.後持続停止 ) );
				s.BL_RutName.Add ( new TName ( ENM_RUT.ジャンプ ) );
				s.BL_RutName.Add ( new TName ( ENM_RUT.特殊 ) );
				s.BL_RutName.Add ( new TName ( ENM_RUT.地上通常技 ) );
				s.BL_RutName.Add ( new TName ( ENM_RUT.地上必殺技 ) );
			} 		
		}

		//----------------------------------------------------------------------
		//Jump
		private void MakeAction_Jump ( EditBehavior eb, ENM_ACTION enm_act, string imgName, int vel_x )
		{
			int indexAction = (int)enm_act;
			eb.SelectSequence ( indexAction );
			Action act = eb.GetAction ();
			act.NextActionName = ENM_ACTION.Drop.ToString ();
			act.Posture = ActionPosture.JUMP;

			MakeScript ( indexAction, 1 );
			foreach ( Script s in act.ListScript )
			{
				s.Group = 1; 
				s.ImgName = imgName;
				s.CalcState = CLC_ST.CLC_ADD;
				s.SetVelX ( vel_x );
				s.SetVelY ( -25 );
				s.SetAccY ( 1 );
			}
		}

		private void MakeAction_Jump ( Action act, int indexAction, string imgName, int vel_x )
		{
			act.NextActionName = ENM_ACTION.Drop.ToString ();
			act.Posture = ActionPosture.JUMP;

			MakeScript ( indexAction, 1 );
			foreach ( Script s in act.ListScript )
			{
				s.Group = 1; 
				s.ImgName = imgName;
				s.CalcState = CLC_ST.CLC_ADD;
				s.SetVelX ( vel_x );
				s.SetVelY ( -25 );
				s.SetAccY ( 1 );
			}
		}

		//----------------------------------------------------------------------
		//Attack
		private void MakeAction_Attack ( EditBehavior eb, ENM_ACTION enm_act, string imgName )
		{
			int indexAction = (int)enm_act;
			MakeScript ( indexAction, 12 );
			eb.SelectSequence ( indexAction );
			eb.EditSequence.EditScriptInSequence ( s =>
			{
				s.Group = 1; 
				s.CalcState = CLC_ST.CLC_SUBSTITUDE;
				s.ImgName = imgName;
			} );

		}


		//--------------------------------------------------------------------------------
		//script
		private Script SetScript ()
		{
			//手動で作成し、値を設定
			Script script0 = new Script ();
			script0.ListCRect.Add ( new Rectangle ( -90, -300, 100, 250 ) );
			script0.ListHRect.Add ( new Rectangle ( -100, -280, 120, 350 ) );
			script0.ListARect.Add ( new Rectangle ( -160, -150, 20, 60 ) );
			script0.ListORect.Add ( new Rectangle ( -80, -230, 40, 60 ) );
//			script0.BD_EfGnrt.Add ( new EffectGenerate () );

			script0.CalcState = CLC_ST.CLC_SUBSTITUDE;

			//コピー
			Script script1 = new Script ( script0 );
			Script script2 = new Script ();
			script2.Copy ( script1 );
			Debug.Assert ( script1.Equals ( script2 ) );

			return script0;
		}

		//内部　スクリプト設定
		//引数：アクションID, スクリプト個数
		private void MakeScript ( int idAction, int numScript )
		{
			EditBehavior eb = EditChara.Inst.EditBehavior;
			eb.SelectSequence ( idAction );
			eb.SelectedSequence.ListScript.Clear ();		//最初の一つを削除
			for ( int i = 0; i < numScript; ++i )
			{
				Script script = new Script ();
				script.ListCRect.Add ( new Rectangle ( -90, -300, 100, 250 ) );
				script.ListHRect.Add ( new Rectangle ( -100, -280, 120, 350 ) );
				script.ListARect.Add ( new Rectangle ( -160, -150, 20, 60 ) );
				script.ListORect.Add ( new Rectangle ( -80, -230, 40, 60 ) );
				script.SetPos ( -157, -424 );
				eb.AddScript ( script );
			}
		}

	}
}
