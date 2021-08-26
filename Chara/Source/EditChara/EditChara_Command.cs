using System;
using System.ComponentModel;

namespace ScriptEditor
{
	public partial class EditChara 
	{
		//---------------------------------------------------------------------
		//コマンド

		//コマンドを追加
		public void AddCommand ()
		{
			Chara.BD_Command.Add ( new Command ( "newCommand" ) );
		}

		public void AddCommand ( string name )
		{
			Chara.BD_Command.Add ( new Command ( name ) );
		}

		//指定コマンドを削除
		public void RemoveCommand ( Command command )
		{
			Chara.BD_Command.GetBindingList().Remove ( command );
		}

		//末尾のコマンドを削除
		public void RemoveCommand ()
		{
			BindingList < Command > ls = Chara.BD_Command.GetBindingList();
			ls.RemoveAt ( ls.Count - 1 );
		}

		//変更の通知
		public void ResetCommand ()
		{
			BindingList < Command > ls = Chara.BD_Command.GetBindingList();
			for ( int i = 0; i < ls.Count; ++i )
			{
				ls.ResetItem ( i );
			}
		}

		//---------------------------------------------------------------------

	}
}
