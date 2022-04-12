using System;
using System.IO;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Collections.Generic;


namespace ScriptEditor
{
	public class SaveImage
	{
		public void Run ( SqcListData dt, string dirname )
		{
			try { _Run(dt, dirname); }
			catch ( Exception e ) { Debug.WriteLine(e); }
		}
		private void _Run ( SqcListData dt, string dirname )
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

			int sqc_index = 0;	//シークエンス番号
			foreach ( SequenceData sd in dt.L_Sqc.GetEnumerable () )
			{


				Debug.WriteLine ( sd.Name );


				int img_index = 0;
				foreach ( ImageData id in sd.BD_ImgDt.GetEnumerable() )
				{
					string s0 = sqc_index.ToString ("000") + "_";
					string s1 = sd.Name + "_" ;
					string s2 = img_index.ToString ("00") + ".png";
					string filepath = dirname + "\\" + s0 + s1 + s2;
					id.Img.Save ( filepath, ImageFormat.Png );
					++ img_index;


					Debug.WriteLine ( filepath );


				}
				++ sqc_index;
			}
		}
	}
}
