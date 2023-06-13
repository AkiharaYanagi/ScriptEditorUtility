using System.Drawing;
using System.Diagnostics;


namespace ScriptEditor
{
	//==================================================================================
	//
	//	OLD_ 書出読込共通の要素を定義する
	//
	//==================================================================================

	//@info 自由個数設定の値はbyteではなく、uintにする

	//Scriptに要素を追加するとき
	//・Scriptクラス
	//・IOChara
	//		CharaToDoc, DocToChara,
	//		SaveBin, LoadBin
	//・TestChara
	//
	//・旧データを変換
#if false
	//配列添字取得用
	public static class IOChara
	{
		public static int AttrToInt ( Element e, int attr )
		{
			int i = 0;
			try
			{
				i = int.Parse ( e.Attributes[ attr ].Value );
			}
			catch ( System.Exception exc )
			{
				Debug.Write ( exc.ToString () );
			}
			return i;
		}

		public static Point AttrToPoint ( Element e, int enumName0, int enumName1 )
		{
			return new Point ( AttrToInt ( e, enumName0 ), AttrToInt ( e, enumName1 ) );
		}
	}


	//共通アトリビュート
	public enum ATTR_
	{
		NAME_0 = 0,	
	}
#endif

	public enum ELEMENT_CHARA_old
	{
		VER,
		MAIN_IMAGE_LIST,
		EF_IMAGE_LIST,
		ACTION_LIST,
		EF_LIST,
		COMMAND_LIST,
		BRANCH_LIST,
		ROUTE_LIST,
	}

	public enum ATTR_ACTION_old
	{
		ELAC_NAME,
		ELAC_NEXT_NAME,
		ELAC_NEXT_ID,
		ELAC_CATEGORY,
		ELAC_POSTURE,
		ELAC_HITNUM,
		ELAC_HIT_PITCH,
		ELAC_BALANCE,
	}

	public enum ATTR_SCP_old
	{
		GROUP,
		IMG_NAME,
		IMG_ID,
		X, Y, 
		CLC_ST, 
		VX, VY,
		AX, AY,
		POWER,
		WARP,
		RECOIL_I, RECOIL_E,
		BALANCE_I, BALANCE_E,
		BLACKOUT,
		VIBRATION,
		STOP,
		ROTATE,
		AFTERIMAGE_PITCH,
		AFTERIMAGE_N,
		AFTERIMAGE_TIME,
		VIBRATION_S,
		COLOR,
		COLOR_TIME,
	}

	public enum ELEMENT_SCRIPT_old
	{
		ELSC_ROUTE,
		ELSC_EFGNRT,
		ELSC_CRECT,
		ELSC_ARECT,
		ELSC_HRECT,
		ELSC_ORECT,
	}

	public enum ATTRIBUTE_COMMAND_old
	{
		NAME,
		LIMIT_TIME,
		ID_LVR,
	}

	public enum ELEMENT_BRANCH_old
	{
		ELBR_COMMAND_NAME,
		ELBR_COMMAND_ID,
		ELBR_ACTION_NAME,
		ELBR_ACTION_ID,
		ELBR_FRAME,
	}

	public enum ATTR_BRANCH_old
	{
		NAME,
		CONDITION,
		CMD_NAME,
		CMD_ID,
		ACT_NAME,
		ACT_ID,
		FRAME,
	}

	public enum ATTR_ROUTE_old
	{
		NAME,
		SUMMARY,
	}

	public enum ELMT_EFGNRT_old
	{
		ELEG_NAME,
		ELEG_EFNAME,
		ELEG_PT_X,
		ELEG_PT_Y,
		ELEG_PT_Z,
		ELEG_GNRT,
		ELEG_LOOP,
		ELEG_SYNC,
	}

}
