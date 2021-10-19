using System;
using System.Collections.Generic;
using System.Drawing;

namespace ScriptEditor
{
	using GKC_ST = GameKeyCommand.GameKeyCommandState;
	using GK_L = GameKeyCommand.LeverCommand;
	using GK_B = GameKeyCommand.ButtonCommand;

	using BD_CMD = BindingDictionary < Command >;
	using BD_BRC = BindingDictionary < Branch >;

	public partial class TestCharaData
	{
		//----------------------------------------------------------------------------
		//Command
		private void MakeCommand ( BD_CMD bd_c )
		{
			GKC_ST IS = GKC_ST.KEY_IS;
			GKC_ST NS = GKC_ST.KEY_NIS;
			GKC_ST PS = GKC_ST.KEY_PUSH;

			//コマンド名から作成
			foreach ( string name in Enum.GetNames ( typeof ( ENM_CMD ) ) )
			{
				EditChara.Inst.AddCommand ( name );
			}

			SetCmdBtn ( bd_c, ENM_CMD.L , 0, 1, PS, GK_B.B_0 );		// L  : Btn0
			SetCmdBtn ( bd_c, ENM_CMD.Ma, 0, 1, PS, GK_B.B_1 );		// Ma : Btn1
			SetCmdBtn ( bd_c, ENM_CMD.Mb, 0, 1, PS, GK_B.B_2 );		// Mb : Btn2
			SetCmdBtn ( bd_c, ENM_CMD.H , 0, 1, PS, GK_B.B_3 );		// H  : Btn3

			SetCmdLvr ( bd_c, ENM_CMD._6,	0, 1, IS, GK_L.C_6 );
			SetCmdLvr ( bd_c, ENM_CMD._4,	0, 1, IS, GK_L.C_4 );
			SetCmdLvr ( bd_c, ENM_CMD._6off,0, 1, NS, GK_L.C_6 );
			SetCmdLvr ( bd_c, ENM_CMD._4off,0, 1, NS, GK_L.C_4 );
			SetCmdLvr ( bd_c, ENM_CMD._8,	0, 1, IS, GK_L.C_8 );
			SetCmdLvr ( bd_c, ENM_CMD._9,	0, 1, IS, GK_L.C_9 );
			SetCmdLvr ( bd_c, ENM_CMD._7,	0, 1, IS, GK_L.C_7 );


			//2_8
			SetCmdLvr ( bd_c, ENM_CMD._2_8, 0, 12, IS, GK_L.C_2 );
			SetCmdLvr ( bd_c, ENM_CMD._2_8, 1, 12, PS, GK_L.C_8 );

			//2_9
			SetCmdLvr ( bd_c, ENM_CMD._2_9, 0, 12, IS, GK_L.C_2 );
			SetCmdLvr ( bd_c, ENM_CMD._2_9, 1, 12, PS, GK_L.C_9 );

			//2_7
			SetCmdLvr ( bd_c, ENM_CMD._2_7, 0, 12, IS, GK_L.C_2 );
			SetCmdLvr ( bd_c, ENM_CMD._2_7, 1, 12, PS, GK_L.C_7 );


			//6_6
			SetCmdLvr ( bd_c, ENM_CMD._6_6, 0, 6, IS, GK_L.C_6 );
			SetCmdLvr ( bd_c, ENM_CMD._6_6, 1, 6, PS, GK_L.C_6 );

			//4_4
			SetCmdLvr ( bd_c, ENM_CMD._4_4, 0, 6, IS, GK_L.C_4 );
			SetCmdLvr ( bd_c, ENM_CMD._4_4, 1, 6, PS, GK_L.C_4 );


			SetCmd_LvrBtn ( bd_c, ENM_CMD._6L , GK_L.C_6, GK_B.B_0 );	//6L
			SetCmd_LvrBtn ( bd_c, ENM_CMD._6Ma, GK_L.C_6, GK_B.B_1 );	//6Ma
			SetCmd_LvrBtn ( bd_c, ENM_CMD._6Mb, GK_L.C_6, GK_B.B_2 );	//6Mb
			SetCmd_LvrBtn ( bd_c, ENM_CMD._6H , GK_L.C_6, GK_B.B_3 );	//6H

			SetCmd_LvrBtn ( bd_c, ENM_CMD._4L , GK_L.C_4, GK_B.B_0 );	//4L
			SetCmd_LvrBtn ( bd_c, ENM_CMD._4Ma, GK_L.C_4, GK_B.B_1 );	//4Ma
			SetCmd_LvrBtn ( bd_c, ENM_CMD._4Mb, GK_L.C_4, GK_B.B_2 );	//4Mb
			SetCmd_LvrBtn ( bd_c, ENM_CMD._4H , GK_L.C_4, GK_B.B_3 );	//4H

			SetCmd_LvrBtn ( bd_c, ENM_CMD._2L , GK_L.C_2, GK_B.B_0 );	//2L
			SetCmd_LvrBtn ( bd_c, ENM_CMD._2Ma, GK_L.C_2, GK_B.B_1 );	//2Ma
			SetCmd_LvrBtn ( bd_c, ENM_CMD._2Mb, GK_L.C_2, GK_B.B_2 );	//2Mb
			SetCmd_LvrBtn ( bd_c, ENM_CMD._2H , GK_L.C_2, GK_B.B_3 );	//2H

			SetCmd_LvrBtn ( bd_c, ENM_CMD._8L , GK_L.C_8, GK_B.B_0 );	//8L
			SetCmd_LvrBtn ( bd_c, ENM_CMD._8Ma, GK_L.C_8, GK_B.B_1 );	//8Ma
			SetCmd_LvrBtn ( bd_c, ENM_CMD._8Mb, GK_L.C_8, GK_B.B_2 );	//8Mb
			SetCmd_LvrBtn ( bd_c, ENM_CMD._8H , GK_L.C_8, GK_B.B_3 );	//8H


			//236L
			SetCmdLvr ( bd_c, ENM_CMD.SP0, 0, 16, IS, GK_L.C_2 );
			SetCmdLvr ( bd_c, ENM_CMD.SP0, 1, 16, IS, GK_L.C_3 );
			SetCmdLvr ( bd_c, ENM_CMD.SP0, 2, 16, IS, GK_L.C_6 );
			SetCmdBtn ( bd_c, ENM_CMD.SP0, 3, 16, PS, 0 );
		}

