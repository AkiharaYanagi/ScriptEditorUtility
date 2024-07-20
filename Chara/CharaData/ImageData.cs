using System.Drawing;

namespace ScriptEditor
{
	//イメージと名前を持つ
	// INameを継承し、BindingDictionaryで扱う
	public class ImageData : IName
	{
		//イメージ
		public Image Img { set; get; }

		//表示用サムネイル
		public Bitmap Thumbnail { set; get; } = new Bitmap ( 100, 100 );

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
			g.DrawImage ( img, new Rectangle ( 0, 0 , 100, 100 ) );
			g.Dispose ();
		}
	}
	

}
