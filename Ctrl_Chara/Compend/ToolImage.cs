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


		//データ編集を設定
		public virtual void SetCtrl ( EditCompend ec )
		{ 
			EditCompend = ec; 
		}

		//--------------------------------------------
		//マウスイベント

		//押下
		public virtual void MouseDown_L ()
		{
			prePt = EditCompend.SelectedScript.Pos;	//前回の位置
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
	}

	//----------------------------------------------------------------------
	//枠設定
	public class ToolImage_Rect : ToolImage 
	{
	}

}
