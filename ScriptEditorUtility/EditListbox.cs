using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;


namespace ScriptEditor
{
	using Func_Check = System.Func < object, bool > ;

	//==========================================================
	//	BindingDictionary < T >を受けて表示と編集をするコントロール
	//==========================================================
	public partial class EditListbox < T >  : UserControl where T : class, IName, new ()
	{
		//対象
		public BindingDictionary < T > BD_T { get; set; } = new BindingDictionary < T > ();

		//新規
		public Func < T > New_T = ()=>new T();

		//取得
		public ListBox GetListBox () { return listBox1; }
		public T Get () { return ( T ) listBox1.SelectedItem; }
		public BindingList < T > GetList () { return BD_T.GetBindingList (); }

		//色替条件
		public Func_Check Func_check { get; set; } = (ob)=>false;


		//コンストラクタ
		public EditListbox ()
		{
			InitializeComponent ();

			listBox1.DataSource = BD_T.GetBindingList ();
			listBox1.DisplayMember = "Name";
			listBox1.DrawMode = DrawMode.OwnerDrawFixed;
			listBox1.DrawItem += new DrawItemEventHandler ( ListBox1_DrawItem );

			//仮の１つ
			T t = New_T ();
			BD_T.Add ( t );
			listBox1.SelectedIndex = 0;
			Tb_Name.Text = t.GetType().Name;

			//イベント
			Btn_Add.Click += new EventHandler ( listBox1_Add );
			Btn_Del.Click += new EventHandler ( listBox1_Del );
			Tb_Name.TextChanged += new EventHandler ( Tb_Name_TextChanged );
		}

		//データ設置
		public void SetData ( BindingDictionary < T > bd_t )
		{
			BD_T = bd_t;
			listBox1.DataSource = BD_T.GetBindingList ();
			listBox1.DisplayMember = "Name";
			BD_T.ResetItems ();

			//変更時イベント
			Changed?.Invoke ();
		}

#if false
		public void SetData ( BindingList < T > bl_t )
		{
			BL_T = bl_t;
			listBox1.DataSource = BL_T;
			listBox1.DisplayMember = "Name";
			ResetItems ();

			//@info 非表示状態でDataSouceを入れ替えると例外が発生するのでコメントアウト
#if false

			if ( listBox1.SelectedIndex < 0 ) { return; }

			int i = listBox1.SelectedIndex;
			if ( i > 0 )
			{
				Tb_Name.Text = bl_t [ i ].Name;
			}
#endif

			//変更時イベント
			Changed?.Invoke ();
		}
#endif

		public void ResetItems ()
		{
			BD_T.ResetItems ();
		}

		//名前指定チェック
		public void CheckNameAssign ( Func_Check f )
		{
		}

		//リストボックスの手動描画
		private void ListBox1_DrawItem ( object sender, DrawItemEventArgs e )
		{
			//背景(選択時強調表示)
			e.DrawBackground ();

			string name = listBox1.GetItemText ( listBox1.Items[e.Index] );
			Brush Brs = Brushes.Black;

			//選択されているものを強調表示の背景から白抜き
			if ( (e.State & DrawItemState.Selected) == DrawItemState.Selected )
			{
				Brs = Brushes.White;
			}

			//指定条件チェック
			if ( Func_check ( listBox1.Items[e.Index] ) )
			{
				Brs = Brushes.Red;
			}


			e.Graphics.DrawString ( 
				name, e.Font, Brs, e.Bounds, StringFormat.GenericDefault );

			//フォーカス枠
			e.DrawFocusRectangle ();
		}


		//------------------------------------------------------------------------------
		//イベント

		//追加ボタン(新規挿入)
		private void Btn_Add_Click ( object sender, EventArgs e )
		{
			int n = listBox1.Items.Count;
			int slct = listBox1.SelectedIndex;

			//リストが０より大きく、選択されているとき
			if ( 0 < n && 0 <= slct )
			{
				//挿入
				BD_T.Insert ( slct, New_T () );
			}
			else
			{
				//末尾に追加
				BD_T.Add ( New_T () );
				listBox1.SelectedIndex = listBox1.Items.Count - 1;
			}
			BD_T.ResetItems ();
		}

		//削除ボタン
		private void Btn_Del_Click ( object sender, EventArgs e )
		{
			if ( listBox1.Items.Count <= 0 ) { return; }

			BD_T.RemoveAt ( listBox1.SelectedIndex );
			BD_T.ResetItems ();
		}

		//上へ移動
		private void Btn_Up_Click ( object sender, EventArgs e )
		{
			//--------------------------------------------------------------------
			//動作条件　下記の条件時は何もしない
			if ( listBox1.Items.Count <= 1 ) { return; }			//対象個数が１以下
			if ( listBox1.SelectedItems.Count <= 0 ) { return; }	//選択されていない
			if ( listBox1.SelectedIndex <= 0 ) { return; }          //選択が先頭のとき
																	//--------------------------------------------------------------------
			//１つ前の位置
			int i = listBox1.SelectedIndex - 1;
			BD_T.Up ( listBox1.SelectedIndex );

			//選択を１つ前へ
			listBox1.SelectedIndex = i;

			//変更時イベント
			Changed?.Invoke ();
		}

		//下へ移動
		private void Btn_Down_Click ( object sender, EventArgs e )
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
			BD_T.Down ( listBox1.SelectedIndex );

			//選択を１つ次へ
			listBox1.SelectedIndex = i - 1;

			//変更時イベント
			Changed?.Invoke ();
		}

		//個数
		public int Count ()
		{
			return listBox1.Items.Count;
		}

		//選択位置
		public int SelectedIndex ()
		{
			return listBox1.SelectedIndex;
		}

		//============================================================
		//イベント
		public delegate void Event ();

		//リストボックスの変更すべて
		public Event Changed { get; set; } = null;


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
			Add?.Invoke();
			Changed?.Invoke();
		}

		//イベント：削除時
		public Event Del { get; set; } = null;
		private void listBox1_Del ( object sender, System.EventArgs e )
		{
			Del?.Invoke ();
			Changed?.Invoke();
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

			//変更時イベント
			Changed?.Invoke ();
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
				BD_T.ResetItems ();

				Tb_KeyPress?.Invoke ();

				//変更時イベント
				Changed?.Invoke ();
			}
		}
	}

}
