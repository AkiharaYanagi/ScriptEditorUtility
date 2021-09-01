using System.Diagnostics;

namespace ScriptEditor
{
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

		//テスト
		public void Test ( Chara ch )
		{
			//EditCharaに設定
			EditChara.Inst.SetCharaDara ( ch );

			//初期化(Clear)テスト
			TestClearChara ( ch );

			//テストデータ作成
			TestCharaData tcd = new TestCharaData ();
			tcd.Make ( ch );

			//コピーテスト
			Chara copyChara = new Chara ();
			TestCopyChara ( copyChara, ch );
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
		}

		//コピーテスト
		public void TestCopyChara ( Chara dstChara , Chara srcChara )
		{
			dstChara.Copy ( srcChara );

			Equal ( dstChara, srcChara );
		}

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

	}

}
