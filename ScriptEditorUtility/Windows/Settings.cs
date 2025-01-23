using System.IO;

namespace ScriptEditor
{
	//----------------------------------------------------------
	//	フォームの基本情報を保存するファイル
	//----------------------------------------------------------
	//	保存読込はXML_IOを用いる
	//	フルパスを保存
	public class Settings
	{
//		public string SettingFilepath { set; get; } = "setting.xml";	//自身のファイルパス

		public string LastDirectory { set; get; } = "";		//前回のディレクトリ
		public string LastFilepath { set; get; } = "";		//前回(今回の最新)のファイルパス

		public Settings ()
		{
			if ( LastDirectory == "" )
			{
				LastDirectory = System.Environment.CurrentDirectory;
			}

			if ( LastFilepath == "" )
			{
				LastFilepath = System.Environment.CurrentDirectory + "\\chara.dat";
			}
		}
	}

}
