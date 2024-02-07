using System;
using System.Collections.Generic;


namespace ScriptEditor
{
	using BD_Sqc = BindingDictionary < Sequence >;
	using BD_Act = BindingDictionary < Action >;
	using BD_Efc = BindingDictionary < Effect >;
	using BD_SqcDt = BindingDictionary < SequenceData >;
	using BD_IMGDT = BindingDictionary < ImageData >;

	//シークエンスとイメージリスト
	public class SequenceData : TName
	{
		//"名前","スクリプト数","対象画像配列" を持ち、スクリプト配列は扱わない
		public Sequence Sqc { get; set; } = new Sequence ();
		public BD_IMGDT BD_ImgDt { get; set; } = new BD_IMGDT ();

		//コンストラクタ
		public SequenceData () {}
		
		//引数でNew Action() か New Effect() を指定する
		public SequenceData ( System.Func < Sequence > New_Ob )
		{
			Sqc = New_Ob ();
		}

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
		//元データの参照
		private Compend Compend = new Compend ();

		//対象データ
		public BD_SqcDt L_Sqc = new BD_SqcDt();

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

			//シークエンスリスト
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

			//シークエンスリストを作ってからイメージの再配置
			foreach ( ImageData imgd in cmpd.BD_Image.GetEnumerable () )
			{
				string sqc_name = ImageName.GetSequenceName ( imgd.Name );

				foreach ( SequenceData sqcd in L_Sqc.GetEnumerable () )
				{
					if ( sqc_name.Equals ( sqcd.Name ) )
					{
						sqcd.BD_ImgDt.Add ( imgd );
						break;
					}
				}
			}
		}

		//既存Compendデータからアップデート
		public void UpdateData ()
		{
			SetData ( Compend );
		}

		//-----------------------------------------------------------------
		//データ適用(Action)
		public void ApplyData_Action ()
		{
			ApplyData ( ApplyData_NewAction );
		}

		//データ適用(Effect)
		public void ApplyData_Effect ()
		{
			ApplyData ( ApplyData_NewEffect );
		}


		//データ適用(共通) Newの部分だけAction/Effect指定
		// L_Sqc から Compendに戻す
		public void ApplyData ( System.Func < Sequence, Sequence > NewSqc )
		{
			// ※Compendの状態を優先する
			//	L_Sqcにおける追加、削除などの変更点のみを反映する

			//一時保存
			BD_Sqc BD_Old = Compend.BD_Sequence;

			//L_SqcにないCompendのシークエンスを削除
			List < string > deleteList = new List<string> ();
			foreach ( Sequence sqc in BD_Old.GetEnumerable() )
			{
				if ( ! L_Sqc.ContainsKey ( sqc.Name ) )
				{
					deleteList.Add ( sqc.Name );
				}
			}
			foreach ( string deleteName in deleteList )
			{
				BD_Old.Remove ( deleteName );
			}


			//CompendにないL_SqcのシークエンスをNewして追加
			//CompendにないL_Sqcのアクションを追加
			BD_Sqc BD_New = new BD_Sqc();

			//List < string > addList = new List<string>();

			//順番はL_Sqcに基づく
			foreach ( SequenceData sqcd in L_Sqc.GetEnumerable () )
			{
				//既存はディープコピーする
				if ( Compend.BD_Sequence.ContainsKey ( sqcd.Name ) )
				{
//					BD_New.Add ( NewSqc ( BD_Old.Get( sqcd.Name ) ) );
					BD_New.Add ( NewSqc ( L_Sqc.Get( sqcd.Name ).Sqc ) );
				}
				else //無いときは新規
				{
					//スクリプト数が０なら１つ追加
					if ( sqcd.Sqc.ListScript.Count == 0 )
					{
						sqcd.Sqc.ListScript.Add ( new Script () );
					}
					BD_New.Add ( NewSqc ( sqcd.Sqc ) );
				}
			}

			//Compendに設置して終了
			Compend.BD_Sequence = BD_New;

			//---------------------------------------------
			//イメージは全部解放してから再作成する
			
			//イメージ　ファイル上ではIDをつけるが、Compendに返すときIDを外し
			//名前検索で指定できるようにする（IDずれ防止）
			SaveImage save_image = new SaveImage();

			Compend.BD_Image.Clear ();
			foreach ( SequenceData sqcDt in L_Sqc.GetEnumerable () )
			{
				foreach ( ImageData imgdt in sqcDt.BD_ImgDt.GetEnumerable () )
				{
//					imgdt.Name = save_image.GetImageName_NoID ( imgdt.Name );
					Compend.BD_Image.Add ( imgdt );
				}
			}
		}

		//データ適用(Action)
		public Sequence ApplyData_NewAction ( Sequence sqc )
		{
			return new Action ( (Action)sqc );
		}


		//データ適用(Effect)
		public Sequence ApplyData_NewEffect ( Sequence sqc )
		{
			return new Effect ( (Effect)sqc );
		}


	}
}
