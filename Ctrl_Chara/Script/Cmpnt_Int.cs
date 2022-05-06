﻿using System;
using System.Windows.Forms;

namespace ScriptEditor
{
	//スクリプト内の整数を設定するコンポーネント
	public class Cmpnt_Int : TextBox, IScriptParam
	{
		//対象スクリプト
		public Script Scp { get; set; } = new Script ();

		//対象に値を設定するゲッタとセッタ
		public ScriptParam < int > ScpPrm { get; set; } = null;

		//表示
		public System.Action Disp { get; set; } = () =>{};

		//対象指定
		private EditTarget editTarget = EditTarget.SINGLE;
	
		//編集用
		private EditCompend EditCompend = null;

		//---------------------------------------------------------------
		//コンストラクタ
		public Cmpnt_Int ()
		{
			this.Width = 30;
		}

		//---------------------------------------------------------------
		// Setter, Getter 指定
		public void SetParam ( ScriptParam < int > scpPrm  )
		{
			ScpPrm = scpPrm;
		}

		//---------------------------------------------------------------
		//インターフェース実装

		// 環境指定
		public void SetEnvironment ( EditCompend ec, System.Action disp )
		{
			EditCompend = ec;
			Disp = disp;
		}

		//更新
		public void UpdateData ()
		{
			int i = ScpPrm.Getter ( Scp );
			this.Text = i.ToString ();
			Disp ();
		}

		//関連付け
		public void Assosiate ( Script s )
		{
			Scp = s;
			UpdateData ();
		}
		
		public void SetTarget_All () { editTarget = EditTarget.ALL; }
		public void SetTarget_Group () { editTarget = EditTarget.GROUP; }
		public void SetTarget_Single () { editTarget = EditTarget.SINGLE; }
		public void SetTarget ( EditTarget et ) { editTarget = et; }

		//---------------------------------------------------------------
		//コントロールによる値設定

		//キー入力(文字コード判定)
		protected override void OnKeyPress ( KeyPressEventArgs e )
		{
			char c = e.KeyChar;

			//数字、マイナス、BackSpaceだけ入力可能(他は除外)
			switch ( c )
			{
			case '\b'	: e.Handled = false; break;
			case '-'	: e.Handled = false; break;
			default : 
				//10進数でなければ除外
				if ( ! char.IsDigit ( c ) )
				{
					e.Handled = true; 
				}
			break;
			}

			base.OnKeyPress ( e ); 
		}

		//キーボード押下
		protected override void OnKeyDown ( KeyEventArgs e )
		{
			//テキストボックスに数値が入力されていてEnterが押されたとき、
			//関連付けられた値を保存
			if ( e.KeyCode == Keys.Enter )
			{
				SetValue ();		//値の設定
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

			//編集対象
			switch ( editTarget )
			{
			case EditTarget.ALL: 
				EditCompend.EditSequence.DoSetterInSqc_T ( ScpPrm.Setter, value );
			break;
			
			case EditTarget.GROUP:
				EditCompend.EditScript.DoSetterInGroup_T ( ScpPrm.Setter, value );
			break;
			
			case EditTarget.SINGLE:
				ScpPrm.Setter ( Scp, value );
			break;

			default: break;
			}
			
		}
	}
}
