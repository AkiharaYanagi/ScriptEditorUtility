using System;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using System.Collections.Generic;

namespace ScriptEditor
{
	using GKC_ST = GameKeyCommand.GameKeyCommandState;
	using GK_L = GameKeyCommand.LeverCommand;
	using GK_B = GameKeyCommand.ButtonCommand;

	using BD_Seq = BindingDictionary < Sequence >;
	using BL_Script = List < Script >;
	using BD_CMD = BindingDictionary < Command >;
	using BD_BRC = BindingDictionary < Branch >;

	public partial class TestCharaData
	{
		//-------------------------------------------------------------------------
		//項目別設定

		//イメージリストの作成(メインとEf)
		private void MakeImage ( Chara chara )
		{
			_MakeImage ( chara.behavior.BD_Image, "Image" );
			_MakeImage ( chara.garnish.BD_Image, "EfImage" );
		}

		//対象ディレクトリからイメージリストの作成
		private void _MakeImage ( BindingDictionary < ImageData > imageList, string imageDir )
		{
			//イメージフォルダ確認
			if ( ! Directory.Exists ( imageDir ) )
			{
				Directory.CreateDirectory ( imageDir );
			}

			//ファイル名を列挙する
			string[] filepaths = Directory.GetFiles ( imageDir );
				
			//何もないとき
			if ( 0 == filepaths.Length )
			{
				//Imageを作成する
				Bitmap bmp = new Bitmap ( 128, 128 );
				Graphics g = Graphics.FromImage ( bmp );
				Font f = new Font ( "Meiryo UI", 20 );
				g.DrawString ( "ダミー", f, Brushes.OrangeRed, 32, 64 );
				f.Dispose ();
				g.Dispose ();
				bmp.Save ( imageDir + "\\000_dummy.png", System.Drawing.Imaging.ImageFormat.Png );
				bmp.Dispose ();
				filepaths = Directory.GetFiles ( imageDir );
			}
			else
			{
				//すべてのファイルに対し
				foreach ( string path in filepaths )
				{
					//画像からImageData型を作成
					string name = Path.GetFileName ( path );
					string fn = name.Substring ( 4 );	//先頭のインデックス("ddd_")を除く
					ImageData imageData = new ImageData ( fn, Image.FromFile ( path ) );
				
					//内部データに保存
					imageList.Add ( imageData );
				}
			}
		}

		//ActionList
		private void MakeActionList ()
		{
			EditBehavior eb = EditChara.Inst.EditBehavior;

			int index = 0;
			string[] names = Enum.GetNames ( typeof ( ENM_ACTION ) );
			foreach ( string name in names )
			{
				Action act = new Action ( name )
				{
					Category = _ACTION_CATEGORY [ index ++ ],
					NextActionName = ENM_ACTION.Stand.ToString (),
				};
				eb.AddAction ( act );
			}
		}

