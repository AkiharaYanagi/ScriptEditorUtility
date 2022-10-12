//
//	キー設定のメインデータ
//
using System;
using System.Collections.Generic;

using SlimDX.DirectInput;


namespace ScriptEditor
{
	using DCT_GM_DVC = Dictionary < GameInput, DeviceInput >;
	
	public class KeySettings
	{
		//ゲーム入力とデバイス入力
		public DCT_GM_DVC Dct_Gm_Dvc = new DCT_GM_DVC ();

		//コンストラクタ
		public KeySettings ()
		{
			Dct_Gm_Dvc.Add ( GameInput.P1_UP, new DeviceInput( DeviceType.Keyboard, Key.UpArrow ) );
			Dct_Gm_Dvc.Add ( GameInput.P1_UP, new DeviceInput( DeviceType.Keyboard, Key.UpArrow ) );
			Dct_Gm_Dvc.Add ( GameInput.P1_UP, new DeviceInput( DeviceType.Keyboard, Key.UpArrow ) );
			Dct_Gm_Dvc.Add ( GameInput.P1_UP, new DeviceInput( DeviceType.Keyboard, Key.UpArrow ) );
			Dct_Gm_Dvc.Add ( GameInput.P1_UP, new DeviceInput( DeviceType.Keyboard, Key.UpArrow ) );
			Dct_Gm_Dvc.Add ( GameInput.P1_UP, new DeviceInput( DeviceType.Keyboard, Key.UpArrow ) );
			Dct_Gm_Dvc.Add ( GameInput.P1_UP, new DeviceInput( DeviceType.Keyboard, Key.UpArrow ) );
			Dct_Gm_Dvc.Add ( GameInput.P1_UP, new DeviceInput( DeviceType.Keyboard, Key.UpArrow ) );
			Dct_Gm_Dvc.Add ( GameInput.P1_UP, new DeviceInput( DeviceType.Keyboard, Key.UpArrow ) );
			Dct_Gm_Dvc.Add ( GameInput.P1_UP, new DeviceInput( DeviceType.Keyboard, Key.UpArrow ) );
			Dct_Gm_Dvc.Add ( GameInput.P1_UP, new DeviceInput( DeviceType.Keyboard, Key.UpArrow ) );
			Dct_Gm_Dvc.Add ( GameInput.P1_UP, new DeviceInput( DeviceType.Keyboard, Key.UpArrow ) );
			Dct_Gm_Dvc.Add ( GameInput.P1_UP, new DeviceInput( DeviceType.Keyboard, Key.UpArrow ) );
			Dct_Gm_Dvc.Add ( GameInput.P1_UP, new DeviceInput( DeviceType.Keyboard, Key.UpArrow ) );
			Dct_Gm_Dvc.Add ( GameInput.P1_UP, new DeviceInput( DeviceType.Keyboard, Key.UpArrow ) );
			Dct_Gm_Dvc.Add ( GameInput.P1_UP, new DeviceInput( DeviceType.Keyboard, Key.UpArrow ) );
			Dct_Gm_Dvc.Add ( GameInput.P1_UP, new DeviceInput( DeviceType.Keyboard, Key.UpArrow ) );
			Dct_Gm_Dvc.Add ( GameInput.P1_UP, new DeviceInput( DeviceType.Keyboard, Key.UpArrow ) );
		}
	}
}
