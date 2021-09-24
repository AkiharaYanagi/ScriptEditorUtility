﻿using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace ScriptEditor
{
	using BL_Sqc = BindingList<Sequence>;
	using L_Scp = List<Script>;

	//---------------------------------------------------------------------
	// コンペンド(アクション/エフェクトの集合)を受けて編集する
	//	スクリプト
	//---------------------------------------------------------------------
	public partial class EditCompend
	{
		//スクリプト追加
		public void AddScript ()
		{
			SelectedSequence.AddScript ();
		}
		public void AddScript ( Script script )
		{
			SelectedSequence.AddScript ( script );
		}

		//スクリプト挿入
		public void InsertScript ()
		{
			SelectedSequence.ListScript.Insert ( SelectedScriptIndex, new Script () );
		}
		public void InsertScript ( Script script )
		{
			SelectedSequence.ListScript.Insert ( SelectedScriptIndex, script );
		}

		//複数挿入
		public void MultiInsert ()
		{
			int s = SelectedSpanStart;
			int e = 1 + SelectedSpanEnd;
			Script[] scripts = new Script [ e - s ];
			for ( int i = 0; i < e - s; ++ i )
			{
				scripts [ i ] = new Script ();
			}
			SelectedSequence.ListScript.InsertRange ( s, scripts );
		}

		//選択中のスクリプトを削除
		public void RemScript ()
		{
			int i = SelectedSequence.ListScript.IndexOf ( SelectedScript );
			SelectedSequence.ListScript.RemoveAt ( i );
		}

		//複数削除
		public void MultiRem ()
		{
			int s = SelectedSpanStart;
			int e = 1 + SelectedSpanEnd;
			SelectedSequence.ListScript.RemoveRange ( s, e - s );
		}



		//--------------------------------------------------------------------
		//コピー
		public SELECTED_SCRIPT CopiedScript { get; } = new SELECTED_SCRIPT ( 0, 0 );

		//コピー元スクリプトを保存
		public void CopyTargetScript ()
		{
//			CopiedScript.Copy ( SelectedScript );
			IsCopy = true;
		}

		//コピー元スクリプトを取得
		private Script GetCopiedScript ()
		{
			//対象シークエンス
			BL_Sqc bl_sqc = Compend.BD_Sequence.GetBindingList ();
			int s = CopiedScript.sequence;
			if ( s < 0 || bl_sqc.Count <= s ) { return null; }
			Sequence seq = bl_sqc [ s ];

			//フレーム
			int f = CopiedScript.frame;
			if ( f < 0 || seq.ListScript.Count <= f ) { return null; }

			return seq.ListScript [ f ];
		}

		//ペースト
		public void PasteScript ()
		{
			//範囲にコピー
			for ( int i = SelectedSpanStart; i < SelectedSpanEnd + 1; ++i )
			{
				Script script = SelectedSequence.ListScript [ i ];
				script.Copy ( new Script ( GetCopiedScript () ) );
			}
		}
		//追加してペースト
		public void AddAndPasteScript ()
		{
			//			AddScript ( new Script ( GetCopiedScript () ) );
		}

		//挿入してペースト
		public void InsertAndPasteScript ()
		{
			//			InsertScript ( new Script ( GetCopiedScript () ) );
		}

#if false
		//ブランチのコピー
		public void SetBranch ( Script scp )
		{
			foreach ( Script s in SelectedSequence.ListScript )
			{
				s.ListBranch.Copy ( scp.ListBranch );
			}
		}
#endif
		//ルートのコピー
		public void CopyRoute ( Script scp )
		{
			foreach ( Script s in SelectedSequence.ListScript )
			{
				s.BL_RutName = new BindingList < TName > ( s.BL_RutName );
			}
		}

#if false

		//---------------------------------------------------------------------
		//	範囲選択
		//---------------------------------------------------------------------
		public struct SELECTED_SCRIPT_SPAN
		{
			public int sequence;
			public int start;		//script start
			public int end;			//script end
		}
		private SELECTED_SCRIPT_SPAN selectedSpan = new SELECTED_SCRIPT_SPAN ();
		public SELECTED_SCRIPT_SPAN SelectedSpan { get { return selectedSpan; } }

		//範囲選択を設定
		//引数
		//	int action : 選択アクション
		//	int start : スクリプト選択開始位置
		//	int end : スクリプト選択終了位置
		public void SetSpan ( int action, int start, int end )
		{
			if ( start > end ) { return; }
			if ( action < 0 || Compend.ListSequence.Count <= action ) { return; }
			Sequence seq = Compend.ListSequence[ selectedScript.sequence ];
			if ( start < 0 || seq.ListScript.Count <= start ) { return; }
			if ( end < 0 || seq.ListScript.Count <= end ) { return; }

			selectedSpan.sequence = action;
			selectedSpan.start = start;
			selectedSpan.end = end;
		}


		//---------------------------------------------------------------------
		//	スクリプト変更後に他スクリプトにコピーするかチェックする
		//---------------------------------------------------------------------
		public void CheckSetOtherScript ()
		{
			//スクリプトの全てに項目全てをコピー
			if ( AllScript )
			{
				CopyToAllScript ();
			}
			//スクリプトの選択範囲に項目全てをコピー
			else if ( SpanScript )
			{
				CopyToSpanScript ();
			}
		}

		//---------------------------------------------------------------------
		//	選択スクリプトに変更項目のみコピー
		//---------------------------------------------------------------------
		//変更されたスクリプト内の項目アドレスと変更値を受け取り、
		//アクション内すべてのスクリプトに変更を伝える
		public void EditSelectedContents ( ScriptAddress scriptAdress, int value )
		{
			if ( null == scriptAdress ) { return; }
			Sequence seq = GetSequence ();
			if ( null == seq ) { return; }
			Script selectedScript = GetScript ();
			if ( null == selectedScript ) { return; }

			//スクリプト選択範囲
			for ( int i = selectedSpan.start; i < selectedSpan.end + 1; ++i )
			{
				//各スクリプトにアドレスと値を渡す
				Script s = seq.ListScript[ i ];
				if ( s == selectedScript ) { continue; }	//コピー元は飛ばす
				s.SetValueInAdress ( scriptAdress, value );
			}
		}

		//シークエンス全てのスクリプトにコピー
		public void CopyToAllScript ()
		{
			Sequence seq = GetSequence ();
			if ( null == seq ) { return; }
			Script selectedScript = GetScript ();
			if ( null == selectedScript ) { return; }

			foreach ( Script s in seq.ListScript )
			{
				if ( s == selectedScript ) { continue; }
				s.Clear ();
				s.Copy ( selectedScript );
			}
		}

		//スクリプトの選択範囲に項目全てをコピー
		public void CopyToSpanScript ()
		{
			Sequence action = GetSequence ();
			if ( null == action ) { return; }
			Script selectedScript = GetScript ();
			if ( null == selectedScript ) { return; }
			EditCompend.SELECTED_SCRIPT_SPAN sss = selectedSpan;

			for ( int i = sss.start; i < sss.end + 1; ++i )
			{
				Script s = action.ListScript[ i ];
				if ( s == selectedScript ) { continue; }
				s.Clear ();
				s.Copy ( selectedScript );
			}
		}

		//---------------------------------------------------------------------
		//スクリプト変更後に他スクリプトにコピーするかチェックする
		public void CheckSetScript ()
		{
			//スクリプトの全てに項目全てをコピー
			if ( AllScript )
			{
				CopyToAllScript ();
			}
			//スクリプトの選択範囲に項目全てをコピー
			else if ( SpanScript )
			{
				CopyToSpanScript ();
			}
		}

		//---------------------------------------------------------------------
		//	エフェクトジェネレート選択位置
		//---------------------------------------------------------------------
		public int selectedIndexEfGnrt = new int ();
#endif
	}
}