		//アクション内の値を指定
		private void MakeAction ( Chara chara )
		{
			//編集
			EditBehavior eb = EditChara.Inst.EditBehavior;
			EditSequence ea = eb.EditSequence;
			BD_Seq bd_act = chara.behavior.BD_Sequence;

			//スクリプト
			Script script_test = SetScript ();

			//スクリプトコピー
			Script scp_cpy = bd_act.Get ( (int)ENM_ACTION.Stand ).ListScript [ 0 ];
			scp_cpy.Copy ( script_test );

			//Stand
			MakeAction_Stand ( bd_act );

			//FrontMove
			MakeAction_Move ( bd_act );

			//BackMove
			MakeAction_BackMove ( bd_act );


			//VerticalJump
#if false
			const int i_VerticalJump = (int)ENM_ACTION.VerticalJump;
			MakeScript ( i_VerticalJump, 1 );
			eb.SelectSequence ( i_VerticalJump );
			eb.GetAction ().NextActionName = ENM_ACTION.Drop.ToString ();
			eb.GetAction ().Posture = ActionPosture.JUMP;
			ea.EditScriptInSequence ( s =>
			{
				s.Group = 1; 
				s.ImgName = "AirFrontDash_00.png";
				s.CalcState = CLC_ST.CLC_ADD;
				s.SetVelY ( -25 );
				s.SetAccY ( 1 );
			} );
#endif
			const int i_VerticalJump = (int)ENM_ACTION.VerticalJump;
			Action act_VerticalJump = (Action)bd_act.Get ( i_VerticalJump );
			MakeAction_Jump ( act_VerticalJump, i_VerticalJump, "AirFrontDash_00.png", 0 );

#if false
			//FrontJump
			const int i_FrontJump = (int)ENM_ACTION.FrontJump;
			MakeScript ( i_FrontJump, 1 );
			eb.SelectSequence ( i_FrontJump );
			eb.GetAction ().NextActionName = ENM_ACTION.Drop.ToString ();
			eb.GetAction ().Posture = ActionPosture.JUMP;
			ea.EditScriptInSequence ( s =>
			{
				s.Group = 1; 
				s.ImgName = "AirFrontDash_00.png";
				s.CalcState = CLC_ST.CLC_ADD;
				s.SetVelX ( 3 );
				s.SetVelY ( -25 );
				s.SetAccY ( 1 );
			} );

			//BackJump
			const int i_BackJump = (int)ENM_ACTION.BackJump;
			MakeScript ( i_BackJump, 1 );
			eb.SelectSequence ( i_BackJump );
			eb.GetAction ().NextActionName = ENM_ACTION.Drop.ToString ();
			eb.GetAction ().Posture = ActionPosture.JUMP;
			ea.EditScriptInSequence ( s =>
			{
				s.Group = 1; 
				s.ImgName = "AirFrontDash_00.png";
				s.CalcState = CLC_ST.CLC_ADD;
				s.SetVelX ( -3 );
				s.SetVelY ( -25 );
				s.SetAccY ( 1 );
			} );
#endif
			const int i_FrontJump = (int)ENM_ACTION.FrontJump;
			Action act_FrontJump = (Action)bd_act.Get ( i_FrontJump );
			MakeAction_Jump ( act_FrontJump, i_FrontJump, "AirFrontDash_00.png", 3 );

			const int i_BackJump = (int)ENM_ACTION.BackJump;
			Action act_BackJump = (Action)bd_act.Get ( i_BackJump );
			MakeAction_Jump ( act_BackJump, i_BackJump, "AirFrontDash_00.png", -3 );

			//Drop
			const int i_Drop = (int)ENM_ACTION.Drop;
			MakeScript ( i_Drop, 1 );
			eb.SelectSequence ( i_Drop );
			eb.GetAction ().NextActionName = ENM_ACTION.Drop.ToString ();
			eb.GetAction ().Posture = ActionPosture.JUMP;
			ea.EditScriptInSequence ( s =>
			{
				s.Group = 1; 
				s.ImgName = "AirBackDash_00.png";
				s.CalcState = CLC_ST.CLC_MAINTAIN;
				s.SetAccY ( 1 );
			} );

#if false
			//FrontDash
			MakeScript ( 3, 12 );
//			SetAllScriptVelX ( 3, 10 );

			//FrontDashDuration
			MakeScript ( 4, 1 );
//			SetAllScriptVelX ( 3, 20 );

			//"BackDash", 
			MakeScript ( 5, 1 );
//			SetAllScriptVelX ( 3, -10 );
#endif

			//"Attack_5L", 
			const int i_Attack_5L = (int)ENM_ACTION.Attack_5L;
			MakeScript ( i_Attack_5L, 12 );
			eb.SelectSequence ( i_Attack_5L );
			ea.EditScriptInSequence ( s =>
			{
				s.Group = 1; 
				s.ImgName = "Part_5L_00.png";
			} );

			//"Attack_5Ma", 
			const int i_Attack_5Ma = (int)ENM_ACTION.Attack_5Ma;
			MakeScript ( i_Attack_5Ma, 12 );
			eb.SelectSequence ( i_Attack_5Ma );
			ea.EditScriptInSequence ( s =>
			{
				s.Group = 1; 
				s.ImgName = "Part_5Ma_00.png";
			} );

			//"Attack_5Mb", 
			const int i_Attack_5Mb = (int)ENM_ACTION.Attack_5Mb;
			MakeScript ( i_Attack_5Mb, 12 );
			eb.SelectSequence ( i_Attack_5Mb );
			ea.EditScriptInSequence ( s =>
			{
				s.Group = 1; 
				s.ImgName = "Part_5Mb_00.png";
			} );

			//"Attack_5H", 
			const int i_Attack_5H = (int)ENM_ACTION.Attack_5H;
			MakeScript ( i_Attack_5H, 20 );
			eb.SelectSequence ( i_Attack_5H );
			ea.EditScriptInSequence ( s =>
			{
				s.Group = 1; 
				s.ImgName = "Part_5H_00.png";
			} );
			BL_Script bl_scp_5H = bd_act.Get ( (int)ENM_ACTION.Attack_5H ).ListScript;
			for ( int i = 0; i < 4; ++ i )
			{
				Script s = bl_scp_5H [ i ];
				s.ImgName = "Part_5H_00.png";
				s.ListARect.Clear ();
			}
			for ( int i = 4; i < 8; ++ i )
			{
				Script s = bl_scp_5H [ i ];
				s.ImgName = "Part_5H_01.png";
				s.ListARect.Clear ();
			}
			for ( int i = 8; i < 12; ++ i )
			{
				Script s = bl_scp_5H [ i ];
				s.ImgName = "Part_5H_02.png";
				s.ListARect.Clear ();
				s.ListARect.Add ( new Rectangle ( 50, -300, 200, 50 ) );
			}
			for ( int i = 12; i < 16; ++ i )
			{
				Script s = bl_scp_5H [ i ];
				s.ImgName = "Part_5H_03.png";
				s.ListARect.Clear ();
			}
			for ( int i = 16; i < 20; ++ i )
			{
				Script s = bl_scp_5H [ i ];
				s.ImgName = "Part_5H_04.png";
				s.ListARect.Clear ();
			}


			//"SP0_L", 投げ技 始動→スカり
			const int i_SP0_L = (int)ENM_ACTION.SP0_L;
			MakeScript ( i_SP0_L, 12 );
			eb.SelectSequence ( i_SP0_L );
			ea.EditScriptInSequence ( s =>
			{
				s.Group = 1; 
				s.ImgName = "StarMassStrike_00.png";
			} );
			BL_Script bl_SP0_L = bd_act.Get ( (int)ENM_ACTION.SP0_L ).ListScript;
			for ( int i = 0; i < 4; ++ i )
			{
				Script s = bl_SP0_L [ i ];
				s.ImgName = "StarMassStrike_00.png";
				s.ListARect.Clear ();
			}
			for ( int i = 4; i < 8; ++ i )
			{
				Script s = bl_SP0_L [ i ];
				s.ImgName = "StarMassStrike_00.png";
				s.ListARect.Clear ();
				s.ListARect.Add ( new Rectangle ( 50, -300, 200, 50 ) );
				s.BL_RutName.Add ( new TName ( ENM_RUT.投げ分岐_自 ) );
				s.BL_RutName.Add ( new TName ( ENM_RUT.投げ分岐_相 ) );
			}
			for ( int i = 8; i < 12; ++ i )
			{
				Script s = bl_SP0_L [ i ];
				s.ImgName = "StarMassStrike_01.png";
				s.ListARect.Clear ();
			}

			//"SP01_L", 投げ技 成立
			const int i_SP01_L = (int)ENM_ACTION.SP01_L;
			MakeScript ( i_SP01_L, 30 );
			eb.SelectSequence ( i_SP01_L );
			ea.EditScriptInSequence ( s =>
			{
				s.Group = 1; 
				s.ImgName = "StarMassStrike_02.png";
			} );


			//"Thrown0", 投げ技 くらい側アクション
			const int i_Thrown0 = (int)ENM_ACTION.Thrown0;
			MakeScript ( i_Thrown0, 30 );
			eb.SelectSequence ( i_Thrown0 );
			ea.EditScriptInSequence ( s =>
			{
				s.Group = 1; 
				s.ImgName = "MotionDrive_05.png";
			} );


#if false
			//"Clang", 
			//"Avoid", 
			//"Dotty", 
			//"Damaged", 
			//"Down", 
			Action actionDown = ( Action ) eb.Compend.BD_Sequence.GetBindingList()[ 14 ];
//			actionDown.NextIndex = 15;
			//"DownDuration", 
			Action actionDownDuration = ( Action ) eb.Compend.BD_Sequence.GetBindingList()[ 15 ];
//			actionDownDuration.NextIndex = 15;
			//"Win", 
			Action actionWin = ( Action ) eb.Compend.BD_Sequence.GetBindingList()[ 16 ];
//			actionWin.NextIndex = 17;
			//"WinDuration", 
			Action actionWinDuration = ( Action ) eb.Compend.BD_Sequence.GetBindingList()[ 17 ];
//			actionWinDuration.NextIndex = 17;
#endif

		}



