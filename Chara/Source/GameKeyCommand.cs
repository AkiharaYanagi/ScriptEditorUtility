using System.Linq;
using System.Collections.Generic;


namespace ScriptEditor
{
	//------------------------------------------------------------------
	//	ゲームキー　コマンド
	//------------------------------------------------------------------
	// ◆格闘ゲーム入力におけるキャラの入力
	// ◆レバーは内部的に全方位別で記録されるが、外部からのコマンド的には１方向を指定する
	// ◆方向は(右正)、左向き時は <1-3>,<4-6>,<7-9>,を入れ替えて判定
	// ◆コマンドとして「否定」フラグとは「指定のゲームキー状態でなくなったとき」
	//------------------------------------------------------------------
	// ◆方向要素入力(2E, 6E, 8E, 4E)も判定する(いずれかでも成立)
	//下方向要素(2E)	前方向要素(6E)	上方向要素(8E)	後方向要素(4E)
	//  * * *		  * * 9			  7 8 9			  7 * *
	//  * * *		  * * 6			  * * *			  4 * *
	//  1 2 3		  * * 3			  * * *			  1 * *
	//------------------------------------------------------------------
	//
	// ◆判定は7種類
	//	[__](押した状態)		
	//	[--](離した状態)		
	//	[-_](押した瞬間)		
	//	[_-](離した瞬間)		
	//	[**](どの状態でも)	
	//	[*_] 現在のみ押した状態
	//	[*-] 現在のみ離した状態
	// 
	//-------------------------------------------------------------------

	using GK_L = GameKeyCommand.LeverCommand;
	using GK_ST = GameKeyCommand.GameKeyCommandState;

	//====================================================================
	//1[F]のコマンド条件に用いるクラス
	//データ的にはレバー全方向の各情報を持つが、現時点で表記上はレバー１種類だけ取得/設定する
	//キャラ状態を受けて、その中の向きを以て前後の判定
	//～でない（否定）の条件
	//====================================================================
	[System.Serializable]
	public class GameKeyCommand
	{
		//---------------------------------------------------
		//キー入力状態
		public enum GameKeyCommandState
		{
			KEY_OFF = 0,		// [__] 入力無
			KEY_ON = 1,			// [--] 入力有
			KEY_PUSH = 2,		// [-_] 押した瞬間
			KEY_RELE = 3,		// [_-] 離した瞬間
			KEY_WILD = 4,		// [**] どの状態でも
			KEY_IS = 5,			// [*_] 現在のみ押した状態
			KEY_NIS = 6,		// [*-] 現在のみ離した状態
		}

		//---------------------------------------------------
		//レバー入力種類
		//矢印テンキー表示を[1]から１回転
		//  7 8 9
		//  4 N 6
		//  1→2 3
		public enum LeverCommand
		{
			//方向
			LVR_1 = 0,
			LVR_2 = 1,
			LVR_3 = 2,
			LVR_6 = 3,
			LVR_9 = 4,
			LVR_8 = 5,
			LVR_7 = 6,
			LVR_4 = 7,

			LVR_N = 8,	//未指定,またはニュートラル
		}
		public const int LeverCommandNum = 8;

		//======================================================================================
		//レバー判定状態 (レバー定義((int)C_X)でインデックスを指定)
//		public GKC_ST [] Lvr { set; get; } = new GKC_ST [ LeverCommandNum ];

		public Dictionary < GK_L, GK_ST > DctLvrSt = new Dictionary < GK_L, GK_ST > ();

		//現在レバーインデックス
//		public GK_L IdLvr { get; set; } = 0;

		//---------------------------------------------------
		//ボタン状態 (8ボタン)
		public enum ButtonCommand
		{
			BTN_0,
			BTN_1,
			BTN_2,
			BTN_3,
			BTN_4,
			BTN_5,
			BTN_6,
			BTN_7,
		}

		//ボタン数
		public const int BtnNum = 8;
		
		public GK_ST [] Btn { set; get; } = new GK_ST [ BtnNum ];

