//※ #defineはソースの先頭のみ
//#define MAKE_CHARA_FROM_SOURCE
//
using System;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using System.ComponentModel;
using System.Collections.Generic;

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
				Debug.Write ( "エラー：テストデータの作成\n" + ex.Message );
				return;
			}
		}

		//作成(内部)
		private void _Make ( Chara ch )
		{
			//EditCharaに設定
			EditChara.Inst.SetCharaDara ( ch );

			//イメージリスト
			MakeImage ( ch );

#if MAKE_CHARA_FROM_SOURCE
			//ソースコードからキャラデータを反映
			SetCharaData ( ch );
#else
			//手動でキャラデータを作成
			_MakeCharaData ( ch );
#endif	//MAKE_CHARA_FROM_SOURCE
		}

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

			//すべてのアクションにおけるスクリプトへの変更
#if false
			void f_editScp ( Script scp ) { scp.ImgName = "000_dummy.png"; }
			EditChara.Inst.EditBehavior.EditAllScript ( chara.behavior, f_editScp );
#endif
			//名前参照　整合性チェック
			foreach ( Sequence sqc in ch.behavior.BD_Sequence.GetBindingList () )
			{
				foreach ( Script scp in sqc.ListScript )
				{
					ImageData imgd = ch.behavior.BD_Image.Get ( scp.ImgName );

					if ( imgd is null )
					{
						scp.ImgName = ch.behavior.BD_Image.Get ( 0 ).Name;
					}
					else
					{
						Debug.Assert ( ! ( imgd is null ) );
					}

					foreach ( TName tn in scp.BL_RutName )
					{
						Route rut = ch.BD_Route.Get ( tn.Name );
						Debug.Assert ( ! ( rut is null ) );
					}
				}
			}

			//--------------------------------------------
			//エディット（ガーニッシュ）のテスト
			MakeGarnish ( ch );

#if false

			//--------------------------------------------
			//すべてのエフェクトにおけるスクリプトへの変更
			BL_Sequence blsqcEf = ch.garnish.BD_Sequence.GetBindingList();
			foreach ( Sequence sqc in blsqcEf )
			{
				BL_Script blsc = sqc.ListScript;
				foreach ( Script sc in blsc )
				{
					sc.ImgName = "dummy.png";
				}
			}
#endif
		}



	}
}