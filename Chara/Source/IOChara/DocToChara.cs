﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace ScriptEditor
{
	using BL_Sqc = BindingList < Sequence >;
	using GKC_ST = GameKeyCommand.GameKeyCommandState;

	//==================================================
	//	ドキュメント型からキャラへ変換する
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
#if false
			//イメージリスト
			Element elemImageList = listElementChara[ ( int ) ELEMENT_CHARA.MAIN_IMAGE_LIST ];

			//イメージ個数(イメージ読込時に使用)
			int numImage = int.Parse ( elemImageList.Attributes[ 0 ].Value );
			//イメージデータ(名前と実体)
			foreach ( Element elemImage in elemImageList.Elements )
			{
				string name = elemImage.Attributes[ 0 ].Value;
				ImageData imageData = new ImageData ( name, null );	//実体は空(後で読み込む)
				chara.behavior.BD_Image.Add ( imageData );
			}

			//Efイメージリスト
			Element elemEfImageList = listElementChara[ ( int ) ELEMENT_CHARA.EF_IMAGE_LIST ];

			//イメージ個数(イメージ読込時に使用)
			int numEfImage = int.Parse ( elemEfImageList.Attributes[ 0 ].Value );
			//イメージ名
			foreach ( Element elemEfImage in elemEfImageList.Elements )
			{
				string name = elemEfImage.Attributes[ 0 ].Value;
				ImageData imageData = new ImageData ( name, null );
				chara.garnish.BD_Image.Add ( imageData );
			}
#endif
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
#if false
			foreach ( Element elemAction in elemActionList.Elements )
			{
				//代入用
				Action action = new Action ();

				//アクション "名前"
				action.Name = elemAction.Attributes[ (int)ATTR_ACTION.ELAC_NAME ].Value;

				//"次アクション" (終了時における次アクション)
				action.NextActionName = elemAction.Attributes [ (int)ATTR_ACTION.ELAC_NEXT_NAME ].Value;

				//アクション属性
				int nCategory = IOChara.Parse ( elemAction, (int)ATTR_ACTION.ELAC_CATEGORY );
				action.Category = (ActionCategory) nCategory;

				//アクション体勢
				int nPosture = IOChara.Parse ( elemAction, (int)ATTR_ACTION.ELAC_POSTURE );
				action.Posture = (ActionPosture) nPosture;

				//消費バランス値
				action._Balance = IOChara.Parse ( elemAction, (int)ATTR_ACTION.ELAC_BALANCE );

				//子Element <Script> 数は不定
				ReadScriptList ( action, elemAction.Elements );

				//アクションに設定
				chara.behavior.BD_Sequence.Add ( action );
			}

#if false
			//一度アクションリストを作成してから指定し直す
			BL_Sqc blsqc = chara.behavior.BD_Sequence.GetBindingList ();
			foreach ( Action action  in blsqc )
			{
				//"次アクション"
//				action.NextAction = (Action)blsqc [ action.NextIndex ];
				action.NextAction = (Action)chara.behavior.BD_Sequence.Get ( action.NextActionName );
			}
#endif
#endif

			//Efリスト
			Element elemEfList = listElementChara[ ( int ) ELEMENT_CHARA.EF_LIST ];
			LoadEffectList ( elemEfList, chara );
#if false

			//<Effect>[]
			foreach ( Element elemEf in elemEfList.Elements )
			{
				//代入用
				Effect effect = new Effect ();

				//エフェクト "名前"
				effect.Name = elemEf.Attributes[ 0 ].Value;

				//子Element <Script> 数は不定
				ReadScriptList ( effect, elemEf.Elements );

				//エフェクトに設定
//				chara.garnish.Bldct_sqc.GetBindingList().Add ( effect );
				chara.garnish.BD_Sequence.Add ( effect );
			}

			//読込時　エフェクトの数が０のときダミーを作成
			if ( 0 == chara.garnish.BD_Sequence.GetBindingList().Count )
			{
//				chara.garnish.Bldct_sqc.GetBindingList().Add ( new Effect () );
				Effect effect = new Effect ();
				chara.garnish.BD_Sequence.Add ( effect );
			}
#endif

			//コマンドリスト
			Element elemCommandList = listElementChara[ ( int ) ELEMENT_CHARA.COMMAND_LIST ];
			LoadCommandList ( elemCommandList, chara );
#if false
			//個数
			int numCommand = int.Parse ( elemCommandList.Attributes[ 0 ].Value );

			//<Command>[]
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
#if false

					for ( int i = 0; i < GameKeyCommand.LeverCommandNum; ++ i )
					{
						string v = elemKey.Attributes[ ++ atrbIndex ].Value;
						gameKey.Lvr [ i ] = ( GKC_ST ) Enum.Parse ( typeof ( GKC_ST ), v );
					}
#endif
					int id = IOChara.Parse ( elemKey, ++ atrbIndex );
					gameKey.IdLvr = id;

					string vLvr = elemKey.Attributes[ ++ atrbIndex ].Value;
					gameKey.Lvr [ id ] = ( GKC_ST ) Enum.Parse ( typeof ( GKC_ST ), vLvr );

					//ボタン
					for ( int i = 0; i < GameKeyCommand.BtnNum; ++ i )
					{
						string v = elemKey.Attributes[ ++ atrbIndex ].Value;
						gameKey.Btn [ i ] = ( GKC_ST ) Enum.Parse ( typeof ( GKC_ST ), v );
					}

					//コマンドに加える
					command.ListGameKeyCommand.Add ( gameKey );
				}

				//キャラに登録
				chara.BD_Command.Add ( command );
			}
