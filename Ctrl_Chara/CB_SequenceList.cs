﻿using System;
using System.Windows.Forms;


namespace ScriptEditor
{
	using BD_Sqc = BindingDictionary < Sequence >;
	using SetFunc = System.Action < ScriptEditor.Sequence >;

	//------------------------------------------------------------
	// シークエンスリストから一つを選択するコンボボックス
	//------------------------------------------------------------
	public class CB_SequenceList : ComboBox 
	{
		//対象の保存
		public BD_Sqc BD_Sqc = new BD_Sqc ();

		//表示の更新用
		public System.Action AllDisp = ()=>{};

		//設定用デリゲート
		public SetFunc SetFunc { get; set; } = sqc=>{};

		//キャラデータ読込時
		public void SetCharaData ( BD_Sqc bd_sqc )
		{
			//保存
			BD_Sqc = bd_sqc;

			//データソースに指定
			this.DataSource = bd_sqc.GetBindingList ();
		}

		public void ResetItems ()
		{
			BD_Sqc.ResetItems ();
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
			AllDisp ();

			base.OnSelectionChangeCommitted ( e );
		}
	}

}

