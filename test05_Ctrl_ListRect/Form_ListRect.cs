using System.Windows.Forms;
using System.Diagnostics;

namespace ScriptEditor
{
	public partial class Form_ListRect : Form
	{
		public Form_ListRect ()
		{
			Debug.WriteLine ( "Form1();\n" );
			//位置
			FormUtility.InitPosition ( this );

			//枠リスト コントロール
			Ctrl_AllRect ctrl_AllRect = new Ctrl_AllRect ();
			this.Controls.Add ( ctrl_AllRect );

			//デフォルトの初期化
			InitializeComponent ();

			this.Text = "FormListRect";

			//キャラ初期化
			Chara ch = new Chara ();
			MakeTestCharaData mtcd = new MakeTestCharaData ();
			mtcd.Make ( ch );
			EditChara.Inst.SetCharaDara ( ch );

			ctrl_AllRect.SetEnvironment ( EditChara.Inst.EditBehavior, ()=>{} );

		}
	}
}
