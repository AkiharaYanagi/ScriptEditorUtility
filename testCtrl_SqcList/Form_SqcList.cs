﻿using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;

namespace ScriptEditor
{
	//========================================
	// testCtrl_SqcList
	//========================================
	public partial class Form1 : Form
	{
		//設定ファイル
		private Ctrl_Settings ctrl_stg = new Ctrl_Settings ();

		public Form1 ()
		{
			//設定ファイル
			ctrl_stg = ( Ctrl_Settings )XML_IO.Load ( typeof ( Ctrl_Settings ) );

			//フォーム初期化
			FormUtility.InitPosition ( this );
			InitializeComponent ();
			this.Text = "test_Ctrl_SqcList";

			//コントロール初期化
			Ctrl_SqcList ctrl_SqcList1 = new Ctrl_SqcList ();
			//配置
			this.Controls.Add ( ctrl_SqcList1 );

			//データ
			Chara chara = new Chara ();
			EditChara.Inst.SetCharaDara ( chara );

			ctrl_SqcList1.SetCompend ( chara.behavior );

			//アクション・エフェクト切替テスト
#if true
			Ctrl_SqcList.CTRL_SQC ac = Ctrl_SqcList.CTRL_SQC.ACTION;
			ctrl_SqcList1.SetEnviroment ( ac, ctrl_stg );
#else
			Ctrl_SqcList.CTRL_SQC ef = Ctrl_SqcList.CTRL_SQC.EFFECT;
			ctrl_SqcList1.SetEnviroment ( ef, ctrl_stg );
#endif

			//読込
			ctrl_SqcList1.SetCharaData ( chara );
		}
	}
}
