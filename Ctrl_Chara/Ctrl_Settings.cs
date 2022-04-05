using System;

namespace ScriptEditor
{
	//Ctrl類で追加する設定保存
	public class Ctrl_Settings : Settings
	{
		public string File_ActionList { get; set; } = "";		//アクションリスト
		public string Dir_ImageListAct { get; set; } = "";		//イメージディレクトリ(アクション)
		public string File_EffectList { get; set; } = "";		//エフェクトリスト
		public string Dir_ImageListEf { get; set; } = "";		//イメージディレクトリ(エフェクト) 
		public string File_CommandList { get; set; } = "";		//コマンドリスト
		public string File_BranchList { get; set; } = "";		//ブランチリスト
		public string File_RouteList { get; set; } = "";		//ルートリスト
	}
}
