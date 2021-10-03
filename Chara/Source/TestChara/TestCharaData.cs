//※ #defineはソースの先頭のみ
//#define MAKE_CHARA_FROM_SOURCE
//
using System;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using System.ComponentModel;
using System.Collections.Generic;

namespace ScriptEditor
{
	//==================================================================================
	//	現在のキャラからテスト用のデータを作成する
	//		データファイルの復旧や、バージョンが異なるときにソースから作成できる状態にしておく
	//==================================================================================
	using GKC_ST = GameKeyCommand.GameKeyCommandState;
	using GK_L = GameKeyCommand.LeverCommand;

	using BD_Seq = BindingDictionary < Sequence >;
	using BL_Sequence = BindingList < Sequence >;
	using BL_Script = List < Script >;
	using BD_CMD = BindingDictionary < Command >;
	using BD_BRC = BindingDictionary < Branch >;

	//-----------------------
	//テストデータの作成
	//-----------------------
	public partial class TestCharaData
	{
		//コンストラクタ
		public TestCharaData ()
		{
		}

		//作成
		public void Make ( Chara ch )
		{
			try
			{
				_Make ( ch );
			}
			catch ( System.OutOfMemoryException ex )
			{
				Debug.Write ( "エラー：テストデータの作成\n" + ex.Message );
				return;
			}
		}

		//作成(内部)
		private void _Make ( Chara ch )
		{
			//EditCharaに設定
			EditChara.Inst.SetCharaDara ( ch );

			//イメージリスト
			MakeImage ( ch );

#if MAKE_CHARA_FROM_SOURCE
			//ソースコードからキャラデータを反映
			SetCharaData ( ch );
#else
			//手動でキャラデータを作成
			_MakeCharaData ( ch );
#endif	//MAKE_CHARA_FROM_SOURCE
		}

		//手動でキャラデータを作成
		private void _MakeCharaData ( Chara ch )
		{
			//アクションリスト
			MakeActionList ();

			//アクション内の値を指定
			SetAction ( ch );

			//コマンド
			MakeCommand ( ch.BD_Command );

			//ブランチ
			MakeBranch ( ch );

			//ルート
			MakeRoute ( ch );

			//すべてのアクションにおけるスクリプトへの変更
#if false
			void f_editScp ( Script scp ) { scp.ImgName = "000_dummy.png"; }
			EditChara.Inst.EditBehavior.EditAllScript ( chara.behavior, f_editScp );
#endif
			foreach ( Sequence sqc in ch.behavior.BD_Sequence.GetBindingList () )
			{
				foreach ( Script scp in sqc.ListScript )
				{
					ImageData imgd = ch.behavior.BD_Image.Get ( scp.ImgName );

					if ( imgd is null )
					{
						scp.ImgName = ch.behavior.BD_Image.Get ( 0 ).Name;
					}
					else
					{
						Debug.Assert ( ! ( imgd is null ) );
					}

					foreach ( TName tn in scp.BL_RutName )
					{
						Route rut = ch.BD_Route.Get ( tn.Name );
						Debug.Assert ( ! ( rut is null ) );
					}
				}
			}

			//--------------------------------------------
			//エディット（ガーニッシュ）のテスト
			MakeGarnish ( ch );

			//--------------------------------------------
			//すべてのエフェクトにおけるスクリプトへの変更
			BL_Sequence blsqcEf = ch.garnish.BD_Sequence.GetBindingList();
			foreach ( Sequence sqc in blsqcEf )
			{
				BL_Script blsc = sqc.ListScript;
				foreach ( Script sc in blsc )
				{
					sc.ImgName = "dummy.png";
				}
			}
		}


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
			//作業用
			EditBehavior eb = EditChara.Inst.EditBehavior;

			//名前からアクション配列の生成
			int SIZE_ACTION = NAME_ACTION.Length;
			Action[] action = new Action[ SIZE_ACTION ];
			for ( int i = 0; i < SIZE_ACTION; ++i )
			{
				action [ i ] = new Action ( NAME_ACTION [ i ] )
				{
					Category = _ACTION_CATEGORY [ i ],
					NextActionName = NAME_ACTION [ 0 ],
				};
			}

			//キャラにEditCharaを用いてアクションを追加
			for ( int i = 0; i < SIZE_ACTION; ++i )
			{
				eb.AddAction ( action[ i ] );
			}
		}

