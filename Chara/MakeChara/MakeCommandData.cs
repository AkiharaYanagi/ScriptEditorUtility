using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;


namespace ScriptEditor
{
	//エイリアスの設定
	using GKC_ST = GameKeyData.GameKeyState;
	using GK_L = GameKeyData.Lever;
	using GK_B = GameKeyData.Button;

	using BD_CMD = BindingDictionary < Command >;
	using BD_BRC = BindingDictionary < Branch >;


	//テキストデータからコマンドリストを作成する
	public class MakeCommandData
	{
		//デフォルトコマンド名
		private enum ENM_CMD
		{ 
			_N,
			L, Ma, Mb, H,
			_6, _4, 
			_6off, _4off, _6_6, _4_4, 
			_8, _9, _7, 
			_2_8, _2_9, _2_7, 
			_6L, _6Ma, _6Mb, _6H, 
			_4L, _4Ma, _4Mb, _4H, 
			_2L, _2Ma, _2Mb, _2H, 
			_8L, _8Ma, _8Mb, _8H, 
			SP0_L, SP0_Ma, SP0_Mb, SP0_H, 
			SP1_L, SP1_Ma, SP1_Mb, SP1_H, 
			SP2_L, SP2_Ma, SP2_Mb, SP2_H, 
			OD0_L, OD0_Ma, OD0_Mb, OD0_H, 
			OD1_L, OD1_Ma, OD1_Mb, OD1_H, 
		};

		public void Make ( Chara ch )
		{
			try
			{
				_Make ( ch );
			}
			//ファイルが無いとき
			catch ( FileNotFoundException e )
			{
				Debug.WriteLine ( e );

				//１つダミーを追加
				EditChara.Inst.AddCommand ();
			}
		}

		private void _Make ( Chara ch )
		{
			string curDir = Environment.CurrentDirectory;
			string filename = "PreData\\CommandList.txt";

			//コマンドデータ(.txtファイル)から作成
			FileStream fstrm = new FileStream ( filename, FileMode.Open, FileAccess.Read );
			StreamReader sr = new StreamReader ( fstrm, Encoding.UTF8 );
#if false
			EditBehavior eb = EditChara.Inst.EditBehavior;

			//カンマ区切り、スペース改行は飛ばす
			List < string > lstr = new List<string> ();
			while ( ! sr.EndOfStream )
			{
				string str = sr.ReadLine ();
				string[] a_str = str.Split ( ',' );
				foreach ( string s in a_str )
				{
					//空白を消去
					string _s = s.TrimStart ( ' ' ).TrimEnd ( ' ' );
					//空文字のみは飛ばす
					if ( _s == "" ) { continue; }
					lstr.Add ( _s );
				}
			}

			//キャラに設定
			foreach ( string str in lstr )
			{
				EditChara.Inst.AddCommand ( str );
			}
#endif
			//テキストからコマンドに変換
			TextToCommand ttc = new TextToCommand ();
			ttc.Do_BD ( sr, ch.BD_Command );

#if false
			//各種詳細設定
			MakeCommand ( ch.BD_Command );
#endif
		}