		//---------------------------------------------------
		//否定のフラグ
		public bool Not { get; set; } = false;


		//======================================================================================
		//コンストラクタ
		public GameKeyCommand ()
		{
#if false
			//enum 値型なのでforeachは使わない
			for ( int i = 0; i < LeverCommandNum; ++ i )
			{
				Lvr [ i ] = GKC_ST.KEY_WILD;
			}
#endif
			DctLvrSt.Add ( GK_L.LVR_1, GK_ST.KEY_WILD );
			DctLvrSt.Add ( GK_L.LVR_2, GK_ST.KEY_WILD );
			DctLvrSt.Add ( GK_L.LVR_3, GK_ST.KEY_WILD );
			DctLvrSt.Add ( GK_L.LVR_6, GK_ST.KEY_WILD );
			DctLvrSt.Add ( GK_L.LVR_9, GK_ST.KEY_WILD );
			DctLvrSt.Add ( GK_L.LVR_8, GK_ST.KEY_WILD );
			DctLvrSt.Add ( GK_L.LVR_7, GK_ST.KEY_WILD );
			DctLvrSt.Add ( GK_L.LVR_4, GK_ST.KEY_WILD );

		
			for ( int i = 0; i < BtnNum; ++ i )
			{
				Btn [ i ] = GK_ST.KEY_WILD;
			}
		}


		//---------------------------------------------------
#if false
		//現在のレバー種類を取得(WILD以外, 先頭優先)
		public GK_L GetLever ()
		{
			foreach ( GK_L key  in DctLvrSt.Keys )
			{
				if ( GK_ST.KEY_WILD != DctLvrSt [ key ] ) { return key; }
			}
			return GK_L.LVR_1;	//該当なしは先頭を返す
		}
#endif

		//レバー設定
		//  7 8 9
		//  4 N 6
		//  1→2 3
		public void SetLever ( GK_L gkl )
		{
			switch ( gkl )
			{
			case GK_L.LVR_1: SetNeighbor ( GK_L.LVR_1, GK_L.LVR_4, GK_L.LVR_2 ); break;
			case GK_L.LVR_2: SetNeighbor ( GK_L.LVR_2, GK_L.LVR_1, GK_L.LVR_3 ); break;
			case GK_L.LVR_3: SetNeighbor ( GK_L.LVR_3, GK_L.LVR_2, GK_L.LVR_6 ); break;
			case GK_L.LVR_4: SetNeighbor ( GK_L.LVR_4, GK_L.LVR_1, GK_L.LVR_7 ); break;
			case GK_L.LVR_6: SetNeighbor ( GK_L.LVR_6, GK_L.LVR_3, GK_L.LVR_9 ); break;
			case GK_L.LVR_7: SetNeighbor ( GK_L.LVR_7, GK_L.LVR_4, GK_L.LVR_8 ); break;
			case GK_L.LVR_8: SetNeighbor ( GK_L.LVR_8, GK_L.LVR_7, GK_L.LVR_9 ); break;
			case GK_L.LVR_9: SetNeighbor ( GK_L.LVR_9, GK_L.LVR_8, GK_L.LVR_6 ); break;
			}
		}

		private void SetNeighbor ( GK_L gkl_on, GK_L gkl_off0, GK_L gkl_off1 )
		{
			ClearLever ();
			DctLvrSt [ gkl_off0 ] = GK_ST.KEY_NIS;
			DctLvrSt [ gkl_on ] = GK_ST.KEY_IS;	//斜め入力を排除するため隣接はNIS
			DctLvrSt [ gkl_off1 ] = GK_ST.KEY_NIS;
		}

		//レバー状態指定を取得
#if false
		public GK_ST GetLvrSt ()
		{
			GK_L gk_l = GetLever ();
			return Lvr [ (int)gk_l ];
		}
#endif
		public GK_ST GetLvrSt ( GK_L gk_l )
		{
			return DctLvrSt [ gk_l ];
		}

