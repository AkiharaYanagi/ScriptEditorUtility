namespace ScriptEditor
{
	//------------------------------------------------
	//	各種条件によるアクション分岐
	//------------------------------------------------
	public class Branch : IName
	{
		//------------------------------------------------
		// IName 名前
		public string Name { get; set; } = "BrcName";
		//------------------------------------------------
			
		//条件 定数
		public BranchCondition Condition { get; set; } = BranchCondition.CMD;

		//条件　コマンド名
		public string NameCommand { get; set; } = "NameCommand";

		//遷移先　アクション名
		public string NameAction { get; set; } = "NameAction";

		//遷移先　フレーム指定
		public int Frame { get; set; } = 0;

		//------------------------------------------------
		//コンストラクタ
		public Branch ()
		{
		}

		public Branch ( string name )
		{
			Name = name;
		}

		public Branch ( string strCommand, string strAction )
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
