using System;
using System.Collections.Generic;


namespace ScriptEditor
{
	public class SubForms_Compend
	{

		//サブフォームリスト
		public List < SubForm_Compend > L_SubForm { get; set; } = new List<SubForm_Compend> ();

		//対象サブフォーム
		public FormRoute FormRoute = new FormRoute ();
		public FormImage FormImage = new FormImage ();
		public FormScript FormScript = new FormScript ();
		public Form_ScriptList Form_ScriptList = new Form_ScriptList ();
		public FormRect FormRect = new FormRect ();

		//コンストラクタ
		public SubForms_Compend ()
		{
			L_SubForm.Add ( Form_ScriptList );
			L_SubForm.Add ( FormScript );
			L_SubForm.Add ( FormImage );
			L_SubForm.Add ( FormRoute );
		}
	}
}
