using System;
using System.Windows.Forms;


namespace ScriptEditor
{
	public partial class Form1 : Form
	{
		private _Ctrl_Image ctrl_image = new _Ctrl_Image ();

		public Form1 ()
		{
			FormUtility.InitPosition ( this );
			InitializeComponent ();

			//テストデータの作成
			Chara chara = new Chara ();
			TestCharaData testCh = new TestCharaData ();
			testCh.Make ( chara );


			EditCompend editCompend = new EditCompend ();
			editCompend.SetCharaData ( chara.behavior );

			ctrl_image.SetEnviron ( editCompend );
			ctrl_image.SetCharaData ( chara, chara.behavior.BD_Image );

			Controls.Add ( ctrl_image );
		}
	}
}
