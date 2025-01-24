using System.Drawing;
using System.Windows.Forms;


namespace ScriptEditor
{
	//----------------------------------------------------------------------
	//ツール基本クラス
	public class ToolImage
	{
		//データ編集
		protected EditCompend EditCompend = new EditCompend ();

		//ドラッグ位置
		protected Point prePt = new Point ( 0, 0 );
		protected Point startPt = new Point ( 0, 0 );
		protected Point dragPt = new Point ( 0, 0 );

		//基準位置
		// Ctrl_ImageのMouseDown()中で手動で更新される
		public Point PtPbImageBase { get; set; } = new Point ();
		public Point PtClient { get; set; } = new Point ();


		//データ編集を設定
		public virtual void SetCtrl ( EditCompend ec )
		{ 
			EditCompend = ec; 
		}

		//--------------------------------------------
		//マウスイベント

		//押下（ドラッグ開始）
		public virtual void MouseDown_L ()
		{
			startPt = Cursor.Position;
			dragPt = new Point ( 0, 0 );
		}

		//移動
		public virtual void MouseMove_L () 
		{
			dragPt = PointUt.PtSub ( Cursor.Position, startPt );
		}

		//離上
		public virtual void MouseUp_L ()
		{
		}
		//--------------------------------------------

	}

	//----------------------------------------------------------------------
	//ツール：メインイメージ移動
	public class ToolImage_Main : ToolImage 
	{
		//マウス押下（ドラッグ開始）
		public override void MouseDown_L ()
		{
			base.MouseDown_L ();

			Script scp = EditCompend.SelectedScript;
			prePt = scp.Pos;	//前回の位置を保存
		}

		//マウス移動
		public override void MouseMove_L ()
		{
			base.MouseMove_L ();

			Script scp = EditCompend.SelectedScript;
			scp.Pos = PointUt.PtAdd ( prePt, dragPt );
		}
	}

	//Efイメージ移動
	public class ToolImage_Ef : ToolImage 
	{
		//マウス押下（ドラッグ開始）
		public override void MouseDown_L ()
		{
			base.MouseDown_L ();

			EditEfGnrt eefg = EditCompend.EditScript.EditEfGnrt;
			prePt = eefg.SelectedEfGnrt.Pt;	//前回の位置を保存
		}

		//マウス移動
		public override void MouseMove_L ()
		{
			base.MouseMove_L ();

			EditEfGnrt eefg = EditCompend.EditScript.EditEfGnrt;
			eefg.SelectedEfGnrt.Pt = PointUt.PtAdd ( prePt, dragPt );
		}
	}

	//----------------------------------------------------------------------
	//枠設定
	public class ToolImage_Rect : ToolImage 
	{
		//対象 枠リスト編集
		public EditListRect EditLRct { get; set; } = new EditListRect ();

		//枠設定の開始位置
		private Point startRectPt = new Point ( 0, 0 );


		//マウス押下（ドラッグ開始）
		public override void MouseDown_L ()
		{
			base.MouseDown_L ();

			startRectPt = PointUt.PtSub ( PtClient, PtPbImageBase );
		}

		//マウス移動
		public override void MouseMove_L ()
		{
			base.MouseMove_L ();

			//Rectangle rct = new Rectangle ( startRectPt.X, startRectPt.Y, dragPt.X, dragPt.Y );
			//EditLRct.SetRect ( rct );
			
			//new を用いないで値を設定する
			EditLRct.SetRect ( startRectPt.X, startRectPt.Y, dragPt.X, dragPt.Y );
		}
	}

}
