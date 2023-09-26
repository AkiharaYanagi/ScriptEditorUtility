using System.Drawing;

namespace ScriptEditor
{
	//イメージと名前を持つ
	// INameを継承し、BindingDictionaryで扱う
	public class ImageData : IName
	{
		//イメージ
		public Image Img { set; get; }

		//名前
		public string Name { set; get; }

		public ImageData ()
		{
		}

		//引数付コンストラクタ
		public ImageData ( string name, Image img )
		{
			Name = name;
			Img = img;
		}
		public ImageData ( ImageData imageData )
		{
			this.Img = imageData.Img;
			this.Name = imageData.Name;
		}

		//文字列変換
		public override string ToString ()
		{
			return Name;
		}
	}
	

}
