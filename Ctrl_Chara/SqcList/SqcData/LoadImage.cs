using System;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Drawing;


namespace ScriptEditor
{
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
				string ex = filename.Substring ( 0, filename.LastIndexOf ( '.' ) );		//拡張子を外す
				string sqcname = ex.Substring ( 4 );		// "000_"で4つ
				string imageName = sqcname.Substring ( 0, sqcname.LastIndexOf ( '_' ) );		//イメージ番号を外す

				//対象がなかったら飛ばす
				SequenceData sqcDt = data.L_Sqc.Get ( imageName );
				if ( sqcDt is null ) { continue; }


				//イメージ作成
				//@info FromFileではファイル共有違反エラーとなり、その回避のためFileStreamを用いる
				FileStream fs = new FileStream ( path, FileMode.Open, FileAccess.Read );
				Image img = Image.FromStream ( fs );
				fs.Close ();

				//データに追加
				sqcDt.BD_ImgDt.Add ( new ImageData ( filename, img ) );
			}
		}
	}
}
