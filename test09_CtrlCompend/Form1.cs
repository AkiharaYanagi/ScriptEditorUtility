using System;
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

			//テストデータの作成
			Chara chara = new Chara ();
			TestCharaData testCh = new TestCharaData ();
			testCh.Make ( chara );

			//編集
			EditCompend editCompend = new EditCompend ();
			editCompend.SetCharaData ( chara.behavior );

			//環境設定
#if false
			ctrl_image.SetEnviron ( editCompend );
			ctrl_image.SetCharaData ( chara, chara.behavior.BD_Image );
			Controls.Add ( ctrl_image );
#endif
			ctrl_Compend.SetEnviron ( editCompend );
			panel1.Controls.Add ( ctrl_Compend );
			ctrl_Compend.SetCharaData ( chara );

		}
	}
}
