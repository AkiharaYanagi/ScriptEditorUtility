using System.Drawing;

namespace ScriptEditor
{
	//イメージと名前を持つ
	// INameを継承し、BindingDictionaryで扱う
	public class ImageData : IName
	{
		public const int THUM_W = 50;
		public const int THUM_H = 50;

		//イメージ
		public Image Img { set; get; }

		//表示用サムネイル
		public Bitmap Thumbnail { set; get; } = new Bitmap ( THUM_W, THUM_H );

		//名前
		public string Name { set; get; }

		public ImageData ()
		{
			Graphics g = Graphics.FromImage ( Thumbnail );
			g.FillRectangle ( Brushes.Red, g.VisibleClipBounds );
			g.Dispose ();
		}

		//引数付コンストラクタ
		public ImageData ( string name, Image img )
		{
			Name = name;
			Img = img;

			MakeThumbnail ( img );
		}
		public ImageData ( ImageData imageData )
		{
			this.Img = imageData.Img;
			this.Name = imageData.Name;

			MakeThumbnail ( this.Img );
		}

		//文字列変換
		public override string ToString ()
		{
			return Name;
		}

		public void MakeThumbnail ( Image img )
		{
			if ( img is null ) { return; }

			Graphics g = Graphics.FromImage ( Thumbnail );
			g.DrawImage ( img, new Rectangle ( 0, 0 , THUM_W, THUM_H ) );
			g.Dispose ();
		}
	}
	

}
