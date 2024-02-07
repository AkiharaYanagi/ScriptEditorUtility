namespace ScriptEditor
{
	using GK_L = GameKeyData.Lever;
	using GK_B = GameKeyData.Button;
	using GK_ST = GameKeyData.GameKeyState;

	//選択されているコマンド表中のキー
	public class SelectKey
	{
		//未選択状態
		public bool Selecting { get; set; } = true;

		//対象データ
		public Command Cmd { get; set; } = new Command ();

		//選択中のフレーム数
		public int Frame { get; set; } = 0;

		//レバー選択位置
		public GK_L SelectedLvr { get; set; } = GK_L.LVR_N;

		//キー種類毎の表示位置
		public enum KeyKind
		{
			ARROW,
			KEY_L,
			KEY_Ma,
			KEY_Mb,
			KEY_H,
			BUTTON,
		}
		public const int DispKeyaNum = 4;

		public KeyKind Kind { get; set; } = KeyKind.ARROW;


		public void Set ( Command cmd, int frame, KeyKind keyKind )
		{
			Cmd = cmd;
			Frame = frame;
			Kind = keyKind;
			Selecting = true;
		}

		public GameKeyCommand GetGKC ( Command cmd )
		{
			if ( cmd.ListGameKeyCommand.Count <= Frame ) { return null; }
			return cmd.ListGameKeyCommand [ Frame ];
		}

#if false
		public GK_ST GetSt ( Command cmd )
		{
			if ( cmd.ListGameKeyCommand.Count <= Frame ) { return GK_ST.KEY_WILD; }

			GameKeyCommand gkc = cmd.ListGameKeyCommand [ Frame ];

			GK_ST ret = GK_ST.KEY_OFF;
			switch ( Kind )
			{
			case KeyKind.ARROW: ret = gkc.GetLvrSt (); break;
			case KeyKind.KEY_L: ret = gkc.Btn [ 0 ]; break;
			case KeyKind.KEY_Ma: ret = gkc.Btn [ 1 ]; break;
			case KeyKind.KEY_Mb: ret = gkc.Btn [ 2 ]; break;
			case KeyKind.KEY_H: ret = gkc.Btn [ 3 ]; break;
			}

			return ret; 
		}
#endif

		//選択中レバー方向の状態を取得
		public GK_ST GetLeverSt ()
		{
			GK_ST ret = GK_ST.KEY_WILD;
			GameKeyCommand gkc = Cmd.ListGameKeyCommand [ Frame ];
			ret = gkc.DctLvrSt [ SelectedLvr ];
			return ret;
		}

		//選択中キー(レバー,ボタンのいずれか)状態を取得
		public GK_ST GeSelectedKeySt ()
		{
			GK_ST ret = GK_ST.KEY_WILD;
			GameKeyCommand gkc = Cmd.ListGameKeyCommand [ Frame ];

			switch ( Kind )
			{
			case KeyKind.ARROW : ret = gkc.DctLvrSt [ SelectedLvr ]; break;
			case KeyKind.KEY_L : ret = gkc.DctBtnSt [ GK_B.BTN_0 ]; break;
			case KeyKind.KEY_Ma: ret = gkc.DctBtnSt [ GK_B.BTN_1 ]; break;
			case KeyKind.KEY_Mb: ret = gkc.DctBtnSt [ GK_B.BTN_2 ]; break;
			case KeyKind.KEY_H : ret = gkc.DctBtnSt [ GK_B.BTN_3 ]; break;
			default:break;
			}
			
			return ret;
		}

		//対象コマンドに状態を設定
		public void SetSt ( Command cmd, GK_ST gkcst )
		{
			if ( cmd.ListGameKeyCommand.Count <= Frame ) { return; }

			GameKeyCommand gkc = cmd.ListGameKeyCommand [ Frame ];

			switch ( Kind )
			{
			case KeyKind.ARROW	: gkc.SetLvrSt ( gkcst, SelectedLvr ); break;
			case KeyKind.KEY_L	: gkc.DctBtnSt [ GK_B.BTN_0 ] = gkcst; break;
			case KeyKind.KEY_Ma	: gkc.DctBtnSt [ GK_B.BTN_1 ] = gkcst; break;
			case KeyKind.KEY_Mb	: gkc.DctBtnSt [ GK_B.BTN_2 ] = gkcst; break;
			case KeyKind.KEY_H	: gkc.DctBtnSt [ GK_B.BTN_3 ] = gkcst; break;
			}
		}

		//表示上の位置からレバーインデックスに変換して保存
		public void SetDispToLvr ( int indexOfDisp )
		{
			SelectedLvr = IndexOfDisp_ToLvr ( indexOfDisp );
		}

		public GK_L IndexOfDisp_ToLvr ( int indexOfDisp )
		{
			//表示インデックスからレバーインデックスに変換
			// 0 1 2		C7 C8 C9
			// 3 4 5	→	C4 CN C6
			// 6 7 8		C1 C2 C3

			GK_L[] ary = { GK_L.LVR_7, GK_L.LVR_8, GK_L.LVR_9, GK_L.LVR_4, GK_L.LVR_N, GK_L.LVR_6, GK_L.LVR_1, GK_L.LVR_2, GK_L.LVR_3 };
			return ary [ indexOfDisp ];
		}

		public int LvrTo_IndexOfDisp ()
		{
			//レバーインデックスから表示インデックスに変換
			//C7 C8 C9		0 1 2		
			//C4 CN C6	→	3 4 5		
			//C1 C2 C3		6 7 8		

			int[] ary = { 6, 7, 8, 5, 2, 1, 0, 3, 8 };
			return ary [ (int)SelectedLvr ];
		}
	}
}
