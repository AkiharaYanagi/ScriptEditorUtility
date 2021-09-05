﻿using System.ComponentModel;

namespace ScriptEditor
{
	//--------------------------------------------
	//	選択、スクリプト内で可能なブランチの集合
	//--------------------------------------------
	public class Route : IName
	{
		//名前
		public string Name { get; set; } = "RutName";
		public string GetName () { return Name; }

		//摘要
		public string Summary { get; set; } = "摘要";

		//ブランチネームリスト
		public BindingList < TName > BL_BranchName { get; set; } = new BindingList < TName > ();


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
	}
}
