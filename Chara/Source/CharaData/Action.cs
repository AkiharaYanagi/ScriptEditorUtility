namespace ScriptEditor
{
	//================================================================
	//	◆アクション		各フレームのスクリプトリストを持つ
	//		┣[]スクリプト
	//		┣次アクション
	//		┣アクション属性
	//		┣アクション体勢
	//		┣消費バランス値
	//================================================================

	//-------------------------------------------------------
	//アクション
	public class Action : Sequence 
	{
		//次アクション名
		public string NextActionName { get; set; } = "Next";

		//アクション属性
		public ActionCategory Category { get; set; } = ActionCategory.NEUTRAL;

		//アクション体勢
		public ActionPosture Posture { get; set; } = ActionPosture.STAND;

		//消費バランス
		public int _Balance { get; set; } = 0;


		//----------------------------------------------------------------------------
		//コンストラクタ
		public Action ()	//ロード時に空白が必要
		{
		}

		//引数付きコンストラクタ
		public Action ( string str ) : base ( str )
		{
			//@info baseのコンストラクタの後でbase.Clear()が呼ばれてしまうのでClear()を用いない
//			Clear ();
		}

		//コピーコンストラクタ
		public Action ( Action action )
		{
			base.Copy ( action );

//			this.NextIndex = action.NextIndex;
			this.NextActionName = action.NextActionName;
			this.Category = action.Category;
			this.Posture = action.Posture;
			this._Balance = action._Balance;
	}

		//クリア
		public override void Clear ()
		{
//			NextIndex = 0;
			NextActionName = "Next";
			Category = ActionCategory.NEUTRAL;
			Posture = ActionPosture.STAND;
			_Balance = 0;

			base.Clear ();
		}

		//コピー
		public void CopyAction ( Action action )
		{
			base.Copy ( action );

//			this.NextIndex = action.NextIndex;
			this.NextActionName = action.NextActionName;
			this.Category = action.Category;
			this.Posture = action.Posture;
			this._Balance = action._Balance;
		}
	}

}
