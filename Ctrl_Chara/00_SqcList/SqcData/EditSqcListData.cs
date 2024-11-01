using System;
using System.Drawing;
using System.IO;


namespace ScriptEditor
{
	using BD_Image = BindingDictionary < ImageData >;


	public class EditSqcListData
	{
		//対象データ
		public SqcListData Dt { get; set; } = new SqcListData ();
		public Compend Compend { get; set; } = new Compend ();

		//選択シークエンス
		public int SelectedSqc { get; set; } = 0;

		//選択イメージID
		public int SelectedImage { get; set; } = 0;

		//全体更新
		public System.Action UpdateAll { get; set; } = ()=>{};

		//名前の更新
		public void UpdateName () { Dt.UpdateName (); }


		//対象を取得
		public SqcListData Get ()
		{
			return Dt;
		}

		//選択中のシークエンスを取得
		public SequenceData GetSequenceData ()
		{
			if ( Dt.L_Sqc.Count () <= SelectedSqc ) { return null; }
			return Dt.L_Sqc.Get ( SelectedSqc );
		}

		//位置指定
		public void SetPt ( Point pt )
		{
			if ( Exist ( pt.Y, pt.X ) )
			{
				//両方成立のとき位置更新
				SelectedSqc = pt.Y;
				SelectedImage = pt.X;
			}
		}

		//画像削除
		public void Remove ()
		{
			if ( Exist () )
			{
				SequenceData sqcDt = Dt.L_Sqc.Get ( SelectedSqc );
				sqcDt.BD_ImgDt.RemoveAt ( SelectedImage );
			}
			
			ImageNameID_Reset ();
		}

		//存在するかどうか
		public bool Exist ()
		{
			return Exist ( SelectedSqc, SelectedImage );
		}

		public bool Exist ( int sqc, int image )
		{
			//シークエンス
			if ( 0 <= sqc && sqc < Dt.L_Sqc.Count() )
			{
				//イメージ
				SequenceData scqDt = Dt.L_Sqc.Get ( sqc );
				if ( 0 <= image && image < scqDt.BD_ImgDt.Count () )
				{
					//両方成立のとき
					return true;
				}
			}
			return false;
		}

		//イメージ配列操作
		//前
		public void Prev ()
		{
			if ( ! Exist () ) { return; }
			SequenceData sqcDt = Dt.L_Sqc.Get ( SelectedSqc );
			if ( sqcDt.BD_ImgDt.Count () < 2 ) { return; }
			if ( SelectedImage == 0 ) { return; }

			ImageData imgDt_temp = sqcDt.BD_ImgDt [ SelectedImage ];
			sqcDt.BD_ImgDt [ SelectedImage ] = sqcDt.BD_ImgDt [ SelectedImage - 1 ];
			sqcDt.BD_ImgDt [ SelectedImage - 1 ] = imgDt_temp;
			
			ImageNameID_Reset ();
		}
		//次
		public void Next ()
		{
			if ( ! Exist () ) { return; }
			SequenceData sqcDt = Dt.L_Sqc.Get ( SelectedSqc );
			if ( sqcDt.BD_ImgDt.Count () < 2 ) { return; }
			if ( SelectedImage == sqcDt.BD_ImgDt.Count () - 1 ) { return; }

			ImageData imgDt_temp = sqcDt.BD_ImgDt [ SelectedImage ];
			sqcDt.BD_ImgDt [ SelectedImage ] = sqcDt.BD_ImgDt [ SelectedImage + 1 ];
			sqcDt.BD_ImgDt [ SelectedImage + 1 ] = imgDt_temp;

			ImageNameID_Reset ();
		}

		//先頭
		public void Head ()
		{
			if ( ! Exist () ) { return; }
			SequenceData sqcDt = Dt.L_Sqc.Get ( SelectedSqc );
			if ( sqcDt.BD_ImgDt.Count () < 2 ) { return; }
			if ( SelectedImage == 0 ) { return; }

			//指定を保存
			ImageData imgDt_temp = sqcDt.BD_ImgDt [ SelectedImage ];

			//指定から先頭まで戻りつつ入替
			for ( int index = SelectedImage - 1; index >= 0; -- index )
			{
				sqcDt.BD_ImgDt [ index + 1 ] = sqcDt.BD_ImgDt [ index ];
			}
			sqcDt.BD_ImgDt [ 0 ] = imgDt_temp;
			
			ImageNameID_Reset ();
		}

