using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;


namespace ScriptEditor
{
	using BD_Sqc = BindingDictionary < Sequence >;
	using BD_IMGD = BindingDictionary < ImageData >;

	//-------------------------------------------------------------------------
	//スクリプトにおけるイメージとそれを編集するツール群をまとめるコントロール
	//-------------------------------------------------------------------------
	public partial class _Ctrl_Image : UserControl
	{
		//イメージ描画
		private _PaintImage paintImage = new _PaintImage ();

		//現在選択ツール
		public ToolImage SelectingTool { get; set; } = null;	//選択中ツール

		//ツール
		private ToolImage_Main Tlimg_Main = new ToolImage_Main ();
		private ToolImage_Rect Tlimg_CRect = new ToolImage_Rect ();
		private ToolImage_Rect Tlimg_HRect = new ToolImage_Rect ();
		private ToolImage_Rect Tlimg_ARect = new ToolImage_Rect ();
		private ToolImage_Rect Tlimg_ORect = new ToolImage_Rect ();
		private ToolImage_Ef Tlimg_Ef = new ToolImage_Ef ();

		//ラジオボタン
		private List < _RB_Tool >	L_RB_Tool = new List<_RB_Tool> ();
		private _RB_Tool RB_Main = new _RB_Tool ();
		private _RB_Tool RB_CRect = new _RB_Tool ();
		private _RB_Tool RB_HRect = new _RB_Tool ();
		private _RB_Tool RB_ARect = new _RB_Tool ();
		private _RB_Tool RB_ORect = new _RB_Tool ();
		private _RB_Tool RB_Ef = new _RB_Tool ();


		//コンストラクタ
		public _Ctrl_Image ()
		{
			InitializeComponent ();

			//イメージ描画
			this.Controls.Add ( paintImage );

			//ラジオボタン
			L_RB_Tool.Add ( RB_Main );
			L_RB_Tool.Add ( RB_CRect );
			L_RB_Tool.Add ( RB_HRect );
			L_RB_Tool.Add ( RB_ARect );
			L_RB_Tool.Add ( RB_ORect );
			L_RB_Tool.Add ( RB_Ef );

			int i = 0;
			foreach ( _RB_Tool rbt in L_RB_Tool )
			{
				this.Controls.Add ( rbt );
				rbt.Location = new Point ( -2, -2 + 36 * i );
				rbt.BringToFront ();

				++i;
			}



			//@info リソースはResources.resxの中、Resources.Desighner.csにおいて
			//「作成時の名前」でnamespaceが定義されている
			//global::Ctrl_Chara.Properties.Resources.～を
			//ScriptEditorに修正

			RB_Main.Image = Properties.Resources.tool_hand;
			RB_CRect.Image =Properties.Resources.tool_CRect;
			RB_HRect.Image =Properties.Resources.tool_HRect;
			RB_ARect.Image =Properties.Resources.tool_ARect;
			RB_ORect.Image =Properties.Resources.tool_ORect;
			RB_Ef.Image = Properties.Resources.tool_efhand;
		}

		//環境設定
		public void SetEnviron ( EditChara editchara )
		{
			//ツールの初期化
			Tlimg_Main.SetCtrl ();

			//ラジオボタンの初期化
		}

		//キャラデータ設定
		public void SetCharaData ( Chara ch, BD_IMGD bd_imgd )
		{
			paintImage.SetCharaData ( ch, bd_imgd );
		}
	}
}
