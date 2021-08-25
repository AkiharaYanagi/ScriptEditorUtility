namespace ScriptEditor
{
	//------------------------------------------------
	//	コマンドによるアクション分岐
	//------------------------------------------------
	public class Branch0 : IName
	{
		//------------------------------------------------
		// IName 名前
		public string Name { get; set; } = "BrcName";
		public string GetName () { return Name; }
		//------------------------------------------------

		//条件　コマンド名
		public string NameCommand { get; set; } = "NameCommand";

		//条件　コマンドの参照
		public Command Condition { get; set; } = null;

		//遷移先　アクション名
		public string NameAction { get; set; } = "NameAction";

		//遷移先　アクションの参照
		public Action Transit { get; set; } = null;


		//遷移先　フレーム指定
		public int Frame { get; set; } = 0;

		//------------------------------------------------
		//コンストラクタ
		public Branch0 ()
		{
		}

		public Branch0 ( string strCommand, Command command, string strAction, Action action )
		{
			NameCommand = strCommand;
			Condition = command;
			NameAction = strAction;
			Transit = action;
		}


		//比較
		public override bool Equals ( object obj )
		{
			return base.Equals ( obj );
		}

		public override int GetHashCode ()
		{
			return base.GetHashCode ();
		}
	}

}
