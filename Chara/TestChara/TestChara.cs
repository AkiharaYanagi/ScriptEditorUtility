using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Data.SqlTypes;


namespace ScriptEditor
{
	using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

	using BD_ImgDt = BindingDictionary < ImageData >;


	//==================================================================================
	//	キャラデータのテスト (IO, Edit)
	//		対象データは変更されるため、テスト用データを外部から設定する
	//==================================================================================
	public class TestChara
	{
		//SE, VCチェック用テキストファイルDir
		public string SOUND_DIR { get; set; } = Directory.GetCurrentDirectory ();
		private readonly string SE_DIR = "\\SE";
		private readonly string VC_DIR = "\\VC";


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
						throw new Exception ( "◆ImageName in Script : " + sqc.Name + "[" + i + "]-> " + scp.ImgName );
					}

					//ルート
					foreach ( TName tn in scp.BD_RutName.GetBindingList () )
					{
						noname = tn.Name;
						if ( ! ch.BD_Route.ContainsKey ( tn.Name ) )
						{
							throw new Exception ( "◆Route in Script : " + sqc.Name + "[" + i + "]-> " + tn.Name );
						}
					}

					//SE, VC
					Check_SOUND_Name ( SOUND_DIR + SE_DIR, scp.StgPrm.SE_name, sqc, scp );
					Check_SOUND_Name ( SOUND_DIR + VC_DIR, scp.StgPrm.VC_name, sqc, scp );
				}
			}


			//ブランチ -> コマンド, アクション
			int iBrc = 0;
			foreach ( Branch brc in ch.BD_Branch.GetBindingList () )
			{
				if ( ! ch.BD_Command.ContainsKey ( brc.NameCommand ) )
				{
					throw new Exception( "◆Branch : " + brc.Name + "[" + iBrc + "]-> " + brc.NameCommand );
				}
				if ( ! ch.behavior.BD_Sequence.ContainsKey ( brc.NameSequence ) )
				{
					throw new Exception( "◆Branch : " + brc.Name + "[" + iBrc + "]-> " + brc.NameSequence );
				}
				++ iBrc;
			}

			//ルート -> ブランチ名
			int iRut = 0;
			foreach ( Route rut in ch.BD_Route.GetBindingList () )
			{
				foreach ( TName tn in rut.BD_BranchName.GetBindingList () )
				{
					if ( ! ch.BD_Branch.ContainsKey ( tn.Name ) )
					{
						throw new Exception( "◆Route : " + rut.Name + "[" + iBrc + "]-> " + tn.Name );
					}
				}
				++ iRut;
			}

		}

		private void Check_SOUND_Name ( string dir, string sound_name, Sequence sqc, Script scp )
		{
			//空欄は何もしない
			if ( sound_name == "" ) { return; }

			//直接音声ファイルを調べる
			string[] files = Directory.GetFiles ( dir );

			//すべてのファイル名をチェック
			foreach ( string file in files )
			{
				string filename = Path.GetFileName ( file );
				//同じ名前があったら終了
				if ( sound_name == filename )
				{
					return;
				}
			}

			//すべてのファイル名と異なったらthrow 
			throw new Exception ( "◆SOUND_Name : " + DispScp ( sqc, scp ) + sound_name ); 
		}

		//表示用
		private string DispScp ( Sequence sqc, Script scp )
		{
			return sqc.Name + "[" + scp.Frame + "]->";
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
