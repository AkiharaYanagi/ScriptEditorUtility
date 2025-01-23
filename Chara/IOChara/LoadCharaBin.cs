using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Drawing;
using System.Collections.Generic;
using System.Drawing.Imaging;


namespace ScriptEditor
{
	using BD_Img = BindingDictionary < ImageData >;


	public partial class LoadCharaBin
	{
		//エラーメッセージ
		public string ErrMsg { get; set; } = "Err Msg.";

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
			STS_TXT.Trace ( "読込開始" );

			try
			{
				_Load ( filepath, chara );
			}
			catch ( ArgumentException e )
			{
#if false
				//仮データ
				TestChara testChara = new TestChara ();
				testChara.Test ( chara );
#endif
				//空データ
				chara = new Chara ();

				//MessageBox.Show ( "LoadChara : 読込データが不適正です\n" + e.Message + "\n" + e.StackTrace );
				ErrMsg = "LoadChara : 読込データが不適正です\n" + e.Message + "\n" + e.StackTrace ;
			}

			STS_TXT.Trace ( "◆◆ 読込完了" );

			ErrMsg = "Load OK.";
		}


		//対象ファイルを読み込みキャラデータをバイナリから変換
		private void _Load ( string filepath, Chara chara )
		{
			//ファイルが存在しないとき何もしない
			if ( ! File.Exists ( filepath ) )
			{
				//MessageBox.Show ( filepath + "が見つかりません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error );
				STS_TXT.Trace_Err ( filepath + "が見つかりません" );
				throw new ArgumentException ( "ファイルが存在しませんでした。" );
			}

			//拡張子確認
			if ( Path.GetExtension ( filepath ).CompareTo ( ".dat" ) != 0 )
			{
				//MessageBox.Show ( filepath + "は拡張子が.datと異なります。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error );
				throw new ArgumentException ( "拡張子が.datと異なります。" );
			}

			//初期化
			chara.Clear ();


			//ファイルストリーム開始
			using ( var fstrm = new FileStream ( filepath, FileMode.Open, FileAccess.Read ) )
			using ( var br = new BinaryReader ( fstrm, Encoding.UTF8 ) )
			{
//				Debug.WriteLine ( "fstrm.Length = " + fstrm.Length );

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
				LoadImage ( filepath, br, chara.behavior.BD_Image );		
				//ガーニッシュ
				LoadImage ( filepath, br, chara.garnish.BD_Image );
			}	//using
		
		}



		public void LoadImage ( string filepath, BinaryReader br, BD_Img bd_img )
		{
			//イメージ個数
			uint n = br.ReadUInt32 ();

			//ディレクトリ名
			string dir = Path.GetDirectoryName ( filepath );
			string chName_bin = Path.GetFileNameWithoutExtension ( filepath );
			int ln = chName_bin.Length;
			string chName = chName_bin.Substring ( 0, ln - 4 );
			string dir_img = dir + "\\" + chName + "_img";
			Directory.CreateDirectory ( dir_img );


			//各イメージ
			for ( uint ui = 0; ui < n; ++ ui )
			{
				//名前 [utf-8] ( byte 名前のサイズ, 実データ )
				string name = br.ReadString ();

				//サイズ( uint -> int ) ( br.ReadBytes(size) のためint )
				uint usize = br.ReadUInt32 ();
				//int size = (int)br.ReadUInt32 ();
				int size = (int)usize; 

				//一時領域
				byte[] buffer = new byte [ size ];

				//読込
				buffer = br.ReadBytes ( size );



				//==============================
				//◆ ver0.21 
				//Imageを外部ファイルに書出
				//必要時にディスクから読み出す
				//windowsディレクトリ "filename_img\\img000.png"
				//==============================

				using ( MemoryStream ms = new MemoryStream ( buffer ) )
				{

				//img変換
				string img_path = dir_img + "\\" + name;
				Image img = Image.FromStream ( ms );
				img.Save ( img_path, ImageFormat.Png );
//				img.Save ( name, ImageFormat.Png );

				//コンストラクタで引数からサムネイルを作成
				Image imgThum = new Bitmap ( img, 50, 50 );

#if false
				//仮■で埋め
				Image imgBmp = new Bitmap ( 10, 10 );
				Graphics gBmp = Graphics.FromImage ( imgBmp );
				gBmp.FillRectangle ( Brushes.Yellow, new Rectangle ( 0, 0, imgBmp.Width, imgBmp.Height ) );
				gBmp.Dispose ();
#endif

				//イメージデータ作成
//				ImageData imgdt = new ImageData ( name, imgBmp );
				ImageData imgdt = new ImageData ( name );
				imgdt.Thumbnail = (Bitmap)imgThum;
				imgdt.Path = img_path;
				bd_img.Add ( imgdt );

				}	//using




#if false
				using (MemoryStream ms = new MemoryStream ( buffer ))
				{
				Image img = Image.FromStream ( ms );


					//イメージデータ作成
					//コンストラクタで引数からサムネイルを作成
					ImageData imgdt = new ImageData ( name, img );
				bd_img.Add ( imgdt );
				
				}	//using
#endif
			}
		}

		//イメージ名の再指定
		public void Respecift_ImageName ( Chara chara )
		{
			BD_Img bdImgBhv = chara.behavior.BD_Image;
			BD_Img bdImgGns = chara.garnish.BD_Image;

			//アクションにおけるイメージ名の再指定
			foreach ( Action act in chara.behavior.BD_Sequence.GetEnumerable () )
			{
				foreach ( Script scp in act.ListScript )
				{
					//メインイメージ名
					int id = GetIndex ( scp.ImgName, "Img_" );
					scp.ImgName = bdImgBhv [ id ].Name;
				}
			}

			//エフェクトにおけるイメージ名の再指定
			foreach ( Effect efc in chara.garnish.BD_Sequence.GetEnumerable () )
			{
				foreach ( Script scp in efc.ListScript )
				{
					//エフェクトイメージ名
					int id = GetIndex ( scp.ImgName, "Img_" );
					scp.ImgName = bdImgGns [ id ].Name;
				}
			}

		}
	}
}
