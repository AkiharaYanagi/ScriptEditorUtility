using System;
using System.Drawing;
using System.Windows.Forms;

using Ctrl_Chara;

using ScriptEditor;


namespace test11_Ctrl_EditCompend
{
	public partial class Form1 : Form
	{
		private Ctrl_EditCompend ctrlEdtCmpd = new Ctrl_EditCompend ();


		public Form1 ()
		{
			InitializeComponent ();

			//テストデータの作成
			Chara chara = new Chara ();
			TestCharaData testCh = new TestCharaData ();
			testCh.Make ( chara );

			//コントロール
			this.Controls.Add ( ctrlEdtCmpd );

			EditCompend ec = new EditCompend ();
			ctrlEdtCmpd.SetEnvironment ( ec );
			ctrlEdtCmpd.Assosiate ();
		}
	}
}