#endif

#if false

			//ブランチリストのコマンドとアクションを登録
			foreach ( Action action in chara.behavior.BD_Sequence.GetBindingList() )
			{
				foreach ( Script script in action.ListScript )
				{
					foreach ( Branch0 branch in script.ListBranch )
					{
						BindingList < Command > ls = chara.BD_Command.GetBindingList();
						branch.Condition = ls [ branch.IndexCommand ];
						branch.Transit = chara.behavior[ branch.IndexAction ];
					}
				}
			}
#endif

			//ブランチリスト
			Element elemBranchList = listElementChara[ ( int ) ELEMENT_CHARA.BRANCH_LIST ];
			LoadBranchList ( elemBranchList, chara );

			//ルートリスト
			Element elemRouteList = listElementChara[ ( int ) ELEMENT_CHARA.ROUTE_LIST ];
			LoadRouteList ( elemRouteList, chara );

#if false

			//特殊状態アクションID (nameは飛ばす)
			Element elemSpAction = listElementChara[ ( int ) ELEMENT_CHARA.BASIC_ACTION_ID ];
			int sizeSpAction = elemSpAction.Elements.Count;
			int spActionIndex = 0;

			foreach ( RefInt refi in chara.BsAction )
			{
				if ( sizeSpAction <= spActionIndex ) { break; }
				refi.i = int.Parse ( elemSpAction.Elements[ spActionIndex++ ].Attributes[ 0 ].Value );
			}

