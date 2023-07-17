﻿using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;


namespace ScriptEditor
{
	public partial class _Ctrl_Compend : UserControl
	{
		private _Ctrl_Image ctrl_image = new _Ctrl_Image ();
		private SqcTree sqc_tree = new SqcTree ();
		private _SqcBoard sqc_board = new _SqcBoard ();


		//コンストラクタ
		public _Ctrl_Compend ()
		{
			InitializeComponent ();

			//this.Dock = DockStyle.Fill;
			this.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top;
			this.Size = new Size ( 1000, 800 );

			//ツリー
			Controls.Add ( sqc_tree );
			sqc_tree.Location = new Point ( 3, 3 );
			sqc_tree.Size = new Size ( sqc_tree.Size.Width, this.Size.Height - 3 - 3 );
			sqc_tree.Anchor = AnchorStyles.Top | AnchorStyles.Bottom  | AnchorStyles.Left;
			sqc_tree.SelectTop ();

			//ボード
			Controls.Add ( sqc_board );
			int pos_board_x = sqc_tree.Location.X + sqc_tree.Size.Width + 3;
			sqc_board.Location = new Point ( pos_board_x, 3 );
			sqc_board.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			int board_size_w = this.Width - sqc_tree.Width - 3 - 3 - 3;
			sqc_board.Size = new Size ( board_size_w, sqc_board.Size.Height );

			//イメージ
			Controls.Add ( ctrl_image );
			int pos_image_y = sqc_board.Location.Y + sqc_board.Size.Height + 3;
			ctrl_image.Location = new Point ( pos_board_x, pos_image_y );
			ctrl_image.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			int img_size_h = this.Height - sqc_board.Height - 3 - 3 - 3;
			ctrl_image.Size = new Size ( board_size_w, img_size_h );
		}

		//環境設定
		public void SetEnviron ( EditCompend ec )
		{
			ctrl_image.SetEnviron ( ec );
			sqc_tree.SetEnviron ( ec, this );
			sqc_board.SetEnviron ( ec, this );
		}

		//キャラデータ設定
		public void SetCharaData ( Chara ch )
		{
			sqc_tree.SetCharaData ( ch.behavior.BD_Sequence );
			AllDisp ();
		}

		public void SelectTop ()
		{
			sqc_tree.SelectTop ();
		}

		//更新
		public void UpdateData ()
		{
			sqc_tree.UpdateData ();
			Assosiate ();
		}

		//関連付け
		public void Assosiate ()
		{
			sqc_board.Assosiate ();
			AllDisp ();
		}

		//すべて表示
		public void AllDisp ()
		{
			ctrl_image.Invalidate ();
			sqc_board.Invalidate ();
			sqc_tree.Invalidate ();
		}
	}
}
