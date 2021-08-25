using System.ComponentModel;
using System.Windows.Forms;

namespace ScriptEditorUtility
{
	public partial class EditListbox_0 :UserControl
	{
		public BindingList < object > BL_T { get; set; } = new BindingList < object > ();
		public ListBox GetListBox () { return listBox1; }
		public object GetObject () { return listBox1.SelectedItem; }

		public delegate object Make_Function ();
		public Make_Function Make { get; set; } = null;

		public EditListbox_0 ()
		{
			InitializeComponent ();
			listBox1.DataSource = BL_T;
		}

		public void ResetItems ()
		{
			for ( int i = 0; i < BL_T.Count; ++ i )
			{
				BL_T.ResetItem ( i );
			}
		}

		private void Btn_Add_Click ( object sender, System.EventArgs e )
		{
			BL_T.Add ( Make?.Invoke () );
			listBox1.DataSource = BL_T;
			BL_T.ResetItem ( BL_T.Count - 1 );
		}

		private void Btn_Del_Click ( object sender, System.EventArgs e )
		{
			if ( listBox1.Items.Count <= 0 ) { return; }

			BL_T.RemoveAt ( listBox1.SelectedIndex );
		}

		private void Btn_Up_Click ( object sender, System.EventArgs e )
		{
			//--------------------------------------------------------------------
			//動作条件　下記の条件時は何もしない
			if ( listBox1.Items.Count <= 1 ) { return; }			//対象個数が１以下
			if ( listBox1.SelectedItems.Count <= 0 ) { return; }	//選択されていない
			if ( listBox1.SelectedIndex <= 0 ) { return; }			//選択が先頭のとき
			//--------------------------------------------------------------------

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

		//イベント：リストボックス選択変更時
		public delegate void Event ();
		public Event SelectedIndexChanged { get; set; } = null;
		private void listBox1_SelectedIndexChanged ( object sender, System.EventArgs e )
		{
			SelectedIndexChanged?.Invoke ();
		}


	}
}
