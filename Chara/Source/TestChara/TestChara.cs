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

			//名前指定
			TestNameAssign ( ch );
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
		//戻値： T:OK, F:ASSERT
		public bool _TestNameAssign ( Chara ch )
		{
			//イメージ名指定
			foreach ( Sequence sqc in ch.behavior.BD_Sequence.GetBindingList () )
			{
				foreach ( Script scp in sqc.ListScript )
				{
					ImageData imgd = ch.behavior.BD_Image.Get ( scp.ImgName );

					//該当イメージが無いとき
					if ( imgd is null ) { return false; }

					//各スクリプトで指定イメージが無いとき
					foreach ( TName tn in scp.BL_RutName )
					{
						Route rut = ch.BD_Route.Get ( tn.Name );
						if ( rut is null ) { return false; }
					}
				}
			}

			//ブランチ名指定
			foreach ( Route rut in ch.BD_Route.GetBindingList () )
			{
				foreach ( TName tn in rut.BL_BranchName )
				{
					Branch brc = ch.BD_Branch.Get ( tn.Name );

					if ( brc is null ) { return false; }
				}
			}

			return true;
		}

		public void TestNameAssign ( Chara ch )
		{
			Debug.Assert ( _TestNameAssign ( ch ) );
		}

	}

}