		//アクション内の値を指定
		private void SetAction ( Chara chara )
		{
			//編集
			EditBehavior eb = EditChara.Inst.EditBehavior;
			EditSequence ea = eb.EditSequence;

			//Stand
			MakeScript ( (int)ENM_ACTION.Stand, 288 );

			//スクリプト
			Script script_test = SetScript ();

			//スクリプトコピー
			BD_Seq bd_seq = chara.behavior.BD_Sequence;
			Script scp_cpy = bd_seq.Get ( (int)ENM_ACTION.Stand ).ListScript [ 0 ];
			scp_cpy.Copy ( script_test );

			BD_Seq bd_act = chara.behavior.BD_Sequence;
			BL_Script bl_scp = bd_act.Get ( (int)ENM_ACTION.Stand ).ListScript;
			for ( int i = 0; i < 128; ++ i )
			{
				bl_scp [ i ].Group = 1;
				bl_scp [ i ].ImgName = "Stand_00.png";
			}
			for ( int i = 128; i < 144; ++ i )
			{
				bl_scp [ i ].Group = 2;
				bl_scp [ i ].ImgName = "Stand_01.png";
			}
			for ( int i = 144; i < 272; ++ i )
			{
				bl_scp [ i ].Group = 3;
				bl_scp [ i ].ImgName = "Stand_02.png";
			}
			for ( int i = 272; i < 288; ++ i )
			{
				bl_scp [ i ].Group = 4;
				bl_scp [ i ].ImgName = "Stand_01.png";
			}
			for ( int i = 0; i < 288; ++ i )
			{
				bl_scp [ i ].Pos = new Point ( -250, -450 );
				bl_scp [ i ].BL_RutName.Add ( new TName ( ENM_RUT.地上移動.ToString() ) );
				bl_scp [ i ].BL_RutName.Add ( new TName ( ENM_RUT.地上通常技.ToString() ) );
			}

			//FrontMove
			const int i_FrontMove = (int)ENM_ACTION.FrontMove;
			MakeScript ( i_FrontMove, 16 );
			eb.SelectSequence ( i_FrontMove );
			eb.GetAction ().NextActionName = ENM_ACTION.FrontMove.ToString ();
			ea.EditScriptInSequence ( s =>
			{
				s.Group = 1; 
				s.ImgName = "FrontMove_00.png";
				s.CalcState = CLC_ST.CLC_ADD;
				s.SetVelX ( 5 );
				s.BL_RutName.Add ( new TName ( ENM_RUT.前持続停止 ) );
				s.BL_RutName.Add ( new TName ( ENM_RUT.ジャンプ ) );
				s.BL_RutName.Add ( new TName ( ENM_RUT.地上通常技 ) );
			} );

			//BackMove
			const int i_BackMove = (int)ENM_ACTION.BackMove;
			MakeScript ( i_BackMove, 16 );
			eb.SelectSequence ( i_BackMove );
			eb.GetAction ().NextActionName = ENM_ACTION.BackMove.ToString ();
			ea.EditScriptInSequence ( s =>
			{
				s.Group = 1; 
				s.ImgName = "BacktMove_00.png";
				s.CalcState = CLC_ST.CLC_ADD;
				s.SetVelX ( -5 );
				s.BL_RutName.Add ( new TName ( ENM_RUT.後持続停止 ) );
				s.BL_RutName.Add ( new TName ( ENM_RUT.ジャンプ ) );
				s.BL_RutName.Add ( new TName ( ENM_RUT.地上通常技 ) );
			} );


			//VerticalJump
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

			//"Attack_5L", 
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
			MakeScript ( i_Attack_5H, 12 );
			eb.SelectSequence ( i_Attack_5H );
			ea.EditScriptInSequence ( s =>
			{
				s.Group = 1; 
				s.ImgName = "Part_5H_00.png";
			} );

#if false
			//"Attack_M", 
			MakeScript ( 7, 12 );

			//"Attack_H", 
			MakeScript ( 8, 12 );
#endif

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
#if false
		//内部　スクリプト設定
		//引数：アクションID, スクリプト個数
		private void SetAllScriptVelX ( int idAction, int velX )
		{
			EditBehavior eb = EditChara.Inst.EditBehavior;
			foreach ( Script script in eb.SelectedSequence.ListScript )
			{
				script.CalcState = CLC_ST.CLC_ADD;
				script.SetVelX ( velX );
			}
		}
#endif

		//Command
		private void MakeCommand ( BD_CMD bd_c )
		{
			//コマンド名から作成
#if false
			int SIZE_COMMAND = NAME_COMMAND.Length;
			for ( int i = 0; i < SIZE_COMMAND; ++i )
			{
				EditChara.Inst.AddCommand ( NAME_COMMAND[ i ] );
				bd_c.Get( i ).ListGameKeyCommand.Add ( new GameKeyCommand () );
			}
#endif
			foreach ( string name in Enum.GetNames ( typeof ( ENM_COMMAND ) ) )
			{
				EditChara.Inst.AddCommand ( name );
				bd_c.Get ( name ).ListGameKeyCommand.Add ( new GameKeyCommand () );
			}

			SetCommandBtn ( bd_c, ENM_COMMAND.L, 0 );		// L  : Btn0
			SetCommandBtn ( bd_c, ENM_COMMAND.Ma, 1 );		// Ma : Btn1
			SetCommandBtn ( bd_c, ENM_COMMAND.Mb, 2 );		// Mb : Btn2
			SetCommandBtn ( bd_c, ENM_COMMAND.H, 3 );		// H  : Btn3

			SetCommandLvr ( bd_c, ENM_COMMAND._6, GKC_ST.KEY_IS, GK_L.C_6 );
			SetCommandLvr ( bd_c, ENM_COMMAND._4, GKC_ST.KEY_IS, GK_L.C_4 );
			SetCommandLvr ( bd_c, ENM_COMMAND._6off, GKC_ST.KEY_NIS, GK_L.C_6 );
			SetCommandLvr ( bd_c, ENM_COMMAND._4off, GKC_ST.KEY_NIS, GK_L.C_4 );
			SetCommandLvr ( bd_c, ENM_COMMAND._8, GKC_ST.KEY_IS, GK_L.C_8 );
			SetCommandLvr ( bd_c, ENM_COMMAND._9, GKC_ST.KEY_IS, GK_L.C_9 );
			SetCommandLvr ( bd_c, ENM_COMMAND._7, GKC_ST.KEY_IS, GK_L.C_7 );

			SetCommandLvr ( bd_c, ENM_COMMAND._6_6, GKC_ST.KEY_IS, GK_L.C_6 );
			SetCommandLvr_i ( bd_c, ENM_COMMAND._6_6, 1, GKC_ST.KEY_PUSH, GK_L.C_6 );
			bd_c.Get( ENM_COMMAND._6_6.ToString() ).LimitTime = 6;

			SetCommandLvr ( bd_c, ENM_COMMAND._4_4, GKC_ST.KEY_IS, GK_L.C_4 );
			SetCommandLvr_i ( bd_c, ENM_COMMAND._4_4, 1, GKC_ST.KEY_PUSH, GK_L.C_4 );
			bd_c.Get( ENM_COMMAND._4_4.ToString() ).LimitTime = 6;
		}

		private void SetCommandBtn ( BD_CMD bd_c, ENM_COMMAND name_cmd, int btn )
		{
			bd_c.Get ( name_cmd.ToString () ).ListGameKeyCommand[ 0 ].Btn[ btn ] = GKC_ST.KEY_PUSH;
			bd_c.Get ( name_cmd.ToString () ).LimitTime = 1;
		}

		private void SetCommandLvr ( BD_CMD bd_c, ENM_COMMAND name_cmd, GKC_ST st, GK_L gk_l )
		{
			bd_c.Get ( name_cmd.ToString() ).ListGameKeyCommand [ 0 ].SetLvrSt ( st, gk_l );
			bd_c.Get ( name_cmd.ToString() ).LimitTime = 1;
		}

		private void SetCommandLvr_i ( BD_CMD bd_c, ENM_COMMAND name_cmd, int index, GKC_ST st, GK_L gk_l )
		{
			bd_c.Get ( name_cmd.ToString() ).ListGameKeyCommand.Add ( new GameKeyCommand () );
			bd_c.Get ( name_cmd.ToString() ).ListGameKeyCommand [ index ].SetLvrSt ( st, gk_l );
			bd_c.Get ( name_cmd.ToString() ).LimitTime = index;
		}


		//テスト用ブランチの作成
		private void MakeBranch ( Chara chara )
		{
			foreach ( string str in Enum.GetNames ( typeof ( ENM_BRANCH ) ) )
			{
				EditChara.Inst.AddBranch ( str );
			}

			BD_BRC b = chara.BD_Branch;
			SetBranch ( b, ENM_BRANCH.L_立L,				ENM_COMMAND.L,		"Attack_5L" );
			SetBranch ( b, ENM_BRANCH.Ma_立Ma,			ENM_COMMAND.Ma,		"Attack_5Ma" );
			SetBranch ( b, ENM_BRANCH.Mb_立Mb,			ENM_COMMAND.Mb,		"Attack_5Mb" );
			SetBranch ( b, ENM_BRANCH.H_立H,				ENM_COMMAND.H,		"Attack_5H" );
			SetBranch ( b, ENM_BRANCH._6_FrontMove,		ENM_COMMAND._6,		"FrontMove" );
			SetBranch ( b, ENM_BRANCH._4_BackMove,		ENM_COMMAND._4,		"BackMove" );
			SetBranch ( b, ENM_BRANCH._6離_Stand,		ENM_COMMAND._6off,	"Stand" );
			SetBranch ( b, ENM_BRANCH._4離_Stand,		ENM_COMMAND._4off,	"Stand" );
			SetBranch ( b, ENM_BRANCH._6_6_FrontDash,	ENM_COMMAND._6_6,	"FrontDash" );
			SetBranch ( b, ENM_BRANCH._4_4_BackDash,	ENM_COMMAND._4_4,	"BackDash" );
			SetBranch ( b, ENM_BRANCH._8_VerticalJump,	ENM_COMMAND._8,		"VerticalJump" );
			SetBranch ( b, ENM_BRANCH._9_FrontJump,		ENM_COMMAND._9,		"FrontJump" );
			SetBranch ( b, ENM_BRANCH._7_BackJump,		ENM_COMMAND._7,		"BackJump" );
		}

		private void SetBranch ( BD_BRC bd_brc, ENM_BRANCH brc_name, ENM_COMMAND cmd_name, string act_name )
		{
			Branch br = bd_brc.Get ( brc_name.ToString () );
			br.NameCommand = cmd_name.ToString ();
			br.NameAction = act_name;
		}

		//ルート
		private void MakeRoute ( Chara chara )
		{
			Route rut0 = new Route ( ENM_RUT.地上通常技.ToString(), "立ち状態で移行する技全般" );
			rut0.BL_BranchName.Add ( new TName ( ENM_BRANCH.L_立L.ToString() ) );
			rut0.BL_BranchName.Add ( new TName ( ENM_BRANCH.Ma_立Ma.ToString() ) );
			rut0.BL_BranchName.Add ( new TName ( ENM_BRANCH.Mb_立Mb.ToString() ) );
			rut0.BL_BranchName.Add ( new TName ( ENM_BRANCH.H_立H.ToString()) );
			chara.BD_Route.Add ( rut0 );

			Route rut1 = new Route ( ENM_RUT.地上移動.ToString(), "立ち状態から出る移動全般" );
			rut1.BL_BranchName.Add ( new TName ( ENM_BRANCH._6_FrontMove.ToString() ) );
			rut1.BL_BranchName.Add ( new TName ( ENM_BRANCH._4_BackMove.ToString() ) );
			rut1.BL_BranchName.Add ( new TName ( ENM_BRANCH._8_VerticalJump.ToString() ) );
			rut1.BL_BranchName.Add ( new TName ( ENM_BRANCH._9_FrontJump.ToString() ) );
			rut1.BL_BranchName.Add ( new TName ( ENM_BRANCH._7_BackJump.ToString() ) );
			chara.BD_Route.Add ( rut1 );

			Route rut2 = new Route ( ENM_RUT.前持続停止.ToString(), "前持続を離す" );
			rut2.BL_BranchName.Add ( new TName ( ENM_BRANCH._6離_Stand.ToString() ) );
			chara.BD_Route.Add ( rut2 );

			Route rut3 = new Route ( ENM_RUT.後持続停止.ToString(), "後持続を離す" );
			rut3.BL_BranchName.Add ( new TName ( ENM_BRANCH._4離_Stand.ToString() ) );
			chara.BD_Route.Add ( rut3 );

			Route rut4 = new Route ( ENM_RUT.ジャンプ.ToString(), "ジャンプ" );
			rut4.BL_BranchName.Add ( new TName ( ENM_BRANCH._8_VerticalJump.ToString() ) );
			rut4.BL_BranchName.Add ( new TName ( ENM_BRANCH._9_FrontJump.ToString() ) );
			rut4.BL_BranchName.Add ( new TName ( ENM_BRANCH._7_BackJump.ToString() ) );
			chara.BD_Route.Add ( rut4 );
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