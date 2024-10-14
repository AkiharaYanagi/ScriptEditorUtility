using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ScriptEditor
{
	public static class ImageName
	{
		//ファイルネーム
		// "[通し番号000]_[シークエンス名]_[シークエンス内番号00].png"
		//通し番号
		public static string GetID ( string imgFileName )
		{
			int size = imgFileName.Length;
			string retStr = imgFileName.Substring ( 0, 4 );
			return retStr;
		}

		//ファイルネームから "シークエンス名" 切出
		public static  string GetSeqName ( string imgFileName )
		{
			int size = imgFileName.Length;
			return imgFileName.Substring ( 4, size - 11 );
		}

		//ファイルネームから "_[シークエンス内番号00].png"
		public static  string GetID_inSeq ( string imgFileName )
		{
			int size = imgFileName.Length;
			return imgFileName.Substring ( size - 7, 7 );
		}


		//イメージネーム
		//"[シークエンス名]_[シークエンス内番号00].png"
		//Compendに返すとき[通し番号000]_は外す
		public static  string GetImageName_NoID ( string imgFileName )
		{
			int size = imgFileName.Length;
			return imgFileName.Substring ( 4, size - 4 );
		}

		//イメージネームからシークエンス名を返す
		public static  string GetSequenceName ( string imgName )
		{
			int size = imgName.Length;
			return imgName.Substring ( 0, size - 7 );
		}
	}
}