		private void SetCmdBtn ( BD_CMD bd_c, ENM_CMD name_cmd, int index, int limit, GKC_ST st, GK_B btn )
		{
			Command cmd = bd_c.Get ( name_cmd.ToString () );
			cmd.LimitTime = limit;

			List < GameKeyCommand > lgk = cmd.ListGameKeyCommand;
			//既存の設定のとき追加しない
			if ( lgk.Count <= index )
			{
				lgk.Add ( new GameKeyCommand () );
			}
			lgk [ index ].Btn[ (int)btn ] = st;
		}

		private void SetCmdLvr ( BD_CMD bd_c, ENM_CMD name_cmd, int index, int limit, GKC_ST st, GK_L gk_l )
		{
			Command cmd = bd_c.Get ( name_cmd.ToString () );
			cmd.ListGameKeyCommand.Add ( new GameKeyCommand () );
			cmd.ListGameKeyCommand [ index ].SetLvrSt ( st, gk_l );
			cmd.LimitTime = limit;
		}

		//レバー入れ１ボタン
		private void SetCmd_LvrBtn ( BD_CMD bd_c, ENM_CMD cmd, GK_L gk_l, GK_B btn )
		{
			GKC_ST IS = GKC_ST.KEY_IS;
			GKC_ST PS = GKC_ST.KEY_PUSH;

			SetCmdLvr ( bd_c, cmd, 0, 1, IS, gk_l );
			SetCmdBtn ( bd_c, cmd, 0, 1, PS, btn );
		}



