using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ScriptEditor
{
	//==========================================================
	//	BindingDictionaryを受けて表示と編集をするコントロール
	//==========================================================
	public partial class EditListbox < T >  : UserControl where T : IName, new ()
	{
		//対象
		public BindingDictionary < T > BD_T { get; set; } = new BindingDictionary < T > ();

		public ListBox GetListBox () { return listBox1; }
		public T Get () { return ( T ) listBox1.SelectedItem; }

		public EditListbox ()
		{
			InitializeComponent ();
			listBox1.DataSource = BD_T.GetBindingList ();
			listBox1.DisplayMember = "Name";

			//イベント
			Btn_Add.Click += new EventHandler ( listBox1_Add );
		}

		public void ResetItems ()
		{
			for ( int i = 0; i < BD_T.Count (); ++ i )
			{
				BD_T.GetBindingList ().ResetItem ( i );
			}
		}

		public void SetData ( BindingDictionary < T > bd_t )
		{
			BD_T = bd_t;
			listBox1.DataSource = bd_t.GetBindingList ();
			
			int i = listBox1.SelectedIndex;
			if ( i > 0 )
			{
				Tb_Name.Text = BD_T.Get ( i ).Name;
			}
		}

		private void Btn_Add_Click ( object sender, System.EventArgs e )
		{
			BD_T.Add ( new T () );
			//末尾を選択
			listBox1.SelectedIndex = listBox1.Items.Count - 1;
			ResetItems ();
		}

		private void Btn_Del_Click ( object sender, System.EventArgs e )
		{
			if ( listBox1.Items.Count <= 0 ) { return; }

			BD_T.RemoveAt ( listBox1.SelectedIndex );
		}

		//上へ移動
		private void Btn_Up_Click ( object sender, System.EventArgs e )
		{
			//--------------------------------------------------------------------
			//動作条件　下記の条件時は何もしない
			if ( listBox1.Items.Count <= 1 ) { return; }			//対象個数が１以下
			if ( listBox1.SelectedItems.Count <= 0 ) { return; }	//選択されていない
			if ( listBox1.SelectedIndex <= 0 ) { return; }			//選択が先頭のとき
			//--------------------------------------------------------------------

			BindingList < T > BL_T = BD_T.GetBindingList ();

			//１つ前の位置
			int i = listBox1.SelectedIndex - 1;
			//前に追加
			BL_T.Insert ( i, BL_T [ listBox1.SelectedIndex ] );
			//後を削除
			BL_T.RemoveAt ( i + 2 );
			//更新
			BL_T.ResetItem ( BL_T.Count );
			//選択を１つ前へ
			listBox1.SelectedIndex = i;
		}

		private void Btn_Down_Click ( object sender, System.EventArgs e )
		{
			//--------------------------------------------------------------------
			//動作条件　下記の条件時は何もしない
			if ( listBox1.Items.Count <= 1 ) { return; }		//対象個数が１以下
			int n = listBox1.SelectedItems.Count;
			if ( n <= 0 ) { return; }	//選択されていない
			if ( listBox1.SelectedIndex >= listBox1.Items.Count - 1) { return; }	//選択が末尾のとき
			//--------------------------------------------------------------------

			BindingList < T > BL_T = BD_T.GetBindingList ();

			//１つ次の位置
			int i = listBox1.SelectedIndex + 2;
			//次に追加
			BL_T.Insert ( i, BL_T [ listBox1.SelectedIndex ] );
			//前を削除
			BL_T.RemoveAt ( i - 2 );
			//更新
			BL_T.ResetItem ( i );
			//選択を１つ次へ
			listBox1.SelectedIndex = i - 1;
		}

		//============================================================
		//イベント
		public delegate void Event ();

		//イベント：リストボックス選択変更時
		public Event SelectedIndexChanged { get; set; } = null;
		private void listBox1_SelectedIndexChanged ( object sender, System.EventArgs e )
		{
			if  ( listBox1.SelectedItem is null ) { return; }
			Tb_Name.Text = ((T)listBox1.SelectedItem).Name;
			SelectedIndexChanged?.Invoke ();
		}

		//イベント：追加時
		public Event Add { get; set; } = null;
		private void listBox1_Add ( object sender, System.EventArgs e )
		{
			Add?.Invoke ();
		}

		//イベント：名前の変更
		public Event _TextChanged { get; set; } = null;
		public string GetName ()
		{
			return Tb_Name.Text;
		}
		private void Tb_Name_TextChanged ( object sender, EventArgs e )
		{
			_TextChanged?.Invoke ();
		}

		//イベント：キー押下時
		public Event Tb_KeyPress { get; set; } = null;
		private void Tb_Name_KeyPress ( object sender, KeyPressEventArgs e )
		{
			if ( e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Escape )
			{
				e.Handled = true;
			}
			if ( e.KeyChar == (char)Keys.Enter )
			{
				T t = (T)listBox1.SelectedItem;
				t.Name = Tb_Name.Text;
				ResetItems ();

				Tb_KeyPress?.Invoke ();
			}
		}
	}

}
