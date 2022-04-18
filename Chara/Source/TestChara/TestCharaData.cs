//※ #defineはソースの先頭のみ
//#define MAKE_CHARA_FROM_SOURCE
#if MAKE_CHARA_FROM_SOURCE
			//ソースコードからキャラデータを反映
			SetCharaData ( ch );
#endif	//MAKE_CHARA_FROM_SOURCE
//

using System;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;

namespace ScriptEditor
{
	//==================================================================================
	//	現在のキャラからテスト用のデータを作成する
	//		データファイルの復旧や、バージョンが異なるときにソースから作成できる状態にしておく
	//==================================================================================
	using BL_Sequence = BindingList < Sequence >;
	using BL_Script = List < Script >;

	//-----------------------
	//テストデータの作成
	//-----------------------
	public partial class TestCharaData
	{
		//コンストラクタ
		public TestCharaData ()
		{
		}

		//作成
		public void Make ( Chara ch )
		{
			try
			{
				_Make ( ch );
			}
			catch ( System.OutOfMemoryException ex )
			{
				Debug.Write ( "OutOfMemory エラー：テストデータの作成\n" + ex.Message );
				return;
			}
			catch ( Exception e )
			{
				Debug.Write ( "エラー：テストデータの作成\n" + e.Message );
				return;
			}
		}

		//作成(内部)
		private void _Make ( Chara ch )
		{
			//EditCharaに設定
			EditChara.Inst.SetCharaDara ( ch );
			ch.Clear ();	//ダミーをクリア

			//イメージリスト
			MakeImage ( ch );

			//アクションの作成
			MakeActionData mk_act = new MakeActionData ();
			mk_act.Make ( ch );

			//コマンドの作成
			MakeCommandData mk_cmd = new MakeCommandData ();
			mk_cmd.Make ( ch );

			//ブランチの作成
			MakeBranchData mk_brc = new MakeBranchData ();
			mk_brc.Make ( ch );

			//ルートの作成
			MakeRouteData mk_rut = new MakeRouteData ();
			mk_rut.Make ( ch );

			//エフェクト（ガーニッシュ）のテスト
			MakeGarnish ( ch );

		}


#if false

		//手動でキャラデータを作成
		private void _MakeCharaData ( Chara ch )
		{
			//アクションリスト
			MakeActionList ();

			//アクション内の値を指定
			MakeAction ( ch );

			//コマンド
			MakeCommand ( ch.BD_Command );

			//ブランチ
			MakeBranch ( ch );

			//ルート
			MakeRoute ( ch );

			//エフェクト（ガーニッシュ）のテスト
			MakeGarnish ( ch );

		}
#endif



	}
}