using System;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Collections.Generic;
using System.Drawing;

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
	public class CharaToDoc
	{
		//内部参照用
		private Chara Chara = null;

		public MemoryStream Run ( Chara ch )
		{
			Chara = ch;

			//対象メモリストリーム
			MemoryStream mstrm = new MemoryStream ();

			//@info using節だと後にストリームが参照できない
			StreamWriter strmWriter = new StreamWriter ( mstrm, Encoding.UTF8 );
//			using ( StreamWriter strmWriter = new StreamWriter ( mstrm, Encoding.UTF8 ) )
			{
			//全体タグとバージョン
			strmWriter.Write ( "<Chara>\n" );
			strmWriter.Write ( "<ver>" + CONST.VER + "</ver>\n\n" );

			//メインイメージリストヘッダ
			WriteListImageHeader ( strmWriter, ch.behavior.BD_Image, "Image" );
			//EFイメージリストヘッダ
			WriteListImageHeader ( strmWriter, ch.garnish.BD_Image, "EfImage" );

			//アクションリスト
			WriteSequence ( strmWriter, ch.behavior.BD_Sequence, "Action", Func_WriteAction );
			//エフェクトリスト
			WriteSequence ( strmWriter, ch.garnish.BD_Sequence, "Effect", Func_WriteEffect );

			//コマンドリスト
			WriteCommandList ( strmWriter, ch.BD_Command );

			//ブランチリスト
			WriteBranchList ( strmWriter, ch.BD_Branch );

			//ルートリスト
			WriteRouteList ( strmWriter, ch.BD_Route );

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

			strmWriter.Flush ();
			} //using strmWriter

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
			int nextID = Chara.behavior.BD_Sequence.IndexOf ( act.NextActionName );

			sw.Write ( " Name=\"" + act.Name + "\"" );					//名前
			sw.Write ( " NextName=\"" + act.NextActionName + "\"" );	//次アクション名
			sw.Write ( " NextID=\"" + nextID.ToString() + "\"" );		//次アクションID
			sw.Write ( " Category=\"" + (int)act.Category + "\"" );		//アクション属性
			sw.Write ( " Posture=\"" + (int)act.Posture + "\"" );		//アクション体勢
			sw.Write ( " Balance=\"" + act._Balance + "\"" );			//消費バランス値
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
		private void WriteListScript ( StreamWriter strmWriter, List<Script> listScript)
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

				//ルートリスト
				strmWriter.Write ( "\t\t\t<RouteList>\n" );
				//ルート
				foreach ( TName rutName in script.BL_RutName )
				{
					int indexRoute = Chara.BD_Route.IndexOf ( rutName.Name );

					strmWriter.Write ( "\t\t\t\t<Route" );
					strmWriter.Write ( " Name=\"" + rutName.Name + "\" ID=\"" + indexRoute.ToString() + "\"" );
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

					//レバー(全方向)
					for ( int i = 0; i < GameKeyCommand.LeverCommandNum; ++ i )
					{
						sw.Write ( " Key_" + i.ToString() + "=\"" );
						sw.Write ( gameKey.Lvr[i].ToString () + "\"" );
					}

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
				int commandID = Chara.BD_Command.IndexOf ( brc.NameCommand );
				int actionID = Chara.behavior.BD_Sequence.IndexOf ( brc.NameAction );

				sw.Write ( "\t<Branch" );
				sw.Write ( " Name=\"" + brc.Name + "\"" );					//名前
				sw.Write ( " NameCommand=\"" + brc.NameCommand + "\"" );	//コマンド名
				sw.Write ( " CommandID=\"" + commandID + "\"" );			//コマンドID
				sw.Write ( " NameAction=\"" + brc.NameAction + "\"" );		//アクション名
				sw.Write ( " ActionID=\"" + actionID + "\"" );				//アクションID
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
				sw.Write ( " Summary=\"" + rut.Summary + "\">\n" );
				//ブランチネームリスト
				BindingList < TName > bl_brrt = rut.BL_BranchName;
				sw.Write ( "\t\t<BranchNameList Num=\"" + bl_brrt.Count + "\">\n" );
				foreach ( TName tn in bl_brrt )
				{
					int BranchID = Chara.BD_Branch.IndexOf ( tn.Name );

					sw.Write ( "\t\t\t<BranchName Name=\"" + tn.Name + "\"" );
					sw.Write ( " ID=\"" + BranchID.ToString() + "\"" );
					sw.Write ( "></BranchName>\n" );
				}
				sw.Write ( "\t\t</BranchNameList>\n" );
				sw.Write ( "\t</Route>\n" );
			}
			sw.Write ( "</RouteList>\n" );
		}

	}
}
