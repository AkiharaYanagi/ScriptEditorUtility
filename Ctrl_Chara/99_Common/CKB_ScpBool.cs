using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


namespace ScriptEditor
{
	//----------------------------------------------------------------------
	//	スクリプトに基づいたブール値を表示・編集するチェックボックス
	//----------------------------------------------------------------------
	using Setter = System.Action < bool >;
	using Getter = System.Func < bool >;

	public class CKB_ScpBool : CheckBox
	{
		public Setter SetFunc { get; set; } = b=>{};
		public Getter GetFunc { get; set; } = ()=>false;

		public CKB_ScpBool()
		{
		}

		public void Assosiate ( Setter setfunc, Getter getfunc )
		{
			GetFunc = getfunc;
			this.Checked = GetFunc();
			SetFunc = setfunc;
		}

		protected override void OnCheckedChanged ( EventArgs e )
		{
			SetFunc ( this.Checked );
			base.OnCheckedChanged ( e );
		}

		void UpdateData ()
		{
			this.Checked = GetFunc();
		}
	}
}
