﻿using System.Windows.Forms;
using System.Drawing;

namespace ScriptEditor
{
	public static class STS_TXT
	{
		public static ToolStripStatusLabel Tssl { get; set; } = new ToolStripStatusLabel ();
		public static Color ControlColor { get; set; } = new Color ();

		public static void Trace ( string str )
		{
			Tssl.BackColor = ControlColor;
			Tssl.Text = str;
		}

		public static void SaveContorlColor ()
		{
			ControlColor = Tssl.BackColor;
		}

		public static void Trace_Err ( string str )
		{
			Tssl.BackColor = Color.Red;
			Tssl.Text = str;
		}
	}
}