		//----------------------------------------------------------------------------
		//テスト用ブランチの作成
		private void MakeBranch ( Chara chara )
		{
			//名前から作成
			foreach ( string str in Enum.GetNames ( typeof ( ENM_BRC ) ) )
			{
				EditChara.Inst.AddBranch ( str );
			}

			BD_BRC b = chara.BD_Branch;
			BranchCondition BC_CMD = BranchCondition.CMD;
			BranchCondition BC_GRD = BranchCondition.GRD;
			BranchCondition BC_THR_I = BranchCondition.THR_I;
			BranchCondition BC_THR_E = BranchCondition.THR_E;
			SetBranch ( b, ENM_BRC.L_立L,		BC_CMD,		ENM_CMD.L,		ENM_ACTION.Attack_5L );
			SetBranch ( b, ENM_BRC.Ma_立Ma,		BC_CMD,		ENM_CMD.Ma,		ENM_ACTION.Attack_5Ma );
			SetBranch ( b, ENM_BRC.Mb_立Mb,		BC_CMD,		ENM_CMD.Mb,		ENM_ACTION.Attack_5Mb );
			SetBranch ( b, ENM_BRC.H_立H,		BC_CMD,		ENM_CMD.H,		ENM_ACTION.Attack_5H );
			SetBranch ( b, ENM_BRC._6_F_Move,	BC_CMD,		ENM_CMD._6,		ENM_ACTION.FrontMove );
			SetBranch ( b, ENM_BRC._4_B_Move,	BC_CMD,		ENM_CMD._4,		ENM_ACTION.BackMove );
			SetBranch ( b, ENM_BRC._6離_Stand,	BC_CMD,		ENM_CMD._6off,	ENM_ACTION.Stand );
			SetBranch ( b, ENM_BRC._4離_Stand,	BC_CMD,		ENM_CMD._4off,	ENM_ACTION.Stand );
			SetBranch ( b, ENM_BRC._6_6_F_Dash,	BC_CMD,		ENM_CMD._6_6,	ENM_ACTION.FrontDash );
			SetBranch ( b, ENM_BRC._4_4_B_Dash,	BC_CMD,		ENM_CMD._4_4,	ENM_ACTION.BackDash );
			SetBranch ( b, ENM_BRC._8_V_Jump,	BC_CMD,		ENM_CMD._2_8,	ENM_ACTION.VerticalJump );
			SetBranch ( b, ENM_BRC._9_F_Jump,	BC_CMD,		ENM_CMD._2_9,	ENM_ACTION.FrontJump );
			SetBranch ( b, ENM_BRC._7_B_Jump,	BC_CMD,		ENM_CMD._2_7,	ENM_ACTION.BackJump );
			SetBranch ( b, ENM_BRC.着地_Stand,	BC_GRD,		ENM_CMD._N,		ENM_ACTION.Stand );
			SetBranch ( b, ENM_BRC.SP01,		BC_CMD,		ENM_CMD._6H,	ENM_ACTION.SP0_L );
			SetBranch ( b, ENM_BRC.SP0,			BC_CMD,		ENM_CMD.SP0,	ENM_ACTION.SP0_L );
			SetBranch ( b, ENM_BRC.投げ分岐_自,	BC_THR_I,	ENM_CMD._N,		ENM_ACTION.SP01_L );
			SetBranch ( b, ENM_BRC.投げ分岐_相,	BC_THR_E,	ENM_CMD._N,		ENM_ACTION.Thrown0 );

			SetBranch ( b, ENM_BRC._6L,			BC_CMD,		ENM_CMD._6L,	ENM_ACTION.Attack_6L );
			SetBranch ( b, ENM_BRC._6Ma,		BC_CMD,		ENM_CMD._6Ma,	ENM_ACTION.Attack_6Ma );
			SetBranch ( b, ENM_BRC._6Mb,		BC_CMD,		ENM_CMD._6Mb,	ENM_ACTION.Attack_6Mb );
			SetBranch ( b, ENM_BRC._6H,			BC_CMD,		ENM_CMD._6H,	ENM_ACTION.Attack_6H );

			SetBranch ( b, ENM_BRC._4L ,		BC_CMD,		ENM_CMD._4L ,	ENM_ACTION.Attack_4L );
			SetBranch ( b, ENM_BRC._4Ma,		BC_CMD,		ENM_CMD._4Ma,	ENM_ACTION.Attack_4Ma );
			SetBranch ( b, ENM_BRC._4Mb,		BC_CMD,		ENM_CMD._4Mb,	ENM_ACTION.Attack_4Mb );
			SetBranch ( b, ENM_BRC._4H ,		BC_CMD,		ENM_CMD._4H ,	ENM_ACTION.Attack_4H );

			SetBranch ( b, ENM_BRC._2L ,		BC_CMD,		ENM_CMD._2L ,	ENM_ACTION.Attack_2L );
			SetBranch ( b, ENM_BRC._2Ma,		BC_CMD,		ENM_CMD._2Ma,	ENM_ACTION.Attack_2Ma );
			SetBranch ( b, ENM_BRC._2Mb,		BC_CMD,		ENM_CMD._2Mb,	ENM_ACTION.Attack_2Mb );
			SetBranch ( b, ENM_BRC._2H ,		BC_CMD,		ENM_CMD._2H ,	ENM_ACTION.Attack_2H );

			SetBranch ( b, ENM_BRC._8L ,		BC_CMD,		ENM_CMD._8L ,	ENM_ACTION.Attack_8L );
			SetBranch ( b, ENM_BRC._8Ma,		BC_CMD,		ENM_CMD._8Ma,	ENM_ACTION.Attack_8Ma );
			SetBranch ( b, ENM_BRC._8Mb,		BC_CMD,		ENM_CMD._8Mb,	ENM_ACTION.Attack_8Mb );
			SetBranch ( b, ENM_BRC._8H ,		BC_CMD,		ENM_CMD._8H,	ENM_ACTION.Attack_8H );

			SetBranch ( b, ENM_BRC.被ダメ_H,		BC_THR_E,	ENM_CMD._N,		ENM_ACTION.Damaged_H );
		}

