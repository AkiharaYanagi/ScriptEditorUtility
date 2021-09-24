using System;

namespace ScriptEditor
{
	using AC = ActionCategory;

	public partial class TestCharaData
	{

		//デフォルトアクション名
		private readonly string[] NAME_ACTION = new string[] 
		{
			"Stand", 
			"FrontMove", "BackMove", 
			"VerticalJump", "FrontJump", "BackJump", "Drop", 
			"VerticalJump(D)", "FrontJump(D)", "BackJump(D)",
			"FrontDash","FrontDashDuration", 
			"BackDash", "BackDashDuration", 

			"Attack_5L", "Attack_5Ma", "Attack_5Mb", "Attack_5H", 
			"Attack_6L", "Attack_6Ma", "Attack_6Mb", "Attack_6H", 
			"Attack_4L", "Attack_4Ma", "Attack_4Mb", "Attack_4H", 
			"Attack_2L", "Attack_2Ma", "Attack_2Mb", "Attack_2H", 
			"Attack_8L", "Attack_8Ma", "Attack_8Mb", "Attack_8H", 
			"Attack_JL", "Attack_JMa", "Attack_JMb", "Attack_JH", 
			"SP0_L", "SP0_M", "SP0_H", 
			"SP1_L", "SP1_M", "SP1_H", 
			"SP2_L", "SP2_M", "SP2_H", 
			"OD0_L", "OD0_M", "OD0_H", 
			"OD1_L", "OD1_M", "OD1_H", 

			"Guard", 
			"Poised", "Clang", "Avoid", "Dotty", 
			"Damaged_L", "Damaged_M", "Damaged_H", 
			"Down", "DownRecover", 

			"Lose", "LoseDuration", 
			"Win", "WinDuration", 
			"Start",
		};


		//デフォルトアクションカテゴリ
		private readonly ActionCategory[] _ACTION_CATEGORY = new ActionCategory[] 
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

			AC.GUARD, 
			AC.POISED, AC.CLANG, AC.AVOID, AC.DOTTY, 
			AC.DAMAGED, AC.DAMAGED, AC.DAMAGED, 
			AC.DAMAGED, AC.DAMAGED, 

			AC.DEMO, AC.DEMO, 
			AC.DEMO, AC.DEMO, 
			AC.DEMO, 
		};

		//デフォルトコマンド名
		private readonly string[] NAME_COMMAND = new string[] 
		{
			"L", "Ma", "Mb", "H", 
			"6", "4", 
			"6離", "4離", "6.6", "4.4", 
			"8", "9", "7", 
		};

		//デフォルトブランチ名
		private readonly string[] NAME_BRANCH = new string[] 
		{
			"L → 立L", "Ma → 立Ma", "Mb → 立Mb", "H → 立H", 
			"6 → FrontMove", "4 → BackMove", 
			"6離 → FrontDuration", "4離 → BackDuration", "6.6 → FrontDash", "4.4 → BackDash", 
			"8 → VerticalJump", "9 → FrontJump", "7 → BackJump", 
		};

		//デフォルトルート名
		private readonly string[] NAME_ROUTE = new string[]
		{
			"地上通常技", "地上移動", 
		};
	}
}
