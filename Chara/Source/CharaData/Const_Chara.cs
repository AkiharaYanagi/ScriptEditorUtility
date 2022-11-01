namespace ScriptEditor
{
#if false
	//-------------------------------------------------------
	//	■ 基本状態アクション
	//		(アクションリストの先頭から固定の位置にあり、外部から変更される対象先のアクション)
	//		立ち, 構え, 打合, 避け, よろけ, ダメージ, 投げられ, ダウン, 勝利
	//-------------------------------------------------------
#endif

	//アクション属性 定義
	public enum ActionCategory
	{
		NEUTRAL, MOVE, JUMP, DASH, 

		ATTACK_L, ATTACK_M, ATTACK_H, 
		ATTACK_J, 
		SPECIAL, OVERDRIVE, 

		DAMAGED, 
		POISED, CLANG, AVOID, 
		DOTTY, THROW, GUARD, 

		DEMO, OTHER
	}

	//-------------------------------------------------------
	//アクション体勢 定義
	public enum ActionPosture
	{
		STAND, CROUCH, JUMP
	}

	//-------------------------------------------------------
	//スクリプト 位置 計算状態
	public enum CLC_ST
	{
		CLC_MAINTAIN,	//持続 (速度：落下時など、0だが計算しない)
		CLC_SUBSTITUDE,	//代入 (速度：急停止、瞬間移動など直接指定)
		CLC_ADD,		//加算 (速度：移動など前回の値に加算)
	}
	
	//-------------------------------------------------------
	//ブランチ 条件	(優先度合はゲームメインで指定)
	//Save :: CharToDoc ではintに変換している 
	public enum BranchCondition
	{
		CMD,	//コマンド成立
		GRD,	//着地

		DMG,	//自分が喰らい
		HIT_I,	//相手にヒット(自身を変更)
		HIT_E,	//相手にヒット(相手を変更)

		//打撃がヒット→バランス値参照→０なら成立
		THR_I,	//投げ成立 (ゲームメイン指定)
		THR_E,	//投げ成立 (ゲームメイン指定)

		OFS,	//相殺時
		END,	//シークエンス終了時

		//他、特殊フラグをゲームメインで設定できる
		//FLG_0, 
		DASH,	//ダッシュ相殺
	}

	//キャラ関連 定数
	public enum ConstChara
	{
		NumRect = 8,
	}

}
