using System;
using System.IO;
using System.Xml.Serialization;

namespace ScriptEditor
{
	public static class XML_IO
	{
		public static string SettingFilename { set; get; } = "setting.xml";	//保存ファイル名
		
		//保存
		public static void Save ( object ob )
		{
			//クラスをXMLファイルに保存
			XmlSerializer serializer = new XmlSerializer ( ob.GetType () );

			//書出
			using ( FileStream fs = new FileStream ( SettingFilename, FileMode.Create ) )
			{
				serializer.Serialize ( fs, ob );
			}
		}

		//読込
		public static object Load ( Type T )
		{
			//ファイルの存在しないとき
			if ( ! File.Exists ( SettingFilename ) ) { return Activator.CreateInstance ( T ); }

			//XMLファイルの設定
			XmlSerializer serializer = new XmlSerializer ( T );

			//読込
			using ( FileStream fs = new FileStream ( SettingFilename, FileMode.Open ) )
			{
				return serializer.Deserialize ( fs );
			}
		}
	}
}
