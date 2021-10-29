using System.ComponentModel;
using System.Collections.Generic;

namespace ScriptEditor
{
	using BL_Sqc = BindingList<Sequence>;
	using L_Scp = List<Script>;

	delegate void FuncEditScript ( Script scp );

	//---------------------------------------------------------------------
	// コンペンド(アクション/エフェクトの集合)を受けて編集する
	//	選択中のシークエンスとスクリプト位置を保持
	//---------------------------------------------------------------------
	public partial class EditCompend
	{
		//---------------------------------------------------------------------
		//編集対象
		public Compend Compend { get; set; } = null;

		//部分編集
		public EditSequence EditSequence { get; set; } = new EditSequence ();
		public EditScript EditScript { get; set; } = new EditScript ();

		//---------------------------------------------------------------------
		//選択位置
		public Sequence SelectedSequence { get; set; } = null;
		public Script SelectedScript { get; set; } = null;

		public int SelectedScriptIndex { get; set; } = 0;
		public int SelectedSpanStart { get; set; } = 0;
		public int SelectedSpanEnd { get; set; } = 0;
		//---------------------------------------------------------------------

		//---------------------------------------------------------------------
		//対象設定
		public void SetCharaData ( Compend cmpd )
		{
			Compend = cmpd;

			BindingList<Sequence> BL_Seq = Compend.BD_Sequence.GetBindingList ();
			if ( 0 == BL_Seq.Count ) { return; }
			SelectedSequence = BL_Seq [ 0 ];
			if ( 0 == SelectedSequence.ListScript.Count ) { return; }
			SelectedScript = SelectedSequence.ListScript [ 0 ];
		}

		//---------------------------------------------------------------------
		public bool IsCopy { get; set; } = false;       //	コピー中フラグ
		public bool AllScript { get; set; } = false;    //	スクリプト全体の変更トグル
		public bool SpanScript { get; set; } = false;   //	スクリプト範囲の変更トグル

		//---------------------------------------------------------------------
		//数値指定(範囲チェック付)
		public void SelectScript ( int sequence, int frame )
		{
			BL_Sqc ls = Compend.BD_Sequence.GetBindingList ();
			if ( sequence < 0 || ls.Count <= sequence ) { return; }
			SelectedSequence = ls [ sequence ];

			L_Scp lscp = SelectedSequence.ListScript;
			if ( frame < 0 || SelectedSequence.ListScript.Count <= 0 ) { return; }
			SelectedScript = lscp [ frame ];

			Assosiate ();
		}

		//フレームのみ選択
		public void SelectFrame ( int frame )
		{
			L_Scp lscp = SelectedSequence.ListScript;
			if ( frame < 0 || lscp.Count <= frame ) { return; }
			SelectedScriptIndex = frame;
			SelectedScript = lscp [ frame ];

			Assosiate ();
		}

		//シークエンスのみ選択
		public void SelectSequence ( int sequence )
		{
			BL_Sqc ls = Compend.BD_Sequence.GetBindingList ();
			if ( sequence < 0 || ls.Count <= sequence ) { return; }
			SelectedSequence = ls [ sequence ];

			//スクリプトは０を選択
			L_Scp lscp = SelectedSequence.ListScript;
			if ( lscp.Count <= 0 ) { return; }
			SelectFrame ( 0 );

			Assosiate ();
		}

		//名前からシークエンス選択
		public void SelectSequence ( string name )
		{
			//アクション名以外のとき何もしない
			Sequence sq = Compend.BD_Sequence.Get ( name );
			if ( null == sq ) { return; }

			SelectedSequence = sq;

			//スクリプトは０を選択
			L_Scp lscp = SelectedSequence.ListScript;
			if ( lscp.Count <= 0 ) { return; }
			SelectFrame ( 0 );

			Assosiate ();
		}

		//関連付け(選択時に同期するオブジェクト)
		public void Assosiate ()
		{
			EditSequence.Assosiate ( SelectedSequence );

			L_Scp lscp = SelectedSequence.ListScript;
			if ( SelectedScriptIndex >= lscp.Count ) { SelectedScriptIndex = lscp.Count; }

			//グループ
			EditScript.Restruct ( lscp, SelectedScript.Frame );

#if false
			//関連付け
			Ctrl_Cmpd.Assosiate ( SelectedScript );
			FormRect.Inst.Assosiate ( SelectedScript );
			FormEfGnrt.Inst.Assosiate ( SelectedScript );

			DispChara.Inst.Disp ();
#endif
		}

		//---------------------------------------------------------------------
		//	シークエンス
		//---------------------------------------------------------------------
		//末尾にシークエンス追加
		public void AddSequence ( Sequence s )
		{
			Compend.BD_Sequence.Add ( s );
		}

		public virtual void Add ()
		{
		}
		public void Add ( Sequence s )
		{
			Compend.BD_Sequence.Add ( s );
		}

		//選択している後にシークエンス挿入
		public void InsertSequence ( Sequence s )
		{
			Compend.BD_Sequence.Insert ( s );
		}
		public virtual void Insert ()
		{
			string name = "new Inserted Sequence";
			Compend.BD_Sequence.Insert ( new Sequence ( name ) );
		}
		public void Insert ( Sequence s )
		{
			Compend.BD_Sequence.Insert ( s );
		}

		//選択中のシークエンスを削除
		public void RemoveSequence ()
		{
			Compend.BD_Sequence.Remove ( SelectedSequence.Name );
		}
		public void Remove ()
		{
			Compend.BD_Sequence.Remove ( SelectedSequence.Name );
		}

	}

}
