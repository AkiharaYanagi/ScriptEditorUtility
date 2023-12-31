﻿using System;

//-------------------------------------------------------
// WindowsAPICodePackを利用したフォルダダイアログ
//-------------------------------------------------------
using Microsoft.WindowsAPICodePack.Dialogs;


namespace ScriptEditor
{
	public class OpenFolder_CodePack
	{
		private CommonOpenFileDialog diag = new CommonOpenFileDialog ();

		public OpenFolder_CodePack ()
		{
			diag.IsFolderPicker = true;
		}

		public void SetInitDir ( string path )
		{
			diag.InitialDirectory = path;
		}

		public void SetDefaultFilename ( string filename )
		{
			diag.DefaultFileName = filename;
		}

		public bool OpenFolder ()
		{
			return diag.ShowDialog () == CommonFileDialogResult.Ok;
		}

		public string GetPath ()
		{
			return diag.FileName;
		}
#if false
#endif
	}
}