		private void MakeCommand ( BD_CMD bd_c )
		{
			GKC_ST IS = GKC_ST.KEY_IS;
			GKC_ST NS = GKC_ST.KEY_NIS;
			GKC_ST PS = GKC_ST.KEY_PUSH;

#if false
			//コマンド名から作成
			foreach ( string name in Enum.GetNames ( typeof ( ENM_CMD ) ) )
			{
				EditChara.Inst.AddCommand ( name );
			}
#endif

			SetCmdBtn ( bd_c, ENM_CMD.L , 0, 1, PS, GK_B.BTN_0 );		// L  : Btn0
			SetCmdBtn ( bd_c, ENM_CMD.Ma, 0, 1, PS, GK_B.BTN_1 );		// Ma : Btn1
			SetCmdBtn ( bd_c, ENM_CMD.Mb, 0, 1, PS, GK_B.BTN_2 );		// Mb : Btn2
			SetCmdBtn ( bd_c, ENM_CMD.H , 0, 1, PS, GK_B.BTN_3 );		// H  : Btn3

			SetCmdLvr ( bd_c, ENM_CMD._6,	0, 1, IS, GK_L.LVR_6 );
			SetCmdLvr ( bd_c, ENM_CMD._4,	0, 1, IS, GK_L.LVR_4 );
			SetCmdLvr ( bd_c, ENM_CMD._6off,0, 1, NS, GK_L.LVR_6 );
			SetCmdLvr ( bd_c, ENM_CMD._4off,0, 1, NS, GK_L.LVR_4 );
			SetCmdLvr ( bd_c, ENM_CMD._8,	0, 1, IS, GK_L.LVR_8 );
			SetCmdLvr ( bd_c, ENM_CMD._9,	0, 1, IS, GK_L.LVR_9 );
			SetCmdLvr ( bd_c, ENM_CMD._7,	0, 1, IS, GK_L.LVR_7 );


			//2_8
			SetCmdLvr ( bd_c, ENM_CMD._2_8, 0, 12, IS, GK_L.LVR_2 );
			SetCmdLvr ( bd_c, ENM_CMD._2_8, 1, 12, PS, GK_L.LVR_8 );

			//2_9
			SetCmdLvr ( bd_c, ENM_CMD._2_9, 0, 12, IS, GK_L.LVR_2 );
			SetCmdLvr ( bd_c, ENM_CMD._2_9, 1, 12, PS, GK_L.LVR_9 );

			//2_7
			SetCmdLvr ( bd_c, ENM_CMD._2_7, 0, 12, IS, GK_L.LVR_2 );
			SetCmdLvr ( bd_c, ENM_CMD._2_7, 1, 12, PS, GK_L.LVR_7 );


			//6_6
			SetCmdLvr ( bd_c, ENM_CMD._6_6, 0, 6, IS, GK_L.LVR_6 );
			SetCmdLvr ( bd_c, ENM_CMD._6_6, 1, 6, PS, GK_L.LVR_6 );

			//4_4
			SetCmdLvr ( bd_c, ENM_CMD._4_4, 0, 6, IS, GK_L.LVR_4 );
			SetCmdLvr ( bd_c, ENM_CMD._4_4, 1, 6, PS, GK_L.LVR_4 );


			SetCmd_LvrBtn ( bd_c, ENM_CMD._6L , GK_L.LVR_6, GK_B.BTN_0 );	//6L
			SetCmd_LvrBtn ( bd_c, ENM_CMD._6Ma, GK_L.LVR_6, GK_B.BTN_1 );	//6Ma
			SetCmd_LvrBtn ( bd_c, ENM_CMD._6Mb, GK_L.LVR_6, GK_B.BTN_2 );	//6Mb
			SetCmd_LvrBtn ( bd_c, ENM_CMD._6H , GK_L.LVR_6, GK_B.BTN_3 );	//6H

			SetCmd_LvrBtn ( bd_c, ENM_CMD._4L , GK_L.LVR_4, GK_B.BTN_0 );	//4L
			SetCmd_LvrBtn ( bd_c, ENM_CMD._4Ma, GK_L.LVR_4, GK_B.BTN_1 );	//4Ma
			SetCmd_LvrBtn ( bd_c, ENM_CMD._4Mb, GK_L.LVR_4, GK_B.BTN_2 );	//4Mb
			SetCmd_LvrBtn ( bd_c, ENM_CMD._4H , GK_L.LVR_4, GK_B.BTN_3 );	//4H

			SetCmd_LvrBtn ( bd_c, ENM_CMD._2L , GK_L.LVR_2, GK_B.BTN_0 );	//2L
			SetCmd_LvrBtn ( bd_c, ENM_CMD._2Ma, GK_L.LVR_2, GK_B.BTN_1 );	//2Ma
			SetCmd_LvrBtn ( bd_c, ENM_CMD._2Mb, GK_L.LVR_2, GK_B.BTN_2 );	//2Mb
			SetCmd_LvrBtn ( bd_c, ENM_CMD._2H , GK_L.LVR_2, GK_B.BTN_3 );	//2H

			SetCmd_LvrBtn ( bd_c, ENM_CMD._8L , GK_L.LVR_8, GK_B.BTN_0 );	//8L
			SetCmd_LvrBtn ( bd_c, ENM_CMD._8Ma, GK_L.LVR_8, GK_B.BTN_1 );	//8Ma
			SetCmd_LvrBtn ( bd_c, ENM_CMD._8Mb, GK_L.LVR_8, GK_B.BTN_2 );	//8Mb
			SetCmd_LvrBtn ( bd_c, ENM_CMD._8H , GK_L.LVR_8, GK_B.BTN_3 );	//8H


			//2146 + (A)
			SetCmd_2146A ( bd_c, ENM_CMD.SP0_L , GK_B.BTN_0 );
			SetCmd_2146A ( bd_c, ENM_CMD.SP0_Ma, GK_B.BTN_1 );
			SetCmd_2146A ( bd_c, ENM_CMD.SP0_Mb, GK_B.BTN_2 );
			SetCmd_2146A ( bd_c, ENM_CMD.SP0_H , GK_B.BTN_3 );

			//412 + (A)
			SetCmd_412A ( bd_c, ENM_CMD.SP1_L , GK_B.BTN_0 );
			SetCmd_412A ( bd_c, ENM_CMD.SP1_Ma, GK_B.BTN_1 );
			SetCmd_412A ( bd_c, ENM_CMD.SP1_Mb, GK_B.BTN_2 );
			SetCmd_412A ( bd_c, ENM_CMD.SP1_H , GK_B.BTN_3 );

			//236 + (A)
			SetCmd_236A ( bd_c, ENM_CMD.SP2_L , GK_B.BTN_0 );
			SetCmd_236A ( bd_c, ENM_CMD.SP2_Ma, GK_B.BTN_1 );
			SetCmd_236A ( bd_c, ENM_CMD.SP2_Mb, GK_B.BTN_2 );
			SetCmd_236A ( bd_c, ENM_CMD.SP2_H , GK_B.BTN_3 );

			//6321463214 + L
			SetCmd_6321463214A ( bd_c, ENM_CMD.OD0_L , GK_B.BTN_0 );

			//236236 + H
			SetCmd_236236A ( bd_c, ENM_CMD.OD1_H , GK_B.BTN_3 );
		}

