using System;

namespace ScriptEditor
{
	//---------------------------------------------------------------------
	//	Setter, Getter
	//---------------------------------------------------------------------
	//スクリプトにおけるパラメータに対し、型に応じたセッタとゲッタを保持するジェネリックスクラス
	public class ScriptParam < T >
	{
		public Action < Script, T > Setter { get; set; } = null;
		public Func < Script, T > Getter { get; set; } = null;

		public ScriptParam ( Action < Script, T > setter, Func < Script, T > getter )
		{
			Setter = setter;
			Getter = getter;
		}
	}

}
