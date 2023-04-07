using System.Collections.Generic;


namespace ScriptEditor
{
	//==================================================================================
	//	キャラの編集
	//		Edit-系のクラスはキャラデータと同様なツリー構造を持ち、
	//		FormやControlから必要な部分だけ参照される
	//		シングルトンとしてグローバルからアクセス可能
	//
	//		アンドゥリドゥ
	//
	//==================================================================================
	public sealed partial class EditChara
	{
		//シングルトン実体
		public static EditChara Inst { get; } = new EditChara ();

		//編集中のキャラ
		public Chara Chara { get; set; } = new Chara ();

		//アンドゥリドゥ用ヒストリー
		public List< Chara > L_Chara = new List<Chara> ();
		private int undo_pointer = -1; //初期値:-1は対象外

		//部分編集
		public EditBehavior EditBehavior { get; set; } = null;
		public EditGarnish EditGarnish { get; set; } = null;

		//---------------------------------------------------------------------
		//プライベートコンストラクタ
		private EditChara ()
		{
			EditBehavior = new EditBehavior ();
			EditGarnish = new EditGarnish ();
		}

		//セット
		public void SetCharaDara ( Chara ch )
		{
			Chara = ch;
			EditBehavior.SetCharaData ( ch.behavior );
			EditGarnish.SetCharaData ( ch.garnish );
		}

		//クリア
		public void Clear ()
		{
			Chara.Clear ();
		}
		
		//	自分が持つオブジェクトに引数オブジェクトをディープコピーする
		public void Copy ( Chara chara )
		{
			chara.Copy ( chara );
		}



		//----------------------------------------------------
		// ◆ ヒストリーに記録
		//	 各種キャラの変更タイミングで、ヒストリーに記録する
		//----------------------------------------------------
		public void RecordChara ()
		{
			L_Chara.Add ( new Chara ( Chara ) );
			++ undo_pointer;
		}

		//アンドゥ
		public void Undo ()
		{
			//初期状態が " 1 ", 変更１回で " 2 " 
			if ( L_Chara.Count < 2 ) { return; }

			//ポインタが個数の範囲外だったら何もしない
			if ( undo_pointer < 0 || L_Chara.Count <= undo_pointer ) { return; }

			//現在指定キャラデータを一つ前にする
			Chara = L_Chara [ -- undo_pointer ];
		}
	}


}

