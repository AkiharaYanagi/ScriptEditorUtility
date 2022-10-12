//-------------------------------------------------------------------
//	デバイス入力保存  
//		ジョイスティック、キーボードの値を１つ、または未入力状態を持つ。
//-------------------------------------------------------------------
using System;

using SlimDX.DirectInput;


namespace ScriptEditor
{
	public class DeviceInput
	{
		//デバイスタイプ
		public DeviceType Type { get; set; } = DeviceType.Other;

		//各デバイス入力
		public JoystickInput JoystickInput { get; set; }
		public Key keyboardInput { get; set; }

		//コンストラクタ
		public DeviceInput ( DeviceType t, Key k )
		{
			Type = t;
			keyboardInput = k;
		}

		//文字列
		public override string ToString ()
		{
			string str = "";
			switch ( Type )
			{
			case DeviceType.Joystick: str = JoystickInput.ToString (); break;
			case DeviceType.Keyboard: str = keyboardInput.ToString (); break;
			default: break;
			}
			return str;
		}
	}

	//レバー定義
	public enum LEVER
	{
		LVR_UP,
		LVR_DOWN,
		LVR_LEFT,
		LVR_RIGHT,
	}

	//ジョイスティック入力
	public class JoystickInput
	{
		public int DeviceID = 0;

		//入力の種類
		public ObjectDeviceType ObDbcType = ObjectDeviceType.NoData;

		//ボタン
		public int ButtonID = 0;

		//レバー
		public LEVER lvr = LEVER.LVR_UP;

		public JoystickInput ()
		{

		}

		public JoystickInput ( int device, int button )
		{
			DeviceID = device;
			ButtonID = button;
		}

		public override string ToString ()
		{
			string ret = "";

			if ( ObDbcType == ObjectDeviceType.Button )
			{
				ret = "Joy" + DeviceID.ToString () + "_Btn" + ButtonID;
			}

			if ( ObDbcType == ObjectDeviceType.PointOfViewController )
			{
				ret = "Joy" + DeviceID.ToString () + "_" + lvr.ToString ();
			}

			if ( ObDbcType == ObjectDeviceType.Axis )
			{
				ret = "Joy" + DeviceID.ToString () + "_" + lvr.ToString ();
			}

			return ret;
		}
	}
}
