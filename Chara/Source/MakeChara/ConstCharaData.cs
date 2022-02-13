using System;

namespace ScriptEditor
{
	using AC = ActionCategory;

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
	//ブランチデータ
	public enum BranchData
	{
		Name, Condition, Command, Action, 
	}

	//-------------------------------------------------------
	//ルートデータ
	public enum RouteData
	{
		Name, Summary, Branch, 
	}

	//-------------------------------------------------------
	//デフォルトアクション名
	public enum ENM_ACT
	{
		Stand, 
		FrontMove, BackMove, 
		VerticalJump, FrontJump, BackJump, Drop, 
		VerticalJump_D, FrontJump_D, BackJump_D,
		FrontDash, BackDash, 

		Attack_5L, Attack_5Ma, Attack_5Mb, Attack_5H, 
		Attack_6L, Attack_6Ma, Attack_6Mb, Attack_6H, 
		Attack_4L, Attack_4Ma, Attack_4Mb, Attack_4H, 
		Attack_2L, Attack_2Ma, Attack_2Mb, Attack_2H, 
		Attack_8L, Attack_8Ma, Attack_8Mb, Attack_8H, 

		Attack_JL, Attack_JMa, Attack_JMb, Attack_JH, 

		SP0_L, SP0_M, SP0_H, 
		SP01_L, SP01_M, SP01_H, 
		SP1_L, SP1_M, SP1_H, 
		SP2_L, SP2_M, SP2_H, 
		OD0_L, OD0_M, OD0_H, 
		OD1_L, OD1_M, OD1_H, 

		Guard, 
		Poised, Clang, Avoid, Dotty, 
		Damaged_L, Damaged_M, Damaged_H, 
		Thrown0, 
		Down, DownRecover, 

		Lose, LoseDuration, 
		Win, WinDuration, 
		Start,
	}

#if false
	//デフォルトアクションカテゴリ
	public readonly AC [] _ACTION_CATEGORY = new AC [] 
	{
		AC.NEUTRAL, 
		AC.MOVE, AC.MOVE, 
		AC.JUMP, AC.JUMP, AC.JUMP, AC.JUMP, 
		AC.JUMP, AC.JUMP, AC.JUMP,
		AC.DASH, AC.DASH, 

		AC.ATTACK_L, AC.ATTACK_M, AC.ATTACK_M, AC.ATTACK_H,
		AC.ATTACK_L, AC.ATTACK_M, AC.ATTACK_M, AC.ATTACK_H,
		AC.ATTACK_L, AC.ATTACK_M, AC.ATTACK_M, AC.ATTACK_H,
		AC.ATTACK_L, AC.ATTACK_M, AC.ATTACK_M, AC.ATTACK_H,
		AC.ATTACK_L, AC.ATTACK_M, AC.ATTACK_M, AC.ATTACK_H,

		AC.ATTACK_J, AC.ATTACK_J, AC.ATTACK_J, AC.ATTACK_J,

		AC.SPECIAL, AC.SPECIAL, AC.SPECIAL,
		AC.SPECIAL, AC.SPECIAL, AC.SPECIAL,
		AC.SPECIAL, AC.SPECIAL, AC.SPECIAL,
		AC.SPECIAL, AC.SPECIAL, AC.SPECIAL,
		AC.OVERDRIVE, AC.OVERDRIVE, AC.OVERDRIVE,
		AC.OVERDRIVE, AC.OVERDRIVE, AC.OVERDRIVE,

		AC.GUARD, 
		AC.POISED, AC.CLANG, AC.AVOID, AC.DOTTY, 
		AC.DAMAGED, AC.DAMAGED, AC.DAMAGED, 
		AC.DAMAGED, 
		AC.DAMAGED, AC.DAMAGED, 

		AC.DEMO, AC.DEMO, 
		AC.DEMO, AC.DEMO, 
		AC.DEMO, 
	};
#endif

	//デフォルトコマンド名
	public enum ENM_CMD
	{ 
		_N,
		L, Ma, Mb, H,
		_6, _4, 
		_6off, _4off, _6_6, _4_4, 
		_8, _9, _7, 
		_2_8, _2_9, _2_7, 
		_6L, _6Ma, _6Mb, _6H, 
		_4L, _4Ma, _4Mb, _4H, 
		_2L, _2Ma, _2Mb, _2H, 
		_8L, _8Ma, _8Mb, _8H, 
		SP0_L, SP0_Ma, SP0_Mb, SP0_H, 
		SP1_L, SP1_Ma, SP1_Mb, SP1_H, 
		SP2_L, SP2_Ma, SP2_Mb, SP2_H, 
		OD0_L, OD0_Ma, OD0_Mb, OD0_H, 
		OD1_L, OD1_Ma, OD1_Mb, OD1_H, 
	};


	//デフォルトブランチ名
	public enum ENM_BRC
	{
		L_立L, Ma_立Ma, Mb_立Mb, H_立H, 
		_6_F_Move, _4_B_Move, 
		_6離_Stand, _4離_Stand, _6_6_F_Dash, _4_4_B_Dash, 
		_8_V_Jump, _9_F_Jump, _7_B_Jump, 
		着地_Stand,
		SP01, 
		SP0, 
		OD0, 
		投げ分岐_自, 投げ分岐_相, 
		_4L, _4Ma, _4Mb, _4H, 
		_2L, _2Ma, _2Mb, _2H, 
		_6L, _6Ma, _6Mb, _6H, 
		_8L, _8Ma, _8Mb, _8H, 
		被ダメ_L, 被ダメ_M, 被ダメ_H,
	}

	//デフォルトルート名
	public enum ENM_RUT
	{
		地上移動, 地上通常技, 前持続停止, 後持続停止, ジャンプ,
		地上必殺技, 特殊, 地上超必, 
		投げ分岐_自, 投げ分岐_相, 
		被ダメ_L, 被ダメ_M, 被ダメ_H,
	}
}
