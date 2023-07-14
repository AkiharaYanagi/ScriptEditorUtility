using System.Windows.Forms;

namespace ScriptEditor
{
	public static class STS_TXT
	{
		public static ToolStripStatusLabel Tssl { get; set; } = new ToolStripStatusLabel ();

		public static void Trace ( string str )
		{
			Tssl.Text = str;
		}
	}
}
