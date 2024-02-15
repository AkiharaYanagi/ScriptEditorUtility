using System;

namespace ScriptEditor
{
	//編集対象切替
	public enum EditTargetScript
	{ 
		SINGLE, GROUP, SELECT, ALL,
	};

	//スクリプトグループ編集のためのパラメータ操作インターフェース
	public interface IScriptParam
	{
		//表示
		System.Action Disp { get; set; }

		// 環境( Setter, Getter )指定
		void SetEnvironment ( EditCompend ec, System.Action disp );

		//更新
		void UpdateData ();

		//関連付け
		void Assosiate ( Script s );

		//編集対象
		void SetTarget_Single ();
		void SetTarget_Group ();
		void SetTarget_Select ();
		void SetTarget_All ();

		void SetTarget ( EditTargetScript editTarget );
	}
}
