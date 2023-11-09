﻿using System;
using System.Windows.Forms;
using System.IO;


namespace ScriptEditor
{
	using ELB_Sqc = EditListbox < SequenceData >;

	public partial class Ctrl_SqcList : UserControl
	{
		//表示編集用コントロール
		private Ctrl_ImageTable ctrl_ImageTable1 = new Ctrl_ImageTable ();

		//コントロール実体
		private ELB_Sqc ELB_Sqc = new ELB_Sqc ();
		

		//起動時１回のみの初期化 コンストラクタ内に移行
		private void LoadCtrl ()
		{
			//----------------------------------------------------
			//コントロール追加

			//イメージ
			panel1.Controls.Add ( ctrl_ImageTable1 );
			ctrl_ImageTable1.Location = new System.Drawing.Point ( 0, 0 );
			ctrl_ImageTable1.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;

			//データ編集
			EditData.Dt = Data;
			EditData.UpdateAll = UpdateAll;

			//データの設定
			ctrl_ImageTable1.LoadCtrl ( ELB_Sqc );

			//エディットリストボックス
			ELB_Sqc.Location = new System.Drawing.Point(0, 27);
			ELB_Sqc.GetListBox().DisplayMember = "Name";
			ELB_Sqc.SetData(Data.L_Sqc);
			this.Controls.Add(ELB_Sqc);

			//エディットリストボックス
			//イベントの設定
			ELB_Sqc.SelectedIndexChanged = () =>
			{
				EditData.SelectedSqc = ELB_Sqc.GetListBox().SelectedIndex;
				ctrl_ImageTable1.ScrollPos ();
			};
			ELB_Sqc.Listbox_Changed = () => UpdateCtrl();
			ELB_Sqc.Listbox_Add = () => UpdateCtrl();
			ELB_Sqc._TextChanged = ()=>
			{
				//シークエンスデータはBDから直接の名前だけでなく、保持しているSqcの更新も必要
				SequenceData sqcd = ELB_Sqc.Get();
				string oldName = sqcd.Sqc.Name;
				sqcd.SetName ( ELB_Sqc.GetName () );

				//@info 名前データの変更は元のCompend.BD_Seqcuenceも更新しないと反映できない
				// ->名前で検索するため
				Cmpd.BD_Sequence.ChangeName ( oldName, sqcd.Name );
			};

			//アクション・エフェクト分岐
			switch ( flag_sqc_derived )
			{
			case CTRL_SQC.ACTION: 
				ELB_Sqc.Func_SavePath = s =>
				{
					Ctrl_Stgs.File_ActionList = s;
					XML_IO.Save ( Ctrl_Stgs );
				};
				New_Object = ()=>new Action();
				break;
			case CTRL_SQC.EFFECT: 
				ELB_Sqc.Func_SavePath = s =>
				{
					Ctrl_Stgs.File_EffectList = s;
					XML_IO.Save ( Ctrl_Stgs );
				};
				New_Object = ()=>new Effect();
				break;
			default: break;
			}
			//IO
			ELB_Sqc.SetIOFunc ( SaveSqcDt, LoadSqcDt );
		}

		//引き渡し用 書出関数
		public void SaveSqcDt ( object ob, StreamWriter sw )
		{
			SequenceData sqcDt = (SequenceData)ob;
			sw.Write ( sqcDt.Name + "," );
			sw.Write ( sqcDt.nScript.ToString () + "," );

			//アクションのみ
			if ( CTRL_SQC.ACTION == flag_sqc_derived )
			{
				Action act = (Action)sqcDt.Sqc;
				sw.Write ( act.Category.ToString () + "," ); 
				sw.Write ( act.NextActionName + "," ); 
			}
		}



		//アクションリストのデータ構造定義
		enum _LoadSqcData
		{
			NAME,	//名前
			N_SCP,	//スクリプト個数
			CTG,	//カテゴリ
			NEXT,	//次アクション名
		};

		//引き渡し用 読込関数
		public void LoadSqcDt ( StreamReader sr )
		{
			SequenceData sqcDt = new SequenceData (){ Sqc = New_Object() };

			//アクションリストのテキストデータをカンマ分割
			string[] str_spl = sr.ReadLine ().Split ( ',' );
			sqcDt.Name = str_spl [ (int)_LoadSqcData.NAME ];
			sqcDt.Sqc.Name = str_spl[ (int)_LoadSqcData.NAME ];
			sqcDt.nScript = int.Parse ( str_spl[ (int)_LoadSqcData.N_SCP ] );
			for ( int i = 0; i < sqcDt.nScript; ++ i )
			{
				sqcDt.Sqc.ListScript.Add ( new Script () );
			}

			//アクションのみ
			if ( CTRL_SQC.ACTION == flag_sqc_derived )
			{
				ScriptEditor.Action act = (ScriptEditor.Action)sqcDt.Sqc;
				
				Type t = typeof ( ActionCategory );
				string[] names = Enum.GetNames ( typeof ( ActionCategory ) );
				int i2 = (int)_LoadSqcData.CTG;
				act.Category = (ActionCategory)Enum.Parse ( t, str_spl [ i2 ] );
				act.NextActionName = str_spl[ (int)_LoadSqcData.NEXT ];
			}

			ELB_Sqc.Add ( sqcDt );
		}
	}
}
