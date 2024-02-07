using System;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;


namespace ScriptEditor
{
	//==========================================================================
	public class LoadImage
	{
		public void Run ( SqcListData data, string dirname )
		{
			try { _Run ( data, dirname ); }
			catch ( Exception e ) { Debug.WriteLine ( e );	}
		}

		private void _Run ( SqcListData data, string dirname )
		{
			//取得データに対し、名前があれば追加。無ければ何もしない
			//'_'アンダースコアは先頭と最後の区切りで用いる
			//"000_シークエンス名_ID.png"

			//----------------------------------------------
			//ファイル名を列挙して並替
			string[] filepaths = Directory.GetFiles ( dirname, "*.png", SearchOption.TopDirectoryOnly );
			if ( 0 >= filepaths.Length ) { return; }
			filepaths.OrderBy ( f => f );

			foreach ( string path in filepaths )
			{
				string filename = Path.GetFileName ( path );	//パスからファイル名
				string ex = filename.Substring ( 0, filename.LastIndexOf ( '.' ) );	//拡張子を外す
				string imageName = ex.Substring ( 4 );		// "000_"で4つ

				//検索用シークエンス名
				string sqcname = imageName.Substring ( 0, imageName.LastIndexOf ( '_' ) );		//イメージ番号を外す

				//対象がなかったら追加しないで飛ばす
				SequenceData sqcDt = data.L_Sqc.Get ( sqcname );
				if ( sqcDt is null ) { continue; }


				//イメージ名
				//ファイル書出時のみ[通し番号000]_を用い、それ以外は
				//"[シークエンス名]_[シークエンス内番号00].png" で扱う

				//イメージ作成
				//@info FromFileではファイル共有違反エラーとなり、その回避のためFileStreamを用いる
				FileStream fs = new FileStream ( path, FileMode.Open, FileAccess.Read );
				Image img = Image.FromStream ( fs );
				fs.Close ();

				//データに追加
				sqcDt.BD_ImgDt.Add ( new ImageData ( imageName + ".png", img ) );
			}
		}
	}
	//==========================================================================


	//コンペンドから画像読込
	public class LoadImageFromCmpd
	{
		void Run ( SqcListData data, Compend cmpd )
		{
			//通し番号のため、画像を一時的に列挙
			List < string > L_serial = new List<string> ();
			
			//シークエンス
			int sqc_index = 0;
			foreach ( Sequence sqc in cmpd.BD_Sequence.GetEnumerable () )
			{
				//イメージ
				int img_index = 0;
				foreach ( ImageData imgd in cmpd.BD_Image.GetEnumerable () )
				{
					//イメージリストはひとまとめなので、シークエンス名でカウントする
					if ( imgd.Name == sqc.Name )
					{
//						string s0 = sqc_index.ToString ("000") + "_";
						string s0 = "";
						string s1 = sqc.Name + "_" ;
						string s2 = img_index.ToString ("00") + ".png";
						L_serial.Add ( s0 + s1 + s2 );
						++ img_index;
					}
				}
				++ sqc_index;
			}

			//イメージデータリストからシークエンスリストに再配置
			foreach ( string name in L_serial )
			{

			}
		}
	}

}
