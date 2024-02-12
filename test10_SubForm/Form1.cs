using System;
using System.Windows.Forms;


namespace ScriptEditor
{
	public partial class Form1 : Form
	{
		private _Ctrl_EfGnrt ctrl_EfGnrt = new _Ctrl_EfGnrt ();

		public Form1 ()
		{
			FormUtility.InitPosition ( this );
			InitializeComponent ();

			this.Controls.Add ( ctrl_EfGnrt );

			//テストデータの作成
			Chara chara = new Chara ();
			TestCharaData testCh = new TestCharaData ();
			testCh.Make ( chara );
			chara.garnish.BD_Sequence.Add ( new Effect ( "Ef0" ) );


			//環境設定
			EditBehavior editBehavior = new EditBehavior ();
			editBehavior.SetCharaData ( chara.behavior );

			All_Ctrl.Inst.EfGnrt = ctrl_EfGnrt;
			ctrl_EfGnrt.SetCharaData ( chara );
			ctrl_EfGnrt.SetEditCompend ( editBehavior );

			//データ
			Script scp = editBehavior.SelectedScript;
			EffectGenerate efgnrt = new EffectGenerate ();
			efgnrt.EfName = "Ef0";
			scp.BD_EfGnrt.Add ( efgnrt );

			All_Ctrl.Inst.Assosiate ();
		}
	}
}
