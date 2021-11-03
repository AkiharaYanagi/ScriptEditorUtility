using System.Windows.Forms;

namespace ScriptEditor
{
	//----------------------------------------------------------------------
	// スクリプトのパラメータに対し、数値のみ入力できるテキストボックス
	//----------------------------------------------------------------------
	using PrmInt = ScriptParam < int >;

	public class TB_ScpNumber : TextBox
	{
		//対象スクリプト
		public Script Scp { get; set; } = null;

		//設定用デリゲート
		public PrmInt PrmInt { get; set; } = null;

		//コンストラクタ
		public TB_ScpNumber ()
		{
			this.Text = "0";
		}

		//設定用、取得用関数
		public void Assosiate ( Script scp )
		{
			Scp = scp;
			int value = PrmInt.Getter ( Scp );
			this.Text = value.ToString ();
			PrmInt.Setter ( Scp, value );
		}

		//キー押下時(文字コード判定)
		protected override void  OnKeyPress ( KeyPressEventArgs e )
		{
			char c = e.KeyChar;

			//数字、マイナス、BackSpaceだけ入力可能(他は弾く)
			if ( c == '\b' )
			{
				e.Handled = false;
			}
			else if ( c == '-' ) 
			{ 
				e.Handled = false; 
			}
			else if ( ! char.IsDigit ( c ) )
			{
				e.Handled = true; 
			}

 			base.OnKeyPress(e);
		}

		//キー入力時(キーボード)
		protected override void OnKeyDown ( KeyEventArgs e )
		{
			//テキストボックスに数値が入力されていてEnterが押されたとき、
			//関連付けられた値を保存
			if ( e.KeyCode == Keys.Enter )
			{
				SetValue ();		//値の設定
				this.Invalidate ();	//画面の更新
			}

			base.OnKeyDown ( e );
		}

		//値を設定
		private void SetValue ()
		{
			int value = 0;
			try
			{
				value = int.Parse ( this.Text );
			}
			catch	//int.Parse(s)が失敗したとき
			{
				return;
			}

			switch ( editTarget )
			{
			case EditTarget.ALL: 
				//AllSetter ( value );
			break;
			
			case EditTarget.GROUP:
				//グループ編集時に他スクリプトにも値を設定する
				//GroupSetter?.Invoke ( value );
			break;
			
			case EditTarget.SINGLE:
				//SetFunc ( value );
				PrmInt.Setter ( Scp, value );
			break;

			default: break;
			}
			
		}

		//更新
		public void UpdateData ()
		{
			//this.Text = GetFunc ().ToString ();
			this.Text = PrmInt.Getter ( Scp ).ToString ();
		}


		//--------------------------------------------------------
		//編集対象切替
		private enum EditTarget
		{ 
			ALL, GROUP, SINGLE,
		};
		private EditTarget editTarget = EditTarget.SINGLE;

		public void SetAll () { editTarget = EditTarget.ALL; }
		public void SetGroup () { editTarget = EditTarget.GROUP; }
		public void SetSingle () { editTarget = EditTarget.SINGLE; }
	}
}
