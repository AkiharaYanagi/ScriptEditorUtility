using System.Windows.Forms;
using System.IO;


namespace ScriptEditor
{
	public partial class Ctrl_SqcList : UserControl
	{
		//メインデータ
		private SqcListData Data = new SqcListData ();

		//データ編集
		private EditSqcListData EditData = new EditSqcListData ();

		//表示編集用コントロール
		private EditListbox < SequenceData > ELB_Sqc = new EditListbox<SequenceData> ();
		
		//設定ファイル
		private Settings settings = new Settings ();

		public bool run = false;

		//コンストラクタ
		public Ctrl_SqcList ()
		{
			//コンポーネント初期化
			InitializeComponent ();
		}

	}
}
