using System;
using System.Windows.Forms;
using System.ComponentModel;


namespace ScriptEditor
{
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
