using System;
using System.ComponentModel;

namespace ScriptEditor
{
	public partial class EditChara 
	{
		//---------------------------------------------------------------------
		//ルート

		//ルートを追加
		public void AddRoute ()
		{
			Chara.BD_Route.Add ( new Route ( "newRoute" ) );
		}

		public void AddRoute ( Route rut )
		{
			Chara.BD_Route.Add ( rut );
		}

		public void AddRoute ( string name )
		{
			Chara.BD_Route.Add ( new Route ( name ) );
		}

		public void AddRoute ( string name, string summary )
		{
			Chara.BD_Route.Add ( new Route ( name, summary ) );
		}

		//指定ルートを削除
		public void RemoveRoute ( Route rot )
		{
			Chara.BD_Route.Remove ( rot );
		}

		//末尾のルートを削除
		public void RemoveRoute ()
		{
			BindingList < Route > ls = Chara.BD_Route.GetBindingList();
			ls.RemoveAt ( ls.Count - 1 );
		}

		//変更の通知
		public void ResetRoute ()
		{
			BindingList < Route > ls = Chara.BD_Route.GetBindingList();
			for ( int i = 0; i < ls.Count; ++i )
			{
				ls.ResetItem ( i );
			}
		}

		//---------------------------------------------------------------------

	}
}
