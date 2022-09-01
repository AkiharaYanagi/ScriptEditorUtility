using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;


namespace ScriptEditor
{
	public class AppSettingsData
	{
		//フルスクリーン / ウィンドウ
		public bool FullScreen { get; set; } = true;

		//ウィンドウサイズ
		public Resolution Resolution { get; set; } = new Resolution ( (int)RESOLUTION.R0_W, (int)RESOLUTION.R0_H );

		//スタート位置
		public enum START_POS { Cursor, Display };
		public START_POS Start_pos { get; set; } = START_POS.Cursor;

		//ディスプレイ番号
		public int DisplayNum { get; set; } = 0;

		//コンストラクタ
		public AppSettingsData ()
		{
		}

		public void Init ()
		{
			FullScreen = true;
			Resolution = new Resolution ( 1280, 960 );
			Start_pos = START_POS.Cursor;
			DisplayNum = 0;
		}

		//書出
		public void Save ( string filename )
		{
			using ( var bw = new BinaryWriter ( new FileStream ( filename, FileMode.Create, FileAccess.Write )) )
			{
				bw.Write ( FullScreen );
				bw.Write ( Resolution.W );
				bw.Write ( Resolution.H );
				bw.Write ( (int)Start_pos );
				bw.Write ( DisplayNum );
			}
		}

		//読込
		public void Load ( string filename )
		{
			try { _Load ( filename ); }
			catch ( Exception e )
			{
				Debug.WriteLine ( e.ToString () );

				Init ();
				Save ( filename );
			}
		}

		private void _Load ( string filename )
		{
			//ファイルチェック
			if ( File.Exists ( filename ) ) //存在するとき読込
			{
				using ( var br = new BinaryReader ( new FileStream ( filename, FileMode.Open, FileAccess.ReadWrite ) ) )
				{
					FullScreen = br.ReadBoolean ();
					Resolution.W = br.ReadInt32 ();
					Resolution.H = br.ReadInt32 ();
					Start_pos = (START_POS)br.ReadInt32 ();
					DisplayNum = br.ReadInt32 ();
				}
			}
			else
			{
				//ファイル作成
				Save ( filename );
			}
		}
	}

	//--------------------------------------------------------
	//	設定コントロール
	//--------------------------------------------------------
	public partial class AppSettings : UserControl
	{
		public const string FileName = "AppSettings.dat" ;
	
		AppSettingsData AppSttg = new AppSettingsData ();
		private	ToolTip tltip;
		private BindingList < Resolution > BL_Resol = new BindingList<Resolution> ();

		public AppSettings ()
		{
			InitializeComponent ();

			tltip = new ToolTip ();
			BL_Resol.Add ( new Resolution ( (int)RESOLUTION.R0_W, (int)RESOLUTION.R0_H ) );
			BL_Resol.Add ( new Resolution ( (int)RESOLUTION.R1_W, (int)RESOLUTION.R1_H ) );
			BL_Resol.Add ( new Resolution ( (int)RESOLUTION.R2_W, (int)RESOLUTION.R2_H ) );
			Cb_WndSz.DataSource = BL_Resol;
		}

		//対象データの設定
		public void SetData ( AppSettingsData appsttg )
		{
			//対象データ
			AppSttg = appsttg;

			//画面サイズ
			Resolution rslt = new Resolution ( appsttg.Resolution.W, appsttg.Resolution.H );
			int index = BL_Resol.IndexOf ( rslt );
			if ( -1 == index ) { Cb_WndSz.SelectedIndex = 0; }
			else { Cb_WndSz.SelectedIndex = index; }

			//ウィンドウ / フルスクリーン
			Rb_Wnd.Checked = ! appsttg.FullScreen;
			Rb_Flscr.Checked = appsttg.FullScreen;

			//スタート位置
			Rb_Cursor.Checked = ( AppSettingsData.START_POS.Cursor == appsttg.Start_pos );
			Rb_Display.Checked = ! Rb_Cursor.Checked;
//			Cb_DisplayN.Enabled = false;

			//ディスプレイの列挙
			foreach ( Screen scrn in Screen.AllScreens )
			{
				Cb_DisplayN.Items.Add ( scrn.DeviceName );
			}
			Cb_DisplayN.SelectedIndex = 0;
			tltip.SetToolTip ( Cb_DisplayN, Cb_DisplayN.SelectedItem.ToString () );
		}


		//ウィンドウサイズ設定時
		private void Cb_WndSz_SelectedIndexChanged ( object sender, EventArgs e )
		{
			AppSttg.Resolution = (Resolution) Cb_WndSz.SelectedItem;
		}


		//ディスプレイ番号設定時
		private void Cb_DisplayN_SelectedIndexChanged ( object sender, EventArgs e )
		{
			tltip.SetToolTip ( Cb_DisplayN, Cb_DisplayN.SelectedItem.ToString () );
		}

		//フォルダ
		private void Btn_Folder_Click ( object sender, EventArgs e )
		{
			FormUtility.OpenCurrentDir ();
		}

		//保存して終了
		private void Btn_Save_Click ( object sender, EventArgs e )
		{
			AppSttg.Save ( FileName );
			Application.Exit ();
		}

		//キャンセルして終了
		private void Btn_cancel_Click ( object sender, EventArgs e )
		{
			Application.Exit ();
		}

		//-----------------------------------------------------------------------
		private void Rb_Wnd_CheckedChanged ( object sender, EventArgs e )
		{
			AppSttg.FullScreen = Rb_Flscr.Checked;
		}

		private void Rb_Flscr_CheckedChanged ( object sender, EventArgs e )
		{
			AppSttg.FullScreen = Rb_Flscr.Checked;
		}

		private void Rb_Cursor_CheckedChanged ( object sender, EventArgs e )
		{
			if ( Rb_Cursor.Checked )
			{
				AppSttg.Start_pos = AppSettingsData.START_POS.Cursor;
			}
			else
			{
				AppSttg.Start_pos = AppSettingsData.START_POS.Display;
			}
		}

		private void groupBox2_Enter ( object sender, EventArgs e )
		{
			if ( Rb_Cursor.Checked )
			{
				AppSttg.Start_pos = AppSettingsData.START_POS.Cursor;
			}
			else
			{
				AppSttg.Start_pos = AppSettingsData.START_POS.Display;
			}
		}
	}
}
