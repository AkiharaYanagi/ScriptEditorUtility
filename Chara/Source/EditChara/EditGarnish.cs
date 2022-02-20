﻿namespace ScriptEditor
{
	//---------------------------------------------------------------------
	// ガーニッシュの編集をする
	//---------------------------------------------------------------------
	public class EditGarnish : EditCompend 
	{
		public EditEffect EditEffect { get; set; } = null;

		public EditGarnish ()
		{
			EditEffect = new EditEffect (); 
		}

		public void SetCharaData ( Chara ch )
		{
			base.SetCharaData ( ch.garnish );
		}

		//取得
		public Effect Get ()
		{
			return ( Effect ) base.SelectedSequence;
		}

		//追加
		public override void Add ()
		{
			Compend.BD_Sequence.Add ( new Effect ( "new Effect" ) );
		}
		public void AddEffect ()
		{
			Compend.BD_Sequence.Add ( new Effect ( "new Effect" ) );
		}
		public void AddEffect ( Effect effect )
		{
			Compend.BD_Sequence.Add ( effect );
		}

		//挿入
		public void InsertEffect ( int index )
		{
			base.InsertSequence ( index, new Effect ( "new Effect" ) );
		}

		//削除
		public void RemoveEffect ()
		{
			base.RemoveSequence ();
		}
	}

}
