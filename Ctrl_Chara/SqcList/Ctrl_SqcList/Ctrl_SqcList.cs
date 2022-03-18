using System.Windows.Forms;


namespace ScriptEditor
{
	public partial class Ctrl_SqcList : UserControl
	{
		//メインデータ
		private SqcListData Data = new SqcListData ();
		private Compend Cmpd = null;

		//データ編集
		private EditSqcListData EditData = new EditSqcListData ();

		//表示編集用コントロール
		private EditListbox < SequenceData > ELB_Sqc = new EditListbox<SequenceData> ();
		
		//設定ファイル
		private Settings settings = new Settings ();

		public bool run = false;

		//新規オブジェクト作成
		public System.Func < Sequence > New_Action = ()=>new Sequence();

		//----------------------------------------------------------------------

		//コンストラクタ
		public Ctrl_SqcList ()
		{
			//コンポーネント初期化
			InitializeComponent ();
		}

		//環境設定
		public void SetEnviroment ( System.Func < Sequence > new_action )
		{
			New_Action = new_action;
			ELB_Sqc.New_T = ()=>new SequenceData(){Sqc=new_action()};
		}

		//データ設置
		public void SetData ( Compend cmpd )
		{
			Cmpd = cmpd;
			Data.SetData ( cmpd );
		}

		//データ更新
		public void UpdateData ()
		{
			ctrl_ImageTable1.UpdateData ();
		}

		//データ適用
		public void ApplyData ()
		{
			//SqcDtからCompendに戻す
			//Sequenceで生成しているとき、Actionにアップキャストできない問題
			//-> New_Action()をデリゲートで指定
			EditData.ApplyData ();
		}

	}
}
