using System;

namespace ScriptEditor
{
	//----------------------------------------------------------------------
	//Ctrl類で用いるパスを保存
	//	保存読込はXML_IOを用いる
	//----------------------------------------------------------------------
	public class Ctrl_Settings : Settings
	{
		public string File_ActionList { get; set; } = "ActionList.txt";		//アクションリスト
		public string Dir_ImageListAct { get; set; } = "Image";				//イメージディレクトリ(アクション)
		public string File_EffectList { get; set; } = "EffectList.txt";		//エフェクトリスト
		public string Dir_ImageListEf { get; set; } = "EfImage";			//イメージディレクトリ(エフェクト) 
		public string File_CommandList { get; set; } = "CommandList.txt";	//コマンドリスト
		public string File_BranchList { get; set; } = "BranchList.txt";		//ブランチリスト
		public string File_RouteList { get; set; } = "RouteList.txt";		//ルートリスト
	}
}
