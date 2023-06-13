using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ctrl_Chara.Compend
{
	public partial class RB_Tool : Component
	{
		public RB_Tool ()
		{
			InitializeComponent ();
		}

		public RB_Tool ( IContainer container )
		{
			container.Add ( this );

			InitializeComponent ();
		}
	}
}