		private void SetCmdBtn ( BD_CMD bd_c, ENM_CMD name_cmd, int index, int limit, GKC_ST st, GK_B btn )
		{
			Command cmd = bd_c.Get ( name_cmd.ToString () );
			cmd.LimitTime = limit;

			List < GameKeyCommand > lgk = cmd.ListGameKeyCommand;
			//既存の設定のとき追加しない
			if ( lgk.Count <= index )
			{
				lgk.Add ( new GameKeyCommand () );
			}
			lgk [ index ].DctBtnSt [ btn ] = st;
		}

		private void SetCmdLvr ( BD_CMD bd_c, ENM_CMD name_cmd, int index, int limit, GKC_ST st, GK_L gk_l )
		{
			Command cmd = bd_c.Get ( name_cmd.ToString () );
			cmd.ListGameKeyCommand.Add ( new GameKeyCommand () );
			cmd.ListGameKeyCommand [ index ].SetLvrSt ( st, gk_l );
			cmd.LimitTime = limit;
		}

		//レバー入れ１ボタン
		private void SetCmd_LvrBtn ( BD_CMD bd_c, ENM_CMD cmd, GK_L gk_l, GK_B btn )
		{
			GKC_ST IS = GKC_ST.KEY_IS;
			GKC_ST PS = GKC_ST.KEY_PUSH;

			SetCmdLvr ( bd_c, cmd, 0, 1, IS, gk_l );
			SetCmdBtn ( bd_c, cmd, 0, 1, PS, btn );
		}

		//コマンド	2146 + (A)
		private void SetCmd_2146A ( BD_CMD bd_c, ENM_CMD cmd, GK_B btn )
		{
			GKC_ST IS = GKC_ST.KEY_IS;
			GKC_ST PS = GKC_ST.KEY_PUSH;
			SetCmdLvr ( bd_c, cmd, 0, 24, IS, GK_L.LVR_2 );
			SetCmdLvr ( bd_c, cmd, 1, 24, IS, GK_L.LVR_1 );
			SetCmdLvr ( bd_c, cmd, 2, 24, IS, GK_L.LVR_4 );
			SetCmdLvr ( bd_c, cmd, 3, 24, IS, GK_L.LVR_6 );
			SetCmdBtn ( bd_c, cmd, 4, 24, PS, btn );
		}

