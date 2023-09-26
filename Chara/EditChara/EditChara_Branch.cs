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
			Chara.BD_Branch.Add ( new Branch ( "newBranch" ) );
		}
		public void AddBranch ( string name )
		{
			Chara.BD_Branch.Add ( new Branch ( name ) );
		}
		public void AddBranch ( Branch brc )
		{
			Chara.BD_Branch.Add ( brc );
		}

		//指定ブランチを削除
		public void RemoveBranch ( Branch brc )
		{
			Chara.BD_Branch.Remove ( brc );
		}

		//末尾のブランチを削除
		public void RemoveBranch ()
		{
			BindingList < Branch > ls = Chara.BD_Branch.GetBindingList();
			ls.RemoveAt ( ls.Count - 1 );
		}

		//変更の通知
		public void ResetBranch ()
		{
			BindingList < Branch > ls = Chara.BD_Branch.GetBindingList();
			for ( int i = 0; i < ls.Count; ++i )
			{
				ls.ResetItem ( i );
			}
		}

		//---------------------------------------------------------------------

	}
}