		private void SetBranch ( BD_BRC bd_brc, ENM_BRC brc_name, BranchCondition bc, ENM_CMD cmd_name, ENM_ACTION act_name )
		{
			Branch brc = bd_brc.Get ( brc_name.ToString () );
			brc.Condition = bc;
			brc.NameCommand = cmd_name.ToString ();
			brc.NameAction = act_name.ToString ();
		}

		//----------------------------------------------------------------------------
		//ルート
		private void MakeRoute ( Chara chara )
		{
			Route rut0 = new Route ( ENM_RUT.地上通常技.ToString(), "立ち状態で移行する技全般" );
			SetEnmBrc ( rut0, ENM_BRC._4L );
			SetEnmBrc ( rut0, ENM_BRC._4Ma );
			SetEnmBrc ( rut0, ENM_BRC._4Mb );
			SetEnmBrc ( rut0, ENM_BRC._4H );

			SetEnmBrc ( rut0, ENM_BRC._6L );
			SetEnmBrc ( rut0, ENM_BRC._6Ma );
			SetEnmBrc ( rut0, ENM_BRC._6Mb );
			SetEnmBrc ( rut0, ENM_BRC._6H );

			SetEnmBrc ( rut0, ENM_BRC._2L );
			SetEnmBrc ( rut0, ENM_BRC._2Ma );
			SetEnmBrc ( rut0, ENM_BRC._2Mb );
			SetEnmBrc ( rut0, ENM_BRC._2H );

			SetEnmBrc ( rut0, ENM_BRC._8L );
			SetEnmBrc ( rut0, ENM_BRC._8Ma );
			SetEnmBrc ( rut0, ENM_BRC._8Mb );
			SetEnmBrc ( rut0, ENM_BRC._8H );

			SetEnmBrc ( rut0, ENM_BRC.L_立L );
			SetEnmBrc ( rut0, ENM_BRC.Ma_立Ma );
			SetEnmBrc ( rut0, ENM_BRC.Mb_立Mb );
			SetEnmBrc ( rut0, ENM_BRC.H_立H );
			chara.BD_Route.Add ( rut0 );


			Route rut1 = new Route ( ENM_RUT.地上移動.ToString(), "立ち状態から出る移動全般" );
			SetEnmBrc ( rut1, ENM_BRC._9_F_Jump );
			SetEnmBrc ( rut1, ENM_BRC._7_B_Jump );
			SetEnmBrc ( rut1, ENM_BRC._8_V_Jump );
			SetEnmBrc ( rut1, ENM_BRC._6_F_Move );
			SetEnmBrc ( rut1, ENM_BRC._4_B_Move );
			chara.BD_Route.Add ( rut1 );

			Route rut2 = new Route ( ENM_RUT.前持続停止.ToString(), "前持続を離す" );
			SetEnmBrc ( rut2, ENM_BRC._6離_Stand );
			chara.BD_Route.Add ( rut2 );

			Route rut3 = new Route ( ENM_RUT.後持続停止.ToString(), "後持続を離す" );
			SetEnmBrc ( rut3, ENM_BRC._4離_Stand );
			chara.BD_Route.Add ( rut3 );

			Route rut4 = new Route ( ENM_RUT.ジャンプ.ToString(), "ジャンプ" );
			SetEnmBrc ( rut4, ENM_BRC._8_V_Jump );
			SetEnmBrc ( rut4, ENM_BRC._9_F_Jump );
			SetEnmBrc ( rut4, ENM_BRC._7_B_Jump );
			chara.BD_Route.Add ( rut4 );

			Route rut5 = new Route ( ENM_RUT.地上必殺技.ToString(), "地上必殺技" );
			SetEnmBrc ( rut5, ENM_BRC.SP0 );
			chara.BD_Route.Add ( rut5 );

			Route rut6 = new Route ( ENM_RUT.特殊.ToString(), "特殊" );
			SetEnmBrc ( rut6, ENM_BRC.SP01 );
			chara.BD_Route.Add ( rut6 );

			Route rut7 = new Route ( ENM_RUT.投げ分岐_自.ToString(), "投げ分岐_自" );
			SetEnmBrc ( rut7, ENM_BRC.投げ分岐_自 );
			chara.BD_Route.Add ( rut7 );

			Route rut8 = new Route ( ENM_RUT.投げ分岐_相.ToString(), "投げ分岐_相" );
			SetEnmBrc ( rut8, ENM_BRC.投げ分岐_相 );
			chara.BD_Route.Add ( rut8 );

			Route rut9 = new Route ( ENM_RUT.被ダメ_H.ToString(), "被ダメ_H" );
			SetEnmBrc ( rut9, ENM_BRC.被ダメ_H );
			chara.BD_Route.Add ( rut9 );
		}

