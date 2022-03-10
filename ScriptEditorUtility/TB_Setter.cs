using System.Windows.Forms;

namespace ScriptEditor
{
	//----------------------------------------------------------------------
	// 入力できるテキストボックス
	//----------------------------------------------------------------------
	using Setter = System.Action < string >;
	using Getter = System.Func < string >;

	public class TB_Setter : TextBox
	{
		//設定用デリゲート
		public Setter SetFunc { get; set; } = null;
		public Getter GetFunc { get; set; } = null;

		//コンストラクタ
		public TB_Setter()
		{
			this.Text = "0";
		}

		//設定用、取得用関数
		public void Assosiate ( Setter setfunc, Getter getfunc )
		{
			GetFunc = getfunc;
			this.Text = GetFunc();
			
			SetFunc = setfunc;
			SetFunc ( this.Text );
		}

		//キー押下時(文字コード判定)
		protected override void  OnKeyPress ( KeyPressEventArgs e )
		{
 			base.OnKeyPress(e);
		}

		//キー入力時(キーボード)
		protected override void OnKeyDown ( KeyEventArgs e )
		{
			//テキストボックスに数値が入力されていてEnterが押されたとき、
			//関連付けられた値を保存
			if ( e.KeyCode == Keys.Enter )
			{
				this.SetFunc( this.Text );		//値の設定
				this.Invalidate ();	//画面の更新
			}

			base.OnKeyDown ( e );
		}

		//更新
		public void UpdateData ()
		{
			this.Text = GetFunc ().ToString ();
		}
	}
}
