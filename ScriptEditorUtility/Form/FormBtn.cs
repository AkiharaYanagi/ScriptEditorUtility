using System;
using System.Windows.Forms;

using ScriptEditor;

namespace ScriptEditorUtility
{
	public partial class FormBtn : UserControl
	{
		public EditorSubForm Form { get; set; } = new EditorSubForm ();

		public FormBtn ()
		{
			InitializeComponent ();
		}

		public FormBtn ( string name )
		{
			InitializeComponent ();
			Btn_SubForm.Text = name;
		}

		private void Btn_SubForm_Click ( object sender, EventArgs e )
		{
			Form.Active ();
		}

		private void Btn_Hide_Click ( object sender, EventArgs e )
		{
			Form.Hide ();
		}
	}
}
