namespace ScriptEditor
{
	//----------------------------------------------------------
	//	フォームの基本情報を保存するファイル
	//----------------------------------------------------------
	//	保存はXML_IOを用いる
	public class Settings
	{
		public string SettingFilename { set; get; } = "setting.xml";	//保存ファイル名

		public string LastDirectory { set; get; } = "";					//前回のディレクトリ
		public string LastFilename { set; get; } = "chara.dat";         //前回のファイル
	}

}
