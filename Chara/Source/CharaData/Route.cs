namespace ScriptEditor
{
	//--------------------------------------------
	//	選択、スクリプト内で可能なブランチの集合
	//--------------------------------------------
	public class Route : IName
	{
		//名前
		public string Name { get; set; } = "CisName";
		public string GetName () { return Name; }

		//ブランチ
		public BindingDictionary < Branch > BD_Branch { get; set; } = new BindingDictionary<Branch> ();
	}
}
