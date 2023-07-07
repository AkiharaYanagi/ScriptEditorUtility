using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;


namespace ScriptEditor
{
	public partial class _Ctrl_Compend : UserControl
	{
		private _Ctrl_Image ctrl_image = new _Ctrl_Image ();
		private SqcTree sqc_tree = new SqcTree ();
		private SqcBoard sqc_board = new SqcBoard ();


		//コンストラクタ
		public _Ctrl_Compend ()
		{
			InitializeComponent ();

			//this.Dock = DockStyle.Fill;
			this.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top;

			Controls.Add ( ctrl_image );
			ctrl_image.Location = new Point ( 200, 300 );


			Controls.Add ( sqc_tree );
			sqc_tree.Location = new Point ( 3, 3 );
//			sqc_tree.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;


			Controls.Add ( sqc_board );
			sqc_board.Location = new Point ( 200, 3 );
		}

		//環境設定
		public void SetEnviron ( EditCompend ec )
		{
			this.Size = new Size ( 1000, 800 );
			ctrl_image.SetEnviron ( ec );
			sqc_tree.SetEnviron ( ec, this );
			sqc_board.SetEnviron ( ec );
		}

		//キャラデータ設定
		public void SetCharaData ( Chara ch )
		{
			sqc_tree.SetCharaData ( ch.behavior.BD_Sequence );
			sqc_tree.Invalidate ();
		}


		//関連付け
		public void Assosiate ()
		{
			sqc_board.Assosiate ();
		}


		//すべて表示
		public void AllDisp ()
		{

		}
	}
}
