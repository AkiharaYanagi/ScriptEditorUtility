using System;
using System.IO;
using System.Media;


namespace ScriptEditor
{
	//サウンド１つを表すクラス
	public class Sound : TName
	{
		private SoundPlayer player;

		public void Load ()
		{
			string crtDir = Directory.GetCurrentDirectory ();
			player = new SoundPlayer ( "01_Tomoe.wav" );
			player.Play ();
		}
	}
}
