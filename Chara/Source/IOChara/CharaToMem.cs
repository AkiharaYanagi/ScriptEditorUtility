using System;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;

namespace ScriptEditor
{
	using BD_IDT = BindingDictionary < ImageData >;
	using BD_SQC = BindingDictionary < Sequence >;
	using BL_SQC = BindingList < Sequence >;
	using BD_CMD = BindingDictionary < Command >;
	using BD_BRC = BindingDictionary < Branch >;
	using BD_RUT = BindingDictionary < Route >;

	//============================================================
	//キャラからをスクリプトの対象となる値を読み込み、
	//キャラスクリプトとしてテキストメモリストリームに変換
	//============================================================
	public class CharaToMem
	{
		public MemoryStream Run ( Chara chara )
		{
			//対象メモリストリーム
			MemoryStream mstrm = new MemoryStream ();

			StreamWriter strmWriter = new StreamWriter ( mstrm, Encoding.UTF8 );
//			using ( StreamWriter strmWriter = new StreamWriter ( mstrm, Encoding.UTF8 ) )
			{
			//全体タグとバージョン
			strmWriter.Write ( "<Chara>\n" );
			strmWriter.Write ( "<ver>" + CONST.VER + "</ver>\n\n" );

			//メインイメージリストヘッダ
			WriteListImageHeader ( strmWriter, chara.behavior.BD_Image, "Image" );
			//EFイメージリストヘッダ
			WriteListImageHeader ( strmWriter, chara.garnish.BD_Image, "EfImage" );
#if false
			//-------------------------------------------------------
			//メインイメージリストヘッダ
			BD_T imageList = chara.behavior.BD_Image;
			strmWriter.Write ( "<ImageList Num=\"" + imageList.GetBindingList().Count + "\">\n" );
			foreach ( ImageData imageData in imageList.GetBindingList() )
			{
				strmWriter.Write ( "\t<Image Name=\"" + imageData.Name + "\"></Image>\n" );
			}
			strmWriter.Write ( "</ImageList>\n" );

			//-------------------------------------------------------
			//Efイメージリストヘッダ
			BD_T efImageList = chara.garnish.BD_Image;
			strmWriter.Write ( "<EfImageList Num=\"" + efImageList.GetBindingList().Count + "\">\n" );
			foreach ( ImageData imageData in efImageList.GetBindingList () )
			{
				strmWriter.Write ( "\t<EfImage Name=\"" + imageData.Name + "\"></EfImage>\n" );
			}
			strmWriter.Write ( "</EfImageList>\n" );
#endif
			//アクションリスト
			WriteSequence ( strmWriter, chara.behavior.BD_Sequence, "Action", Func_WriteAction );
			//エフェクトリスト
			WriteSequence ( strmWriter, chara.garnish.BD_Sequence, "Effect", Func_WriteEffect );
#if false
			//-------------------------------------------------------
			//アクションリスト
			BL_SQC BL_Act = chara.behavior.BD_Sequence.GetBindingList();
			strmWriter.Write ( "<ActionList Num=\"" + BL_Act.Count + "\">\n" );

			//アクション
			foreach ( Action action in BL_Act )
			{
				//attribute値はダブルクォーテーションで囲む
				strmWriter.Write ( "\t<Action" );
				strmWriter.Write ( " Name=\"" + action.Name + "\"" );					//名前
				strmWriter.Write ( " NextName=\"" + action.NextActionName + "\"" );		//次アクション名
				strmWriter.Write ( " Category=\"" + (int)action.Category + "\"" );		//アクション属性
				strmWriter.Write ( " Posture=\"" + (int)action.Posture + "\"" );		//アクション体勢
				strmWriter.Write ( " Balance=\"" + action._Balance + "\"" );		//消費バランス値
				strmWriter.Write ( ">\n" );

				//スクリプト
				WriteListScript ( strmWriter, action.ListScript );

				strmWriter.Write ( "\t</Action>\n" );
			}
			strmWriter.Write ( "</ActionList>\n" );

			//-------------------------------------------------------
			//エフェクトリスト
			BL_SQC BL_Ef = chara.behavior.BD_Sequence.GetBindingList();
			strmWriter.Write ( "<EfList Num=\"" + BL_Ef.Count + "\">\n" );

			//エフェクト
			foreach ( Effect effect in BL_Ef )
			{
				//attribute値はダブルクォーテーションで囲む
				strmWriter.Write ( "\t<Effect" );
				strmWriter.Write ( " Name=\"" + effect.Name + "\"" );	//名前
				strmWriter.Write ( ">\n" );

				//スクリプト
				WriteListScript ( strmWriter, effect.ListScript );

				strmWriter.Write ( "\t</Effect>\n" );
			}
			strmWriter.Write ( "</EfList>\n" );
#endif
			//コマンドリスト
			WriteCommandList ( strmWriter, chara.BD_Command );
#if false
			//-------------------------------------------------------
			//コマンドリスト
			BindingList < Command > ls = chara.BD_Command.GetBindingList ();
			strmWriter.Write ( "<CommandList Num=\"" + ls.Count + "\">\n" );
			//コマンド
			foreach ( Command command in ls )
			{
				strmWriter.Write ( "\t<Command Name=\"" + command.Name + "\"" );
				strmWriter.Write ( " Limit=\"" + command.LimitTime.ToString () + "\">\n" );

				//キー
				foreach ( GameKeyCommand gameKey in command.ListGameKeyCommand )
				{
					strmWriter.Write ( "\t\t<Key" );
					
					//否定
					strmWriter.Write ( " Not=\"" + gameKey.Not.ToString () + "\"" );

					//レバー
#if false
					for ( int i = 0; i < GameKeyCommand.LeverCommandNum; ++ i )
					{
						strmWriter.Write ( " Key_" + i.ToString() + "=\"" );
						strmWriter.Write ( gameKey.Lvr[i].ToString () + "\"" );
					}
#endif
					strmWriter.Write ( " IdLvr =\"" );
					strmWriter.Write ( gameKey.IdLvr + "\"" );
					strmWriter.Write ( " Lvr =\"" + gameKey.Lvr[gameKey.IdLvr].ToString () + "\"" );

					//ボタン
					for ( int i = 0; i < GameKeyCommand.BtnNum; ++ i )
					{
						strmWriter.Write ( " Btn_" + i.ToString () + " =\"" + gameKey.Btn[ i ].ToString () + "\"" );
					}
					strmWriter.Write ( ">" );

					strmWriter.Write ( "</Key>\n" );
				}
				strmWriter.Write ( "\t</Command>\n" );
			}
			strmWriter.Write ( "</CommandList>\n" );
#endif
			//ブランチリスト
			WriteBranchList ( strmWriter, chara.BD_Branch );
#if false
			//-------------------------------------------------------
			//ブランチリスト
			BindingList < Branch > bl_brc = chara.BD_Branch.GetBindingList ();
			strmWriter.Write ( "<BranchList Num=\"" + bl_brc.Count + "\">\n" );
			//ブランチ
			foreach ( Branch brc in bl_brc )
			{
				strmWriter.Write ( "\t<Branch" );
				strmWriter.Write ( " Name=\"" + brc.Name + "\"" );					//名前
				strmWriter.Write ( " NameCommand=\"" + brc.NameCommand + "\"" );	//コマンド名
				strmWriter.Write ( " NameAction=\"" + brc.NameAction + "\"" );		//アクション名
				strmWriter.Write ( " Frame=\"" + brc.Frame + "\"" );				//遷移先フレーム
				strmWriter.Write ( ">\n" );
				strmWriter.Write ( "\t</Branch>\n" );
			}
			strmWriter.Write ( "</BranchList>\n" );
#endif
			//ルートリスト
			WriteRouteList ( strmWriter, chara.BD_Route );
#if false
			//ルートリスト
			BindingList < Route > bl_rut = chara.BD_Route.GetBindingList ();
			strmWriter.Write ( "<RouteList>\n" );
			//ルート
			foreach ( Route rut in bl_rut )
			{
				strmWriter.Write ( "\t<Route Name=\"" + rut.Name + "\"" );
				strmWriter.Write ( " Summary=\"" + rut.Summary + "\" >\n" );
				//ブランチネームリスト
				BindingList < string > bl_brrt = rut.BL_BranchName;
				strmWriter.Write ( "\t\t<BranchNameList Num=\"" + bl_brrt.Count + "\">\n" );
				foreach ( string name in bl_brrt )
				{
					strmWriter.Write ( "\t\t\t<BranchName Name=\"" + name + "\">" );
					strmWriter.Write ( "</BranchName>\n" );
				}
				strmWriter.Write ( "\t\t</BranchNameList>\n" );
				strmWriter.Write ( "\t</Route>\n" );
			}
			strmWriter.Write ( "</RouteList>\n" );
#endif

			//基本状態アクションID
#if false
			strmWriter.Write ( "<BasicAction>\n" );

			int indexSpAction = 0;
			string[] enumnames = Enum.GetNames ( typeof ( Chara.BasicAction ) );
			foreach ( string str in enumnames )
			{
				strmWriter.Write ( "\t<" + str );
				strmWriter.Write ( " ID=\"" + chara.BsAction[ indexSpAction ].i + "\"" );
				strmWriter.Write ( " Name=\"" + chara.behavior.Bldct_sqc.GetBindingList()[ chara.BsAction[ indexSpAction ].i ].ToString () + "\"" );
				strmWriter.Write ( "></" + str + ">\n" );
				++indexSpAction;
			}

			strmWriter.Write ( "</BasicAction>\n" );
#endif
			//全体タグ
			strmWriter.Write ( "</Chara>\n" );

			} //using strmWriter
			strmWriter.Flush ();

			return mstrm;
		}


