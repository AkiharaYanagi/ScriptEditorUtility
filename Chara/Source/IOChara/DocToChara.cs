using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace ScriptEditor
{
	using BL_Sqc = BindingList < Sequence >;
	using GK_ST = GameKeyCommand.GameKeyCommandState;
	using GK_L = GameKeyCommand.LeverCommand;

	//==================================================
	//	ドキュメント型からキャラへ変換する
	//		主にLoadChara, LoadTextCharaで用いる
	//==================================================
	public class DocToChara
	{
		public void Load ( Document document, Chara chara )
		{
			//Root直下のChara操作用一時エレメント
			List < Element > listElementChara = document.Root.Elements [ 0 ].Elements;

			//--------------------------------------
			//イメージ部ヘッダ

			//メインイメージリスト
			Element elemImageList = listElementChara[ ( int ) ELEMENT_CHARA.MAIN_IMAGE_LIST ];
			LoadImageHeader ( elemImageList, chara.behavior );
			//Efイメージリスト
			Element elemEfImageList = listElementChara[ ( int ) ELEMENT_CHARA.EF_IMAGE_LIST ];
			LoadImageHeader ( elemEfImageList, chara.garnish );

			//--------------------------------------
			//スクリプト部
			LoadScriptData ( listElementChara, chara );
		}

		//イメージヘッダ読込
		private void LoadImageHeader ( Element elemImageList, Compend cmp )
		{
			//イメージ個数(イメージ読込時に使用)
			int numImage = int.Parse ( elemImageList.Attributes[ 0 ].Value );
			//イメージデータ(名前と実体)
			foreach ( Element elemImage in elemImageList.Elements )
			{
				string name = elemImage.Attributes[ 0 ].Value;
				ImageData imageData = new ImageData ( name, null );	//実体は空(後で読み込む)
				cmp.BD_Image.Add ( imageData );
			}
		}

		//キャラデータにおけるスクリプト部のみの読込
		public void LoadScriptData ( Document doc, Chara chara )
		{
			//Root直下のChara操作用一時エレメント
			List < Element > listElementChara = doc.Root.Elements [ 0 ].Elements;

			//イメージリストは飛ばし、スクリプト部のみ読込
			LoadScriptData ( listElementChara, chara );
		}

		//キャラデータにおけるスクリプト部の読込
		public void LoadScriptData ( List < Element > listElementChara, Chara chara )
		{
			//アクションリスト
			Element elemActionList = listElementChara[ ( int ) ELEMENT_CHARA.ACTION_LIST ];
			LoadActionList ( elemActionList, chara );

			//Efリスト
			Element elemEfList = listElementChara[ ( int ) ELEMENT_CHARA.EF_LIST ];
			LoadEffectList ( elemEfList, chara );

			//コマンドリスト
			Element elemCommandList = listElementChara[ ( int ) ELEMENT_CHARA.COMMAND_LIST ];
			LoadCommandList ( elemCommandList, chara );

			//ブランチリスト
			Element elemBranchList = listElementChara[ ( int ) ELEMENT_CHARA.BRANCH_LIST ];
			LoadBranchList ( elemBranchList, chara );

			//ルートリスト
			Element elemRouteList = listElementChara[ ( int ) ELEMENT_CHARA.ROUTE_LIST ];
			LoadRouteList ( elemRouteList, chara );
		}


		//アクションリスト読込
		private void LoadActionList ( Element elemActionList, Chara chara )
		{
			foreach ( Element elemAction in elemActionList.Elements )
			{
				//代入用
				Action action = new Action ();

				//アクション "名前"
				action.Name = elemAction.Attributes[ (int)ATTR_ACTION.ELAC_NAME ].Value;

				//"次アクション名" (終了時における次アクション)
				action.NextActionName = elemAction.Attributes [ (int)ATTR_ACTION.ELAC_NEXT_NAME ].Value;

				//"次アクションID" (終了時における次アクション)
				//action.NextActionID = elemAction.Attributes [ (int)ATTR_ACTION.ELAC_NEXT_ID ].Value;

				//アクション属性
				int nCategory = IOChara.AttrToInt ( elemAction, (int)ATTR_ACTION.ELAC_CATEGORY );
				action.Category = (ActionCategory) nCategory;

				//アクション体勢
				int nPosture = IOChara.AttrToInt ( elemAction, (int)ATTR_ACTION.ELAC_POSTURE );
				action.Posture = (ActionPosture) nPosture;

				//ヒット数
				action.HitNum = IOChara.AttrToInt ( elemAction, (int)ATTR_ACTION.ELAC_HITNUM );

				//ヒット間隔[F}
				action.HitPitch = IOChara.AttrToInt ( elemAction, (int)ATTR_ACTION.ELAC_HIT_PITCH );

				//子Element <Script> 数は不定
				ReadScriptList ( action, elemAction.Elements );

				//アクションに設定
				chara.behavior.BD_Sequence.Add ( action );
			}
		}

		//エフェクトリスト読込
		private void LoadEffectList ( Element elemActionList, Chara chara )
		{
			//<Effect>[]
			foreach ( Element elemEf in elemActionList.Elements )
			{
				//代入用
				Effect effect = new Effect ();

				//エフェクト "名前"
				effect.Name = elemEf.Attributes[ 0 ].Value;

				//子Element <Script> 数は不定
				ReadScriptList ( effect, elemEf.Elements );

				//エフェクトに設定
				chara.garnish.BD_Sequence.Add ( effect );
			}
		}


		//スクリプトリストの読込
		public void ReadScriptList ( Sequence sequence, List<Element> listElemnt )
		{
			//子Element <Script> 数は不定
			int frame = 0;	//フレーム数
			foreach ( Element e in listElemnt )
			{
				//代入用
				Script s = new Script ();

				//フレーム数
				s.Frame = frame;

				//グループ
				s.Group = IOChara.AttrToInt ( e, (int)ATTR_SCP.GROUP );

				//イメージ名
				s.ImgName =  e.Attributes[ (int)ATTR_SCP.IMG_NAME ].Value;

				//位置, 速度, 加速度
				s.Pos = IOChara.AttrToPoint ( e, (int)ATTR_SCP.X, (int)ATTR_SCP.Y );
				s.Param_Btl.Vel = IOChara.AttrToPoint ( e, (int)ATTR_SCP.VX, (int)ATTR_SCP.VY );
				s.Param_Btl.Acc = IOChara.AttrToPoint ( e, (int)ATTR_SCP.AX, (int)ATTR_SCP.AY );

				//計算状態
				int clcst = IOChara.AttrToInt ( e, (int)ATTR_SCP.CLC_ST );
				s.CalcState = (CLC_ST)clcst;


				//値
				s.Param_Btl.Power = IOChara.AttrToInt ( e, (int)ATTR_SCP.POWER );			//攻撃値
				s.Param_Ef.BlackOut = IOChara.AttrToInt ( e, (int)ATTR_SCP.BLACKOUT );		//暗転[F]
				s.Param_Ef.Vibration = IOChara.AttrToInt ( e, (int)ATTR_SCP.VIBRATION );	//振動[F]
				s.Param_Ef.Stop = IOChara.AttrToInt ( e, (int)ATTR_SCP.STOP );				//停止[F]
				s.Param_Ef.Radian = IOChara.AttrToInt ( e, (int)ATTR_SCP.RADIAN );			//回転[rad]


				//-----------------------------------------------------------------------------
				//Script以下のElement

				//ルートネームリスト
				Element elemRutList = e.Elements [( int ) ELEMENT_SCRIPT.ELSC_ROUTE ];
				foreach ( Element elemRut in elemRutList.Elements )
				{
					s.BD_RutName.Add ( new TName ( elemRut.Attributes [ 0 ].Value ) );
				}

				//-----------------------------------------------------------------------------
				//Efジェネレートリスト
				Element elemEfGnrtList = e.Elements[ ( int ) ELEMENT_SCRIPT.ELSC_EFGNRT ];
				foreach ( Element elmEfGnrt in elemEfGnrtList.Elements )
				{
					EffectGenerate efGnrt = new EffectGenerate ();
					List < Attribute > la = elmEfGnrt.Attributes;
					
					//Nameを読込
					efGnrt.Name = la[ (int)ELMT_EFGNRT.ELEG_NAME ].Value;

					//EfIDを読込
					efGnrt.EfName = la[ (int)ELMT_EFGNRT.ELEG_EFNAME ].Value;

					//PtGnrtを読込
					int pt_x = int.Parse ( la[ (int)ELMT_EFGNRT.ELEG_PT_X ].Value );
					int pt_y = int.Parse ( la[ (int)ELMT_EFGNRT.ELEG_PT_Y ].Value );
					efGnrt.Pt = new Point ( pt_x, pt_y );

					//Z位置
					efGnrt.Z = IOChara.AttrToInt ( elmEfGnrt, (int)ELMT_EFGNRT.ELEG_PT_Z );

					//生成
					efGnrt.Gnrt = bool.Parse ( la[ (int)ELMT_EFGNRT.ELEG_GNRT ].Value );

					//繰返
					efGnrt.Loop = bool.Parse ( la[ (int)ELMT_EFGNRT.ELEG_LOOP ].Value );

					//位置同期
					efGnrt.Sync = bool.Parse ( la[ (int)ELMT_EFGNRT.ELEG_SYNC ].Value );

					//スクリプトに設定
					s.BD_EfGnrt.Add ( efGnrt );
				}

				//-----------------------------------------------------------------------------
				List<Element> le = e.Elements;
				ReadRect ( s.ListCRect, le[ ( int ) ELEMENT_SCRIPT.ELSC_CRECT ] );	/*接触枠*/
				ReadRect ( s.ListARect, le[ ( int ) ELEMENT_SCRIPT.ELSC_ARECT ] );	/*攻撃枠*/
				ReadRect ( s.ListHRect, le[ ( int ) ELEMENT_SCRIPT.ELSC_HRECT ] );	/*当り枠*/
				ReadRect ( s.ListORect, le[ ( int ) ELEMENT_SCRIPT.ELSC_ORECT ] );	/*相殺枠*/		
				//-----------------------------------------------------------------------------

				//シークエンスにスクリプトを追加
				sequence.ListScript.Add ( s );

				//フレームを一つ進める
				++frame;
			}
		}

		private void ReadRect ( List<Rectangle> listRect, Element elemRectList )
		{
			//個数
//			int numRect = int.Parse ( elemRectList.Attributes[ 0 ].Value );

			//枠
			foreach ( Element elemRect in elemRectList.Elements )
			{
				//新規作成
				int x = IOChara.AttrToInt ( elemRect, 0 );
				int y = IOChara.AttrToInt ( elemRect, 1 );
				int w = IOChara.AttrToInt ( elemRect, 2 );
				int h = IOChara.AttrToInt ( elemRect, 3 );
				Rectangle rect = new Rectangle ( x, y, w, h );

				//スクリプトに登録
				listRect.Add ( rect );
			}
		}


		//コマンドリスト読込
		private void LoadCommandList ( Element elemCommandList, Chara chara )
		{
			//個数
			int numCommand = int.Parse ( elemCommandList.Attributes[ 0 ].Value );

			//[]<Command>
			foreach ( Element elemCommand in elemCommandList.Elements )
			{
				//代入用新規作成
				Command command = new Command ();

				//attributeは２つ
				//コマンド "名前"
				command.Name = elemCommand.Attributes[ ( int ) ATTRIBUTE_COMMAND.NAME ].Value;

				//コマンド "制限時間"
				command.LimitTime = int.Parse ( elemCommand.Attributes[ ( int ) ATTRIBUTE_COMMAND.LIMIT_TIME ].Value );

				//子ElementはGameKeyを保持
				foreach ( Element elemKey in elemCommand.Elements )
				{
					//代入用新規作成
					GameKeyCommand gameKey = new GameKeyCommand ();

					//アトリビュートインデックス
					int atrbIndex = 0;

					//否定
					gameKey.Not = elemKey.Attributes[ atrbIndex ].Value.CompareTo ( "True" ) == 0;

					//レバー
					//for ( int i = 0; i < GameKeyCommand.LeverCommandNum; ++ i )
					foreach ( GK_L key in gameKey.DctLvrSt.Keys )
					{
						string strLvr = elemKey.Attributes [ ++ atrbIndex ].Value;
						gameKey.DctLvrSt [ key ] = ( GK_ST )Enum.Parse ( typeof ( GK_ST ), strLvr );
					}
#if false
					int id = IOChara.Parse ( elemKey, ++ atrbIndex );
					gameKey.IdLvr = (GameKeyCommand.LeverCommand)id;

					string vLvr = elemKey.Attributes[ ++ atrbIndex ].Value;
					gameKey.Lvr [ id ] = ( GKC_ST ) Enum.Parse ( typeof ( GKC_ST ), vLvr );
#endif

					//ボタン
					for ( int i = 0; i < GameKeyCommand.BtnNum; ++ i )
					{
						string v = elemKey.Attributes[ ++ atrbIndex ].Value;
						gameKey.Btn [ i ] = ( GK_ST ) Enum.Parse ( typeof ( GK_ST ), v );
					}

					//コマンドに加える
					command.ListGameKeyCommand.Add ( gameKey );
				}

				//キャラに登録
				chara.BD_Command.Add ( command );
			}
		}


		//ブランチリスト読込
		private void LoadBranchList ( Element elemBranchList, Chara chara )
		{
			//[]<Branch>
			foreach ( Element elemBrc in elemBranchList.Elements )
			{
				//代入用新規作成
				Branch brc = new Branch
				{
					Name = elemBrc.Attributes [ ( int ) ATTR_BRANCH.NAME ].Value,
					Condition = (BranchCondition) IOChara.AttrToInt ( elemBrc, (int)ATTR_BRANCH.CONDITION ),
					NameCommand = elemBrc.Attributes [ ( int ) ATTR_BRANCH.CMD_NAME ].Value,
					NameSequence = elemBrc.Attributes [ ( int ) ATTR_BRANCH.ACT_NAME ].Value,
					Frame = 0
				};

				//キャラに登録
				chara.BD_Branch.Add ( brc );
			}
		}

		//ルートリスト読込
		private void LoadRouteList ( Element elemRouteList, Chara chara )
		{
			//[]<Route>
			foreach ( Element elemRut in elemRouteList.Elements )
			{
				//代入用新規作成
				Route rut = new Route
				{
					Name = elemRut.Attributes [ (int)ATTR_ROUTE.NAME ].Value,
					Summary = elemRut.Attributes [ (int)ATTR_ROUTE.SUMMARY ].Value,
				};

				//ブランチネームリスト
				Element elemBranchNameList = elemRut.Elements [ 0 ];

				//個数
				int numBrunchName = int.Parse ( elemBranchNameList.Attributes[ 0 ].Value );

				foreach ( Element elemBrcName in elemBranchNameList.Elements )
				{
					string name = elemBrcName.Attributes [ (int)ATTR_.NAME_0 ].Value;
					rut.BD_BranchName.Add ( new TName ( name ) );
				}

				//キャラに登録
				chara.BD_Route.Add ( rut );
			}
		}

	}
}
