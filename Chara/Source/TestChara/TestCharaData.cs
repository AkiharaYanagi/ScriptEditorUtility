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

	delegate void FuncEditScript ( Script scp );

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
			MakeActionList ();

			//スクリプト
			Script script0 = MakeScript ();

			//アクション内の値を指定
			SetAction ( chara );

			//===================================================================
			//@info 手動操作はBindingListではなく、BindingDictionaryを用いる
			//===================================================================
			BD_Seq bd_seq = chara.behavior.BD_Sequence;

			//スクリプトコピー
			Script scp_cpy = bd_seq.Get ( 0 ).ListScript [ 0 ];
	//		scp_cpy.Copy ( script0 );

			//コマンド
			MakeCommand ( chara.BD_Command );

			//ブランチ
			MakeBranch ( chara );

			//ルート
			MakeRoute ( chara );

			//すべてのアクションにおけるスクリプトへの変更
#if false
			void f_editScp ( Script scp ) { scp.ImgName = "dummy.png"; }
			EditChara.Inst.EditBehavior.EditAllScript ( chara.behavior, f_editScp );
#endif

			//--------------------------------------------
			//エディット（ガーニッシュ）のテスト
			MakeGarnish ( chara );

			//--------------------------------------------
			//すべてのエフェクトにおけるスクリプトへの変更
			BL_Sequence blsqcEf = chara.garnish.BD_Sequence.GetBindingList();
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
				imageList.Add ( imageData );
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
			EditBehavior eb = EditChara.Inst.EditBehavior;

			//Stand
			SetScript ( 0, 288 );

			BD_Seq bd_act = chara.behavior.BD_Sequence;
			BL_Script bl_scp = bd_act.Get ( 0 ).ListScript;
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
				bl_scp [ i ].BL_RutName.Add ( new TName ( NAME_ROUTE [ 0 ] ) );
				bl_scp [ i ].BL_RutName.Add ( new TName ( NAME_ROUTE [ 1 ] ) );
			}

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
			Action actionAttack_L = ( Action ) eb.Compend.BD_Sequence.Get ( 6 );
			Script scriptAttack_L_0 = actionAttack_L.ListScript[ 0 ];
