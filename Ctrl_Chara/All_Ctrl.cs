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
		public Ctrl_AllRect Rct { get; set; } = new Ctrl_AllRect ();	//枠

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

		//----------------------------------------------
		//コントロール全体

		//関連付け
		public void Assosiate ()
		{
			SqcList_Act.Assosiate ();
			SqcList_Efc.Assosiate ();

			Assosiate_scp ();

			UpdateData ();
		}

		//データ更新
		public void UpdateData ()
		{
			SqcList_Act.UpdateData ();
			SqcList_Efc.UpdateData ();

			UpdateData_scp ();

			Disp ();
		}

		//表示
		public void Disp ()
		{
			SqcList_Act.Disp ();
			SqcList_Efc.Disp ();

			Disp_scp ();
		}


		//----------------------------------------------
		//スクリプトのみ
		
		//関連付け
		public void Assosiate_scp ()
		{ 
			Compend_Bhv.Assosiate_scp ();
			Compend_Gns.Assosiate_scp ();
			Scp.Assosiate ();
			Rct.Assosiate ();
		}

		//データ更新
		public void UpdateData_scp ()
		{
			Compend_Bhv.UpdateData ();
			Compend_Gns.UpdateData ();
			Scp.UpdateData ();
			Rct.UpdateData ();

			Disp_scp ();
		}

		//表示
		public void Disp_scp ()
		{
			Compend_Bhv.Disp ();
			Compend_Gns.Disp ();
			Scp.Disp ();
			Rct.Disp ();
		}

		//----------------------------------------------
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
