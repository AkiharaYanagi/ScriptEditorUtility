﻿//
//※ #defineはソースの先頭のみ
//#define MAKE_CHARA_FROM_SOURCE


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
	using BL_Sequence = BindingList < Sequence >;
	using BL_Script = List < Script >;

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
		private void _MakeCharaData ( Chara chara )
		{
			//アクションリスト
			TestActionList ();

			//スクリプト
			Script script0 = TestScript ();

			//アクション内の値を指定
			SetAction ();

			//スクリプト再設定
			BindingList < Sequence > bl_s = chara.behavior.Bldct_sqc.GetBindingList ();
			bl_s[ 0 ].ListScript[ 0 ].Copy ( script0 );

			//コマンド
			TestCommand ( chara.ListCommand );

			//----------------------
			EditBehavior eb = EditChara.Inst.EditBehavior;

			//test Branch
			Action action_B = ( Action ) chara.behavior.Bldct_sqc.GetBindingList()[ 0 ];
			script0.ListBranch.Add ( new Branch ( 0, chara.ListCommand[ 0 ], 0, action_B ) );

			//Stand
			eb.SelectScript ( 0, 0 );
			
			List < Script > ls0 = eb.GetAction ().ListScript;
			foreach ( Script script in ls0 )
			{
				SetBranch ( chara, script, 0, 6 );
				SetBranch ( chara, script, 1, 7 );
				SetBranch ( chara, script, 2, 8 );
				SetBranch ( chara, script, 7, 3 );
				SetBranch ( chara, script, 8, 5 );
				SetBranch ( chara, script, 3, 1 );
				SetBranch ( chara, script, 4, 2 );
			}

			//FrontMove
			eb.SelectScript ( 1, 0 );
			List < Script > ls1 = eb.GetAction ().ListScript;
			foreach ( Script script in ls1 )
			{
				SetBranch ( chara, script, 0, 6 );
				SetBranch ( chara, script, 1, 7 );
				SetBranch ( chara, script, 2, 8 );
				SetBranch ( chara, script, 5, 0 );
			}

			//BackMove
			eb.SelectScript ( 2, 0 );
			List < Script > ls2 = eb.GetAction ().ListScript;
			foreach ( Script script in ls2 )
			{
				SetBranch ( chara, script, 0, 6 );
				SetBranch ( chara, script, 1, 7 );
				SetBranch ( chara, script, 2, 8 );
				SetBranch ( chara, script, 6, 0 );
			}

			//FrontDash
			eb.SelectScript ( 3, 0 );
			List < Script > ls3 = eb.GetAction ().ListScript;
			foreach ( Script script in ls3 )
			{
				SetBranch ( chara, script, 0, 6 );
				SetBranch ( chara, script, 1, 7 );
				SetBranch ( chara, script, 2, 8 );
				SetBranch ( chara, script, 5, 0 );
			}

			//FrontDashDuration
			eb.SelectScript ( 4, 0 );
			List < Script > ls4 = eb.GetAction ().ListScript;
			foreach ( Script script in ls4 )
			{
				SetBranch ( chara, script, 0, 6 );
				SetBranch ( chara, script, 1, 7 );
				SetBranch ( chara, script, 2, 8 );
				SetBranch ( chara, script, 5, 0 );
			}

			//EditCompend位置を元に戻す
			eb.SelectScript ( 0, 0 );


			//すべてのアクションにおけるスクリプトへの変更
			BL_Sequence blsqc = chara.behavior.Bldct_sqc.GetBindingList();
			foreach ( Sequence sqc in blsqc )
			{
				BL_Script blsc = sqc.ListScript;
				foreach ( Script sc in blsc )
				{
					sc.ImgName = "dummy.png";
				}
			}

			//--------------------------------------------
			//エディット（ガーニッシュ）のテスト
			//test Effect
			EditGarnish eg = EditChara.Inst.EditGarnish;
			eg.AddEffect ( new Effect ( "testEffect0" ) );
			eg.AddEffect ( new Effect ( "testEffect1" ) );

			if ( null == eg.SelectedSequence ) { return; }
			eg.SelectedSequence.ListScript[ 0 ].SetPos ( -120, -220 );

			Script efScript = new Script ();
//			efScript.RefPt.Set ( -120, -240 );

			eg.AddScript ( efScript );

			chara.garnish.Bldct_sqc.GetBindingList()[ 1 ].ListScript[ 0 ].ImgIndex = 1;
//			chara.garnish.ListSequence[ 1 ].ListScript[ 0 ].RefPt.Set ( -140, -240 );

			//スクリプトでエフェクト生成を指定する
			Script sc_ef = chara.behavior.Bldct_sqc.GetBindingList()[ 0 ].ListScript[ 0 ];
			EffectGenerate efGnrt = sc_ef.ListGenerateEf[ 0 ];

			efGnrt.Id = 0;
			efGnrt.Name = chara.garnish.Bldct_sqc.GetBindingList()[ efGnrt.Id ].Name;
			efGnrt.Pt = new Point ( -20, -50 );

			
			//--------------------------------------------
			//すべてのアクションにおけるスクリプトへの変更
			BL_Sequence blsqcEf = chara.garnish.Bldct_sqc.GetBindingList();
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
			_MakeImage ( chara.behavior.ListImage, "Image" );
			_MakeImage ( chara.garnish.ListImage, "EfImage" );
		}

		//対象ディレクトリからイメージリストの作成
		private void _MakeImage ( ImageList imageList, string imageDir )
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
				bmp.Save ( imageDir + "\\dummy.png", System.Drawing.Imaging.ImageFormat.Png );
				bmp.Dispose ();
				filepaths = Directory.GetFiles ( imageDir );
			}

			//すべてのファイルに対し
			foreach ( string path in filepaths )
			{
				//画像からImageData型を作成
				string name = Path.GetFileName ( path );
				ImageData imageData = new ImageData ( name, Image.FromFile ( path ) );
				
				//内部データに保存
				imageList.Add ( imageData.Name, imageData );
			}
		}

		//デフォルトアクション名
		private readonly string[] NAME_ACTION = new string[] 
		{
			"Stand", 
			"FrontMove", "BackMove", 
			"Crouch",
			"VerticalJump", "FrontJump", "BackJump", "Drop", 
			"VerticalJump(D)", "FrontJump(D)", "BackJump(D)",
			"FrontDash","FrontDashDuration", 
			"BackDash", "BackDashDuration", 
			"Guard_S", "Guard_C", "Guard_J", 
			"Attack_L", "Attack_M", "Attack_H", 
			"Attack_JL", "Attack_JM", "Attack_JH", 
			"Attack_GL", "Attack_GM", "Attack_GH", 
			"SP0_L", "SP0_M", "SP0_H", 
			"SP1_L", "SP1_M", "SP1_H", 
			"SP2_L", "SP2_M", "SP2_H", 
			"OD0_L", "OD0_M", "OD0_H", 
			"Poised", "Clang", "Avoid", "Dotty", 
			"Damaged_L", "Damaged_M", "Damaged_H", 
			"Down", "DownDuration", 
			"Win", "WinDuration", 
		};

		//デフォルトコマンド名
		private readonly string[] NAME_COMMAND = new string[] 
		{
			"Attack_L", "Attack_M", "Attack_H", 
			"FrontMove", "BackMove", 
			"FrontDuration", "BackDuration", "FrontDash", "BackDash", 
			"VerticalJump", "FrontJump", "BackJump", 
		};

		//ActionList
		private void TestActionList ()
		{
			//作業用
			EditBehavior eb = EditChara.Inst.EditBehavior;

			//名前からアクション配列の生成
			int SIZE_ACTION = NAME_ACTION.Length;
			Action[] action = new Action[ SIZE_ACTION ];
			for ( int i = 0; i < SIZE_ACTION; ++i )
			{
				action[ i ] = new Action ( NAME_ACTION[ i ] );
			}

			//キャラにEditCharaを用いてアクションを追加
			for ( int i = 0; i < SIZE_ACTION; ++i )
			{
				eb.AddAction ( action[ i ] );
			}
		}

		//アクション内の値を指定
		private void SetAction ()
		{
			EditBehavior eb = EditChara.Inst.EditBehavior;

			//Stand
			SetScript ( 0, 32 );

			//FrontMove
			SetScript ( 1, 16 );
			SetAllScriptVelX ( 1, 10 );

			//BackMove
			SetScript ( 2, 16 );
			SetAllScriptVelX ( 2, -10 );

			//FrontDash
			SetScript ( 3, 12 );
			SetAllScriptVelX ( 3, -20 );

			//FrontDashDuration
			SetScript ( 4, 1 );
			SetAllScriptVelX ( 3, 20 );

			//"BackDash", 
			SetScript ( 5, 1 );
			SetAllScriptVelX ( 3, -20 );

			//"Attack_L", 
			SetScript ( 6, 12 );
			Action actionAttack_L = ( Action ) eb.Compend.Bldct_sqc.GetBindingList()[ 6 ];
			Script scriptAttack_L_0 = actionAttack_L.ListScript[ 0 ];
			scriptAttack_L_0.ImgIndex = 10;

			//"Attack_M", 
			SetScript ( 7, 12 );

			//"Attack_H", 
			SetScript ( 8, 12 );

			//"Clang", 
			//"Avoid", 
			//"Dotty", 
			//"Damaged", 
			//"Down", 
			Action actionDown = ( Action ) eb.Compend.Bldct_sqc.GetBindingList()[ 14 ];
			actionDown.NextIndex = 15;
			//"DownDuration", 
			Action actionDownDuration = ( Action ) eb.Compend.Bldct_sqc.GetBindingList()[ 15 ];
			actionDownDuration.NextIndex = 15;
			//"Win", 
			Action actionWin = ( Action ) eb.Compend.Bldct_sqc.GetBindingList()[ 16 ];
			actionWin.NextIndex = 17;
			//"WinDuration", 
			Action actionWinDuration = ( Action ) eb.Compend.Bldct_sqc.GetBindingList()[ 17 ];
			actionWinDuration.NextIndex = 17;

		}

		//script
		private Script TestScript ()
		{
			//手動で作成し、値を設定
			Script script0 = new Script ();
			script0.ListCRect.Add ( new Rectangle ( -90, -300, 100, 150 ) );
			script0.ListHRect.Add ( new Rectangle ( -100, -200, 200, 350 ) );
			script0.ListARect.Add ( new Rectangle ( 60, -150, 80, 20 ) );
			script0.ListORect.Add ( new Rectangle ( 0, -280, 40, 60 ) );
			script0.ListGenerateEf.Add ( new EffectGenerate () );

			//コピー
			Script script1 = new Script ( script0 );
			Script script2 = new Script ();
			script2.Copy ( script1 );
			Debug.Assert ( script1.Equals ( script2 ) );

			return script0;
		}

		//内部　スクリプト設定
		//引数：アクションID, スクリプト個数
		private void SetScript ( int idAction, int numScript )
		{
			EditBehavior eb = EditChara.Inst.EditBehavior;
			eb.SelectSequence ( idAction );
			eb.SelectedSequence.ListScript.Clear ();		//最初の一つを削除
			for ( int i = 0; i < numScript; ++i )
			{
				Script script = new Script ();
				script.SetPos ( -87, -224 );
				eb.AddScript ( script );
			}
		}

		//内部　スクリプト設定
		//引数：アクションID, スクリプト個数
		private void SetAllScriptVelX ( int idAction, int velX )
		{
			EditBehavior eb = EditChara.Inst.EditBehavior;
			foreach ( Script script in eb.SelectedSequence.ListScript )
			{
				script.SetVelX ( velX );
			}
		}

		private void SetBranch ( Chara ch, Script sc, int indexCommand, int indexAction )
		{
			BindingList < Branch > BL_Brc = sc.ListBranch;
			BindingList < Command > BL_Cmd = ch.ListCommand;
			BindingList < Sequence > BL_Sqc = ch.behavior.Bldct_sqc.GetBindingList();
			int ic = indexCommand;
			int ia = indexAction;

			BL_Brc.Add ( new Branch ( ic, BL_Cmd[ ic ], ia, (Action)BL_Sqc[ ia ] ) );
		}


		//Test Command
		private void TestCommand ( BindingList < Command > bl_c )
		{
			//コマンド名から作成
			int SIZE_COMMAND = NAME_COMMAND.Length;
			for ( int i = 0; i < SIZE_COMMAND; ++i )
			{
				EditChara.Inst.AddCommand ();
				bl_c[ i ].Name = NAME_COMMAND[ i ];
				bl_c[ i ].ListGameKeyCommand.Add ( new GameKeyCommand () );
			}
			SetCommand ( bl_c, 0, 0 );		// L : Btn0
			SetCommand ( bl_c, 1, 1 );		// M : Btn1
			SetCommand ( bl_c, 2, 2 );		// H : Btn2
		}

		private void SetCommand ( BindingList < Command > bl_c, int indexCmd, int btn )
		{
			bl_c[ indexCmd ].ListGameKeyCommand.Add ( new GameKeyCommand () );
			bl_c[ indexCmd ].ListGameKeyCommand[ 1 ].Btn[ btn ] = GKC_ST.KEY_PUSH;
			bl_c[ indexCmd ].LimitTime = 2;
		}

	}
}