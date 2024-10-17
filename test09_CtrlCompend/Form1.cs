using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace ScriptEditor
{
	public partial class Form1 : Form
	{
		private _Ctrl_Compend ctrl_behavior = new _Ctrl_Compend();

		public Form1 ()
		{
			FormUtility.InitPosition ( this );
			InitializeComponent ();

			this.Size = new Size ( 1200, 900 );
			STS_TXT.Tssl = toolStripStatusLabel1;

			//テストデータの作成
			Chara chara = new Chara ();
			TestCharaData testCh = new TestCharaData ();
			testCh.Make ( chara );

			//編集
			EditBehavior editBehavior = new EditBehavior ();
			editBehavior.SetCharaData ( chara.behavior );
			EditGarnish editGarnish = new EditGarnish ();
			editGarnish.SetCharaData ( chara.garnish );

			//環境設定
			All_Ctrl.Inst.Compend_Bhv = ctrl_behavior;

			panel1.Controls.Add ( ctrl_behavior );
			ctrl_behavior.BoolAction = true;
			ctrl_behavior.SetEnviron ( editBehavior );
			ctrl_behavior.SetCharaData ( chara );

			All_Ctrl.Inst.Scp.LoadData ( Directory.GetCurrentDirectory() );
		}

		private void Form1_Shown ( object sender, EventArgs e )
		{
			ctrl_behavior.SelectTop ();
		}
	}
}
