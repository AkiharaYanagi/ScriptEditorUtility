﻿using System;
using System.Drawing;
using System.IO;


namespace ScriptEditor
{
	public class EditSqcListData
	{
		//対象データ
		public SqcListData Dt { get; set; } = null;

		//選択シークエンス
		public int SelectedSqc { get; set; } = 0;

		//選択イメージID
		public int SelectedImage { get; set; } = 0;

		//全体更新
		public System.Action UpdateAll { get; set; } = null;

		//名前の更新
		public void UpdateName () { Dt.UpdateName (); }

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

		//選択中のシークエンスを取得
		public SequenceData GetSequenceData ()
		{
			if ( Dt.L_Sqc.Count () <= SelectedSqc ) { return null; }
			return Dt.L_Sqc.Get ( SelectedSqc );
		}

		//イメージ配列操作
		//前
		public void Prev ()
		{
			if ( ! Exist () ) { return; }
			if ( SelectedImage == 0 ) { return; }

			SequenceData sqcDt = Dt.L_Sqc.Get ( SelectedSqc );
			if ( sqcDt.BD_ImgDt.Count () < 2 ) { return; }

			ImageData imgDt_temp = sqcDt.BD_ImgDt [ SelectedImage ];
			sqcDt.BD_ImgDt [ SelectedImage ] = sqcDt.BD_ImgDt [ SelectedImage - 1 ];
			sqcDt.BD_ImgDt [ SelectedImage - 1 ] = imgDt_temp;

//			STS_TXT.Trace ("Prev.");
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

//			STS_TXT.Trace ("Next.");
		}

		//データをCompend型に戻す
		public void ApplyData ()
		{
			Dt.ApplyData ();
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
			saveImage.Run ( Dt, imgDir );
		}
	}
}