		//=================================================================================
		//個別項目

		//イメージリストヘッダの書出
		private void WriteListImageHeader ( StreamWriter strmWriter, BD_IDT imageList, string tagname )
		{
			string tn0 = tagname;
			string tn_l = tagname + "List";
			strmWriter.Write ( "<" + tn_l + " Num=\"" + imageList.GetBindingList().Count + "\">\n" );
			foreach ( ImageData imageData in imageList.GetBindingList() )
			{
				strmWriter.Write ( "\t<" + tn0 + " Name=\"" + imageData.Name + "\"></" + tn0 + ">\n" );
			}
			strmWriter.Write ( "</" + tn_l + ">\n" );
		}


		//シークエンス(アクション, エフェクト)リストの書出
		private delegate void Func_WriteSqc ( StreamWriter sw, Sequence sqc );
		private void WriteSequence ( StreamWriter sw, BD_SQC bd_sqc, string tagname, Func_WriteSqc fws )
		{
			BL_SQC bl_sqc = bd_sqc.GetBindingList ();
			string tn = tagname;
			string tn_l = tagname + "List";

			//シークエンスリスト
			sw.Write ( "<" + tn_l + " Num=\"" + bl_sqc.Count + "\">\n" );
			foreach ( Sequence sqc in bl_sqc )
			{
				sw.Write ( "\t<" + tn );
				fws ( sw, sqc );	//(アクション, エフェクト)個別アトリビュート
				WriteListScript ( sw, sqc.ListScript );	//スクリプト
				sw.Write ( "\t</" + tn + ">\n" );
			}
			sw.Write ( "</" + tn_l + ">\n" );
		}

