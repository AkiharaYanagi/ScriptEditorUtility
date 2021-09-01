using System.ComponentModel;

namespace ScriptEditor
{
	//--------------------------------------------
	//	選択、スクリプト内で可能なブランチの集合
	//--------------------------------------------
//	using BD_Brc = BindingDictionary < Branch0 >;

	public class Route : IName
	{
		//名前
		public string Name { get; set; } = "RutName";
		public string GetName () { return Name; }

		//ブランチネームリスト
//		public BD_Brc BD_Branch { get; set; } = new BD_Brc ();
		public BindingList < string > BL_BranchName { get; set; } = new BindingList < string > ();

		//コンストラクタ
		public Route ()
		{
		}

		public Route ( string name )
		{
			Name = name;
		}

		public Route ( string name, string summary )
		{
			Name = name;
			Summary = summary;
		}

		//摘要
		public string Summary { get; set; } = "摘要";
	}
}
