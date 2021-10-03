namespace ScriptEditor
{
	//---------------------------------------------------------------------
	// シークエンスの編集をする
	//---------------------------------------------------------------------
	public class EditSequence
	{
		public Sequence Sqc { get; set; } = null;

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
		public delegate void FuncEditScript ( Script scp );
		public void EditAllScript ( Compend cmpd, FuncEditScript editScp )
		{
			foreach ( Sequence sqc in cmpd.BD_Sequence.GetBindingList () )
			{
				foreach ( Script scp in sqc.ListScript )
				{
					editScp ( scp );
				}
			}
		}

		public void EditScriptInSequence ( FuncEditScript FuncES )
		{
			foreach ( Script scp in Sqc.ListScript )
			{
				FuncES ( scp );
			}
		}
	}

}