		//アクションの書出
		private void Func_WriteAction ( StreamWriter sw, Sequence sqc )
		{
			Action act = (Action)sqc;

			sw.Write ( " Name=\"" + act.Name + "\"" );					//名前
			sw.Write ( " NextName=\"" + act.NextActionName + "\"" );		//次アクション名
			sw.Write ( " Category=\"" + (int)act.Category + "\"" );		//アクション属性
			sw.Write ( " Posture=\"" + (int)act.Posture + "\"" );		//アクション体勢
			sw.Write ( " Balance=\"" + act._Balance + "\"" );		//消費バランス値
			sw.Write ( ">\n" );
		}

		//エフェクトの書出
		private void Func_WriteEffect ( StreamWriter sw, Sequence sqc )
		{
			Effect ef = (Effect)sqc;

			sw.Write ( " Name=\"" + ef.Name + "\"" );					//名前
			sw.Write ( ">\n" );
		}


		//スクリプトの書出
		private void WriteListScript ( StreamWriter strmWriter, List<Script> listScript )
		{
			//スクリプト
			foreach ( Script script in listScript )
			{
				strmWriter.Write ( "\t\t<Script" );
				strmWriter.Write ( " group=\"" + script.Group + "\"" );
				strmWriter.Write ( " Image Name=\"" + script.ImgName + "\"" );
				strmWriter.Write ( " X=\"" + script.Pos.X + "\" Y=\"" + script.Pos.Y + "\"" );
				strmWriter.Write ( " VX=\"" + script.Vel.X + "\" VY=\"" + script.Vel.Y + "\"" );
				strmWriter.Write ( " AX=\"" + script.Acc.X + "\" AY=\"" + script.Acc.Y + "\"" );
				strmWriter.Write ( " CLC_ST=\"" + (int)script.CalcState + "\"" );
				strmWriter.Write ( " power=\"" + script.Power + "\"" );
				strmWriter.Write ( ">\n" );

				//ブランチリスト
#if false
				strmWriter.Write ( "\t\t\t<BranchList Num=\"" + script.ListBranch.Count() + "\">\n" );
				//ブランチ
				foreach ( Branch0 branch in script.ListBranch.GetBindingList () )
				{
					strmWriter.Write ( "\t\t\t\t<Branch" );
//					strmWriter.Write ( " Command=\"" + branch.IndexCommand + "\"" );
					strmWriter.Write ( " CommandName=\"" + branch.NameCommand + "\"" );
//					strmWriter.Write ( " Action=\"" + branch.IndexAction + "\"" );
					strmWriter.Write ( " ActionName=\"" + branch.NameAction + "\"" );
					strmWriter.Write ( " Frame=\"" + branch.Frame + "\"" );
					strmWriter.Write ( "></Branch>\n" );
				}
				strmWriter.Write ( "\t\t\t</BranchList>\n" );
#endif
				//ルートリスト
				strmWriter.Write ( "\t\t\t<RouteList>\n" );
				//ルート
				foreach ( string rutName in script.BL_RutName )
				{
					strmWriter.Write ( "\t\t\t\t<Route" );
					strmWriter.Write ( " Name=\"" + rutName + "\"" );
					strmWriter.Write ( "></Route>\n" );
				}
				strmWriter.Write ( "\t\t\t</RouteList>\n" );


				//Efジェネレートリスト
				strmWriter.Write ( "\t\t\t<EfGenerateList Num=\"" + script.BD_EfGnrt.Count() + "\">\n" );
				//Efジェネレート
				foreach ( EffectGenerate efGnrt in script.BD_EfGnrt.GetBindingList () )
				{
					strmWriter.Write ( "\t\t\t\t<EfGenerate" );
					strmWriter.Write ( " Name=\"" + efGnrt.Name + "\"" );
					strmWriter.Write ( " EfName=\"" + efGnrt.EfName + "\"" );
					strmWriter.Write ( " EfGnrt_PtX=\"" + efGnrt.Pt.X + "\"" );
					strmWriter.Write ( " EfGnrt_PtY=\"" + efGnrt.Pt.Y + "\"" );
					strmWriter.Write ( " EfGnrt_PtZ=\"" + efGnrt.Z + "\"" );
					strmWriter.Write ( " Gnrt=\"" + efGnrt.Gnrt + "\"" );
					strmWriter.Write ( " Loop=\"" + efGnrt.Loop + "\"" );
					strmWriter.Write ( " Sync=\"" + efGnrt.Sync + "\"" );
					strmWriter.Write ( "></EfGenerate>\n" );
				}
				strmWriter.Write ( "\t\t\t</EfGenerateList>\n" );

				WriteListRect ( strmWriter, script.ListCRect, "CollisionRect" );	//ぶつかり枠リスト
				WriteListRect ( strmWriter, script.ListARect, "AttackRect" );		//攻撃枠リスト
				WriteListRect ( strmWriter, script.ListHRect, "HitRect" );			//当り枠リスト
				WriteListRect ( strmWriter, script.ListORect, "OffsetRect" );		//相殺枠リスト

				strmWriter.Write ( "\t\t</Script>\n" );
			}
		}

