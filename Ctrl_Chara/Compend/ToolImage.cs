using System;


namespace ScriptEditor
{
	//----------------------------------------------------------------------
	//ツール基本クラス
	public class ToolImage
	{
		public virtual void SetCtrl () { }
	}


	//----------------------------------------------------------------------
	//ツール：メインイメージ移動
	public class ToolImage_Main : ToolImage 
	{
		public override void SetCtrl ()
		{
			base.SetCtrl ();
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
