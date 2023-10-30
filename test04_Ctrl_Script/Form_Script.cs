using System;
using System.Windows.Forms;

namespace ScriptEditor
{
	public partial class Form_Script : Form
	{
		public Form_Script ()
		{
			FormUtility.InitPosition ( this );
			InitializeComponent ();

			EditCompend eb = EditChara.Inst.EditBehavior;
			ctrl_Script1.SetEnvironment ( eb );


			Chara ch = new Chara ();
			MakeTestCharaData mtcd = new MakeTestCharaData ();
			mtcd.Make ( ch );

			EditChara.Inst.SetCharaDara ( ch );

			eb.Assosiate ();
			ctrl_Script1.Assosiate ();
		}
	}
}