		//枠の書出	
		private void WriteListRect ( StreamWriter strmWriter, List<Rectangle> lsRect, string name )
		{
			//枠リスト
			strmWriter.Write ( "\t\t\t<" + name + "List Num=\"" + lsRect.Count + "\">\n" );
			//枠
			foreach ( Rectangle rect in lsRect )
			{
				strmWriter.Write ( "\t\t\t\t<" + name );
				strmWriter.Write ( " X=\"" + rect.X + "\"" );
				strmWriter.Write ( " Y=\"" + rect.Y + "\"" );
				strmWriter.Write ( " W=\"" + rect.Width + "\"" );
				strmWriter.Write ( " H=\"" + rect.Height + "\"" );
				strmWriter.Write ( "></" + name + ">\n" );
			}
			strmWriter.Write ( "\t\t\t</" + name + "List>\n" );
		}

		//コマンドリスト
		private void WriteCommandList ( StreamWriter sw, BD_CMD bd_cmd )
		{
			BindingList < Command > ls = bd_cmd.GetBindingList ();
			sw.Write ( "<CommandList Num=\"" + ls.Count + "\">\n" );
			//コマンド
			foreach ( Command command in ls )
			{
				sw.Write ( "\t<Command Name=\"" + command.Name + "\"" );
				sw.Write ( " Limit=\"" + command.LimitTime.ToString () + "\">\n" );

				//キー
				foreach ( GameKeyCommand gameKey in command.ListGameKeyCommand )
				{
					sw.Write ( "\t\t<Key" );
					
					//否定
					sw.Write ( " Not=\"" + gameKey.Not.ToString () + "\"" );

					//レバー
#if false
					for ( int i = 0; i < GameKeyCommand.LeverCommandNum; ++ i )
					{
						strmWriter.Write ( " Key_" + i.ToString() + "=\"" );
						strmWriter.Write ( gameKey.Lvr[i].ToString () + "\"" );
					}
#endif
					sw.Write ( " IdLvr =\"" );
					sw.Write ( gameKey.IdLvr + "\"" );
					sw.Write ( " Lvr =\"" + gameKey.Lvr[gameKey.IdLvr].ToString () + "\"" );

					//ボタン
					for ( int i = 0; i < GameKeyCommand.BtnNum; ++ i )
					{
						sw.Write ( " Btn_" + i.ToString () + " =\"" + gameKey.Btn[ i ].ToString () + "\"" );
					}
					sw.Write ( ">" );

					sw.Write ( "</Key>\n" );
				}
				sw.Write ( "\t</Command>\n" );
			}
			sw.Write ( "</CommandList>\n" );
		}
		
