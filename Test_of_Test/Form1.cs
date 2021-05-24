using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;

namespace ScriptEditor
{
	public partial class Form1 :Form
	{
		public Form1 ()
		{
			InitializeComponent ();
			InitPosition ();

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

		//フォーム位置初期化
		private const int start_X = -600;	//フォーム位置補正定数
		private const int start_Y = -10;	//フォーム位置補正定数

		private void InitPosition ()
		{
			//フォーム開始位置をマウス位置にする
			this.StartPosition = FormStartPosition.Manual;
			Point ptStart = Cursor.Position;
			ptStart.X += start_X;
			if ( ptStart.X < 0 ) { ptStart.X = 0; }
			ptStart.Y += start_Y;
			if ( ptStart.Y < 0 ) { ptStart.Y = 0; }
			this.Location = ptStart;
		}
	}
}
