using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace ScriptEditor
{
	using BD_Sqc = BindingDictionary < Sequence >;
	using BL_Sqc = BindingList < Sequence >;
	using L_Scp = List < Script >;

//	delegate void FuncEditScript ( Script scp );
	using Fc_Scp = System.Action < Script >;

	//---------------------------------------------------------------------
	// コンペンド(アクション/エフェクトの集合)を受けて編集する
	//	選択中のシークエンスとスクリプト位置を保持
	//---------------------------------------------------------------------
	public partial class EditCompend
	{
		//---------------------------------------------------------------------
		//編集対象
		public Compend Compend { get; set; } = new Compend ();

		//部分編集
		public EditSequence EditSequence { get; set; } = new EditSequence ();
		public EditScript EditScript { get; set; } = new EditScript ();

		//---------------------------------------------------------------------
		//選択位置
		public Sequence SelectedSequence { get; set; } = new Sequence ();
		public Script SelectedScript { get; set; } = new Script ();

		public int SelectedSpanStart { get; set; } = 0;
		public int SelectedSpanEnd { get; set; } = 0;
		//---------------------------------------------------------------------

		//コンストラクタ
		public EditCompend ()
		{
			SetEnviron ();
		}

		//---------------------------------------------------------------------
		//環境設定
		public void SetEnviron ()
		{
			EditScript.EditEfGnrt.SetEnviron ( this );
		}

		//---------------------------------------------------------------------
		//対象設定
		public virtual void SetCharaData ( Compend cmpd )
		{
			Compend = cmpd;
			BD_Sqc bd_sqc = cmpd.BD_Sequence;

			//個数が０のときダミー生成
			if ( 0 == bd_sqc.Count () ) { bd_sqc.New (); }
			//選択指定
			SelectedSequence = bd_sqc.Get ( 0 );

			//個数が０のときダミー生成
			L_Scp l_scp = SelectedSequence.ListScript;
			if ( 0 == l_scp.Count ) { l_scp.Add ( new Script () ); }
			//選択指定
			SelectedScript = l_scp [ 0 ];

			Assosiate ();
		}

		//---------------------------------------------------------------------
		public bool IsCopy { get; set; } = false;       //	コピー中フラグ
		public bool AllScript { get; set; } = false;    //	スクリプト全体の変更トグル
		public bool SpanScript { get; set; } = false;   //	スクリプト範囲の変更トグル

		//---------------------------------------------------------------------
		//スクリプトを数値で指定(範囲チェック付)
		public void SelectScript ( int sequence, int frame )
		{
			_AssignScript ( sequence, frame );
			Assosiate ();
		}

		//フレームのみ選択
		public void SelectFrame ( int frame )
		{
			_AssignFrame ( frame );
			Assosiate ();
		}

		//シークエンスのみ選択
		public void SelectSequence ( int sequence )
		{
			_AssignSequence ( sequence );
			_AssignFrame ( 0 );			//スクリプトは(存在すれば)０番目を選択
			Assosiate ();
		}

		//名前からシークエンス選択
		public void SelectSequence ( string name )
		{
			_AssignSequence ( name );
			_AssignFrame ( 0 );			//スクリプトは０を選択
			Assosiate ();
		}

		//---------------------------------------------------------------------
		//内部用：直接指定
		private void _AssignScript ( int sqc, int frame )
		{
			_AssignSequence ( sqc );
			_AssignFrame ( frame );
		}

		//内部用：フレーム直接指定
		private void _AssignFrame ( int frame )
		{
			L_Scp lscp = SelectedSequence.ListScript;
			if ( frame < 0 || lscp.Count <= frame ) { return; }

			SelectedScript = lscp [ frame ];
			SelectedSpanStart = frame;
			SelectedSpanEnd = frame;
		}

		//内部用：シークエンス直接指定
		private void _AssignSequence ( int sqc )
		{
			BL_Sqc ls = Compend.BD_Sequence.GetBindingList ();
			if ( sqc < 0 || ls.Count <= sqc ) { return; }
			SelectedSequence = ls [ sqc ];
		}

		//内部用：シークエンス名前指定
		private void _AssignSequence ( string name )
		{
			//アクション名以外のとき何もしない
			Sequence sq = Compend.BD_Sequence.Get ( name );
			if ( null == sq ) { return; }
			SelectedSequence = sq;
		}

		//---------------------------------------------------------------------
		//関連付け(選択時に同期するオブジェクト)
		public void Assosiate ()
		{
			EditSequence.Assosiate ( SelectedSequence );

			L_Scp lscp = SelectedSequence.ListScript;
//			if ( SelectedScriptIndex >= lscp.Count ) { SelectedScriptIndex = lscp.Count; }

			//スクリプト編集(グループ)
			EditScript.Restruct ( lscp, SelectedScript.Frame );

			//スクリプトの関連付け
			EditScript.Assosiate ( SelectedScript );
		}
		//---------------------------------------------------------------------

		//---------------------------------------------------------------------
		//	シークエンスリストに対しての編集
		//---------------------------------------------------------------------

		//すべてのスクリプトの編集
		//internal void EditAllScript ( Compend compend, Fc_Scp f_editScp )
		public void EditAllScript ( Compend compend, Fc_Scp f_editScp )
		{
			foreach ( Sequence sqc in compend.BD_Sequence.GetBindingList () )
			{
				foreach ( Script scp in sqc.ListScript )
				{
					f_editScp ( scp );
				}
			}
		}

		//シークエンス内スクリプトすべてに対して編集
		public void EditScriptInSequence ( Fc_Scp Fc )
		{
			foreach ( Script scp in SelectedSequence.ListScript )
			{
				Fc ( scp );
			}
		}

		//末尾にシークエンス追加
		public void AddSequence ( Sequence s )
		{
			Compend.BD_Sequence.Add ( s );
		}

		//継承先で型指定の追加関数を実装
		public virtual void Add ()
		{
		}
		public void Add ( Sequence s )
		{
			Compend.BD_Sequence.Add ( s );
		}

		//選択している後にシークエンス挿入
		public void InsertSequence ( int index, Sequence s )
		{
			Compend.BD_Sequence.Insert ( index, s );
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
