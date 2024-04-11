using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System;


namespace ScriptEditor
{
	//テキストデータからブランチリストを作成する
	public class MakeBranchData
	{
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
				EditChara.Inst.AddBranch ();
			}
		}

		private void _Make ( Chara ch )
		{
			string curDir = Environment.CurrentDirectory;
			string filename = "PreData\\BranchList.txt";

			//ブランチデータ(.txtファイル)から作成
			FileStream fstrm = new FileStream ( filename, FileMode.Open, FileAccess.Read );
			StreamReader sr = new StreamReader ( fstrm, Encoding.UTF8 );

#if false
			EditBehavior eb = EditChara.Inst.EditBehavior;

			//ファイル末尾まで
			while ( ! sr.EndOfStream )
			{
				//１行ごと読込
				string str = sr.ReadLine ();
				string[] branchData = str.Split ( ',' );

				//----
				//名前
				string name = branchData [ (int)BranchData.Name ];
				Branch brc = new Branch ( name );

				//----
				//条件
				int condition = 0;
				BranchCondition bc = BranchCondition.CMD;
				brc.Condition = bc;

				//----
				//コマンド名
				string CmdName = branchData [ (int)BranchData.Command ];
				brc.NameCommand = CmdName;

				//----
				//アクション名
				string ActName = branchData [ (int)BranchData.Action ];
				brc.NameSequence = ActName;

				//----------------------------------------------------------
				//キャラに設定
				EditChara.Inst.AddBranch ( brc );
			}
#endif
			//キャラに設定
			TextToBranch ttb = new TextToBranch ();
			ttb.Do_BD ( sr, ch.BD_Branch );
		}
	}
}
