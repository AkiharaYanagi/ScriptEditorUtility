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
		//データ編集
		public EditCompend EditCompend { get; set; } = null;


		//イメージ描画
		private _PaintImage paintImage = new _PaintImage ();

		//ツール
		private List < ToolImage >	L_Tool = new List<ToolImage> ();
		private ToolImage_Main Tlimg_Main = new ToolImage_Main ();
		private ToolImage_Rect Tlimg_CRect = new ToolImage_Rect ();
		private ToolImage_Rect Tlimg_HRect = new ToolImage_Rect ();
		private ToolImage_Rect Tlimg_ARect = new ToolImage_Rect ();
		private ToolImage_Rect Tlimg_ORect = new ToolImage_Rect ();
		private ToolImage_Ef Tlimg_Ef = new ToolImage_Ef ();

		//現在選択ツール
		public ToolImage SelectingTool { get; set; } = null;	//選択中ツール

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
			paintImage.Dock = DockStyle.Fill;
			paintImage.MouseDown += new MouseEventHandler ( this.PI_MouseDown );
			paintImage.MouseMove += new MouseEventHandler ( this.PI_MouseMove );
			paintImage.MouseUp += new MouseEventHandler ( this.PI_MouseUp);

			//ツール
			L_Tool.Add ( Tlimg_Main );
			L_Tool.Add ( Tlimg_CRect );
			L_Tool.Add ( Tlimg_HRect );
			L_Tool.Add ( Tlimg_ARect );
			L_Tool.Add ( Tlimg_ORect );
			L_Tool.Add ( Tlimg_Ef );
			SelectingTool = Tlimg_Main;

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
				rbt.Tl_Img = L_Tool [ i ];

				++i;
			}

			//ラジオボタンにリソース画像を指定

			//@info リソースはResources.resxの中、Resources.Desighner.csにおいて
			//「作成時の名前」でnamespaceが定義されている
			//global::Ctrl_Chara.global::Ctrl_Chara.Properties.Resources.～を
			//ScriptEditorに修正

			RB_Main.SetEnviron ( this, global::Ctrl_Chara.Properties.Resources.tool_hand, "メインイメージ移動" );
			RB_CRect.SetEnviron ( this, global::Ctrl_Chara.Properties.Resources.tool_CRect, "接触枠" );
			RB_HRect.SetEnviron ( this, global::Ctrl_Chara.Properties.Resources.tool_HRect, "当り枠" );
			RB_ARect.SetEnviron ( this, global::Ctrl_Chara.Properties.Resources.tool_ARect, "攻撃枠" );
			RB_ORect.SetEnviron ( this, global::Ctrl_Chara.Properties.Resources.tool_ORect, "相殺枠" );
			RB_Ef.SetEnviron ( this, global::Ctrl_Chara.Properties.Resources.tool_efhand, "Efイメージ移動" );

			//選択
			RB_Main.Select ();

			//イベント
//			this.MouseDoubleClick += new MouseEventHandler ( this );
		}

		//環境設定
		public void SetEnviron ( EditCompend ec )
		{
			//編集の設定
			EditCompend = ec;

			//表示の設定
			paintImage.SetEnviron ( ec );

			//ツールの初期化
			foreach ( ToolImage ti in L_Tool )
			{
				ti.SetCtrl ( EditCompend );
			}
			Tlimg_CRect.EditLRct = ec.EditScript.EditAllRect.L_CRect;
			Tlimg_HRect.EditLRct = ec.EditScript.EditAllRect.L_HRect;
			Tlimg_ARect.EditLRct = ec.EditScript.EditAllRect.L_ARect;
			Tlimg_ORect.EditLRct = ec.EditScript.EditAllRect.L_ORect;

			//ラジオボタンの初期化
		}

		//キャラデータ設定
		public void SetCharaData ( Chara ch, BD_IMGD bd_imgd )
		{
			paintImage.SetCharaData ( ch, bd_imgd );
		}


		protected override void OnPaint ( PaintEventArgs e )
		{
			paintImage.Invalidate ();
			base.OnPaint ( e );
		}

		//-----------------------------------------------------------
		//マウスイベント
		//@info 最前面のコントロールでなければイベントが発生しない
		//	最前面であるPaintImageのイベントをここで記述する。
		//	ツール類の実体を持つCtrl_Imageが定義する。

		//マウス押下
		void PI_MouseDown ( object sender, MouseEventArgs e )
		{

			if ( MouseButtons.Left == e.Button )
			{
				paintImage.UpdatePtClient ();
				SelectingTool.PtPbImageBase = paintImage.PtPbImageBase;
				SelectingTool.PtClient = paintImage.PtClient;
				SelectingTool.MouseDown_L ();
			}

			base.OnMouseDown ( e );
		}

		//マウス移動
		void PI_MouseMove ( object sender, MouseEventArgs e )
		{
			if ( MouseButtons.Left == e.Button )
			{
				SelectingTool.MouseMove_L ();
			}

			paintImage.Invalidate ();

			base.OnMouseMove ( e ); 
		}

		//マウス離上
		void PI_MouseUp ( object sender, MouseEventArgs e )
		{
			if ( MouseButtons.Left == e.Button )
			{
				SelectingTool.MouseUp_L ();
			}

			paintImage.Invalidate ();

			base.OnMouseUp ( e );
		}
	}
}
