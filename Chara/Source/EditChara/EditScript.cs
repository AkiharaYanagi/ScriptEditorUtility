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


	//スクリプト編集
	public partial class EditScript
	{
		//スクリプトが持つ値の編集
		public EditEfGnrt EditEfGnrt { get; set; } = new EditEfGnrt ();
		public EditAllRect EditAllRect { get; set; } = new EditAllRect ();


		//対象データとの関連付け
		public void Assosiate ( Script scp )
		{
			EditEfGnrt.Assosiate ( scp );
			EditAllRect.Assosiate ( scp );
		}
	}


}
