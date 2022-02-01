﻿namespace ScriptEditor
{
#if false
	//-------------------------------------------------------
	//	■ 基本状態アクション
	//		(アクションリストの先頭から固定の位置にあり、外部から変更される対象先のアクション)
	//		立ち, 構え, 打合, 避け, よろけ, ダメージ, 投げられ, ダウン, 勝利
#endif

	//-------------------------------------------------------
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
	//アクションデータ
	public enum ActionData
	{
		Name,
		Category,
		ScriptNum,
	}

	//-------------------------------------------------------
	//コマンドデータ
	public enum CommandData
	{
		Name,
	}

	//-------------------------------------------------------
	//スクリプト 位置 計算状態
	public enum CLC_ST
	{
		CLC_MAINTAIN,	//持続
		CLC_SUBSTITUDE,	//代入
		CLC_ADD,		//加算
	}
	
	//-------------------------------------------------------
	//ブランチ 条件	(優先度合はゲームメインで指定)
	public enum BranchCondition
	{
		DMG,	//自分が喰らい
		HIT_I,	//相手にヒット(自身を変更)
		HIT_E,	//相手にヒット(相手を変更)
		THR_I,	//投げ成立 (ゲームメイン指定)
		THR_E,	//投げ成立 (ゲームメイン指定)
					//打撃がヒット→バランス値参照→０なら成立
		CMD,	//コマンド成立
		GRD,	//着地

		//他、特殊フラグをゲームメインで設定できる
		//FLG_0, 
	}


}
