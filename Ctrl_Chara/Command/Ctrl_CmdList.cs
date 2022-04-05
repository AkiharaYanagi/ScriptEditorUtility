using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;


namespace ScriptEditor
{
	using GKC_L = GameKeyCommand.LeverCommand;
	using GKC_ST = GameKeyCommand.GameKeyCommandState;

	public partial class Ctrl_CmdList :UserControl
	{
		//エディットリストボックス
		private EditListbox < Command > EL_Cmd = new EditListbox<Command> ();
		
		//設定ファイル
		private Ctrl_Settings Ctrl_Stgs { get; set; } = new Ctrl_Settings ();

		public Ctrl_CmdList ()
		{
			InitializeComponent ();

			//----------------------------------
			//エディットリストボックス
			EL_Cmd.Location = new Point ( 3, 3 );
			this.Controls.Add ( EL_Cmd );

			//変更時の更新
			EL_Cmd.SelectedIndexChanged = ()=>
			{
				Command cmd = EL_Cmd.Get ();
				ctrl_Command1.Set ( cmd );
			};
			EL_Cmd.UpdateData = ()=>
			{
				Command cmd = EL_Cmd.Get ();
				ctrl_Command1.Set ( cmd );
			};

			//IO
			EL_Cmd.SetIOFunc ( SaveCommand, LoadCommand );
			EL_Cmd.Func_SavePath = s=>
			{
				Ctrl_Stgs.File_CommandList = s;
				XML_IO.Save ( Ctrl_Stgs );
			};
		}

		public void SetEnvironment ( Ctrl_Settings stgs )
		{
			Ctrl_Stgs = stgs;
		}

		public void SetCharaData ( Chara ch )
		{
			EL_Cmd.SetData ( ch.BD_Command );
			Command cmd = EL_Cmd.Get ();
			if ( cmd != null )
			{
				ctrl_Command1.Set ( cmd );
			}
		}

		//データ読込
		public void LoadData ()
		{
			EL_Cmd.LoadData ( Ctrl_Stgs.File_CommandList );
		}


		//-------------------------------------------------------------------------
		//IO

		//単体保存
		public void SaveCommand ( object ob, StreamWriter sw )
		{
			Command cmd = (Command)ob;
			//名前
			sw.Write ( cmd.Name + ",");
			//受付時間
			sw.Write ( cmd.LimitTime.ToString () + "," );

			//ゲームキー
			foreach ( GameKeyCommand gkc in cmd.ListGameKeyCommand )
			{
				//否定
				sw.Write ( gkc.Not.ToString () + "," );

				//レバー
				foreach ( GKC_ST gkcst in gkc.Lvr )
				{
					sw.Write ( ((int)gkcst).ToString () );
				}
				sw.Write ( "," );

				//ボタン
				foreach ( GKC_ST gkcst in gkc.Btn )
				{
					sw.Write ( ((int)gkcst).ToString () );
				}

				//ゲームキー区切り
				sw.Write ( ",");
			}
		}

		//単体読込
		public void LoadCommand ( StreamReader sr )
		{
			Command cmd = new Command ();

			string str = sr.ReadLine ();
			string str_n = str.Substring ( 0, str.Length - 1 );
			string[] str_split = str_n.Split(',');
			int index = 0;
			int size = str_split.Length;

			//名前
			cmd.Name = str_split [ index ++ ];
			//受付時間
			cmd.LimitTime = int.Parse ( str_split [ index ++ ] );

			while ( index < size )
			{
				//代入用新規作成
				GameKeyCommand gameKey = new GameKeyCommand ();

				//否定
				gameKey.Not = Boolean.Parse ( str_split [ index ++ ] );

				//レバー
				string str_lvr = str_split [ index ++ ];
				for ( int i = 0; i < GameKeyCommand.LeverCommandNum; ++ i )
				{
					string chLvr = str_lvr [ i ].ToString();
					gameKey.Lvr [ i ] = (GKC_ST)int.Parse ( chLvr );
				}

				//ボタン
				string str_btn = str_split [ index ++ ];
				for ( int i = 0; i < GameKeyCommand.BtnNum; ++ i )
				{
					string chBtn = str_btn [ i ].ToString();
					gameKey.Btn [ i ] = (GKC_ST)int.Parse ( chBtn );
				}

				//コマンドに加える
				cmd.ListGameKeyCommand.Add ( gameKey );
			}

			EL_Cmd.Add ( cmd );
		}

	}
}