//			scriptAttack_L_0.ImgIndex = 10;

			//"Attack_M", 
			SetScript ( 7, 12 );

			//"Attack_H", 
			SetScript ( 8, 12 );

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

		}

		//script
		private Script MakeScript ()
		{
			//手動で作成し、値を設定
			Script script0 = new Script ();
			script0.ListCRect.Add ( new Rectangle ( -90, -300, 100, 150 ) );
			script0.ListHRect.Add ( new Rectangle ( -100, -200, 200, 350 ) );
			script0.ListARect.Add ( new Rectangle ( 60, -150, 80, 20 ) );
			script0.ListORect.Add ( new Rectangle ( 0, -280, 40, 60 ) );
//			script0.BD_EfGnrt.Add ( new EffectGenerate () );

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

		//Command
		private void MakeCommand ( BD_CMD bd_c )
		{
			//コマンド名から作成
			int SIZE_COMMAND = NAME_COMMAND.Length;
			for ( int i = 0; i < SIZE_COMMAND; ++i )
			{
				EditChara.Inst.AddCommand ( NAME_COMMAND[ i ] );
				bd_c.Get( i ).ListGameKeyCommand.Add ( new GameKeyCommand () );
			}
			SetCommand ( bd_c, "L", 0 );		// L  : Btn0
			SetCommand ( bd_c, "Ma", 1 );		// Ma : Btn1
			SetCommand ( bd_c, "Mb", 2 );		// Mb : Btn2
			SetCommand ( bd_c, "H", 3 );        // H  : Btn3

			SetCommandLvr ( bd_c, "6", GKC_ST.KEY_IS, GK_L.C_6 );
			SetCommandLvr ( bd_c, "4", GKC_ST.KEY_IS, GK_L.C_4 );
			SetCommandLvr ( bd_c, "6離", GKC_ST.KEY_NIS, GK_L.C_6 );
			SetCommandLvr ( bd_c, "4離", GKC_ST.KEY_NIS, GK_L.C_4 );
			SetCommandLvr ( bd_c, "8", GKC_ST.KEY_IS, GK_L.C_8 );
			SetCommandLvr ( bd_c, "9", GKC_ST.KEY_IS, GK_L.C_9 );
			SetCommandLvr ( bd_c, "7", GKC_ST.KEY_IS, GK_L.C_7 );

			SetCommandLvr ( bd_c, "6.6", GKC_ST.KEY_IS, GK_L.C_6 );
			SetCommandLvr_i ( bd_c, "6.6", 1, GKC_ST.KEY_PUSH, GK_L.C_6 );
			bd_c.Get(  Array.IndexOf ( NAME_COMMAND, "6.6" ) ).LimitTime = 6;

			SetCommandLvr ( bd_c, "4.4", GKC_ST.KEY_IS, GK_L.C_4 );
			SetCommandLvr_i ( bd_c, "4.4", 1, GKC_ST.KEY_PUSH, GK_L.C_4 );
			bd_c.Get(  Array.IndexOf ( NAME_COMMAND, "4.4" ) ).LimitTime = 6;
		}

		private void SetCommand ( BD_CMD bd_c, string name_cmd, int btn )
		{
			int index = Array.IndexOf ( NAME_COMMAND, name_cmd );
			bd_c.Get ( index ).ListGameKeyCommand[ 0 ].Btn[ btn ] = GKC_ST.KEY_PUSH;
			bd_c.Get ( index ).LimitTime = 1;
		}

		private void SetCommandLvr ( BD_CMD bd_c, string name_cmd, GKC_ST st, GK_L gk_l )
		{
			int index = Array.IndexOf ( NAME_COMMAND, name_cmd );
			bd_c.Get ( index ).ListGameKeyCommand [ 0 ].SetLvrSt ( st, gk_l );
			bd_c.Get ( index ).LimitTime = 1;
		}

		private void SetCommandLvr_i ( BD_CMD bd_c, string name_cmd, int index, GKC_ST st, GK_L gk_l )
		{
			int i = Array.IndexOf ( NAME_COMMAND, name_cmd );
			bd_c.Get ( i ).ListGameKeyCommand.Add ( new GameKeyCommand () );
			bd_c.Get ( i ).ListGameKeyCommand [ index ].SetLvrSt ( st, gk_l );
			bd_c.Get ( i ).LimitTime = index;
		}


		//テスト用ブランチの作成
		private void MakeBranch ( Chara chara )
		{
			foreach ( string str in NAME_BRANCH )
			{
				EditChara.Inst.AddBranch ( str );
			}
			SetBranch ( chara.BD_Branch, "L → 立L", "L", "Attack_5L" );
			SetBranch ( chara.BD_Branch, "Ma → 立Ma", "Ma", "Attack_5Ma" );
			SetBranch ( chara.BD_Branch, "Mb → 立Mb", "Mb", "Attack_5Mb" );
			SetBranch ( chara.BD_Branch, "H → 立H", "H", "Attack_5H" );
			SetBranch ( chara.BD_Branch, "6 → FrontMove", "6", "FrontMove" );
			SetBranch ( chara.BD_Branch, "4 → BackMove", "4", "BackMove" );
			SetBranch ( chara.BD_Branch, "6離 → FrontDuration", "6離", "Stand" );
			SetBranch ( chara.BD_Branch, "4離 → BackDuration", "4離", "Stand" );
			SetBranch ( chara.BD_Branch, "6.6 → FrontDash", "6.6", "FrontDash" );
			SetBranch ( chara.BD_Branch, "4.4 → BackDash", "4.4", "BackDash" );
			SetBranch ( chara.BD_Branch, "8 → VerticalJump", "8", "VerticalJump" );
			SetBranch ( chara.BD_Branch, "9 → FrontJump", "9", "FrontJump" );
			SetBranch ( chara.BD_Branch, "7 → BackJump", "7", "BackJump" );
		}

		private void SetBranch ( BD_BRC bd_brc, string brc_name, string cmd_name, string act_name )
		{
			Branch br = bd_brc.Get ( brc_name );
			br.NameCommand = cmd_name;
			br.NameAction = act_name;
		}

		//ルート
		private void MakeRoute ( Chara chara )
		{
			Route rut0 = new Route ( "地上通常技", "立ち状態で移行する技全般" );
			rut0.BL_BranchName.Add ( new TName ( "L → 立L" ) );
			rut0.BL_BranchName.Add ( new TName ( "Ma → 立Ma" ) );
			rut0.BL_BranchName.Add ( new TName ( "Mb → 立Mb" ) );
			rut0.BL_BranchName.Add ( new TName ( "H → 立H") );
			chara.BD_Route.Add ( rut0 );

			Route rut1 = new Route ( "地上移動", "立ち状態から出る移動全般" );
			rut1.BL_BranchName.Add ( new TName ( "6 → FrontMove" ) );
			rut1.BL_BranchName.Add ( new TName ( "4 → BackMove" ) );
			rut1.BL_BranchName.Add ( new TName ( "8 → VerticalJump" ) );
			rut1.BL_BranchName.Add ( new TName ( "9 → FrontJump" ) );
			rut1.BL_BranchName.Add ( new TName ( "7 → BackJump" ) );
			chara.BD_Route.Add ( rut1 );
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