namespace ScriptEditor
{
	//---------------------------------------------------------------------
	// シークエンスの編集をする
	//---------------------------------------------------------------------
	public class EditSequence
	{
		public Sequence Seq { get; set; } = null;

		public void Assosiate ( Sequence s )
		{
			Seq = s;
		}

		public void SetName ( string strName )
		{
			Seq.Name = strName;
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
	}

}
