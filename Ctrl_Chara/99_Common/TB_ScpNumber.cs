using System.Windows.Forms;

namespace ScriptEditor
{
	using PrmInt = ScriptParam < int >;

	//----------------------------------------------------------------------
	// スクリプトのパラメータに対し、数値のみ入力できるテキストボックス
	//	[シークエンス全体]、[グループ]、[単体]、のいずれかで編集対象を設定できる
	//----------------------------------------------------------------------
	using Setter = System.Action < Script, int >;
	using Getter = System.Func < Script, int >;

	public class TB_ScpNumber : TextBox
	{
		//対象スクリプト
		public Script Scp { get; set; } = null;

		//設定用デリゲート
//		public Setter Setter = (s,i)=>{};
//		public Getter Getter = (s)=>0;

		public PrmInt PrmInt { get; set; } = null;

		//編集用
		EditCompend EditCompend = null;

		//表示
		public System.Action Disp { get; set; } = ()=>{};

		//コンストラクタ
		public TB_ScpNumber ()
		{
			this.Text = "0";
		}

		//環境設定
		public void SetEnvironment ( EditCompend ec, Setter setter, Getter getter )
		{
			EditCompend = ec;
//			Getter = getter;
//			Setter = setter;
		}
		public void SetEnvironment ( EditCompend ec )
		{
			EditCompend = ec;
		}
		public void SetEnvironment ( EditCompend ec, PrmInt prmInt )
		{
			EditCompend = ec;
			PrmInt = prmInt;
		}
#if false
#endif
		public void SetEnvironment ( EditCompend ec, System.Action disp )
		{
			EditCompend = ec;
			Disp = disp;
		}


		//更新
		public void UpdateData ()
		{
//			this.Text = Getter (Scp).ToString ();
			this.Text = PrmInt.Getter ( Scp ).ToString ();
		}


		//スクリプト関連付け
		public void Assosiate ( Script scp )
		{
			Scp = scp;
			UpdateData ();
		}

		//キー押下時(文字列判定)
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
				Disp?.Invoke ();	//他全体の更新
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
			case EditTargetScript.ALL: 
				EditCompend.EditSequence.DoSetterInSqc_T ( PrmInt.Setter, value );
			break;
			
			case EditTargetScript.GROUP:
				EditCompend.EditScript.DoSetterInGroup_T ( PrmInt.Setter, value );
			break;
			
			case EditTargetScript.SINGLE:
				PrmInt.Setter ( Scp, value );
			break;

			default: break;
			}
			
		}

		//--------------------------------------------------------
		//編集対象切替
		private EditTargetScript editTarget = EditTargetScript.SINGLE;

		public void SetTarget_All () { editTarget = EditTargetScript.ALL; }
		public void SetTarget_Group () { editTarget = EditTargetScript.GROUP; }
		public void SetTarget_Single () { editTarget = EditTargetScript.SINGLE; }
	}
}
