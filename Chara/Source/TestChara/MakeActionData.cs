using System;
using System.Text;
using System.IO;
using System.Drawing;


namespace ScriptEditor
{
	//テキストデータからアクションリストを作成する
	public class MakeActionData
	{
		public void Make ( Chara ch )
		{
			//=================================================================
			//アクションデータ(.txtファイル)から作成
			FileStream fstrm = new FileStream ( "ActionList.txt", FileMode.Open, FileAccess.Read );
			StreamReader sr = new StreamReader ( fstrm, Encoding.UTF8 );

			EditBehavior eb = EditChara.Inst.EditBehavior;
			while ( ! sr.EndOfStream )
			{
				string str = sr.ReadLine ();
				string[] actionData = str.Split(',');
				int index = (int)ActionData.Name;
				Action act = new Action ( actionData[index] );
				act.NextActionName = "Stand";

				act.ListScript.Clear ();
				for ( int i = 0; i < 5; ++ i )
				{
					Script s = new Script ()
					{	
						Group = 1,
						Pos = new Point ( -250, -450 ),		
					};

					string imgname = ch.behavior.BD_Image.Get ( 0 )?.Name;
					if ( imgname != null )
					{
						s.ImgName = imgname;
					}
					act.AddScript ( s ); 
				}
				eb.AddAction ( act );
			}

			//=================================================================
		}
	}
}
