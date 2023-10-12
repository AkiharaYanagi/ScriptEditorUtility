using System;
using System.Diagnostics;
using System.IO;


namespace ScriptEditor
{
	using BD_ImgDt = BindingDictionary < ImageData >;


	//==================================================================================
	//	キャラデータのテスト (IO, Edit)
	//		対象データは変更されるため、テスト用データを外部から設定する
	//==================================================================================
	public class TestChara
	{
		//コンストラクタ
		public TestChara ()
		{
		}

		//総合テスト
		public void Test ( Chara ch )
		{
			//EditCharaに設定
			EditChara.Inst.SetCharaDara ( ch );

			//コピーテスト
			Chara copyChara = new Chara ();
			TestCopyChara ( copyChara, ch );

			//初期化(Clear)テスト
			TestClearChara ( copyChara );

			//名前指定
			TestNameAssign ( ch );

			//IOテスト
			TestIO ( ch );
		}



		//=====================================================================
		//	個別テスト
		//=====================================================================
		//初期化テスト
		public void TestClearChara ( Chara chara )
		{
			//test chara.Clear()
			//		データ個数が０に初期化されているかテスト
			chara.Clear ();
			Debug.Assert ( 0 == chara.behavior.BD_Sequence.Count() );
			Debug.Assert ( 0 == chara.garnish.BD_Sequence.Count() );
			Debug.Assert ( 0 == chara.BD_Command.Count () );
			Debug.Assert ( 0 == chara.BD_Branch.Count () );
			Debug.Assert ( 0 == chara.BD_Route.Count () );
		}

		//コピーテスト
		public void TestCopyChara ( Chara dstChara , Chara srcChara )
		{
			dstChara.Copy ( srcChara );

			Equal ( dstChara, srcChara );
		}

		//同値テスト
		public void Equal ( Chara ch0, Chara ch1 )
		{
			//リスト個数が同数かテスト
			Behavior bhv0 = ch0.behavior;
			Behavior bhv1 = ch1.behavior;
			Debug.Assert ( bhv0.BD_Sequence.Count() == bhv1.BD_Sequence.Count() );
			Debug.Assert ( bhv0.BD_Image.Count() == bhv1.BD_Image.Count() );

			Garnish g0 = ch0.garnish;
			Garnish g1 = ch1.garnish;
			Debug.Assert ( g0.BD_Sequence.GetBindingList().Count == g1.BD_Sequence.GetBindingList().Count );
			Debug.Assert ( g0.BD_Image.Count() == g1.BD_Image.Count() );

			Debug.Assert ( ch0.BD_Command.Count () == ch1.BD_Command.Count () );
			Debug.Assert ( ch0.BD_Branch.Count () == ch1.BD_Branch.Count () );
			Debug.Assert ( ch0.BD_Route.Count () == ch1.BD_Route.Count () );
		}

		//名前指定テスト
		public void TestNameAssign ( Chara ch )
		{
			try
			{
				_TestNameAssign ( ch );
			}
			catch ( Exception e )
			{
				Debug.Write ( e );
				Debug.Assert ( false );
			}
		}

		//イメージ、アクション、コマンド、ブランチ、ルート
		//無いときExceptionをthrow
		string noname = "";
		public void _TestNameAssign ( Chara ch )
		{
			BD_ImgDt bd_img = ch.behavior.BD_Image;

			//スクリプト -> イメージ, ルート
			foreach ( Sequence sqc in ch.behavior.BD_Sequence.GetBindingList () )
			{
				int i = 0;
				foreach ( Script scp in sqc.ListScript )
				{
					noname = scp.ImgName;

					//イメージ
					if ( ! bd_img.ContainsKey ( scp.ImgName ) )
					{
						throw new Exception ( sqc.Name + "[" + i + "]-> " + scp.ImgName );
					}

					//ルート
					foreach ( TName tn in scp.BD_RutName.GetBindingList () )
					{
						noname = tn.Name;
						ch.BD_Route.Try_Exist ( tn.Name );
					}

					++ i;
				}
			}

			//ブランチ -> コマンド, アクション
			foreach ( Branch brc in ch.BD_Branch.GetBindingList () )
			{
				ch.BD_Command.Try_Exist ( brc.NameCommand );
				ch.behavior.BD_Sequence.Try_Exist ( brc.NameSequence );
			}

			//ルート -> ブランチ名
			foreach ( Route rut in ch.BD_Route.GetBindingList () )
			{
				foreach ( TName tn in rut.BD_BranchName.GetBindingList () )
				{
					ch.BD_Branch.Try_Exist ( tn.Name );
				}
			}
		}

		//-----------------------------------------------------------
		//IOテスト

		//対象ファイル名
		private const string Filename = "testChara.dat";
		private const string FilenameBin = "testCharaBin.dat";

		public void TestIO ( Chara ch )
		{
			TestIO_Document ( ch );
			TestIO_TextFile ( ch );
			TestIO_Bin ( ch );
		}

		//ドキュメント
		public void TestIO_Document ( Chara ch )
		{
			CharaToDoc ctd = new CharaToDoc ();
			MemoryStream ms = ctd.Run ( ch );
			Document doc = new Document ( ms );

			Chara ch_new = new Chara ();
			DocToChara dtc = new DocToChara ();
			dtc.Load ( doc, ch_new );

			Equal ( ch, ch_new );

			Debug.WriteLine ( "TestIO_Document: OK." );
		}

		//テキストファイル
		public void TestIO_TextFile ( Chara ch )
		{
			SaveChara saveChara = new SaveChara ();
			saveChara.Do ( Filename, ch );

			LoadChara loadChara = new LoadChara ();
			loadChara.Do ( Filename, ch );

			Debug.WriteLine ( "TestIO_TextFile: OK." );
		}

		//バイナリファイル
		public void TestIO_Bin ( Chara ch )
		{
			SaveCharaBin saveCharaBin = new SaveCharaBin ();
			saveCharaBin.Do ( FilenameBin, ch );

			LoadCharaBin loadCharaBin = new LoadCharaBin ();
			loadCharaBin.Do ( FilenameBin, ch );

			Debug.WriteLine ( "TestIO_Bin: OK." );
		}


	}

}
