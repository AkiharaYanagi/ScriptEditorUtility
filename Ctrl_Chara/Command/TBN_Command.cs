using System;
using System.Windows.Forms;

namespace ScriptEditor
{
	//----------------------------------------------------------------------
	// 数値のみ入力でき、関連付けられたコマンドを変更するテキストボックス
	//----------------------------------------------------------------------
	public class TBN_Command : TextBox
	{
		//全てのスクリプトに適用するための操作用
		public EditCommand EdtCmd { get; set; }

		//スクリプト表示の更新用
		public DispCommand DspCmd { get; set; }

		//設定対象
		Command Cmd { get; set; }

		//デリゲートを用いたTypeへの値設定
		public System.Action < Command, int > SetFunc { get; set; }

		//------------------------------------------------------
		//初期化
		public void Load ( EditCommand ec, DispCommand dc )
		{
			EdtCmd = ec;
			DspCmd = dc;
			Cmd = EdtCmd.Cmd;
		}

		//文字キー押下時
		protected override void OnKeyPress ( KeyPressEventArgs e )
		{
			char c = e.KeyChar;

			//数字、マイナス、BackSpaceだけ入力可能
			if ( c == '\b' )
			{
				e.Handled = false;
			}
			else if ( c == '-' ) 
			{ 
				e.Handled = false; 
			}
			else if ( ! Char.IsDigit ( c ) )
			{
				e.Handled = true; 
			}

 			base.OnKeyPress(e);
		}

		//関連付と更新
		public void Select ( Command cmd )
		{
			this.Cmd = cmd;
			Invalidate ();
		}

		//------------------------------------------------------
		//コンポーネント初期化
		private void InitializeComponent ()
		{
			this.SuspendLayout();
			this.ResumeLayout(false);
		}

	}
}
