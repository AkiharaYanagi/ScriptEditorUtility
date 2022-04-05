﻿using System;
using System.Windows.Forms;
using System.Drawing;

namespace ScriptEditor
{
	//アクションのみフォームで指定
	public partial class Form_Action : Form
	{
		//全体更新
		public System.Action UpdateAll { get; set; } = null;

		//コンストラクタ
		public Form_Action()
		{
			InitializeComponent();

			//コンボボックスの初期化
			foreach ( string name in Enum.GetNames ( typeof ( ActionCategory ) ) )
			{
				comboBox1.Items.Add ( name );
			}
			comboBox1.SelectedIndex = 0;
			comboBox1.Size = new Size ( 220, 40 );
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			e.Cancel = true;
			UpdateAll?.Invoke ();
			this.Hide ();
		}

		public void Assosiate ( SequenceData sqcDt )
		{
			tB_Setter1.Assosiate ( s=>sqcDt.SetName(s), ()=>{return sqcDt.Sqc.Name;} );
			tB_Number1.Assosiate ( i=>sqcDt.nScript = i, ()=>{ return sqcDt.nScript; } );

			Action act = (Action)sqcDt.Sqc;
			SetCategory = ( s ) =>
			{
				act.Category = (ActionCategory)Enum.Parse ( typeof (ActionCategory), s ); 
			};
			comboBox1.SelectedItem = act.Category.ToString();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			this.Close ();
		}

		//カテゴリ選択
		public System.Action < string > SetCategory = (s)=>{};
		private void comboBox1_SelectionChangeCommitted ( object sender, EventArgs e )
		{
			SetCategory ( (string)comboBox1.SelectedItem );
		}
	}
}
