using System;
using System.Collections.Generic;


namespace ScriptEditor
{
	using BD_SqcDt = BindingDictionary < SequenceData >;
	using BD_IMGDT = BindingDictionary < ImageData >;

	//シークエンスとイメージリスト
	public class SequenceData : TName
	{
		//"名前","スクリプト数" を持ち、スクリプト配列は扱わない
		public Sequence Sqc { get; set; } = new Sequence ();
		public BD_IMGDT BD_ImgDt { get; set; } = new BD_IMGDT ();

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
		// "[通し番号000]_[シークエンス名]_[シークエンス内番号00].png"
		public void UpdateName ()
		{
			//シークエンスデータに名前を反映
			foreach ( SequenceData sqcDt in L_Sqc.GetBindingList () )
			{
				sqcDt.SetName ( sqcDt.Name );
			}

			//---------------------------------------------
			//各イメージに名前をつける
			//名前検索用
			List < string > L_sqcName = new List < string > ();

			int sqc_index = 0;	//シークエンス番号
			foreach ( SequenceData sqcDt in L_Sqc.GetEnumerable () )
			{
				string sqcName = sqcDt.Name;
				int img_index = 0;
				foreach ( ImageData imgdt in sqcDt.BD_ImgDt.GetEnumerable() )
				{
					string s0 = sqc_index.ToString ("000") + "_";
					string s1 = sqcDt.Name + "_" ;
					string s2 = img_index.ToString ("00") + ".png";
					imgdt.Name = s0 + s1 + s2;
					++ img_index;
				}
				++ sqc_index;
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
			if ( Compend is null ) { return; }

			//L_SqcからCompendに戻す
			// ※Compendの状態を優先する
			//	L_Sqcにおける追加、削除などの変更点のみを反映する
			Compend.BD_Sequence.Clear ();
			foreach ( SequenceData sqcDt in L_Sqc.GetEnumerable () )
			{
				Compend.BD_Sequence.Add ( sqcDt.Sqc );
			}

			//イメージ
			Compend.BD_Image.Clear ();
			foreach ( SequenceData sqcDt in L_Sqc.GetEnumerable () )
			{
				foreach ( ImageData imgdt in sqcDt.BD_ImgDt.GetEnumerable () )
				Compend.BD_Image.Add ( imgdt );
			}
		}
	}
}
