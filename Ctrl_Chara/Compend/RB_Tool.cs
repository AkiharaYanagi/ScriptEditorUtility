using System.Windows.Forms;
using System.Drawing;
using System;

namespace ScriptEditor
{

	//ツール選択ラジオボタン共通クラス
	public partial class _RB_Tool : RadioButton
	{
		//対象ツール
		public ToolImage Tl_Img { get; set; } = null;

		//表示ツールチップ
		private ToolTip TLTP = new ToolTip ();

		//親
		public _Ctrl_Image Ctrl_Img = null;


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
		public void SetEnviron ( _Ctrl_Image ctrlimg, Image img, string tooltip )
		{
			Ctrl_Img = ctrlimg;
			Image = img;
			TLTP.SetToolTip ( this, tooltip );
		}

		//チェック変更時
		protected override void OnCheckedChanged ( EventArgs e )
		{
			//自身が選択されたとき
			if ( this.Checked )
			{
				Ctrl_Img.SelectingTool = Tl_Img;
			}

			base.OnCheckedChanged ( e );
		}
	}
}
