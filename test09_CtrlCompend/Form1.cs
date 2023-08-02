﻿using System;
using System.Drawing;
using System.Windows.Forms;


namespace ScriptEditor
{
	public partial class Form1 : Form
	{
		private _Ctrl_Compend ctrl_Compend = new _Ctrl_Compend();
//		private _Ctrl_Image ctrl_image = new _Ctrl_Image ();

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
#if true
			EditBehavior editCompend = new EditBehavior ();
			editCompend.SetCharaData ( chara.behavior );
#else		
			EditGarnish editCompend = new EditGarnish ();
			editCompend.SetCharaData ( chara.garnish );
#endif

			//環境設定
			panel1.Controls.Add ( ctrl_Compend );
			ctrl_Compend.BoolAction = true;
			ctrl_Compend.SetEnviron ( editCompend );
			ctrl_Compend.SetCharaData ( chara );

		}

		private void Form1_Shown ( object sender, EventArgs e )
		{
			ctrl_Compend.SelectTop ();
		}
	}
}
