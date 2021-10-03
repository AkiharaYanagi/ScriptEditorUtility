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

				//イメージID
				//script.ImgID =  IOChara.Parse ( elemScript, (int)ATTRIBUTE_SCRIPT.IMG_ID );

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

				//ルートネームリスト
				Element elemRutList = elemScript.Elements [( int ) ELEMENT_SCRIPT.ELSC_ROUTE ];
				foreach ( Element elemRut in elemRutList.Elements )
				{
					script.BL_RutName.Add ( new TName ( elemRut.Attributes [ 0 ].Value ) );
				}

				//-----------------------------------------------------------------------------
				//Efジェネレートリスト
				Element elemEfGnrtList = elemScript.Elements[ ( int ) ELEMENT_SCRIPT.ELSC_EFGNRT ];
				foreach ( Element elemEfGenerate in elemEfGnrtList.Elements )
				{
					EffectGenerate efGnrt = new EffectGenerate ();
					
					//Nameを読込
					int iName = ( int ) ELEMENT_EFGNRT.ELEG_NAME;
					efGnrt.Name = elemEfGenerate.Attributes[ iName ].Value;

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
					for ( int i = 0; i < GameKeyCommand.LeverCommandNum; ++ i )
					{
						string strLvr = elemKey.Attributes [ ++ atrbIndex ].Value;
						gameKey.Lvr [ i ] = ( GK_ST )Enum.Parse ( typeof ( GK_ST ), strLvr );
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
					NameCommand = elemBrc.Attributes [ ( int ) ATTR_BRANCH.CMD_NAME ].Value,
					NameAction = elemBrc.Attributes [ ( int ) ATTR_BRANCH.ACT_NAME ].Value,
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
					rut.BL_BranchName.Add ( new TName ( name ) );
				}

				//キャラに登録
				chara.BD_Route.Add ( rut );
			}
		}

	}
}
