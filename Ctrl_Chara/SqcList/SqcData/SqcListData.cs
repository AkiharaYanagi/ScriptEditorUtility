using System;
using System.Collections.Generic;

namespace ScriptEditor
{
	using BD_SqcDt = BindingDictionary < SequenceData >;

	//シークエンスとイメージリスト
	public class SequenceData : TName
	{
		//名前、スクリプト数、次シークエンス名 を持ち、スクリプト配列は扱わない
		public Sequence Sqc { get; set; } = new Sequence ();
		public List < ImageData > L_ImgDt { get; set; } = new List<ImageData> ();

		public void SetName ( string name )
		{
			base.Name = name;
			Sqc.Name = name;
		}

		public int nScript { get; set; } = 0;
	}

	//データ集合
	public class SqcListData
	{
		public BD_SqcDt L_Sqc = new BD_SqcDt();

		public void Clear ()
		{
			L_Sqc.Clear ();
		}

		public void UpdateName ()
		{
			foreach ( SequenceData sqcDt in L_Sqc.GetBindingList () )
			{
				sqcDt.SetName ( sqcDt.Name );
			}
		}
	}
}
