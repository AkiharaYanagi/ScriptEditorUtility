using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;


namespace ScriptEditor
{
	public partial class Form1 :Form
	{
		public Form1 ()
		{
			FormUtility.InitPosition ( this );
			InitializeComponent ();

			Debug.Write ( "test\n" );

			Chara chara = new Chara ();

			//テストオブジェクトによる機能のテスト
			TestChara testChara = new TestChara ();
			testChara.Test ( chara );

			TestTree testTree = new TestTree ();
			testTree.Set ( treeView1, textBox1 );
			testTree.SelectSequence = SetString;

			testTree.SetCharaData ( chara.behavior.BD_Sequence );
		}

		public void SetString ( string str )
		{
			textBox1.Text = str;
		}

		private void フォルダToolStripMenuItem_Click ( object sender, System.EventArgs e )
		{
		}
	}
}
