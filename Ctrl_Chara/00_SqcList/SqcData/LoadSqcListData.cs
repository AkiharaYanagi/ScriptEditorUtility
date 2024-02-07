using System;
using System.IO;
using System.Diagnostics;


namespace ScriptEditor
{
	public class LoadSqcListData
	{
		public void Run ( SqcListData data, string filename )
		{
			try { _Run ( data, filename ); }
			catch ( Exception e ) { Debug.WriteLine ( e );	}
		}

		private void _Run ( SqcListData data, string filename )
		{
			using ( StreamReader sr = new StreamReader ( filename ) )
			{
				while ( ! sr.EndOfStream )
				{
					string line = sr.ReadLine ();
					string[] part = line.Split ( ',' );

					SequenceData sqcDt = new SequenceData ();
					sqcDt.SetName ( part[(int)ConstSqcList.LoadData_Const.LD_NAME] );
					data.L_Sqc.Add (sqcDt);
				}

			}
		}
	}
}
