﻿using System;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using System.Collections.Generic;

namespace ScriptEditor
{
	using BD_Seq = BindingDictionary < Sequence >;
	using BL_Script = List < Script >;

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
				act.ListScript.Clear ();
				for ( int i = 0; i < 5; ++ i )
				{
					Script s = new Script ()
					{	
						Group = 1,
						Pos = new Point ( -250, -450 ),						
					};
					act.AddScript ( s ); 
				}
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

			//----------------------------------------------------------------
			//Stand
			MakeAction_Stand ( bd_act );

			//FrontMove
			MakeAction_Move ( bd_act );

			//BackMove
			MakeAction_BackMove ( bd_act );

			//Jump
			MakeAction_Jump ( eb, ENM_ACTION.VerticalJump, "AirFrontDash_00.png", 0 );
			MakeAction_Jump ( eb, ENM_ACTION.FrontJump, "AirFrontDash_00.png", 20 );
			MakeAction_Jump ( eb, ENM_ACTION.BackJump, "AirFrontDash_00.png", -20 );

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
				s.SetAccY ( 2 );
				s.ListCRect.Clear ();
			} );

			//Attack
			MakeAction_Attack ( eb, ENM_ACTION.Attack_5L , "Part_5L_00.png" );
			MakeAction_Attack ( eb, ENM_ACTION.Attack_5Ma, "Part_5Ma_00.png" );
			MakeAction_Attack ( eb, ENM_ACTION.Attack_5Mb, "Part_5Mb_00.png" );
			MakeAction_Attack ( eb, ENM_ACTION.Attack_5H , "Part_5H_00.png" );

			MakeAction_Attack ( eb, ENM_ACTION.Attack_6L , "Part_6L_00.png" );
			MakeAction_Attack ( eb, ENM_ACTION.Attack_6Ma, "Part_6Ma_00.png" );
			MakeAction_Attack ( eb, ENM_ACTION.Attack_6Mb, "Part_6Mb_00.png" );
			MakeAction_Attack ( eb, ENM_ACTION.Attack_6H , "Part_6H_00.png" );

			MakeAction_Attack ( eb, ENM_ACTION.Attack_4L , "Part_4L_00.png" );
			MakeAction_Attack ( eb, ENM_ACTION.Attack_4Ma, "Part_4Ma_00.png" );
			MakeAction_Attack ( eb, ENM_ACTION.Attack_4Mb, "Part_4Mb_00.png" );
			MakeAction_Attack ( eb, ENM_ACTION.Attack_4H , "Part_4H_00.png" );

			MakeAction_Attack ( eb, ENM_ACTION.Attack_2L , "Part_2L_00.png" );
			MakeAction_Attack ( eb, ENM_ACTION.Attack_2Ma, "Part_2Ma_00.png" );
			MakeAction_Attack ( eb, ENM_ACTION.Attack_2Mb, "Part_2Mb_00.png" );
			MakeAction_Attack ( eb, ENM_ACTION.Attack_2H , "Part_2H_00.png" );

			MakeAction_Attack ( eb, ENM_ACTION.Attack_8L , "Part_8L_00.png" );
			MakeAction_Attack ( eb, ENM_ACTION.Attack_8Ma, "Part_8Ma_00.png" );
			MakeAction_Attack ( eb, ENM_ACTION.Attack_8Mb, "Part_8Mb_00.png" );
			MakeAction_Attack ( eb, ENM_ACTION.Attack_8H , "Part_8H_00.png" );

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
				s.BL_RutName.Add ( new TName ( ENM_RUT.被ダメ_H ) );
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
				s.CalcState = CLC_ST.CLC_SUBSTITUDE;
				s.ImgName = "StarMassStrike_00.png";
			} );
			BL_Script bl_SP0_L = bd_act.Get ( (int)ENM_ACTION.SP0_L ).ListScript;
			for ( int i = 0; i < 4; ++ i )
			{
				Script s = bl_SP0_L [ i ];
				s.CalcState = CLC_ST.CLC_SUBSTITUDE;
				s.ImgName = "StarMassStrike_00.png";
				s.ListARect.Clear ();
			}
			for ( int i = 4; i < 8; ++ i )
			{
				Script s = bl_SP0_L [ i ];
				s.CalcState = CLC_ST.CLC_SUBSTITUDE;
				s.ImgName = "StarMassStrike_00.png";
				s.ListARect.Clear ();
				s.ListARect.Add ( new Rectangle ( 50, -300, 200, 50 ) );
				s.BL_RutName.Add ( new TName ( ENM_RUT.投げ分岐_自 ) );
				s.BL_RutName.Add ( new TName ( ENM_RUT.投げ分岐_相 ) );
			}
			for ( int i = 8; i < 12; ++ i )
			{
				Script s = bl_SP0_L [ i ];
				s.CalcState = CLC_ST.CLC_SUBSTITUDE;
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
				s.CalcState = CLC_ST.CLC_SUBSTITUDE;
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



			//Damaged
			MakeAction_Damaged ( eb, ENM_ACTION.Damaged_L, "BackDash_00.png" );
			MakeAction_Damaged ( eb, ENM_ACTION.Damaged_H, "MotionDrive_02.png" );


			//OD0
			const int i_OD0 = (int)ENM_ACTION.OD0_L;
			MakeScript ( i_OD0, 30 );
			eb.SelectSequence ( i_OD0 );
			ea.EditScriptInSequence ( s =>
			{
				s.Group = 1; 
				s.ImgName = "Part_6H_02.png";
				s.CalcState = CLC_ST.CLC_SUBSTITUDE;
				s.Vel = new Point ( 10, 0 );
			} );
			BL_Script bl_OD0_L = bd_act.Get ( i_OD0 ).ListScript;
			Script scpOD0 = bl_OD0_L [ 0 ];
			scpOD0.Vel = new Point ( 0, 0 );
			scpOD0.BlackOut = 30;
			scpOD0.Stop = 10;
			scpOD0.ImgName = "StarMassStrike_01.png";

			EffectGenerate efgn = new EffectGenerate ()
			{
				Name = "EfGn",
				EfName = "testEffect0",
				Pt = new Point ( 180, -250 ),
				Gnrt = true,
			};
			scpOD0.BD_EfGnrt.Add ( efgn );


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



	}
}
