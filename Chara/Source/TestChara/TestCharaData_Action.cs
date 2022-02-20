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
			MakeScript ( (int)ENM_ACT.Stand, 288 );

			BL_Scp bl_scp = bd_act.Get ( (int)ENM_ACT.Stand ).ListScript;
			_MakeAction_Part ( bl_scp, 0  , 128, 1, "Stand_00.png" );
			_MakeAction_Part ( bl_scp, 128, 144, 2, "Stand_01.png" );
			_MakeAction_Part ( bl_scp, 144, 272, 3, "Stand_02.png" );
			_MakeAction_Part ( bl_scp, 272, 288, 4, "Stand_01.png" );

#if false
			EffectGenerate efgn = new EffectGenerate ()
			{
				Name = "EfGn",
				EfName = "testEffect0",
				Pt = new Point ( 100, -150 ),
				Gnrt = true,
			};
			bl_scp[0].BD_EfGnrt.Add ( efgn );
#endif
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

				s.BD_RutName.Add ( new TName ( ENM_RUT.地上超必 ) );
				s.BD_RutName.Add ( new TName ( ENM_RUT.地上必殺技 ) );
				s.BD_RutName.Add ( new TName ( ENM_RUT.地上通常技 ) );
				s.BD_RutName.Add ( new TName ( ENM_RUT.ジャンプ ) );
				s.BD_RutName.Add ( new TName ( ENM_RUT.地上移動 ) );
			}
		}

		//----------------------------------------------------------------------
		//FrontMove
		private void MakeAction_Move ( BD_Seq bd_act )
		{
			const int i_FrontMove = (int)ENM_ACT.FrontMove;
			Action act = (Action)bd_act.Get ( i_FrontMove );
			act.NextActionName = ENM_ACT.FrontMove.ToString ();

			MakeScript ( i_FrontMove, 16 );
			foreach ( Script s in act.ListScript )
			{
				s.Group = 1; 
				s.ImgName = "FrontMove_00.png";
				s.CalcState = CLC_ST.CLC_SUBSTITUDE;
				s.SetVelX ( 15 );
				s.BD_RutName.Add ( new TName ( ENM_RUT.地上超必 ) );
				s.BD_RutName.Add ( new TName ( ENM_RUT.地上必殺技 ) );
				s.BD_RutName.Add ( new TName ( ENM_RUT.特殊 ) );
				s.BD_RutName.Add ( new TName ( ENM_RUT.地上通常技 ) );
				s.BD_RutName.Add ( new TName ( ENM_RUT.ジャンプ ) );
				s.BD_RutName.Add ( new TName ( ENM_RUT.前持続停止 ) );
			}
		}

		//BackMove
		private void MakeAction_BackMove ( BD_Seq bd_act )
		{
			const int i_BackMove = (int)ENM_ACT.BackMove;
			Action act = (Action)bd_act.Get ( i_BackMove );
			act.NextActionName = ENM_ACT.BackMove.ToString ();

			MakeScript ( i_BackMove, 16 );
			foreach ( Script s in act.ListScript )
			{
				s.Group = 1; 
				s.ImgName = "BacktMove_00.png";
				s.CalcState = CLC_ST.CLC_SUBSTITUDE;
				s.SetVelX ( -15 );
				s.BD_RutName.Add ( new TName ( ENM_RUT.地上超必 ) );
				s.BD_RutName.Add ( new TName ( ENM_RUT.地上必殺技 ) );
				s.BD_RutName.Add ( new TName ( ENM_RUT.特殊 ) );
				s.BD_RutName.Add ( new TName ( ENM_RUT.地上通常技 ) );
				s.BD_RutName.Add ( new TName ( ENM_RUT.ジャンプ ) );
				s.BD_RutName.Add ( new TName ( ENM_RUT.後持続停止 ) );
			} 		
		}

		//----------------------------------------------------------------------
		//Jump
		private void MakeAction_Jump ( EditBehavior eb, ENM_ACT enm_act, string imgName, int vel_x )
		{
			int indexAction = (int)enm_act;
			eb.SelectSequence ( indexAction );
			Action act = eb.GetAction ();
			act.NextActionName = ENM_ACT.Drop.ToString ();
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
				s.ListCRect.Clear ();
			}
		}

		//----------------------------------------------------------------------
		//FrontDash
		private void MakeAction_FrontDash ( BD_Seq bd_act )
		{
			const int i_FrontDash = (int)ENM_ACT.FrontDash;
			Action act = (Action)bd_act.Get ( i_FrontDash );
			act.NextActionName = ENM_ACT.FrontDash.ToString ();

			MakeScript ( i_FrontDash, 20 );
			BL_Scp bl_scp = bd_act.Get ( i_FrontDash ).ListScript;
			_MakeAction_Part_FrontDash ( bl_scp, 0 , 5 , 1, "FrontDash_00.png" );
			_MakeAction_Part_FrontDash ( bl_scp, 5 , 10, 2, "FrontDash_01.png" );
			_MakeAction_Part_FrontDash ( bl_scp, 10, 15, 3, "FrontDash_02.png" );
			_MakeAction_Part_FrontDash ( bl_scp, 15, 20, 4, "FrontDash_03.png" );
		}

		private void _MakeAction_Part_FrontDash ( BL_Scp bl_s, int start, int end, int group, string imgname )
		{
			for ( int i = start; i < end; ++ i )
			{
				Script s = bl_s [ i ];
				s.Group = group;
				s.ImgName = imgname;
				s.Vel = new Point ( 20, 0 );
				s.CalcState = CLC_ST.CLC_SUBSTITUDE;
				s.Pos = new Point ( -250, -450 );

				s.BD_RutName.Add ( new TName ( ENM_RUT.地上超必 ) );
				s.BD_RutName.Add ( new TName ( ENM_RUT.地上必殺技 ) );
				s.BD_RutName.Add ( new TName ( ENM_RUT.地上通常技 ) );
				s.BD_RutName.Add ( new TName ( ENM_RUT.ジャンプ ) );
				s.BD_RutName.Add ( new TName ( ENM_RUT.前持続停止 ) );
			}
		}

		//----------------------------------------------------------------------
		//Attack
		private void MakeAction_Attack ( EditBehavior eb, ENM_ACT enm_act, string imgName )
		{
			int indexAction = (int)enm_act;
			MakeScript ( indexAction, 25 );
			eb.SelectSequence ( indexAction );
			eb.EditSequence.EditScriptInSequence ( s =>
			{
				s.Group = 1; 
				s.CalcState = CLC_ST.CLC_SUBSTITUDE;
				s.ImgName = imgName;
				s.ListARect.Add ( new Rectangle ( 130, -250, 60, 100 ) );
//				s.ListORect.Add ( new Rectangle ( 20, -220, 40, 40 ) );
			} );

		}

		private void MakeAction_Damaged ( EditBehavior eb, ENM_ACT enm_act, string imgName )
		{
			int indexAction = (int)enm_act;
			MakeScript ( indexAction, 40 );
			eb.SelectSequence ( indexAction );
			eb.EditSequence.EditScriptInSequence ( s =>
			{
				s.Group = 1; 
				s.CalcState = CLC_ST.CLC_SUBSTITUDE;
				s.ImgName = imgName;
				s.SetVelX ( -5 );
			} );

		}


		//--------------------------------------------------------------------------------
		//script
