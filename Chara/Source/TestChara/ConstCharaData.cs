using System;

namespace ScriptEditor
{
	using AC = ActionCategory;

	public partial class TestCharaData
	{
		//デフォルトアクション名
		private enum ENM_ACTION
		{
			Stand, 
			FrontMove, BackMove, 
			VerticalJump, FrontJump, BackJump, Drop, 
			VerticalJump_D, FrontJump_D, BackJump_D,
			FrontDash,FrontDashDuration, 
			BackDash, BackDashDuration, 

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
			   
		//デフォルトアクションカテゴリ
		private readonly AC [] _ACTION_CATEGORY = new AC [] 
		{
			AC.NEUTRAL, 
			AC.MOVE, AC.MOVE, 
			AC.MOVE, AC.MOVE, AC.MOVE, AC.MOVE, 
			AC.MOVE, AC.MOVE, AC.MOVE,
			AC.DASH, AC.DASH, 
			AC.DASH, AC.DASH, 

			AC.ATTACK_L, AC.ATTACK_M, AC.ATTACK_M, AC.ATTACK_H,
			AC.ATTACK_L, AC.ATTACK_M, AC.ATTACK_M, AC.ATTACK_H,
			AC.ATTACK_L, AC.ATTACK_M, AC.ATTACK_M, AC.ATTACK_H,
			AC.ATTACK_L, AC.ATTACK_M, AC.ATTACK_M, AC.ATTACK_H,
			AC.ATTACK_L, AC.ATTACK_M, AC.ATTACK_M, AC.ATTACK_H,
			AC.ATTACK_L, AC.ATTACK_M, AC.ATTACK_M, AC.ATTACK_H,

			AC.ATTACK_L, AC.ATTACK_M, AC.ATTACK_H,
			AC.ATTACK_L, AC.ATTACK_M, AC.ATTACK_H,
			AC.ATTACK_L, AC.ATTACK_M, AC.ATTACK_H,
			AC.ATTACK_L, AC.ATTACK_M, AC.ATTACK_H,
			AC.ATTACK_L, AC.ATTACK_M, AC.ATTACK_H,
			AC.ATTACK_L, AC.ATTACK_M, AC.ATTACK_H,

			AC.GUARD, 
			AC.POISED, AC.CLANG, AC.AVOID, AC.DOTTY, 
			AC.DAMAGED, AC.DAMAGED, AC.DAMAGED, 
			AC.DAMAGED, 
			AC.DAMAGED, AC.DAMAGED, 

			AC.DEMO, AC.DEMO, 
			AC.DEMO, AC.DEMO, 
			AC.DEMO, 
		};

		//デフォルトコマンド名
		private enum ENM_CMD
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
			SP0,
		};


		//デフォルトブランチ名
		private enum ENM_BRC
		{
			L_立L, Ma_立Ma, Mb_立Mb, H_立H, 
			_6_F_Move, _4_B_Move, 
			_6離_Stand, _4離_Stand, _6_6_F_Dash, _4_4_B_Dash, 
			_8_V_Jump, _9_F_Jump, _7_B_Jump, 
			着地_Stand,
			SP01, 
			SP0, 
			投げ分岐_自, 投げ分岐_相, 
			_6L, _6Ma, _6Mb, _6H, 
			_4L, _4Ma, _4Mb, _4H, 
			_2L, _2Ma, _2Mb, _2H, 
			_8L, _8Ma, _8Mb, _8H, 
			被ダメ_H,
		}

		//デフォルトルート名
		private enum ENM_RUT
		{
			地上移動, 地上通常技, 前持続停止, 後持続停止, ジャンプ,
			地上必殺技, 特殊, 
			投げ分岐_自, 投げ分岐_相, 
			被ダメ_H,
		}
	}
}
