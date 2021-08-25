namespace ScriptEditor
{
	//各種条件によるアクション分岐
	public class Branch : IName
	{
		//------------------------------------------------
		// IName 名前
		public string Name { get; set; } = "BrcName";
		public string GetName () { return Name; }
		//------------------------------------------------

		//------------------------------------------------
		//コマンドとアクションは実行上において参照を用いるが、
		//スクリプトをテキストファイルに書き出すため、インデックスも保存する
		//	->名前で検索に変更
		//------------------------------------------------

		//条件　コマンドリストの添字 indexCommand
		public int IndexCommand { get; set; } = 0;
		public string NameCommand { get; set; } = "NameCommand";
		//条件　コマンド名
//		public BindingDictionary < Command > BD_Command = new BindingDictionary < Command > ();

		//条件　コマンドの参照
		public Command Condition { get; set; } = null;


		//遷移先　アクションリストの添字 indexAction
		public int IndexAction { get; set; } = 0;
		public string NameAction { get; set; } = "NameAction";
		//遷移先　アクション名
//		public BindingDictionary < Action > BD_Action = new BindingDictionary < Action > ();


		//遷移先　アクションの参照
		public Action Transit { get; set; } = null;


		//遷移先　フレーム指定
		public int Frame { get; set; } = 0;

		//------------------------------------------------
		//コンストラクタ
		public Branch ()
		{
		}

		public Branch ( int indexCommand, Command command, int indexAction, Action action )
		{
			IndexCommand = indexCommand;
			Condition = command;
			IndexAction = indexAction;
			Transit = action;
		}

		public Branch ( string strCommand, Command command, string strAction, Action action )
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