		//----------------------------------------------------------------------------
		//Command
		private void MakeCommand ( BD_CMD bd_c )
		{
			GKC_ST IS = GKC_ST.KEY_IS;
			GKC_ST NS = GKC_ST.KEY_NIS;
			GKC_ST PS = GKC_ST.KEY_PUSH;

			//コマンド名から作成
			foreach ( string name in Enum.GetNames ( typeof ( ENM_COMMAND ) ) )
			{
				EditChara.Inst.AddCommand ( name );
			}

			SetCmdBtn ( bd_c, ENM_COMMAND.L , 0, 1, PS, GK_B.B_0 );		// L  : Btn0
			SetCmdBtn ( bd_c, ENM_COMMAND.Ma, 0, 1, PS, GK_B.B_1 );		// Ma : Btn1
			SetCmdBtn ( bd_c, ENM_COMMAND.Mb, 0, 1, PS, GK_B.B_2 );		// Mb : Btn2
			SetCmdBtn ( bd_c, ENM_COMMAND.H , 0, 1, PS, GK_B.B_3 );		// H  : Btn3

			SetCmdLvr ( bd_c, ENM_COMMAND._6,	0, 1, IS, GK_L.C_6 );
			SetCmdLvr ( bd_c, ENM_COMMAND._4,	0, 1, IS, GK_L.C_4 );
			SetCmdLvr ( bd_c, ENM_COMMAND._6off,0, 1, NS, GK_L.C_6 );
			SetCmdLvr ( bd_c, ENM_COMMAND._4off,0, 1, NS, GK_L.C_4 );
			SetCmdLvr ( bd_c, ENM_COMMAND._8,	0, 1, IS, GK_L.C_8 );
			SetCmdLvr ( bd_c, ENM_COMMAND._9,	0, 1, IS, GK_L.C_9 );
			SetCmdLvr ( bd_c, ENM_COMMAND._7,	0, 1, IS, GK_L.C_7 );

			//6_6
			SetCmdLvr ( bd_c, ENM_COMMAND._6_6, 0, 6, IS, GK_L.C_6 );
			SetCmdLvr ( bd_c, ENM_COMMAND._6_6, 1, 6, PS, GK_L.C_6 );

			//4_4
			SetCmdLvr ( bd_c, ENM_COMMAND._4_4, 0, 6, IS, GK_L.C_4 );
			SetCmdLvr ( bd_c, ENM_COMMAND._4_4, 1, 6, PS, GK_L.C_4 );

			//6H
			SetCmdLvr ( bd_c, ENM_COMMAND._6H, 0, 6, IS, GK_L.C_6 );
			SetCmdBtn ( bd_c, ENM_COMMAND._6H, 0, 6, PS, GK_B.B_3 );

			//236L
			SetCmdLvr ( bd_c, ENM_COMMAND.SP0, 0, 16, IS, GK_L.C_2 );
			SetCmdLvr ( bd_c, ENM_COMMAND.SP0, 1, 16, IS, GK_L.C_3 );
			SetCmdLvr ( bd_c, ENM_COMMAND.SP0, 2, 16, IS, GK_L.C_6 );
			SetCmdBtn ( bd_c, ENM_COMMAND.SP0, 3, 16, PS, 0 );
		}

