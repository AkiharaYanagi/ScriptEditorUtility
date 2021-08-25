using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScriptEditor
{
	public partial class Form1 :Form
	{
		public Form1 ()
		{
			FormUtility.InitPosition ( this );
			InitializeComponent ();

			Chara ch_test = new Chara ();
			TestChara testChara = new TestChara ();
			testChara.Test ( ch_test );

			ctrl_Branch1.SetCharaData  ( ch_test );
		}
	}
}
