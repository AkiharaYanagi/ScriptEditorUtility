using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;

namespace ScriptEditor
{
	public partial class Form_ListRect : Form
	{
		public Form_ListRect ()
		{
			//位置
			FormUtility.InitPosition ( this );

			//枠リスト コントロール
			Ctrl_AllRect ctrl_AllRect = new Ctrl_AllRect ();
			this.Controls.Add ( ctrl_AllRect );

			//デフォルトの初期化
			InitializeComponent ();

			this.Text = "FormListRect";
		}
	}
}
