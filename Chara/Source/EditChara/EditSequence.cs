namespace ScriptEditor
{
	using Fc_Scp = System.Action < Script >;

	//---------------------------------------------------------------------
	// シークエンスの編集をする
	//---------------------------------------------------------------------
	public class EditSequence
	{
		//編集対象シークエンス
		public Sequence Sqc { get; set; } = new	Sequence ();

		public void Assosiate ( Sequence s )
		{
			Sqc = s;
		}

		//対象シークエンスのスクリプトにセッタと値を反映
		public void DoSetterInSqc_T < T > ( System.Action < Script, T > Setter, T t )
		{
			foreach ( Script s in Sqc.ListScript ) { Setter ( s, t ); }
		}

		//対象シークエンスのスクリプトに対し編集
		public void EditScriptInSequence ( Fc_Scp F_EditScp )
		{
			foreach ( Script scp in Sqc.ListScript ) { F_EditScp ( scp ); }
		}
	}

}
