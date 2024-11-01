using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;


namespace ScriptEditor
{
	public partial class _Ctrl_Compend : UserControl
	{
		private _Ctrl_Image ctrl_image = new _Ctrl_Image ();
		private SqcTree sqc_tree = new SqcTree ();
		private _SqcBoard sqc_board = new _SqcBoard ();

		//アクションのときtrue, エフェクトのときfalse
		public bool BoolAction { get; set; } = true;


		//test
		public void Call ()
		{
			sqc_board.Call ();
		}


		//コンストラクタ
		public _Ctrl_Compend ()
		{
			InitializeComponent ();

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
//			sqc_board.Size = new Size ( board_size_w, sqc_board.Size.Height );
//			sqc_board.Size = new Size ( 600, sqc_board.Size.Height );
			sqc_board.SetSize ( new Size ( board_size_w, sqc_board.Size.Height ) );

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
			BoolAction = ( ec is EditBehavior );

			ctrl_image.SetEnviron ( ec );
			sqc_tree.SetEnviron ( ec, this );
			sqc_board.SetEnviron ( ec, this );

			int board_size_w = this.Width - sqc_tree.Width - 3 - 3 - 3;
			sqc_board.SetSize ( new Size ( board_size_w, sqc_board.Size.Height ) );
		}

		//キャラデータ設定
		public void SetCharaData ( Chara ch )
		{
			if ( BoolAction )
			{
				ctrl_image.SetCharaData ( ch, ch.behavior.BD_Image );
				sqc_tree.SetCharaData ( ch.behavior.BD_Sequence );
			}
			else
			{
				ctrl_image.SetCharaData ( ch, ch.garnish.BD_Image );
				sqc_tree.SetCharaData ( ch.garnish.BD_Sequence );
			}
			Disp ();
		}

		//関連付け
		public void Assosiate ()
		{
			sqc_board.Assosiate ();
			UpdateData ();
		}

		//スクリプトのみ関連付け
		public void Assosiate_scp ()
		{
			sqc_board.Assosiate ();
			sqc_board.Invalidate ();
			//ctrl_image.Assosiate ();
			ctrl_image.Invalidate ();
		}

		//更新
		public void UpdateData ()
		{
//			sqc_tree.UpdateData ();
			Disp ();
		}

		//すべて表示
		public void Disp ()
		{
			ctrl_image.Invalidate ();
			sqc_board.Invalidate ();
			sqc_tree.Invalidate ();
		}

		public void SelectTop ()
		{
			sqc_tree.SelectTop ();
		}

		//ツリーのみ外側から更新
		public void UpdateSqcTree ()
		{
			sqc_tree.UpdateData ();
			sqc_tree.SelectTop ();
		}


		//シークエンス名から選択
		public void SelectFromName ( string sqcName )
		{
			sqc_tree.SelectFromName ( sqcName );
		}
	}
}