		//ブランチリスト
		private void WriteBranchList ( StreamWriter sw, BD_BRC bd_cmd )
		{
			BindingList < Branch > ls = bd_cmd.GetBindingList ();
			sw.Write ( "<BranchList Num=\"" + ls.Count + "\">\n" );
			//ブランチ
			foreach ( Branch brc in ls )
			{
				sw.Write ( "\t<Branch" );
				sw.Write ( " Name=\"" + brc.Name + "\"" );					//名前
				sw.Write ( " NameCommand=\"" + brc.NameCommand + "\"" );	//コマンド名
				sw.Write ( " NameAction=\"" + brc.NameAction + "\"" );		//アクション名
				sw.Write ( " Frame=\"" + brc.Frame + "\"" );				//遷移先フレーム
				sw.Write ( ">\n" );
				sw.Write ( "\t</Branch>\n" );
			}
			sw.Write ( "</BranchList>\n" );
		}
		
		//ルートリスト
		private void WriteRouteList ( StreamWriter sw, BD_RUT bd_rut )
		{
			BindingList < Route > ls = bd_rut.GetBindingList ();
			sw.Write ( "<RouteList Num=\"" + ls.Count + "\">\n" );
			//ブランチ
			foreach ( Route rut in ls )
			{
				sw.Write ( "\t<Route Name=\"" + rut.Name + "\"" );
				sw.Write ( " Summary=\"" + rut.Summary + "\" >\n" );
				//ブランチネームリスト
				BindingList < TName > bl_brrt = rut.BL_BranchName;
				sw.Write ( "\t\t<BranchNameList Num=\"" + bl_brrt.Count + "\">\n" );
				foreach ( TName tn in bl_brrt )
				{
					sw.Write ( "\t\t\t<BranchName Name=\"" + tn.Name + "\">" );
					sw.Write ( "</BranchName>\n" );
				}
				sw.Write ( "\t\t</BranchNameList>\n" );
				sw.Write ( "\t</Route>\n" );
			}
			sw.Write ( "</RouteList>\n" );
		}

	}
}
