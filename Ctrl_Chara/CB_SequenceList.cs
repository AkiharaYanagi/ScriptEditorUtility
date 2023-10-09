﻿using System;
using System.Windows.Forms;
using System.ComponentModel;

namespace ScriptEditor
{
	using BD_Sqc = BindingDictionary < Sequence >;
	using BL_Sqc = BindingList < Sequence >;
	using SetFunc = System.Action < ScriptEditor.Sequence >;
//	using GetFunc = System.Func < int >;

	//------------------------------------------------------------
	// シークエンスバインディングリストから一つを選択するコンボボックス
	//------------------------------------------------------------
	public class CB_SequenceList : ComboBox 
	{
		//対象の保存
		public BD_Sqc BD_Sqc = null;

		//表示の更新用
//		public DispCompend DispCompend { get; set; } = null;
		public System.Action AllDisp = ()=>{};

		//設定用デリゲート
		public SetFunc SetFunc { get; set; } = sqc=>{};

#if false
		//初期化
		public void SetDisp ()
//		public void SetDisp ( DispCompend dc )
		{
//			DispCompend = dc;
		}
#endif

		public void SetDisp ( System.Action disp )
		{
			AllDisp = disp;
		}

		//キャラデータ読込時
		public void SetCharaData ( BD_Sqc bd_sqc )
		{
			//保存
			BD_Sqc = bd_sqc;

			//データソースに指定
			this.DataSource = bd_sqc.GetBindingList ();
		}

		//関連付け
		public void Associate ( SetFunc sf )
		{
			SetFunc = sf;
		}

		//名前から選択
		public void SelectName ( string name )
		{
			SelectedItem = BD_Sqc.Get ( name );
		}

		//閉じたときのイベント
		protected override void OnSelectionChangeCommitted ( EventArgs e )
		{
			//キャラに選択された値を反映
			SetFunc ( (Sequence)this.SelectedItem );

			//表示更新
			//DispCompend.Disp ();
			//Ctrl_All.Inst.AllDisp ();

			base.OnSelectionChangeCommitted ( e );
		}
	}

}

