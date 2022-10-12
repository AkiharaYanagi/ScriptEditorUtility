namespace ScriptEditor
{
	//ゲームで用いる入力種類
    public enum GameInput
    {
        P1_UP,
		P1_DOWN,
		P1_LEFT,
		P1_RIGHT,
		P1_KEY0,
        P1_KEY1,
        P1_KEY2,
        P1_KEY3,
        P1_KEY4,
        P1_KEY5,
        P1_KEY6,
        P1_KEY7,

        P2_UP,
        P2_DOWN,
        P2_LEFT,
        P2_RIGHT,
        P2_KEY0,
        P2_KEY1,
        P2_KEY2,
        P2_KEY3,
        P2_KEY4,
        P2_KEY5,
        P2_KEY6,
        P2_KEY7,
    };

    //ゲームで利用できるデバイスの種類
    public enum DeviceTypeForGame
    {
        KEYBOARD = 0,
        JOYSTICK = 1,
		MOUSE = 2,
        NODEVICE = 3,
    }

}
