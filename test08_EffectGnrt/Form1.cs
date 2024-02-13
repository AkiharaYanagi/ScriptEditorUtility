using System.Windows.Forms;


namespace ScriptEditor
{
	public partial class Form1 : Form
	{
		//対象データ
		private Chara chara = new Chara ();

		//コントロール
		private Ctrl_EfGnrt ctrl_efgnrt = new Ctrl_EfGnrt ();


		//コンストラクタ
		public Form1 ()
		{
			InitializeComponent ();

			this.Controls.Add ( ctrl_efgnrt );


			//テストデータの作成
			Script scp = new Script ();
			scp.BD_EfGnrt.Add ( new EffectGenerate () );

			//編集
			EditChara.Inst.SetCharaDara ( chara );
			EditCompend eb = EditChara.Inst.EditBehavior;
			ctrl_efgnrt.SetEditCompend ( eb );



			ctrl_efgnrt.SetCharaData ( chara );
			ctrl_efgnrt.Assosiate ();
		}
	}
}
