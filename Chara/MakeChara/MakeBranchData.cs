using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;


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
			//コマンドデータ(.txtファイル)から作成
			FileStream fstrm = new FileStream ( "BranchList.txt", FileMode.Open, FileAccess.Read );
			StreamReader sr = new StreamReader ( fstrm, Encoding.UTF8 );

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
				try
				{
					condition = int.Parse ( branchData[(int)BranchData.Condition] );
					bc = (BranchCondition)condition;
				}
				catch
				{
					condition = 0;
					bc = BranchCondition.CMD;
				}
				brc.Condition = bc;

				//----
				//コマンド名
				string CmdName = branchData [ (int)BranchData.Command ];
#if false
				try
				{
					ch.BD_Command.Try_Exist ( CmdName );
				}
				catch
				{
					CmdName = "";
				}
#endif
				brc.NameCommand = CmdName;

				//----
				//アクション名
				string ActName = branchData [ (int)BranchData.Action ];
#if false
				try
				{
					ch.behavior.BD_Sequence.Try_Exist ( ActName );
				}
				catch
				{
					ActName = "";
				}
#endif
				brc.NameSequence = ActName;

				//----------------------------------------------------------
				//キャラに設定
				EditChara.Inst.AddBranch ( brc );
			}
		}
	}
}