		//末尾
		public void Tail ()
		{
			if ( ! Exist () ) { return; }
			SequenceData sqcDt = Dt.L_Sqc.Get ( SelectedSqc );
			if ( sqcDt.BD_ImgDt.Count () < 2 ) { return; }
			int tail = sqcDt.BD_ImgDt.Count () - 1;
			if ( SelectedImage == tail ) { return; }

			//指定を保存
			ImageData imgDt_temp = sqcDt.BD_ImgDt [ SelectedImage ];

			//指定から末尾１つ前まで入替
			for ( int index = SelectedImage; index + 1 <= tail; ++ index )
			{
				sqcDt.BD_ImgDt [ index ] = sqcDt.BD_ImgDt [ index + 1 ];
			}
			sqcDt.BD_ImgDt [ tail ] = imgDt_temp;
			
			ImageNameID_Reset ();
		}

		//選択中シークエンスデータのイメージを全消去
		public void EraseImage ()
		{
			if ( ! Exist () ) { return; }
			SequenceData sqcDt = Dt.L_Sqc.Get ( SelectedSqc );

			sqcDt.BD_ImgDt.Clear ();
		}

		//番号振り直し
		public void ImageNameID_Reset ()
		{
			SequenceData sqcDt = Dt.L_Sqc.Get ( SelectedSqc );
			
			int i = 0;
			foreach ( ImageData imgd in sqcDt.BD_ImgDt.GetEnumerable () )
			{
				int len = imgd.Name.Length;
				string front = imgd.Name.Substring ( 0, len - 7 );	//" *_00.png "
				string newname = "_" + i.ToString ( "00" ) + ".png";
				imgd.Name = front + newname;
				++ i;
			}
		}

		//データをCompend型に戻す
		public void ApplyData_Action ()
		{
			Dt.ApplyData_Action ();
		}
		public void ApplyData_Effect ()
		{
			Dt.ApplyData_Effect ();
		}

		//イメージ名をリセット
		public void ResetImageName ()
		{
			//イメージ個数
			int nImage = 0;
			foreach ( SequenceData sqcDt in Dt.L_Sqc.GetEnumerable () )
			{
				nImage += sqcDt.BD_ImgDt.Count ();
			}

			//すべてのスクリプトに設定
			if ( nImage > 0 )
			{
				string strImage_0 = "ImgName";	//仮イメージ名指定
				foreach ( SequenceData sqcDt in Dt.L_Sqc.GetEnumerable () )
				{
					if ( 0 < sqcDt.BD_ImgDt.Count () )
					{
						strImage_0 = sqcDt.BD_ImgDt[0].Name;
						break;
					}
				}

				foreach ( SequenceData sqcDt in Dt.L_Sqc.GetEnumerable () )
				{
					foreach ( Script scp in sqcDt.Sqc.ListScript )
					{
						if ( sqcDt.BD_ImgDt.Count () > 0 )
						{
							scp.ImgName = sqcDt.BD_ImgDt [ 0 ].Name;
						}
						else
						{
							scp.ImgName = strImage_0;
						}
					}
				}
			}
		}

		//イメージのクリア
		public void ClearImage ()
		{
			foreach ( SequenceData sqcDt in Dt.L_Sqc.GetEnumerable () )
			{
				sqcDt.BD_ImgDt.Clear ();
			}
		}

		//イメージディレクトリを指定して読込
		public void LoadImageFromDir ( string imgDir )
		{
			if ( Directory.Exists ( imgDir ) )
			{
				LoadImage li = new LoadImage ();
				li.Run ( Dt, imgDir );
				UpdateAll ();
			}
		}

		//ディレクトリ指定ダイアログから読込
		public string LoadImageFromDialog ( string imgDir )
		{
			string ret = "";
			OpenFolder_CodePack opF = new OpenFolder_CodePack ();
			opF.SetDefaultFilename ( imgDir );
			if( opF.OpenFolder () )
			{
				ret = opF.GetPath ();
			}

			if ( Directory.Exists ( ret ) )
			{
				LoadImage li = new LoadImage ();
				li.Run ( Dt, ret );
				UpdateAll ();
			}

			return ret;
		}

		//保存
		public void SaveImageToDir ( string imgDir )
		{
			SaveImage saveImage = new SaveImage ();
			saveImage.Run ( Dt, imgDir, Compend );
		}
		

		//シークエンス名から選択状態にする
		public void SelectFromName ( string sqcName )
		{
			SelectedSqc = Dt.L_Sqc.IndexOf ( sqcName );
		}

		//選択中のシークエンス名を取得する
		public string SelectedSqcName ()
		{
			return Dt.L_Sqc [ SelectedSqc ].Name; 
		}

	}
}
