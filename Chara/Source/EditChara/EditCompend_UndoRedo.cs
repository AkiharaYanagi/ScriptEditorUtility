using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptEditor
{
	public partial class EditCompend
	{
		//アンドゥリドゥ
		private List < Compend > L_Cmpd = new List<Compend> ();
		private const int MAX_Undo = 20; 

		//編集を記録
		public void Step ()
		{
			Compend cmpd = new Compend ();
			cmpd.Copy ( Compend );

			L_Cmpd.Add ( cmpd );
		}

		public void Undo ()
		{

		}

	}
}
