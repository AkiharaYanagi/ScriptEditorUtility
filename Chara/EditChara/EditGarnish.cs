using System.ComponentModel;
using System.Collections.Generic;

namespace ScriptEditor
{
	using BD_Sqc = BindingDictionary < Sequence >;
	using L_Scp = List < Script >;

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

		public override void SetCharaData ( Compend cmpd )
		{
			//@info New ()を用いるためSequenceではなくEffectを指定し、Baseに渡さない

			base.Compend = cmpd;
			BD_Sqc bd_act = cmpd.BD_Sequence;	//BD型はSequenceだが、実体はEffect

			//個数が０のときダミー生成
			if ( 0 == bd_act.Count () ) { bd_act.Add ( new Effect () ); }
			//選択指定
			base.SelectedSequence = bd_act.Get ( 0 );

			//個数が０のときダミー生成
			L_Scp l_scp = SelectedSequence.ListScript;
			if ( 0 == l_scp.Count ) { l_scp.Add ( new Script () ); }
			//選択指定
			base.SelectedScript = l_scp [ 0 ];
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
