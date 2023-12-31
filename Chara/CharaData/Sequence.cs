﻿using System;
using System.Collections.Generic;

namespace ScriptEditor
{
	//================================================================
	//	<シークエンス>		各フレーム毎のスクリプトをリスト状に持つ
	//		┣名前
	//		┣[]スクリプト
	//		///┣汎用パラメータ(SqcParamを継承)
	//================================================================
	[Serializable]
	public class Sequence : IName
	{
		//名前
		public string Name { get; set; } = "SqcName";
		public string GetName () { return Name; }
		public override string ToString () { return this.Name; }

		//スクリプトリスト
		public List<Script> ListScript { get; set; } = new List<Script> ();

		//コンストラクタ
		public Sequence ()	//ロード時に空白が必要
		{
		}

		public Sequence ( string str )
		{
			this.Name = str;
			//ListScript.Add ( new Script () );   //自動的にスクリプトを持つ
		}

		//コピーコンストラクタ
		public Sequence ( Sequence sequence )
		{
			this.Name = sequence.Name;
			foreach ( Script script in sequence.ListScript )
			{
				ListScript.Add ( new Script ( script ) );
			}
		}

		//フレームの追加
		public void AddScript ()
		{
			Script script = new Script ();
			script.Frame = ListScript.Count;
			ListScript.Add ( script );
		}
		public void AddScript ( Script script )
		{
			script.Frame = ListScript.Count;
			ListScript.Add ( script );
		}

		//クリア
		//	※　スクリプトリストが０のまま扱わない
		public virtual void Clear ()
		{
			Name = "Clear";
			foreach ( Script script in ListScript )
			{
				script.Clear ();
			}
			ListScript.Clear ();
		}

		//全体コピー
		public virtual void Copy ( Sequence sequence )
		{
			Clear ();
			this.Name = sequence.Name;
			foreach ( Script script in sequence.ListScript )
			{
				ListScript.Add ( new Script ( script ) );
			}
		}

		//スクリプトリストのみコピー
		public virtual void CopyScpList ( Sequence sequence )
		{
			ListScript.Clear ();
			foreach ( Script script in sequence.ListScript )
			{
				ListScript.Add ( new Script ( script ) );
			}
		}
	}

}
