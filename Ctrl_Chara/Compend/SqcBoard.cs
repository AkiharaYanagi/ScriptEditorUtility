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
		private Panel panel1 = new Panel ();

		//メニュ
		private Menu_Board MN_Brd = new Menu_Board ();
	

		//親コントロール
		private System.Action ActDisp = ()=>{};		//からの表示
		private System.Action ActAssosiate = ()=>{};	//関連付け


		//コンストラクタ
		public _SqcBoard ()
		{
			InitializeComponent ();

			this.SuspendLayout ();

			//メニュ
			this.Controls.Add ( MN_Brd );
			MN_Brd.Location = new Point ( 3, 3 );

			//パネル
			this.Controls.Add ( panel1 );
			panel1.BackColor = Color.White;
			int panel_y = MN_Brd.Height + 3 + 3;
			panel1.Location = new Point ( 3, panel_y );
			panel1.Size = new Size( this.Width, 120 );
			panel1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			panel1.AutoScroll = true;

			//ボード
			panel1.Controls.Add ( PB_SqcBrd );
//			PB_SqcBrd.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

			//全体
			this.Height = 3 + MN_Brd.Height + 3 + panel1.Height + 3;

			this.ResumeLayout ( false );
		}

		public void SetEnviron ( EditCompend ec, _Ctrl_Compend ctrl_cmpd )
		{
			EditCompend = ec;
			ActDisp = ctrl_cmpd.Disp;
			ActAssosiate = ctrl_cmpd.Assosiate;

			PB_SqcBrd.SetEnviron ( ec, ctrl_cmpd );
			MN_Brd.SetEnviron ( ec, ctrl_cmpd );
		}

		//関連付け
		public void Assosiate ()
		{
			PB_SqcBrd.Assosiate ();
		}

		protected override void OnPaint ( PaintEventArgs e )
		{
			PB_SqcBrd.Ctrl_Size = this.Size;
			PB_SqcBrd.Invalidate ();
			base.OnPaint ( e );
		}
	}
}