		//レバー状態指定を設定
#if false
		public void SetLvrSt ( GK_ST st )
		{
			//未指定だったらC1
			GK_L gk_l = GetLever ();
			Lvr [ (int)gk_l ] = st;
		}
#endif
		public void SetLvrSt ( GK_ST st, GK_L gk_l )
		{
			DctLvrSt [ gk_l ] = st;
		}

		public bool AreAllLvrWild ()
		{
			foreach ( GK_L key in DctLvrSt.Keys )
			{
				if ( DctLvrSt [ key ] == GK_ST.KEY_WILD ) { return false; }
			}
			return true;
		}

		//レバー状態初期化
		private void ClearLever ()
		{
			foreach ( GK_L key in DctLvrSt.Keys )
			{
				DctLvrSt [ key ] = GK_ST.KEY_WILD;
			}
		}

		//--------------------------------------------------------------
		//レバー回転
		public void Lever_R ()
		{
			//右方向にシフト
			//  7 8→9
			//  4 N 6
			//  1←2 3
			
			//12369874
			GK_ST st1 = DctLvrSt [ GK_L.LVR_1 ];	//初期値保存
			for ( int i = (int)GK_L.LVR_1; i < (int)GK_L.LVR_4; ++ i )
			{
				DctLvrSt [ (GK_L)i ] = DctLvrSt [ (GK_L)(i + 1) ];
			}
			DctLvrSt [ GK_L.LVR_4 ] = st1;
		}

		public void Lever_L ()
		{
			//  7 8←9
			//  4 N 6
			//  1→2 3
			
			//47896321
			GK_ST st4 = DctLvrSt [ GK_L.LVR_4 ];	//初期値保存
			for ( int i = (int)GK_L.LVR_4; i > (int)GK_L.LVR_1; -- i )
			{
				DctLvrSt [ (GK_L)i ] = DctLvrSt [ (GK_L)(i - 1) ];
			}
			DctLvrSt[ (int)GK_L.LVR_1 ] = st4;
		}
		//--------------------------------------------------------------

		//======================================================================
		//比較
		//thisの状態がチェックするコマンド条件、引数がプレイヤ入力
		//引数：比較するゲームキー状態, キャラクタ向き(右正)
		//戻値：適合したらtrue、それ以外はfalse
		public bool CompareTarget ( GameKeyData gameKeyData, bool dirRight )
		{
			//チェック用(反転などの操作をするためにディープコピーとする)
			GameKeyData gk = new GameKeyData ( gameKeyData );

			//比較するかどうか(条件がワイルドの時は比較しない)
			bool[] bWildLvr = new bool [ LeverCommandNum ];
			bool[] bWildBtn = new bool[ BtnNum ];

			//比較結果
			bool[] b_Lvr = new bool [ LeverCommandNum ];
			bool[] b_Btn = new bool[ BtnNum ];

			InitBoolArray ( bWildLvr, LeverCommandNum );
			InitBoolArray ( bWildBtn, BtnNum );
			InitBoolArray ( b_Lvr, LeverCommandNum );
			InitBoolArray ( b_Btn, BtnNum );

			//-----------------------------------------------------------------
			//左向きのとき左右を入れ替え
			FlipData ( gk, dirRight );

			//-----------------------------------------------------------------
			//比較して結果を保存
			//レバー
			CompareKey ( LeverCommandNum, DctLvrSt, bWildLvr, b_Lvr, gameKeyData.Lvr, gameKeyData.PreLbr );
			//ボタン
			CompareKey ( BtnNum, Btn, bWildBtn, b_Btn, gameKeyData.Btn, gameKeyData.PreBtn );

			//-----------------------------------------------------------------
			//まとめ

			//すべてワイルドの場合trueを返す
			if ( AreAllTrue ( bWildLvr ) && AreAllTrue ( bWildBtn ) ) { return true; }

			//いずれかを返す場合
			bool ret = true;

			//調査対象かつ適合かどうか
			ret &= CheckWild ( LeverCommandNum, bWildLvr, b_Lvr );
			ret &= CheckWild ( BtnNum, bWildBtn, b_Btn );

			//否定の場合は反転して返す (排他的論理和)
			return ret ^ this.Not;
		}

