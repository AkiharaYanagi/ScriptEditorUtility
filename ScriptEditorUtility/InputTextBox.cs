using System;
using System.Drawing;
using System.Windows.Forms;


namespace ScriptEditor
{
	public class InputTextBox : TextBox
	{
		//親コントロール
		public Control Ctrl = null;

		//終了時イベント
		public System.Action End { get; set; } = null;

		//サイズ
		public static Size Cell_Size = new Size ( 101, 20 );
		
		//文字設定用デリゲート
		public Action < string > SetData = null;

		//コンストラクタ
		public InputTextBox ()
		{
			this.Font = new Font ( "MSゴシック", 9 );
			this.Size = Cell_Size;
			this.Visible = false;
		}

		//初期化
		public void Init ( int x, int y, int w )
		{
			Text = "";
			Location = new Point ( x, y );
			Width = w;
			ImeMode = ImeMode.Off;
		}

		//キー入力
		protected override void OnKeyDown ( KeyEventArgs e )
		{
			//Enterで決定
			if ( e.KeyCode == Keys.Enter )
			{
				//文字設定デリゲート呼び出し
				SetData?.Invoke ( this.Text );

				this.Visible = false;

				//親の更新
				Ctrl?.Invalidate ();
				End?.Invoke ();
			}
			base.OnKeyDown ( e );
		}

		//キー押下
		protected override void OnKeyPress ( KeyPressEventArgs e )
		{
			//EnterとESCで音を鳴らさない
			if ( e.KeyChar == (char)Keys.Enter || 
				 e.KeyChar == (char)Keys.Escape )
			{
				e.Handled = true;
			}
			base.OnKeyPress ( e );
		}

		//入力開始
		protected override void OnEnter ( EventArgs e )
		{
			base.OnEnter ( e );
		}

		private void InitializeComponent ()
		{
			this.SuspendLayout();
			// 
			// InputTextBox
			// 
			this.ImeMode = ImeMode.On;
			this.ResumeLayout(false);

		}

		//入力用テキストボックスの始動
		public void Start ( Point pt, int width, string str, Action<string> SetData )
		{
			//位置の初期化
			this.Init ( pt.X, pt.Y, width );
			this.Text = str;
			this.SetData = SetData;
			this.Visible = true;
			this.Focus ();
			this.SelectAll ();
		}
	}
}
