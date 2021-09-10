﻿using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace ScriptEditor
{
	using BD_IDT = BindingDictionary < ImageData >;

	//==================================================================================
	//	SaveChara
	//		キャラのスクリプトをDocument形式、
	//		イメージアーカイブをpngのbinary形式で、
	//		両方を.datファイルに書出する
	//		また、確認用にDocumentのみテキストファイルにも書出する
	//==================================================================================
	public partial class SaveChara
	{
		//-------------------------------------------------------------
		//コンストラクタ
		//引数　filename : 保存ファイル名, chara : 保存対象キャラ
		//-------------------------------------------------------------
		public SaveChara ( string filename, Chara chara )
		{
			//データが無かったら何もしない
			if ( chara == null ) { return; }

			//キャラスクリプトをDocument形式でテキストメモリストリームに変換
//			MemoryStream mstrmChara = GetTextMemoryStream ( chara );
			CharaToDoc chtom = new CharaToDoc ();
			MemoryStream mstrmChara = chtom.Run ( chara );

			//ストリームリーダにセット
			StreamReader strmReaderChara = new StreamReader ( mstrmChara, Encoding.UTF8 );

			//==========================================================================
			//確認用テキストファイル(.txt)書出

			//ファイル名の拡張子を.datから.txtに変える
			string filenameTxt = Path.GetFileNameWithoutExtension ( filename ) + ".txt";

			//ファイルにテキスト書出用ストリームを設定する
			StreamWriter strmWriter = new StreamWriter ( filenameTxt, false, Encoding.UTF8 );

			//書出
			mstrmChara.Seek ( 0, SeekOrigin.Begin );			//先頭から
			strmWriter.Write ( strmReaderChara.ReadToEnd () );	//最後まで書出し
			strmWriter.Close ();


			//==========================================================================
			//(スクリプト + イメージ) メモリストリーム書出

			//------------------------------
			//スクリプト部
			//メモリストリーム
			MemoryStream mstrmScript = new MemoryStream ();

#if false
			BinaryWriter biWriterScript = new BinaryWriter ( mstrmScript, Encoding.UTF8 );

			//キャラスクリプトを先頭から読み、ファイル書出用にバイナリで書き込む
			mstrmChara.Seek ( 0, SeekOrigin.Begin );
			BinaryReader biReaderChara = new BinaryReader ( mstrmChara, Encoding.UTF8 );

			//byte毎に読み込む
			int buf = 0;
			while ( -1 != ( buf = biReaderChara.Read () ) )
			{
				biWriterScript.Write ( ( byte ) buf );
			}
			biWriterScript.Flush ();
#endif
			//マルチバイト文字を扱うためStreamWriterとUTF8を用いる
			StreamWriter strmWriterScp = new StreamWriter ( mstrmScript, Encoding.UTF8 );

			//キャラスクリプトを先頭から読み、ファイル書出用にバイナリで書き込む
			mstrmChara.Seek ( 0, SeekOrigin.Begin );
			strmWriterScp.Write ( strmReaderChara.ReadToEnd () );
			strmWriterScp.Flush ();


			//------------------------------
			//イメージバイナリデータ部
			//メモリストリーム
			MemoryStream mstrmImage = new MemoryStream ();
			BinaryWriter biWriterImage = new BinaryWriter ( mstrmImage );

			WriteListImage ( biWriterImage, chara.behavior.BD_Image );
			WriteListImage ( biWriterImage, chara.garnish.BD_Image );

			biWriterImage.Flush ();


			//==========================================================================
			//(スクリプト + イメージ) ファイル(.dat)書出

			//書出対象ファイル
			FileStream fstrm = new FileStream ( filename, FileMode.Create, FileAccess.Write );
//			BinaryWriter biWriterFile = new BinaryWriter ( fstrm, Encoding.ASCII );
			BinaryWriter biWriterFile = new BinaryWriter ( fstrm, Encoding.UTF8 );

			//ストリーム読書用一時領域
			const int size = 4096;	//バッファサイズ
			byte[] buffer = new byte[ size ];	//バッファ
			int numBytes;		//バイト数

			//ver書出
			biWriterFile.Write ( ( uint ) CONST.VER );

			//スクリプト部・サイズ書出
			biWriterFile.Write ( ( uint ) mstrmScript.Length );

			//スクリプト部・ストリーム書出
			mstrmScript.Seek ( 0, SeekOrigin.Begin );
			while ( ( numBytes = mstrmScript.Read ( buffer, 0, size ) ) > 0 )
			{
				biWriterFile.Write ( buffer, 0, numBytes );
			}

			//イメージ部・ストリーム書出
			mstrmImage.Seek ( 0, SeekOrigin.Begin );
			while ( ( numBytes = mstrmImage.Read ( buffer, 0, size ) ) > 0 )
			{
				biWriterFile.Write ( buffer, 0, numBytes );
			}

			Debug.WriteLine ( "fstrm.Length = " + fstrm.Length );

			//終了
			biWriterFile.Close ();
//			biWriterScript.Close ();
			biWriterImage.Close ();
			strmReaderChara.Close ();
		}

		//イメージリストの書出
		private void WriteListImage ( BinaryWriter biWriterImage, BD_IDT imagelist )
		{
			//ストリーム読込→書出用
			const int size = 4096;	//バッファサイズ
			byte[] buffer = new byte[ size ];	//バッファ
			int numBytes;		//バイト数

			//イメージリスト
			IEnumerable list =  imagelist.GetBindingList ();
			foreach ( ImageData imageData in list )
			{
				//各イメージを一時領域に書出
				MemoryStream tempMstrm = new MemoryStream ();
				imageData.Img.Save ( tempMstrm, ImageFormat.Png );

				//サイズの書込
				biWriterImage.Write ( ( uint ) tempMstrm.Length );

				Debug.Write ( imageData.Name + ", " + ( uint ) tempMstrm.Length );

				//実データの書込
				tempMstrm.Seek ( 0, SeekOrigin.Begin );
				int sumBytes = 0;
				while ( ( numBytes = tempMstrm.Read ( buffer, 0, size ) ) > 0 )
				{
					biWriterImage.Write ( buffer, 0, numBytes );
					sumBytes += numBytes;
				}
				
				//Debug.Write ( " : sumBytes = " + sumBytes + "\n" );
				tempMstrm.Close ();
			}
		}

#if false
		//キャラからをスクリプトの対象となる値を読み込み、
		//キャラスクリプトとしてテキストメモリストリームに変換
		private MemoryStream GetTextMemoryStream ( Chara chara )
		{
			MemoryStream mstrm = new MemoryStream ();
			StreamWriter strmWriter = new StreamWriter ( mstrm, Encoding.UTF8 );

			//-------------------------------------------------------
			//全体タグとバージョン
			strmWriter.Write ( "<Chara>\n" );
			strmWriter.Write ( "<ver>" + CONST.VER + "</ver>\n\n" );

			//-------------------------------------------------------
			//イメージリスト
			BindingDictionary < ImageData > imageList = chara.behavior.BD_Image;
			strmWriter.Write ( "<ImageList Num=\"" + imageList.GetBindingList().Count + "\">\n" );
			foreach ( ImageData imageData in imageList.GetBindingList() )
			{
				strmWriter.Write ( "\t<Image Name=\"" + imageData.Name + "\"></Image>\n" );
			}
			strmWriter.Write ( "</ImageList>\n" );

			//-------------------------------------------------------
			//Efイメージリスト
			BindingDictionary < ImageData > efImageList = chara.garnish.BD_Image;
			strmWriter.Write ( "<EfImageList Num=\"" + efImageList.GetBindingList().Count + "\">\n" );
			foreach ( ImageData imageData in efImageList.GetBindingList () )
			{
				strmWriter.Write ( "\t<EfImage Name=\"" + imageData.Name + "\"></EfImage>\n" );
			}
			strmWriter.Write ( "</EfImageList>\n" );

			//-------------------------------------------------------
			//アクションリスト
			strmWriter.Write ( "<ActionList Num=\"" + chara.behavior.BD_Sequence.GetBindingList().Count + "\">\n" );

			//アクション
			foreach ( Action action in chara.behavior.BD_Sequence.GetBindingList() )
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
				WriteListScript ( strmWriter, chara, action.ListScript );

				strmWriter.Write ( "\t</Action>\n" );
			}
			strmWriter.Write ( "</ActionList>\n" );

			//-------------------------------------------------------
			//エフェクトリスト
			strmWriter.Write ( "<EfList Num=\"" + chara.garnish.BD_Sequence.GetBindingList().Count + "\">\n" );

			//エフェクト
			foreach ( Effect effect in chara.garnish.BD_Sequence.GetBindingList() )
			{
				//attribute値はダブルクォーテーションで囲む
				strmWriter.Write ( "\t<Effect" );
				strmWriter.Write ( " Name=\"" + effect.Name + "\"" );	//名前
				strmWriter.Write ( ">\n" );

				//スクリプト
				WriteListScript ( strmWriter, chara, effect.ListScript );

				strmWriter.Write ( "\t</Effect>\n" );
			}
			strmWriter.Write ( "</EfList>\n" );

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

			//-------------------------------------------------------
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

			//-------------------------------------------------------
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

			//-------------------------------------------------------
			//全体タグ
			strmWriter.Write ( "</Chara>\n" );

			//終了
			strmWriter.Flush ();

			return mstrm;
		}



		//スクリプトの書出
		private void WriteListScript ( StreamWriter strmWriter, Chara chara, List<Script> listScript )
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

#if false
				//ブランチリスト
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
#endif
	}
}
