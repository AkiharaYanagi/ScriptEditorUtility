using System;

namespace ScriptEditor
{
	//スクリプトグループ編集のためのパラメータ操作クラス
	public class IScriptParam
	{
		//表示
		public System.Action Disp { get; set; } = ()=>{};

		// 環境( Setter, Getter )指定
		public virtual void SetEnvironment ( EditCompend ec, System.Action disp )
		{
		}

		// 環境( Setter, Getter )指定
		public virtual void SetParam < T > ( ScriptParam < T > ScpPrm  )
		{
		}

		//更新
		public virtual void UpdateData ()
		{
		}

		//関連付け
		public virtual void Assosiate ( Script s )
		{
		}

		//編集対象切替
		protected enum EditTarget
		{ 
			ALL, GROUP, SINGLE,
		};
		protected EditTarget editTarget = EditTarget.SINGLE;

		public void SetTarget_All () { editTarget = EditTarget.ALL; }
		public void SetTarget_Group () { editTarget = EditTarget.GROUP; }
		public void SetTarget_Single () { editTarget = EditTarget.SINGLE; }
	}
}
