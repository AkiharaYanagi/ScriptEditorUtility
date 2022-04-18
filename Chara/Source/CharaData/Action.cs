namespace ScriptEditor
{
	//================================================================
	//	◆アクション		各フレームのスクリプトリストを持つ
	//		base	┣[]スクリプト
	//		┣次アクション
	//		┣アクション属性
	//		┣アクション体勢
	//
	//		┣ヒット数
	//		┣ヒット間隔
	//================================================================

	//-------------------------------------------------------
	//アクション
	public class Action : Sequence 
	{
		//次アクション名
		public string NextActionName { get; set; } = "Stand";

		//アクション属性
		public ActionCategory Category { get; set; } = ActionCategory.NEUTRAL;

		//アクション体勢
		public ActionPosture Posture { get; set; } = ActionPosture.STAND;

		//ヒット数
		public int HitNum { get; set; } = 1;

		//ヒット間隔[F}
		public int HitPitch { get; set; } = 10;


		//----------------------------------------------------------------------------
		//コンストラクタ
		public Action ()	//ロード時に空白が必要
		{
		}

		//引数付きコンストラクタ
		public Action ( string str ) : base ( str )
		{
			//@info baseのコンストラクタの後でbase.Clear()が呼ばれてしまうのでClear()を用いない
		}

		//継承元から生成するコンストラクタ
		public Action ( Sequence sqc )
		{
			base.Copy ( sqc );
		}

		//コピーコンストラクタ
		public Action ( Action action )
		{
			base.Copy ( action );

			this.NextActionName = action.NextActionName;
			this.Category = action.Category;
			this.Posture = action.Posture;
			this.HitNum = action.HitNum;
			this.HitPitch = action.HitPitch;
	}

		//クリア
		public override void Clear ()
		{
			NextActionName = "Next";
			Category = ActionCategory.NEUTRAL;
			Posture = ActionPosture.STAND;
			HitNum = 0;
			HitPitch = 0;

			base.Clear ();
		}

		//コピー
		public void CopyAction ( Action action )
		{
			base.Copy ( action );

			this.NextActionName = action.NextActionName;
			this.Category = action.Category;
			this.Posture = action.Posture;
			this.HitNum = action.HitNum;
			this.HitPitch = action.HitPitch;
		}
	}

}
