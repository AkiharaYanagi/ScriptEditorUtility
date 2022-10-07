using System;
using System.Collections.Generic;
using SlimDX.DirectInput;

namespace KeyConfig
{
	public class DxKeyboard
	{
		private Keyboard keyboard;
		private KeyboardState kbState;
		private KeyboardState pre_kbState;
	

		//コンストラクタ
		public DxKeyboard ( DirectInput dInput )
		{
			keyboard = new Keyboard ( dInput );
			keyboard.Acquire ();
			kbState = keyboard.GetCurrentState ();
			pre_kbState = keyboard.GetCurrentState ();
		}

		//毎回の更新
		public void Update ()
		{
			kbState = keyboard.GetCurrentState ();
		}

		//キーボードステートのコピー
		private void CopyKeyboardState ( KeyboardState dst , KeyboardState src )
		{
			foreach ( Key k in src.PressedKeys )
			{
			}
		}
	}


	//------------------------------------------------------------------
	//キーボード入力保存 < bool >
	//SlimDx.DirectInput の KeyboardStateは「押したキー」「離したキー」を１つずつ保存している
	//前状態を参照するため、Keyすべてにおいて記録する
	public class b_KeyState
	{
		private List < Key > bKey;
	}


}
