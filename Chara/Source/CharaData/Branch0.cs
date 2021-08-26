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

		//遷移先　アクション名
		public string NameAction { get; set; } = "NameAction";

		//遷移先　フレーム指定
		public int Frame { get; set; } = 0;

		//------------------------------------------------
		//コンストラクタ
		public Branch0 ()
		{
		}

		public Branch0 ( string strCommand, string strAction )
		{
			NameCommand = strCommand;
			NameAction = strAction;
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
