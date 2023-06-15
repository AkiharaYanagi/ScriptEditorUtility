using System.Windows.Forms;
using System.Drawing;


namespace ScriptEditor
{

	//ツール選択ラジオボタン共通クラス
	public partial class _RB_Tool : RadioButton
	{
		//対象ツール
		public ToolImage Tl_Img { get; set; } = null;

		//表示ツールチップ
		private ToolTip TLTP = new ToolTip ();


		//コンストラクタ
		public _RB_Tool ()
		{
			InitializeComponent ();
			Appearance = Appearance.Button;
			ImageAlign = ContentAlignment.MiddleCenter;
			AutoSize = true;
			Size = new Size ( 38, 38 );
		}

		//初期設定
		public void SetEnviron ( Image img, string tooltip )
		{
			Image = img;
			TLTP.SetToolTip ( this, tooltip );
		}
	}
}