#endif
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

				//"次アクション" (終了時における次アクション)
				action.NextActionName = elemAction.Attributes [ (int)ATTR_ACTION.ELAC_NEXT_NAME ].Value;

				//アクション属性
				int nCategory = IOChara.Parse ( elemAction, (int)ATTR_ACTION.ELAC_CATEGORY );
				action.Category = (ActionCategory) nCategory;

				//アクション体勢
				int nPosture = IOChara.Parse ( elemAction, (int)ATTR_ACTION.ELAC_POSTURE );
				action.Posture = (ActionPosture) nPosture;

				//消費バランス値
				action._Balance = IOChara.Parse ( elemAction, (int)ATTR_ACTION.ELAC_BALANCE );

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
		private void ReadScriptList ( Sequence sequence, List<Element> listElemnt )
		{
			//子Element <Script> 数は不定
			int frame = 0;	//フレーム数
			foreach ( Element elemScript in listElemnt )
			{
				//代入用
				Script script = new Script ();

				//フレーム数
				script.Frame = frame;

				//グループ
				script.Group = IOChara.Parse ( elemScript, (int)ATTRIBUTE_SCRIPT.GROUP );

				//イメージ名
				script.ImgName =  elemScript.Attributes[ (int)ATTRIBUTE_SCRIPT.IMG_NAME ].Value;

				//X, Y
				int x = IOChara.Parse ( elemScript, (int)ATTRIBUTE_SCRIPT.X );
				int y = IOChara.Parse ( elemScript, (int)ATTRIBUTE_SCRIPT.Y );
				script.SetPos ( x, y );

				//VX,VY
				int vx = IOChara.Parse ( elemScript, (int)ATTRIBUTE_SCRIPT.VX );
				int vy = IOChara.Parse ( elemScript, (int)ATTRIBUTE_SCRIPT.VY );
				script.SetVel ( vx, vy );

				//AX,AY
				int ax = IOChara.Parse ( elemScript, (int)ATTRIBUTE_SCRIPT.AX );
				int ay = IOChara.Parse ( elemScript, (int)ATTRIBUTE_SCRIPT.AY );
				script.SetAcc ( ax, ay );

				//計算状態
				int clcst = IOChara.Parse ( elemScript, (int)ATTRIBUTE_SCRIPT.CLC_ST );
				script.CalcState = (CLC_ST)clcst;

				//power
				script.Power = IOChara.Parse ( elemScript, (int)ATTRIBUTE_SCRIPT.POWER );

				//-----------------------------------------------------------------------------
				//Script以下のElement

				//ブランチリスト
				Element elemBranchList = elemScript.Elements[ ( int ) ELEMENT_SCRIPT.ELSC_BRANCH ];

				//個数
				int numBranch = int.Parse ( elemBranchList.Attributes[ 0 ].Value );

				//ブランチ
				foreach ( Element elemBranch in elemBranchList.Elements )
				{
					//代入用に新規作成
					Branch0 branch = new Branch0 ();

#if false
					//CommandId
					int ic = ( int ) ELEMENT_BRANCH.ELBR_COMMAND_INDEX;
					branch.IndexCommand = int.Parse ( elemBranch.Attributes[ ic ].Value );

					//ActionId
					int ia = ( int ) ELEMENT_BRANCH.ELBR_ACTION_INDEX;
					branch.IndexAction = int.Parse ( elemBranch.Attributes[ ia ].Value );
#endif
					//CommandName
					int ic = ( int ) ELEMENT_BRANCH.ELBR_COMMAND_NAME;
					branch.NameCommand = elemBranch.Attributes[ ic ].Value;
					//ActionName
					int ia = ( int ) ELEMENT_BRANCH.ELBR_ACTION_NAME;
					branch.NameAction = elemBranch.Attributes[ ia ].Value;

					//Frame
					int iFrame = ( int ) ELEMENT_BRANCH.ELBR_FRAME;
					branch.Frame = int.Parse ( elemBranch.Attributes[ iFrame ].Value );

					//スクリプトに登録
					//(Command,Actionの参照は最後に登録する)
					script.ListBranch.Add ( branch );
				}

				//-----------------------------------------------------------------------------
				//Efジェネレートリスト
				Element elemEfGenerateList = elemScript.Elements[ ( int ) ELEMENT_SCRIPT.ELSC_EFGNRT ];

				foreach ( Element elemEfGenerate in elemEfGenerateList.Elements )
				{
					EffectGenerate efGnrt = new EffectGenerate ();
					
					//Nameを読込
					int iName = ( int ) ELEMENT_EFGNRT.ELEG_NAME;
					efGnrt.Name = elemEfGenerate.Attributes[ iName ].Value;

					//EfIDを読込
//					int efid = ( int ) ELEMENT_EFGNRT.ELEG_EFID;
//					efGnrt.Id = int.Parse ( elemEfGenerate.Attributes[ efid ].Value );

					//EfIDを読込
					int efname = ( int ) ELEMENT_EFGNRT.ELEG_EFNAME;
					efGnrt.EfName = elemEfGenerate.Attributes[ efname ].Value;

					//PtGnrtを読込
					int iptx = ( int ) ELEMENT_EFGNRT.ELEG_PT_X;
					int pt_x = int.Parse ( elemEfGenerate.Attributes[ iptx ].Value );
					int ipty = ( int ) ELEMENT_EFGNRT.ELEG_PT_Y;
					int pt_y = int.Parse ( elemEfGenerate.Attributes[ ipty ].Value );
					efGnrt.Pt = new Point ( pt_x, pt_y );

					//Z位置
					int iptz = ( int ) ELEMENT_EFGNRT.ELEG_PT_Z;
					efGnrt.Z = int.Parse ( elemEfGenerate.Attributes[ iptz ].Value );

					//生成
					int ignrt = ( int ) ELEMENT_EFGNRT.ELEG_GNRT;
					efGnrt.Gnrt = bool.Parse ( elemEfGenerate.Attributes[ ignrt ].Value );

					//繰返
					int iloop = ( int ) ELEMENT_EFGNRT.ELEG_LOOP;
					efGnrt.Loop = bool.Parse ( elemEfGenerate.Attributes[ iloop ].Value );

					//位置同期
					int isync = ( int ) ELEMENT_EFGNRT.ELEG_SYNC;
					efGnrt.Sync = bool.Parse ( elemEfGenerate.Attributes[ isync ].Value );

					//スクリプトに設定
					script.BD_EfGnrt.Add ( efGnrt );
				}

				//-----------------------------------------------------------------------------
				List<Element> le = elemScript.Elements;
				ReadRect ( script.ListCRect, le[ ( int ) ELEMENT_SCRIPT.ELSC_CRECT ] );	/*接触枠*/
				ReadRect ( script.ListARect, le[ ( int ) ELEMENT_SCRIPT.ELSC_ARECT ] );	/*攻撃枠*/
				ReadRect ( script.ListHRect, le[ ( int ) ELEMENT_SCRIPT.ELSC_HRECT ] );	/*当り枠*/
				ReadRect ( script.ListORect, le[ ( int ) ELEMENT_SCRIPT.ELSC_ORECT ] );	/*相殺枠*/		
				//-----------------------------------------------------------------------------

				//シークエンスにスクリプトを追加
				sequence.ListScript.Add ( script );

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
				int x = IOChara.Parse ( elemRect, 0 );
				int y = IOChara.Parse ( elemRect, 1 );
				int w = IOChara.Parse ( elemRect, 2 );
				int h = IOChara.Parse ( elemRect, 3 );
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
					int id = IOChara.Parse ( elemKey, ++ atrbIndex );
					gameKey.IdLvr = id;

					string vLvr = elemKey.Attributes[ ++ atrbIndex ].Value;
					gameKey.Lvr [ id ] = ( GKC_ST ) Enum.Parse ( typeof ( GKC_ST ), vLvr );

					//ボタン
					for ( int i = 0; i < GameKeyCommand.BtnNum; ++ i )
					{
						string v = elemKey.Attributes[ ++ atrbIndex ].Value;
						gameKey.Btn [ i ] = ( GKC_ST ) Enum.Parse ( typeof ( GKC_ST ), v );
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
				Branch0 brc = new Branch0
				{
					Name = elemBrc.Attributes [ ( int ) ATTR_BRANCH.NAME ].Value,
					NameCommand = elemBrc.Attributes [ ( int ) ATTR_BRANCH.CMD_N ].Value,
					NameAction = elemBrc.Attributes [ ( int ) ATTR_BRANCH.ACT_N ].Value,
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
					Name = elemRut.Attributes [ 0 ].Value,
				};

				//ブランチネーム
				foreach ( Element elemBrcName in elemRut.Elements )
				{
					rut.BL_BranchName.Add ( elemBrcName.Attributes [ 0 ].Value );
				}

				//キャラに登録
				chara.BD_Route.Add ( rut );
			}
		}

	}
}
