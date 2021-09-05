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
			"Crouch",
			"VerticalJump", "FrontJump", "BackJump", "Drop", 
			"VerticalJump(D)", "FrontJump(D)", "BackJump(D)",
			"FrontDash","FrontDashDuration", 
			"BackDash", "BackDashDuration", 
			"Guard_S", "Guard_C", "Guard_J", 
			"Attack_L", "Attack_M", "Attack_H", 
			"Attack_JL", "Attack_JM", "Attack_JH", 
			"Attack_GL", "Attack_GM", "Attack_GH", 
			"SP0_L", "SP0_M", "SP0_H", 
			"SP1_L", "SP1_M", "SP1_H", 
			"SP2_L", "SP2_M", "SP2_H", 
			"OD0_L", "OD0_M", "OD0_H", 
			"Poised", "Clang", "Avoid", "Dotty", 
			"Damaged_L", "Damaged_M", "Damaged_H", 
			"Down", "DownDuration", 
			"Win", "WinDuration", 
		};


		//デフォルトアクションカテゴリ
		private readonly ActionCategory[] _ACTION_CATEGORY = new ActionCategory[] 
		{
			AC.NEUTRAL, 
			AC.MOVE, AC.MOVE, 
			AC.NEUTRAL,
			AC.MOVE, AC.MOVE, AC.MOVE, AC.MOVE, 
			AC.MOVE, AC.MOVE, AC.MOVE,
			AC.DASH, AC.DASH, 
			AC.DASH, AC.DASH, 
			AC.GUARD, AC.GUARD, AC.GUARD, 
			AC.ATTACK_L, AC.ATTACK_M, AC.ATTACK_H,
			AC.ATTACK_L, AC.ATTACK_M, AC.ATTACK_H,
			AC.ATTACK_L, AC.ATTACK_M, AC.ATTACK_H,
			AC.ATTACK_L, AC.ATTACK_M, AC.ATTACK_H,
			AC.ATTACK_L, AC.ATTACK_M, AC.ATTACK_H,
			AC.ATTACK_L, AC.ATTACK_M, AC.ATTACK_H,
			AC.ATTACK_L, AC.ATTACK_M, AC.ATTACK_H,
			AC.MOVE, AC.CLANG, AC.AVOID, AC.DOTTY, 
			AC.DAMAGED, AC.DAMAGED, AC.DAMAGED, 
			AC.DAMAGED, AC.DAMAGED, 
			AC.DEMO, AC.DEMO, 
		};

		//デフォルトコマンド名
		private readonly string[] NAME_COMMAND = new string[] 
		{
			"Attack_L", "Attack_M", "Attack_H", 
			"FrontMove", "BackMove", 
			"FrontDuration", "BackDuration", "FrontDash", "BackDash", 
			"VerticalJump", "FrontJump", "BackJump", 
		};

		//デフォルトブランチ名
		private readonly string[] NAME_BRANCH = new string[] 
		{
			"立L", "立M", "立H", 
			"FrontMove", "BackMove", 
			"FrontDuration", "BackDuration", "FrontDash", "BackDash", 
			"VerticalJump", "FrontJump", "BackJump", 
		};

	}
}
