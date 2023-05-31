using System;
using System.IO;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Collections.Generic;


namespace ScriptEditor
{
	public class SaveImage
	{
		public void Run ( SqcListData dt, string dirname, Compend cmpd )
		{
			try { _Run ( dt, dirname, cmpd ); }
			catch ( Exception e ) { Debug.WriteLine(e); }
		}

		private void _Run ( SqcListData dt, string dirname, Compend cmpd )
		{
			//-------------------------------------------------------
			//ディレクトリ
			if ( ! Directory.Exists ( dirname ) )
			{
				//存在しないとき作成
				Directory.CreateDirectory ( dirname );
			}

			//-------------------------------------------------------
			//既存を全削除
			IEnumerable < string > files = Directory.EnumerateFiles ( dirname, "*" );
			foreach ( string s in files )
			{
				FileInfo f = new FileInfo ( s );
				f.Delete ();
			}

			//-------------------------------------------------------
			//名前を付けてファイルとして保存
			// "[通し番号000]_[シークエンス名]_[シークエンス内番号00].png"

			//再検索用
			List < string > L_str = new List<string> ();

			int sqc_index = 0;	//シークエンス番号
			foreach ( SequenceData sd in dt.L_Sqc.GetEnumerable () )
			{
				//Debug.WriteLine ( sd.Name );

				int img_index = 0;
				foreach ( ImageData id in sd.BD_ImgDt.GetEnumerable() )
				{
					string s0 = sqc_index.ToString ("000") + "_";
					string s1 = sd.Name + "_" ;
					string s2 = img_index.ToString ("00") + ".png";
					string filepath = dirname + "\\" + s0 + s1 + s2;
					id.Img.Save ( filepath, ImageFormat.Png );

					L_str.Add ( s0 + s1 + s2 );

					++ img_index;

					//Debug.WriteLine ( filepath );
				}
				++ sqc_index;
			}


			//@info 保存するときに旧データを検索して名前を更新する
			//シークエンス名で検索して、シークエンス内番号は残し、通し番号を更新
			// "[通し番号000]_[シークエンス名]_[シークエンス内番号00].png"

			//旧データ シークエンスリスト検索
			foreach ( Sequence sqc in cmpd.BD_Sequence.GetEnumerable () )
			{
				foreach ( Script scp in sqc.ListScript )
				{
					//名前切出
					string s = GetSeqName ( scp.ImgName );

					//新規イメージデータ検索
					foreach ( string str in L_str )
					{
						if ( s == GetSeqName ( str ) )
						{
							scp.ImgName = GetID ( str ) + s + GetID_inSeq ( scp.ImgName );
							break;
						}
					}
				}
			}
		}

		
		// "[通し番号000]_[シークエンス名]_[シークエンス内番号00].png"
		//通し番号
		private string GetID ( string imgFileName )
		{
			int size = imgFileName.Length;
			string retStr = imgFileName.Substring ( 0, 4 );
			return retStr;
		}

		//シークエンス名切出
		private string GetSeqName ( string imgFileName )
		{
			int size = imgFileName.Length;
			string retStr = imgFileName.Substring ( 4, size - 11 );
			return retStr;
		}

		//シークエンス内番号
		private string GetID_inSeq ( string imgFileName )
		{
			int size = imgFileName.Length;
			string retStr = imgFileName.Substring ( size - 7, 7 );
			return retStr;
		}


	}
}