		//コマンド	412 + (A)
		private void SetCmd_412A ( BD_CMD bd_c, ENM_CMD cmd, GK_B btn )
		{
			GKC_ST IS = GKC_ST.KEY_IS;
			GKC_ST PS = GKC_ST.KEY_PUSH;
			SetCmdLvr ( bd_c, cmd, 0, 16, IS, GK_L.LVR_4 );
			SetCmdLvr ( bd_c, cmd, 1, 16, IS, GK_L.LVR_1 );
			SetCmdLvr ( bd_c, cmd, 2, 16, IS, GK_L.LVR_2 );
			SetCmdBtn ( bd_c, cmd, 3, 16, PS, btn );
		}

		//コマンド	236 + (A)
		private void SetCmd_236A ( BD_CMD bd_c, ENM_CMD cmd, GK_B btn )
		{
			GKC_ST IS = GKC_ST.KEY_IS;
			GKC_ST PS = GKC_ST.KEY_PUSH;
			SetCmdLvr ( bd_c, cmd, 0, 16, IS, GK_L.LVR_2 );
			SetCmdLvr ( bd_c, cmd, 1, 16, IS, GK_L.LVR_3 );
			SetCmdLvr ( bd_c, cmd, 2, 16, IS, GK_L.LVR_6 );
			SetCmdBtn ( bd_c, cmd, 3, 16, PS, btn );
		}

		//コマンド	6321463214 + (A)
		private void SetCmd_6321463214A ( BD_CMD bd_c, ENM_CMD cmd, GK_B btn )
		{
			GKC_ST IS = GKC_ST.KEY_IS;
			GKC_ST PS = GKC_ST.KEY_PUSH;
			SetCmdLvr ( bd_c, cmd, 0, 80, IS, GK_L.LVR_6 );
			SetCmdLvr ( bd_c, cmd, 1, 80, IS, GK_L.LVR_3 );
			SetCmdLvr ( bd_c, cmd, 2, 80, IS, GK_L.LVR_2 );
			SetCmdLvr ( bd_c, cmd, 3, 80, IS, GK_L.LVR_1 );
			SetCmdLvr ( bd_c, cmd, 4, 80, IS, GK_L.LVR_4 );
			SetCmdLvr ( bd_c, cmd, 5, 80, IS, GK_L.LVR_6 );
			SetCmdLvr ( bd_c, cmd, 6, 80, IS, GK_L.LVR_3 );
			SetCmdLvr ( bd_c, cmd, 7, 80, IS, GK_L.LVR_2 );
			SetCmdLvr ( bd_c, cmd, 8, 80, IS, GK_L.LVR_1 );
			SetCmdLvr ( bd_c, cmd, 9, 80, IS, GK_L.LVR_4 );
			SetCmdBtn ( bd_c, cmd, 10, 80, PS, btn );
		}

		//コマンド	236236 + (A)
		private void SetCmd_236236A ( BD_CMD bd_c, ENM_CMD cmd, GK_B btn )
		{
			GKC_ST IS = GKC_ST.KEY_IS;
			GKC_ST PS = GKC_ST.KEY_PUSH;
			SetCmdLvr ( bd_c, cmd, 0, 32, IS, GK_L.LVR_2 );
			SetCmdLvr ( bd_c, cmd, 1, 32, IS, GK_L.LVR_3 );
			SetCmdLvr ( bd_c, cmd, 2, 32, IS, GK_L.LVR_6 );
			SetCmdLvr ( bd_c, cmd, 3, 32, IS, GK_L.LVR_2 );
			SetCmdLvr ( bd_c, cmd, 4, 32, IS, GK_L.LVR_3 );
			SetCmdLvr ( bd_c, cmd, 5, 32, IS, GK_L.LVR_6 );
			SetCmdBtn ( bd_c, cmd, 6, 32, PS, btn );
		}

	}
}
