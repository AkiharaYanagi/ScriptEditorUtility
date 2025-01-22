using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Drawing;
using System.Collections.Generic;


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
				LoadImage ( br, chara.behavior.BD_Image );		   
				//ガーニッシュ
				LoadImage ( br, chara.garnish.BD_Image );
				
				//スクリプトにおけるイメージ名の再設定
				//Respecift_ImageName ( chara );
			}
		}

		public void LoadImage ( BinaryReader br, BD_Img bd_img )
		{
			//イメージ個数
			uint n = br.ReadUInt32 ();

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
				using (MemoryStream ms = new MemoryStream ( buffer ))
				{
					
				Image img = Image.FromStream ( ms );

				//イメージデータ作成
				//コンストラクタで引数からサムネイルを作成
				ImageData imgdt = new ImageData ( name, img );
				bd_img.Add ( imgdt );
				
				}	//using
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
