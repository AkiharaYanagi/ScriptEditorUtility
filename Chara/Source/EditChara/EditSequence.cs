namespace ScriptEditor
{
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

		public void SetName ( string strName )
		{
			Sqc.Name = strName;
		}


		//コンペンドを指定してすべてのシークエンス中のスクリプトを編集
		//引数：スクリプトを対象とした編集デリゲート
		public void EditAllScript ( Compend cmpd, System.Action < Script > F_EditScp )
		{
			foreach ( Sequence sqc in cmpd.BD_Sequence.GetEnumerable () )
			{
				foreach ( Script scp in sqc.ListScript )
				{
					F_EditScp ( scp );
				}
			}
		}

		//対象シークエンスのスクリプトに対し編集
		public void EditScriptInSequence ( System.Action < Script > F_EditScp )
		{
			foreach ( Script scp in Sqc.ListScript )
			{
				F_EditScp ( scp );
			}
		}

		//汎用
		public void DoSetterInSqc_T < T > ( System.Action < Script, T > Setter, T t )
		{


			//@todo Edit におけるSelectedSequenceの見直し



			foreach ( Script s in Sqc.ListScript ) { Setter ( s, t ); }
		}
	}

}
