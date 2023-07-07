using System;
using System.Drawing;
using System.Windows.Forms;


namespace ScriptEditor
{
	//------------------------------------------------------------------
	//	シークエンス内のスクリプトを表示し、編集対象を選択する
	//	ツールボタンにより編集する
	//------------------------------------------------------------------
	public partial class _SqcBoard : UserControl
	{
		//対象シークエンス
		public Sequence Sqc { get; set; } = new Sequence ();

		//編集
		public EditCompend EditCompend { get; set; } = null;

		//表示部
		private PB_SqcBrd PB_SqcBrd = new PB_SqcBrd ();

		
		//コンストラクタ
		public _SqcBoard ()
		{
			InitializeComponent ();

			panel1.Controls.Add ( PB_SqcBrd );
			panel1.Size = new Size( this.Width, panel1.Height );
		}

		public void SetEnviron ( EditCompend ec )
		{
			EditCompend = ec;
			PB_SqcBrd.SetEnviron ( ec );
		}

		//関連付け
		public void Assosiate ()
		{
			PB_SqcBrd.Assosiate ();
		}

		protected override void OnPaint ( PaintEventArgs e )
		{
			PB_SqcBrd.Invalidate ();
			base.OnPaint ( e );
		}
	}
}
