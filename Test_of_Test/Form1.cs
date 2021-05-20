using System.Windows.Forms;
using System.Diagnostics;


namespace ScriptEditor
{
	public partial class Form1 :Form
	{
		public Form1 ()
		{
			InitializeComponent ();

			Debug.Write ( "test\n" );

			Chara chara = new Chara ();

			//テストオブジェクトによる機能のテスト
			TestChara testChara = new TestChara ();
			testChara.Test ( chara );
		}
	}
}