		private void SetEnmBrc ( Route rut, ENM_BRC enm_brc )
		{
			rut.BL_BranchName.Add ( new TName ( enm_brc.ToString() ) );
		}


		//エディット（ガーニッシュ）のテスト
		private void MakeGarnish ( Chara chara )
		{
			//ガーニッシュの編集
			EditGarnish eg = EditChara.Inst.EditGarnish;

			//エフェクト
			eg.AddEffect ( new Effect ( "testEffect0" ) );
			eg.AddEffect ( new Effect ( "testEffect1" ) );

			//選択
			eg.SelectSequence ( 0 );
			eg.SelectedScript.SetPos ( -120, -220 );

			//新規
			Script efScript = new Script ();
			eg.AddScript ( efScript );

			//値設定
//			chara.garnish.BD_Sequence.GetBindingList()[ 1 ].ListScript[ 0 ].ImgIndex = 1;
//			chara.garnish.ListSequence[ 1 ].ListScript[ 0 ].RefPt.Set ( -140, -240 );

			//スクリプトでエフェクト生成を指定する
//			Script sc_ef = chara.behavior.BD_Sequence.GetBindingList()[ 0 ].ListScript[ 0 ];
			eg.SelectScript ( 0, 0 );
			EffectGenerate efGnrt = new EffectGenerate
			{
				Name = "Effect0",
				EfName = chara.garnish.BD_Sequence.Get ( 0 ).Name,
				Pt = new Point ( -20, -50 )
			};

			Script main_scp = chara.behavior.BD_Sequence.Get ( 0 ).ListScript [ 0 ];
			main_scp.BD_EfGnrt.Add ( efGnrt );
		}
	}
}
