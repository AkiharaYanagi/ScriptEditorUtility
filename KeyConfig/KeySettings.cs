//
//	キー設定のメインデータ
//
using System;
using System.Collections.Generic;
using System.IO;

using SlimDX.DirectInput;


namespace ScriptEditor
{
	using DCT_GM_DVC = Dictionary < GameInput, DeviceInput >;
	
	public class KeySettings
	{
		//保存ファイル名
		private string Filename = "keyConfig.dat";

		//ゲーム入力とデバイス入力
		public DCT_GM_DVC Dct_Gm_Dvc = new DCT_GM_DVC ();

		//コンストラクタ
		public KeySettings ()
		{
			Dct_Gm_Dvc.Add ( GameInput.P1_UP	, new DeviceInput( DeviceType.Keyboard, Key.UpArrow ) );
			Dct_Gm_Dvc.Add ( GameInput.P1_DOWN	, new DeviceInput( DeviceType.Keyboard, Key.DownArrow ) );
			Dct_Gm_Dvc.Add ( GameInput.P1_LEFT	, new DeviceInput( DeviceType.Keyboard, Key.LeftArrow ) );
			Dct_Gm_Dvc.Add ( GameInput.P1_RIGHT	, new DeviceInput( DeviceType.Keyboard, Key.RightArrow ) );
			Dct_Gm_Dvc.Add ( GameInput.P1_KEY0	, new DeviceInput( DeviceType.Keyboard, Key.Z ) );
			Dct_Gm_Dvc.Add ( GameInput.P1_KEY1	, new DeviceInput( DeviceType.Keyboard, Key.X) );
			Dct_Gm_Dvc.Add ( GameInput.P1_KEY2	, new DeviceInput( DeviceType.Keyboard, Key.C ) );
			Dct_Gm_Dvc.Add ( GameInput.P1_KEY3	, new DeviceInput( DeviceType.Keyboard, Key.V ) );
			Dct_Gm_Dvc.Add ( GameInput.P1_KEY4	, new DeviceInput( DeviceType.Keyboard, Key.Comma ) );
			Dct_Gm_Dvc.Add ( GameInput.P1_KEY5	, new DeviceInput( DeviceType.Keyboard, Key.Period ) );
			Dct_Gm_Dvc.Add ( GameInput.P1_KEY6	, new DeviceInput( DeviceType.Keyboard, Key.Slash ) );
			Dct_Gm_Dvc.Add ( GameInput.P1_KEY7	, new DeviceInput( DeviceType.Keyboard, Key.Backslash ) );

			Dct_Gm_Dvc.Add ( GameInput.P2_UP	, new DeviceInput( DeviceType.Keyboard, Key.Home ) );
			Dct_Gm_Dvc.Add ( GameInput.P2_DOWN	, new DeviceInput( DeviceType.Keyboard, Key.End ) );
			Dct_Gm_Dvc.Add ( GameInput.P2_LEFT	, new DeviceInput( DeviceType.Keyboard, Key.Delete) );
			Dct_Gm_Dvc.Add ( GameInput.P2_RIGHT	, new DeviceInput( DeviceType.Keyboard, Key.PageUp ) );
			Dct_Gm_Dvc.Add ( GameInput.P2_KEY0	, new DeviceInput( DeviceType.Keyboard, Key.A ) );
			Dct_Gm_Dvc.Add ( GameInput.P2_KEY1	, new DeviceInput( DeviceType.Keyboard, Key.S ) );
			Dct_Gm_Dvc.Add ( GameInput.P2_KEY2	, new DeviceInput( DeviceType.Keyboard, Key.D ) );
			Dct_Gm_Dvc.Add ( GameInput.P2_KEY3	, new DeviceInput( DeviceType.Keyboard, Key.F ) );
			Dct_Gm_Dvc.Add ( GameInput.P2_KEY4	, new DeviceInput( DeviceType.Keyboard, Key.L ) );
			Dct_Gm_Dvc.Add ( GameInput.P2_KEY5	, new DeviceInput( DeviceType.Keyboard, Key.Semicolon ) );
			Dct_Gm_Dvc.Add ( GameInput.P2_KEY6	, new DeviceInput( DeviceType.Keyboard, Key.Colon ) );
			Dct_Gm_Dvc.Add ( GameInput.P2_KEY7	, new DeviceInput( DeviceType.Keyboard, Key.RightBracket ) );
		}

		//保存
		public void Save ()
		{
			ConvertSlimToDx cnvSToD = new ConvertSlimToDx ();

			using ( FileStream fs = new FileStream ( Filename, FileMode.Create, FileAccess.Write ) )
			using ( BinaryWriter bw = new BinaryWriter ( fs ) )
			{
				foreach ( GameInput gmIpt in Dct_Gm_Dvc.Keys )
				{
					DeviceInput dvcIpt = Dct_Gm_Dvc [ gmIpt ];
					
					//デバイスタイプ
					bw.Write ( cnvSToD.CnvDeviceType ( dvcIpt.Type ) );

					//各デバイスの記録
					switch ( dvcIpt.Type )
					{
					case DeviceType.Keyboard : 
						int iKey = cnvSToD.Keyboard ( dvcIpt.keyboardInput );
						bw.Write ( iKey );
					break;
					
					case DeviceType.Joystick : 
						bw.Write (		 dvcIpt.JoystickInput.DeviceID );
						bw.Write ( cnvSToD.CnvJoyType ( dvcIpt.JoystickInput.ObDvcType ) );
						bw.Write ( (int) dvcIpt.JoystickInput.lvr );
						bw.Write (		 dvcIpt.JoystickInput.ButtonID );
					break;
					}

				}

				bw.Flush ();
			}
		}

		//読込
		public void Load ()
		{
			ConvertDxToSlim cnvDToS = new ConvertDxToSlim ();

			using ( FileStream fs = new FileStream ( Filename, FileMode.Open, FileAccess.Read ) )
			using ( BinaryReader br = new BinaryReader ( fs ) )
			{
				foreach ( GameInput gmIpt in Dct_Gm_Dvc.Keys )
				{
					DeviceInput dvcIpt = Dct_Gm_Dvc [ gmIpt ];

					int dvcType = br.ReadInt32 ();
					dvcIpt.Type = cnvDToS.CnvDeviceType ( (DeviceTypeForGame)dvcType );

					//各デバイスの記録
					switch ( dvcIpt.Type )
					{
					case DeviceType.Keyboard : 
						int iKey = br.ReadInt32 ();
						dvcIpt.keyboardInput = cnvDToS.Keyboard ( (DxKey)iKey );
					break;
					
					case DeviceType.Joystick : 
						dvcIpt.JoystickInput.DeviceID = br.ReadInt32 ();
						dvcIpt.JoystickInput.ObDvcType = cnvDToS.CnvJoyType ( (JOY_INPUT_TYPE)br.ReadInt32 () );
						dvcIpt.JoystickInput.lvr = (LEVER)br.ReadInt32 ();
						dvcIpt.JoystickInput.ButtonID = br.ReadInt32 ();
					break;
					}
				}
			}
		}

		//データ指定
		public void Set ( GameInput gmIpt, DeviceInput dvcIpt )
		{
			Dct_Gm_Dvc [ gmIpt ] = dvcIpt;
		}

	}
}