#if false
		private Script SetScript ()
		{
			//手動で作成し、値を設定
			Script script0 = new Script ();
			script0.ListCRect.Add ( new Rectangle ( -90, -300, 100, 250 ) );
			script0.ListHRect.Add ( new Rectangle ( -100, -280, 120, 350 ) );

//			script0.ListARect.Add ( new Rectangle ( -160, -150, 20, 60 ) );
//			script0.ListORect.Add ( new Rectangle ( -80, -230, 40, 60 ) );
//			script0.BD_EfGnrt.Add ( new EffectGenerate () );

			script0.CalcState = CLC_ST.CLC_SUBSTITUDE;

			//コピー
			Script script1 = new Script ( script0 );
			Script script2 = new Script ();
			script2.Copy ( script1 );
			Debug.Assert ( script1.Equals ( script2 ) );

			return script0;
		}
#endif

		//内部　スクリプト設定
		//引数：アクションID, スクリプト個数
		private void MakeScript ( int idAction, int numScript )
		{
			EditBehavior eb = EditChara.Inst.EditBehavior;
			eb.SelectSequence ( idAction );
			eb.SelectedSequence.ListScript.Clear ();		//既存のスクリプトを削除
			for ( int i = 0; i < numScript; ++i )
			{
				Script script = new Script ();
				script.ListCRect.Add ( new Rectangle ( -90, -300, 100, 250 ) );
				script.ListHRect.Add ( new Rectangle ( -100, -280, 120, 350 ) );
//				script.ListARect.Add ( new Rectangle ( -160, -150, 20, 60 ) );
//				script.ListORect.Add ( new Rectangle ( -80, -230, 40, 60 ) );
				script.SetPos ( -157, -424 );
				eb.AddScript ( script );
			}
		}

	}
}
