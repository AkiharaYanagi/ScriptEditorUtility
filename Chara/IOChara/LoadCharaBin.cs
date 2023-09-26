using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Drawing;


namespace ScriptEditor
{
	using BD_Img = BindingDictionary < ImageData >;


	public partial class LoadCharaBin
	{
		//-------------------------------------------------------------
		//	コンストラクタ
		//-------------------------------------------------------------
		public LoadCharaBin ()
		{
		}

		//-------------------------------------------------------------
		//	実行
		//-------------------------------------------------------------
		public void Do ( string filepath, Chara chara )
		{
			try
			{
				_Load ( filepath, chara );
			}
			catch ( ArgumentException e )
			{
				//仮データ
				TestChara testChara = new TestChara ();
				testChara.Test ( chara );

				MessageBox.Show ( "LoadChara : 読込データが不適正です\n" + e.Message + "\n" + e.StackTrace );
			}
		}


		//対象ファイルを読み込みキャラデータをバイナリから変換
		private void _Load ( string filepath, Chara chara )
		{
			//ファイルが存在しないとき何もしない
			if ( ! File.Exists ( filepath ) )
			{
				MessageBox.Show ( filepath + "が見つかりません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error );
				throw new ArgumentException ( "ファイルが存在しませんでした。" );
			}

			//拡張子確認
			if ( Path.GetExtension ( filepath ).CompareTo ( ".dat" ) != 0 )
			{
				MessageBox.Show ( filepath + "は拡張子が.datと異なります。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error );
				throw new ArgumentException ( "拡張子が.datと異なります。" );
			}

			//初期化
			chara.Clear ();

			//ファイルストリーム開始
			using ( var fstrm = new FileStream ( filepath, FileMode.Open, FileAccess.Read ) )
			using ( var br = new BinaryReader ( fstrm, Encoding.UTF8 ) )
			{
				Debug.WriteLine ( "fstrm.Length = " + fstrm.Length );

				//バージョン(uint)
				uint ver = br.ReadUInt32 ();

				//サイズ(uint)
				uint size = br.ReadUInt32 ();

				//キャラデータ
				LoadBinBehavior ( br, chara );
				LoadBinGarnish ( br, chara );
				LoadBinCommand ( br, chara );
				LoadBinBranch ( br, chara );
				LoadBinRoute ( br, chara );

				//ビヘイビア
				LoadImage ( br, chara.behavior.BD_Image );		   
				//ガーニッシュ
				LoadImage ( br, chara.garnish.BD_Image );		   
			}
		}

		public void LoadImage ( BinaryReader br, BD_Img bd_img )
		{
			//イメージ個数
			uint n = br.ReadUInt32 ();

			for ( uint ui = 0; ui < n; ++ ui )
			{
				//サイズ( uint -> int ) ( br.ReadBytes(size) のためint )
				int size = (int)br.ReadUInt32 ();
				//一時領域
				byte[] buffer = new byte [ size ];

				//読込
				buffer = br.ReadBytes ( size );
				MemoryStream ms = new MemoryStream ( buffer );
				Image img = Image.FromStream ( ms );

				ImageData imgdt = new ImageData ()
				{
					Img = img,
					Name = img.ToString(),
				};

				bd_img.Add ( imgdt );
			}
		}
	}
}
