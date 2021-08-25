using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptEditorUtility
{
	public partial class testCmp :Component
	{
		public testCmp ()
		{
			InitializeComponent ();
		}

		public testCmp ( IContainer container )
		{
			container.Add ( this );

			InitializeComponent ();
		}
	}
}