		private void SetCmdBtn ( BD_CMD bd_c, ENM_COMMAND name_cmd, int index, int limit, GKC_ST st, GK_B btn )
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

		private void SetCmdLvr ( BD_CMD bd_c, ENM_COMMAND name_cmd, int index, int limit, GKC_ST st, GK_L gk_l )
		{
			Command cmd = bd_c.Get ( name_cmd.ToString () );
			cmd.ListGameKeyCommand.Add ( new GameKeyCommand () );
			cmd.ListGameKeyCommand [ index ].SetLvrSt ( st, gk_l );
			cmd.LimitTime = limit;
		}

		//----------------------------------------------------------------------------
		//テスト用ブランチの作成
		private void MakeBranch ( Chara chara )
		{
			//名前から作成
			foreach ( string str in Enum.GetNames ( typeof ( ENM_BRANCH ) ) )
			{
				EditChara.Inst.AddBranch ( str );
			}

			BD_BRC b = chara.BD_Branch;
			BranchCondition BC_CMD = BranchCondition.CMD;
			BranchCondition BC_GRD = BranchCondition.GRD;
			BranchCondition BC_THR_I = BranchCondition.THR_I;
			BranchCondition BC_THR_E = BranchCondition.THR_E;
			SetBranch ( b, ENM_BRANCH.L_立L,				BC_CMD,		ENM_COMMAND.L,		ENM_ACTION.Attack_5L );
			SetBranch ( b, ENM_BRANCH.Ma_立Ma,			BC_CMD,		ENM_COMMAND.Ma,		ENM_ACTION.Attack_5Ma );
			SetBranch ( b, ENM_BRANCH.Mb_立Mb,			BC_CMD,		ENM_COMMAND.Mb,		ENM_ACTION.Attack_5Mb );
			SetBranch ( b, ENM_BRANCH.H_立H,				BC_CMD,		ENM_COMMAND.H,		ENM_ACTION.Attack_5H );
			SetBranch ( b, ENM_BRANCH._6_FrontMove,		BC_CMD,		ENM_COMMAND._6,		ENM_ACTION.FrontMove );
			SetBranch ( b, ENM_BRANCH._4_BackMove,		BC_CMD,		ENM_COMMAND._4,		ENM_ACTION.BackMove );
			SetBranch ( b, ENM_BRANCH._6離_Stand,		BC_CMD,		ENM_COMMAND._6off,	ENM_ACTION.Stand );
			SetBranch ( b, ENM_BRANCH._4離_Stand,		BC_CMD,		ENM_COMMAND._4off,	ENM_ACTION.Stand );
			SetBranch ( b, ENM_BRANCH._6_6_FrontDash,	BC_CMD,		ENM_COMMAND._6_6,	ENM_ACTION.FrontDash );
			SetBranch ( b, ENM_BRANCH._4_4_BackDash,	BC_CMD,		ENM_COMMAND._4_4,	ENM_ACTION.BackDash );
			SetBranch ( b, ENM_BRANCH._8_VerticalJump,	BC_CMD,		ENM_COMMAND._8,		ENM_ACTION.VerticalJump );
			SetBranch ( b, ENM_BRANCH._9_FrontJump,		BC_CMD,		ENM_COMMAND._9,		ENM_ACTION.FrontJump );
			SetBranch ( b, ENM_BRANCH._7_BackJump,		BC_CMD,		ENM_COMMAND._7,		ENM_ACTION.BackJump );
			SetBranch ( b, ENM_BRANCH.着地_Stand,		BC_GRD,		ENM_COMMAND._N,		ENM_ACTION.Stand );
			SetBranch ( b, ENM_BRANCH.SP01,				BC_CMD,		ENM_COMMAND._6H,	ENM_ACTION.SP0_L );
			SetBranch ( b, ENM_BRANCH.SP0,				BC_CMD,		ENM_COMMAND.SP0,	ENM_ACTION.SP0_L );
			SetBranch ( b, ENM_BRANCH.投げ分岐_自,		BC_THR_I,	ENM_COMMAND._N,		ENM_ACTION.SP01_L );
			SetBranch ( b, ENM_BRANCH.投げ分岐_相,		BC_THR_E,	ENM_COMMAND._N,		ENM_ACTION.Thrown0 );
		}

