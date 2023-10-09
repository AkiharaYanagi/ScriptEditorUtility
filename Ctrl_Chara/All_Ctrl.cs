using System;


namespace ScriptEditor
{
	//=====================================================
	//	◆[ScripEditorUtility]
	//
	// CtrlCompend   
	//	↓			
	// Ctrl_All(シングルトン)
	//	↓	
	//	↓			 // EditChara(シングルトン)
	//	↓				↓
	//	↓→ → UpdateData(), Assosiate() など
	//	↓				↓
	//----------------------------------
	//◆[ScripEditor]	
	//		
	//	↓				↓
	//	↓				↓
	//	[------ Chara ------](編集対象キャラデータ)
	//
	//=====================================================

	public sealed class All_Ctrl
	{
		//---------------------------------------------------------------------
		//シングルトン ( 継承不可： sealed )
		public static All_Ctrl Inst { get; } = new All_Ctrl ();

		//プライベートコンストラクタ
		private All_Ctrl ()
		{
		}
		//---------------------------------------------------------------------


		//@todo シークエンスツリーが毎回更新していると遅いので、更新範囲を指定する



		//コントロール参照

		//ビヘイビア（アクション）
		public Ctrl_SqcList SqcList_Act { get; set; } = new Ctrl_SqcList ();
		public _Ctrl_Compend Compend_Bhv { get; set; } = new _Ctrl_Compend ();

		//ガーニッシュ（エフェクト）
		public Ctrl_SqcList SqcList_Efc { get; set; } = new Ctrl_SqcList ();
		public _Ctrl_Compend Compend_Gns { get; set; } = new _Ctrl_Compend ();

		public Ctrl_CmdList Cmd { get; set; } = new Ctrl_CmdList ();		//コマンド
		public Ctrl_Branch Brc { get; set; } = new Ctrl_Branch ();		//ブランチ
		public Ctrl_Route Rut { get; set; } = new Ctrl_Route ();		//ルート

		//各サブフォームのコントロール
		public _Ctrl_Script Scp { get; set; } = new _Ctrl_Script ();	//スクリプト

		//-------------------------

		//キャラデータ設置
		public void SetCharaData ( Chara ch )
		{
			SqcList_Act.SetCharaData ( ch.behavior );
			Compend_Bhv.SetCharaData ( ch );
			SqcList_Efc.SetCharaData ( ch.garnish );
			Compend_Gns.SetCharaData ( ch );

			Cmd.SetCharaData ( ch );
			Brc.SetCharaData ( ch );
			Rut.SetCharaData ( ch );

			Assosiate ();
		}

		//関連付け
		public void Assosiate ()
		{
			SqcList_Act.Assosiate ();
			Compend_Bhv.Assosiate ();
			SqcList_Efc.Assosiate ();
			Compend_Gns.Assosiate ();

			Scp.Assosiate ();

			UpdateData ();
		}

		//スクリプトのみ関連付け
		public void Assosiate_scp ()
		{ 
			Compend_Bhv.Assosiate_scp ();
			Compend_Gns.Assosiate_scp ();
			Scp.Assosiate ();
		}

		//データ更新
		public void UpdateData ()
		{
			SqcList_Act.UpdateData ();
			Compend_Bhv.UpdateData ();
			SqcList_Efc.UpdateData ();
			Compend_Gns.UpdateData ();

			Disp ();
		}

		//表示
		public void Disp ()
		{
			SqcList_Act.Disp ();
			Compend_Bhv.Disp ();
			SqcList_Efc.Disp ();
			Compend_Gns.Disp ();
		}


		//プレデータ読込
		public void LoadPreData ()
		{
			SqcList_Act.LoadPreData ();
			SqcList_Efc.LoadPreData ();
			Cmd.LoadPreData ();
			Brc.LoadPreData ();
			Rut.LoadPreData ();
		}

	}
}
