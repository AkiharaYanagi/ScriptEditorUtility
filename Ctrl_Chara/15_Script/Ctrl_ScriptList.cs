using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;


namespace ScriptEditor
{
	using L_Scp = List < Script >;


	public partial class Ctrl_ScriptList : UserControl
	{
		//編集
		EditCompend EditCompend = new EditCompend ();

		public Ctrl_ScriptList ()
		{
			InitializeComponent ();

			//ボタン初期 不可
			Btn_Replace.Enabled = false;
			Btn_Add.Enabled = false;
			Btn_Ins.Enabled = false;
		}

		public void SetEditCompend ( EditCompend ec )
		{
			EditCompend = ec;
		}

		//============================================

		//書出
		private void Btn_Save_Click ( object sender, EventArgs e )
		{
			using ( SaveFileDialog sfd = new SaveFileDialog () )
			{
				sfd.DefaultExt = "txt";
				sfd.InitialDirectory = Directory.GetCurrentDirectory ();
				sfd.FileName = "ScriptList.txt";

				if ( sfd.ShowDialog () == DialogResult.OK )
				{
					using ( StreamWriter sw = new StreamWriter ( sfd.FileName, false, Encoding.UTF8 ) )
					{
						Chara ch = EditChara.Inst.Chara;
						CharaToDoc ctod = new CharaToDoc ();
						ctod.WriteListScript ( ch, sw, EditCompend.Compend, EditCompend.SelectedSequence.ListScript ) ;
					}
				}
			}
		}

		//読込
		private void Btn_Load_Click ( object sender, EventArgs e )
		{
			using ( OpenFileDialog ofd = new OpenFileDialog () )
			{
				ofd.DefaultExt = "txt";
				ofd.InitialDirectory = Directory.GetCurrentDirectory ();
				ofd.FileName = "ScriptList.txt";

				if ( ofd.ShowDialog () == DialogResult.OK )
				{
					using ( StreamReader sr = new StreamReader ( ofd.FileName, Encoding.UTF8 ) )
					{
						Document doc = new Document ( sr.BaseStream );
						DocToChara dtoc = new DocToChara ();
						Sequence sqc = new Sequence ();
						dtoc.ReadScriptList ( sqc, doc.Root.Elements );

						//選択
						string name = EditCompend.SelectedSequence.Name;
						EditCompend.SelectedSequence.CopyScpList ( sqc );
						EditCompend.SelectSequence ( name );
						
						//表示の更新
						All_Ctrl.Inst.UpdateData ();
					}
				}
			}
		}

		//============================================

		//コピーしたリスト
		private L_Scp L_Script = new L_Scp ();


		private void Btn_Copy_Click ( object sender, EventArgs e )
		{
			L_Scp ls = EditCompend.SelectedSequence.ListScript;
			L_Script = new L_Scp ();

			int start = EditCompend.SelectedSpanStart;
			int end = EditCompend.SelectedSpanEnd;
			int n = end + 1;

			if ( ls.Count < end ) { end = ls.Count; }	//範囲内に丸め

			for ( int i = start; i < n; ++ i )
			{
				L_Script.Add ( new Script ( ls [ i ] ) );
			}

			string name = EditCompend.SelectedSequence.Name;
			textBox1.Text = name + "[" + start + "]...[" + end + "]";

			Btn_Replace.Enabled = true;
			Btn_Add.Enabled = true;
			Btn_Ins.Enabled = true;
		}

		//全部コピー
		private void Btn_AllCpy_Click ( object sender, EventArgs e )
		{
			L_Scp ls = EditCompend.SelectedSequence.ListScript;
			L_Script = new L_Scp ();
			foreach ( Script scp in ls )
			{
				L_Script.Add ( new Script ( scp ) );
			}

			string name = EditCompend.SelectedSequence.Name;
			textBox1.Text = name + "[0]...[" + ls.Count + "]";

			Btn_Replace.Enabled = true;
			Btn_Add.Enabled = true;
			Btn_Ins.Enabled = true;
		}

		//------------------------------------------------------------
		//置換 リプレイス
		private void Btn_Replace_Click ( object sender, EventArgs e )
		{
			//コピーが無いとき何もしない
			if ( L_Script.Count == 0 ) { return; }

			//すべてクリアして追加
			L_Scp ls = EditCompend.SelectedSequence.ListScript;
			ls.Clear ();
			foreach ( Script scp in L_Script )
			{
				ls.Add ( new Script ( scp ) );
			}

			//選択
			EditCompend.SelectSequence ( EditCompend.SelectedSequence.Name );
			
			//表示更新
			All_Ctrl.Inst.UpdateData ();
		}

		private void Btn_Add_Click ( object sender, EventArgs e )
		{
			EditCompend.MultiAdd ( L_Script );
			
			//表示更新
			All_Ctrl.Inst.UpdateData ();
		}

		private void Btn_Ins_Click ( object sender, EventArgs e )
		{
			EditCompend.MultiInsert ( L_Script );
			
			//表示更新
			All_Ctrl.Inst.UpdateData ();
		}

	}
}
