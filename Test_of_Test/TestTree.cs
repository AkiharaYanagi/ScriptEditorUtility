using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace ScriptEditor
{
	public class TestTree
	{
		private TreeView treeView1 = new TreeView ();
		private TextBox textBox1 = new TextBox ();

		//シークエンスの選択デリゲート
		public System.Action < string > SelectSequence { get; set; } = null;


		public TestTree ()
		{

		}

		public void Set ( TreeView tv, TextBox tb )
		{
			treeView1 = tv;
			treeView1.AfterSelect -= treeView1_AfterSelect;
			treeView1.AfterSelect += treeView1_AfterSelect;

			textBox1 = tb;
		}

		public void Select ()
		{

		}

		public void SetCharaData ( BindingDictionary < Sequence > bd_sq )
		{
			if ( null == bd_sq ) { return; }
			treeView1.Nodes.Clear ();

			if ( 0 >= bd_sq.Count() ) { return; }

			BindingList < Sequence > bl = bd_sq.GetBindingList();

			//アクションのときアクションカテゴリで分類
			if ( bl[0] is Action ) 
			{
				//アクションカテゴリで初期化
				string[] names = Enum.GetNames ( typeof ( ActionCategory ) );
				//木の根
				TreeNode[] root = new TreeNode [ names.Length ];

				int i = 0;
				foreach ( string name in names )
				{
					root [ i ] = new TreeNode ( name );
					++ i;
				}

				//各アクションを分類
				foreach ( Action a in bl )
				{
					string ctg_name = Enum.GetName ( typeof ( ActionCategory ), a.Category );
					int nodeIndex = 0;

					//カテゴリからサーチ
					foreach ( TreeNode n in root )
					{
						if ( ctg_name == n.Text )
						{
//							nodeIndex = n.Index;
							break;
						}
						++ nodeIndex;
					}

					//追加
					root [ nodeIndex ].Nodes.Add ( a.Name );
				}
				treeView1.Nodes.AddRange ( root );

				//先頭を選択
				treeView1.SelectedNode = root[0].Nodes[0];
				textBox1.Text = root[0].Nodes[0].Text;
			}
			//アクションではない(エフェクト)のとき
			else
			{
				//名前で直接分類する
				TreeNode [] root = new TreeNode [ bl.Count ];
			
				int index = 0;
				foreach ( Sequence s in bl )
				{
					root [ index ] = new TreeNode ( s.Name );
					++ index;
				}

				treeView1.Nodes.AddRange ( root );


				//先頭を選択
				treeView1.SelectedNode = root[0];
				textBox1.Text = root[0].Text;
			}
		}

		//更新
		public void UpdateCategory ( Sequence sq )
		{
			//アクションのときカテゴリの更新
			if ( sq is Action a )
			{
				//対象のアクションは１つだがすべてを走査しておく

				string ctg_name = Enum.GetName ( typeof ( ActionCategory ), a.Category );

				//すべてのノードから対象を削除
				foreach ( TreeNode nodes in treeView1.Nodes )
				{
					foreach ( TreeNode n0 in nodes.Nodes )
					{
						if ( a.Name == n0.Text )
						{
							n0.Remove ();
							break;
						}
					}
				}

				//カテゴリの正しい箇所へ追加
				foreach ( TreeNode nodes in treeView1.Nodes )
				{
					if ( ctg_name == nodes.Text )
					{
						nodes.Nodes.Add ( a.Name );
					}
				}

			}
		}

		//ツリー選択時
		private void treeView1_AfterSelect ( object sender, TreeViewEventArgs e )
		{
			string name = treeView1.SelectedNode.Text;

			//アクション名以外(カテゴリ名など)の選択のとき何もしない
		
			bool b = false;
			string[] names = Enum.GetNames( typeof ( ActionCategory ) );
			foreach ( string s in names )
			{
				if ( s == name ) { b = true; break; }
			}
			if ( b ) { return; }

		//	textBox1.Text = name;
			SelectSequence ( name );
		}
	}
}