		//---------------------------------------------------------------------------
		//bool配列初期化
		private void InitBoolArray ( bool[] ary, int length )
		{
			for ( int i = 0; i < length; ++ i )
			{
				ary [ i ] = false;
			}
		}

		//キーデータ左右入替
		private void FlipData ( GameKeyData gk, bool dirRight )
		{
			//左向きのとき左右を入れ替え
			//7<-->9
			//4<-->6
			//1<-->3
			if ( ! dirRight )
			{
				bool tempBool;

				tempBool = gk.Lvr [ (int)GK_L.LVR_1 ];
				gk.Lvr [ (int)GK_L.LVR_1 ] = gk.Lvr [ (int)GK_L.LVR_3 ];
				gk.Lvr [ (int)GK_L.LVR_3 ] = tempBool;

				tempBool = gk.Lvr [ (int)GK_L.LVR_4 ];
				gk.Lvr [ (int)GK_L.LVR_4 ] = gk.Lvr [ (int)GK_L.LVR_6 ];
				gk.Lvr [ (int)GK_L.LVR_6 ] = tempBool;

				tempBool = gk.Lvr [ (int)GK_L.LVR_7 ];
				gk.Lvr [ (int)GK_L.LVR_7 ] = gk.Lvr [ (int)GK_L.LVR_9 ];
				gk.Lvr [ (int)GK_L.LVR_9 ] = tempBool;
			}
		}

		//すべてtrueかどうか
		private bool AreAllTrue ( bool [] bAry )
		{
			foreach ( bool b in bAry )
			{
				if ( ! b ) { return false; }
			}
			return true;
		}

		//比較
		private void CompareKey ( int num, GK_ST [] stAry, bool[] bWildAry, bool[] bResultAry, bool[] bDataAry, bool[] bPreAry )
		{
			for ( int i = 0; i < num; ++ i )
			{
				//一時変数
				bool b = bDataAry[i];
				bool pb = bPreAry[i];

				switch ( stAry [ i ] )
				{
				//条件がワイルドの場合は比較しない
				case GK_ST.KEY_WILD: bWildAry[i] = true; break;
				case GK_ST.KEY_ON  : bResultAry[i] = b; break;
				case GK_ST.KEY_OFF : bResultAry[i] = ! b; break;
				case GK_ST.KEY_PUSH: bResultAry[i] = b && ! pb; break;
				case GK_ST.KEY_RELE: bResultAry[i] = ! b && pb; break;
				}
			}
		}

		//すべて調査対象かつ適合かどうか
		private bool CheckWild ( int num, bool [] bWildAry, bool [] bCheckAry )
		{
			for ( int i = 0; i < num; ++ i )
			{
				//一つでも調査対象が不適であったらその時点でfalse
				if ( bWildAry[i] ) { if ( bCheckAry[i] ) { return false; } }
			}			
			//すべて適合だったらtrue
			return true;
		}

		//======================================================================================
		public override bool Equals ( object obj )
		{
			//(Object)型で比較する
			//nullまたは型が異なるときfalse
			if ( null == obj || this.GetType () != obj.GetType () ) { return false; }

			//キャストして比較
			GameKeyCommand g = (GameKeyCommand)obj;
			
			if ( ! this.DctLvrSt.SequenceEqual ( g.DctLvrSt ) ) { return false; }
			if ( ! this.Btn.SequenceEqual ( g.Btn ) ) { return false; }
			if ( ! (this.Not == g.Not) ) { return false; }

			return true;
		}

		public override int GetHashCode ()
		{
			int i0 = DctLvrSt.GetHashCode ();
			int i2 = i0 ^ Btn.GetHashCode ();
			int i3 = i2 ^ Not.GetHashCode ();
			return i3;
		}

	}
}