		private void SetBranch ( BD_BRC bd_brc, ENM_BRANCH brc_name, BranchCondition bc, ENM_COMMAND cmd_name, ENM_ACTION act_name )
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
			SetEnmBrc ( rut0, ENM_BRANCH.L_立L );
			SetEnmBrc ( rut0, ENM_BRANCH.Ma_立Ma );
			SetEnmBrc ( rut0, ENM_BRANCH.Mb_立Mb );
			SetEnmBrc ( rut0, ENM_BRANCH.H_立H );
			chara.BD_Route.Add ( rut0 );


			Route rut1 = new Route ( ENM_RUT.地上移動.ToString(), "立ち状態から出る移動全般" );
			SetEnmBrc ( rut1, ENM_BRANCH._6_FrontMove );
			SetEnmBrc ( rut1, ENM_BRANCH._4_BackMove );
			SetEnmBrc ( rut1, ENM_BRANCH._8_VerticalJump );
			SetEnmBrc ( rut1, ENM_BRANCH._9_FrontJump );
			SetEnmBrc ( rut1, ENM_BRANCH._7_BackJump );
			chara.BD_Route.Add ( rut1 );

			Route rut2 = new Route ( ENM_RUT.前持続停止.ToString(), "前持続を離す" );
			SetEnmBrc ( rut2, ENM_BRANCH._6離_Stand );
			chara.BD_Route.Add ( rut2 );

