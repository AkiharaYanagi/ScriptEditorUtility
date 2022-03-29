using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;


namespace ScriptEditor
{
	public static class FormUtility
	{
		//==============================================================
		//フォーム位置初期化
		private const int start_X = -600;	//フォーム位置補正定数
		private const int start_Y = -10;	//フォーム位置補正定数

		public static void InitPosition ( Form f )
		{
			//フォーム開始位置をマウス位置にする
			f.StartPosition = FormStartPosition.Manual;
			Point ptStart = Cursor.Position;
			ptStart.X += start_X;
			if ( ptStart.X < 0 ) { ptStart.X = 0; }
			ptStart.Y += start_Y;
			if ( ptStart.Y < 0 ) { ptStart.Y = 0; }
			f.Location = ptStart;
		}

		//==============================================================
		//現在ディレクトリをエクスプローラで開く
		public static void OpenCurrentDir ()
		{
			//"@"はコマンドラインに文字列が表示されないようにする構文
			// Explorer.exe . (ピリオドで現在ディレクトリ)
			Process.Start ( "Explorer.exe", @"." );
		}
		
		//指定パスをエクスプローラで開く
		public static void OpenDir ( string path )
		{
			Process.Start ( "Explorer.exe", @path );
		}

		//==============================================================
		//上のディレクトリ
		public static string UpDir ( string path )
		{
			return path.Substring ( 0, path.LastIndexOf ( @"\" ) + 1 );
		}

		//==============================================================
		//ステータスバー表示
		public static ToolStripStatusLabel ToolStripStatusLabel { get; set; } = null;

		public static void Trace ( string str )
		{
			ToolStripStatusLabel.Text = str;
		}

	}
}
