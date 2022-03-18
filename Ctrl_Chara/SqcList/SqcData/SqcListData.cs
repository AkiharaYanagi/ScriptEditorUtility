using System;

namespace ScriptEditor
{
	using BD_SqcDt = BindingDictionary < SequenceData >;
	using BD_IMGDT = BindingDictionary < ImageData >;

	//シークエンスとイメージリスト
	public class SequenceData : TName
	{
		//名前、スクリプト数、次シークエンス名 を持ち、スクリプト配列は扱わない
		public Sequence Sqc { get; set; } = new Sequence ();
		public BD_IMGDT L_ImgDt { get; set; } = new BD_IMGDT ();

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
		private Compend Compend = null;

		public void Clear ()
		{
			L_Sqc.Clear ();
		}

		//名前の更新
		public void UpdateName ()
		{
			foreach ( SequenceData sqcDt in L_Sqc.GetBindingList () )
			{
				sqcDt.SetName ( sqcDt.Name );
			}
		}

		//データ設定
		public void SetData ( Compend cmpd )
		{
			Compend = cmpd;

			L_Sqc.Clear ();
			foreach ( Sequence sqc in cmpd.BD_Sequence.GetEnumerable () )
			{
				SequenceData sqcDt = new SequenceData ()
				{
					Name = sqc.Name,
					nScript = sqc.ListScript.Count,
					Sqc = sqc,
				};

				L_Sqc.Add ( sqcDt );
			}
		}

		//データ適用
		public void ApplyData ()
		{
			//L_SqcからCompendに戻す
			Compend.BD_Sequence.Clear ();
			foreach ( SequenceData sqcDt in L_Sqc.GetEnumerable () )
			{
				Compend.BD_Sequence.Add ( sqcDt.Sqc );
			}

			//イメージ
		}
	}
}
