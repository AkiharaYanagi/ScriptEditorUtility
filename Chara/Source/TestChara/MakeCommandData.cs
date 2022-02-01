using System;
using System.Text;
using System.IO;
using System.Drawing;
using System.Collections.Generic;


namespace ScriptEditor
{
	//テキストデータからコマンドリストを作成する
	public class MakeCommandData
	{
		public void Make ( Chara ch )
		{
			//=================================================================
			//コマンドデータ(.txtファイル)から作成
			FileStream fstrm = new FileStream ( "CommandList.txt", FileMode.Open, FileAccess.Read );
			StreamReader sr = new StreamReader ( fstrm, Encoding.UTF8 );

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

			foreach ( string str in lstr )
			{
				EditChara.Inst.AddCommand ( str );
			}

			//=================================================================
		}
	}
}
