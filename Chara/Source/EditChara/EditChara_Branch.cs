using System;
using System.ComponentModel;

namespace ScriptEditor
{
	public partial class EditChara 
	{
		//---------------------------------------------------------------------
		//ブランチ

		//ブランチを追加
		public void AddBranch ()
		{
			Chara.BD_Branch.Add ( new Branch0 ( "newBranch" ) );
		}

		public void AddBranch ( string name )
		{
			Chara.BD_Branch.Add ( new Branch0 ( name ) );
		}

		//指定ブランチを削除
		public void RemoveBranch ( Branch0 brc )
		{
			Chara.BD_Branch.Remove ( brc );
		}

		//末尾のブランチを削除
		public void RemoveBranch ()
		{
			BindingList < Branch0 > ls = Chara.BD_Branch.GetBindingList();
			ls.RemoveAt ( ls.Count - 1 );
		}

		//変更の通知
		public void ResetBranch ()
		{
			BindingList < Branch0 > ls = Chara.BD_Branch.GetBindingList();
			for ( int i = 0; i < ls.Count; ++i )
			{
				ls.ResetItem ( i );
			}
		}

		//---------------------------------------------------------------------

	}
}
