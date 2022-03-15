using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace ScriptEditor
{
	public partial class Form_Sqc : Form
	{
		//全体更新
		public System.Action UpdateAll { get; set; } = null;

		public Form_Sqc()
		{
			InitializeComponent();

			foreach ( string name in Enum.GetNames ( typeof ( ConstSqcList.Sequence_Category ) ) )
			{
				comboBox1.Items.Add ( name );
			}
			comboBox1.SelectedIndex = 0;
			comboBox1.Size = new Size ( 220, 40 );
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			e.Cancel = true;
			UpdateAll?.Invoke ();
			this.Hide ();
		}

		public void Assosiate ( SequenceData sqcDt )
		{
			tB_Setter1.Assosiate ( s=>sqcDt.SetName(s), ()=>{return sqcDt.Sqc.Name;} );
			tB_Number1.Assosiate ( i=>sqcDt.nScript = i, ()=>{ return sqcDt.nScript; } );
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			this.Close ();
		}
	}
}
