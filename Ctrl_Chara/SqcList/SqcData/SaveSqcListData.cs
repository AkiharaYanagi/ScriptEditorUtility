using System;
using System.IO;
using System.Diagnostics;

namespace ScriptEditor
{
	public class SaveSqcListData
	{
		public void Run ( SqcListData dt, string filename )
		{
			try
			{
				_Run ( dt, filename );
			}
			catch ( Exception e )
			{
				Debug.WriteLine ( e );
			}
		}

		private void _Run ( SqcListData dt, string filename )
		{
			//ファイル書出
			using ( StreamWriter sw = new StreamWriter ( filename ) )
			{
				foreach ( SequenceData sqcD in dt.L_Sqc.GetEnumerable () )
				{
					sw.Write ( sqcD.Sqc.Name );
					sw.Write ( "\n" );
				}

			}	//using
//			STS_TXT.Trace ( "Save." );
		}
	}
}
