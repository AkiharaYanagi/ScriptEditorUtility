//-------------------------------------------------------------------
//	デバイス入力保存  
//		ジョイスティック、キーボードの値を１つ、または未入力状態を持つ。
//-------------------------------------------------------------------
using System;

using SlimDX.DirectInput;


namespace KeyConfig
{
	public class DeviceInput
	{
		//デバイスタイプ
		public DeviceType Type { get; set; } = DeviceType.Keyboard;

		public JoyStickInput JoyStickInput { get; set; }
		public Key keyboard { get; set; }

		public DeviceInput ()
		{

		}
	}


	//ジョイスティック入力
	public struct JoyStickInput
	{
		public int DeviceID;
		public int ButtonID;

		public JoyStickInput ( int device, int button )
		{
			DeviceID = device;
			ButtonID = button;
		}
	}
}