			Route rut3 = new Route ( ENM_RUT.後持続停止.ToString(), "後持続を離す" );
			SetEnmBrc ( rut3, ENM_BRANCH._4離_Stand );
			chara.BD_Route.Add ( rut3 );

			Route rut4 = new Route ( ENM_RUT.ジャンプ.ToString(), "ジャンプ" );
			SetEnmBrc ( rut4, ENM_BRANCH._8_VerticalJump );
			SetEnmBrc ( rut4, ENM_BRANCH._9_FrontJump );
			SetEnmBrc ( rut4, ENM_BRANCH._7_BackJump );
			chara.BD_Route.Add ( rut4 );

			Route rut5 = new Route ( ENM_RUT.地上必殺技.ToString(), "地上必殺技" );
			SetEnmBrc ( rut5, ENM_BRANCH.SP0 );
			chara.BD_Route.Add ( rut5 );

			Route rut6 = new Route ( ENM_RUT.特殊.ToString(), "特殊" );
			SetEnmBrc ( rut6, ENM_BRANCH.SP01 );
			chara.BD_Route.Add ( rut6 );

			Route rut7 = new Route ( ENM_RUT.投げ分岐_自.ToString(), "投げ分岐_自" );
			SetEnmBrc ( rut7, ENM_BRANCH.投げ分岐_自 );
			chara.BD_Route.Add ( rut7 );

			Route rut8 = new Route ( ENM_RUT.投げ分岐_相.ToString(), "投げ分岐_相" );
			SetEnmBrc ( rut8, ENM_BRANCH.投げ分岐_相 );
			chara.BD_Route.Add ( rut8 );
		}

		private void SetEnmBrc ( Route rut, ENM_BRANCH enm_brc )
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
