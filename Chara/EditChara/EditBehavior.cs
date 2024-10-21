using System.Collections.Generic;

namespace ScriptEditor
{
	using BD_Sqc = BindingDictionary < Sequence >;
	using L_Scp = List < Script >;

	//---------------------------------------------------------------------
	// ビヘイビアの編集をする
	//	(CompendにはできないAction指定)
	//---------------------------------------------------------------------
	public class EditBehavior : EditCompend 
	{
		//シークエンス種類名
		public string kindSequence = "アクション";

		//部分編集：１アクション
		public EditAction EditAction { get; set; } = new EditAction ();

		//キャラデータの設定
		public override void SetCharaData ( Compend cmpd )
		{
			base.Compend = cmpd;
			BD_Sqc bd_act = cmpd.BD_Sequence;	//BD型はSequenceだが、実体はAction

			//個数が０のときダミー生成
			if ( 0 == bd_act.Count () ) { bd_act.Add ( new Action () ); }
			//選択指定
			base.SelectedSequence = bd_act.Get ( 0 );

			//個数が０のときダミー生成
			L_Scp l_scp = SelectedSequence.ListScript;
			if ( 0 == l_scp.Count ) { l_scp.Add ( new Script () ); }
			//選択指定
			base.SelectedScript = l_scp [ 0 ];
		}

		//取得
		public Action GetAction ()
		{
			return ( Action ) base.SelectedSequence;
		}

		//設定
		public void SetAction ( int indexAction, Action act )
		{
			base.SelectedSequence = act;
		}

		//追加
		public void AddAction ()
		{
			Compend.BD_Sequence.Add ( new Action () );
		}
		public void AddAction ( Action action )
		{
			Compend.BD_Sequence.Add ( action );
		}

		//挿入
		public void InsertAction ( int index )
		{
			base.InsertSequence ( index, new Action () );
		}

		//削除
		public void RemoveAction ()
		{
			base.RemoveSequence ();
		}

	}

}
