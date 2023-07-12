using System;
using System.Windows.Forms;


namespace ScriptEditor
{
	//エイリアス
	using BD_Sqc = BindingDictionary < Sequence >;

	//------------------------------------------------------------------
	//	シークエンスの一覧をツリー状に表示し、EditCompendに１つを選択させる
	//------------------------------------------------------------------
	public class SqcTree : UserControl
	{
		//対象シークエンス
		public BD_Sqc BD_Sqc { get; set; } = null;

		//編集
		public EditCompend EditCompend { get; set; } = null;

		//親コントロール
		public _Ctrl_Compend CtrlCompend = null;

		//親コントロールからの表示
		public System.Action ActDisp = ()=>{};
		
		//親コントロールからの関連付け
		public System.Action ActAssosiate = ()=>{};


		//コンストラクタ
		public SqcTree ()
		{
			InitializeComponent ();
		}


		private void InitializeComponent ()
		{
			this.components = new System.ComponentModel.Container();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.すべて展開ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.すべて閉鎖ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// treeView1
			// 
			this.treeView1.ContextMenuStrip = this.contextMenuStrip1;
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView1.Location = new System.Drawing.Point(0, 0);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(148, 385);
			this.treeView1.TabIndex = 0;
			this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.すべて展開ToolStripMenuItem,
            this.すべて閉鎖ToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(128, 48);
			// 
			// すべて展開ToolStripMenuItem
			// 
			this.すべて展開ToolStripMenuItem.Name = "すべて展開ToolStripMenuItem";
			this.すべて展開ToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
			this.すべて展開ToolStripMenuItem.Text = "すべて展開";
			this.すべて展開ToolStripMenuItem.Click += new System.EventHandler(this.すべて展開ToolStripMenuItem_Click);
			// 
			// すべて閉鎖ToolStripMenuItem
			// 
			this.すべて閉鎖ToolStripMenuItem.Name = "すべて閉鎖ToolStripMenuItem";
			this.すべて閉鎖ToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
			this.すべて閉鎖ToolStripMenuItem.Text = "すべて閉鎖";
			this.すべて閉鎖ToolStripMenuItem.Click += new System.EventHandler(this.すべて閉鎖ToolStripMenuItem_Click);
			// 
			// SqcTree
			// 
			this.AutoScroll = true;
			this.Controls.Add(this.treeView1);
			this.Name = "SqcTree";
			this.Size = new System.Drawing.Size(148, 385);
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		private TreeView treeView1;


		//環境設定
		public void SetEnviron ( EditCompend ec, _Ctrl_Compend ctrl_cmp )
		{
			EditCompend = ec;
			CtrlCompend = ctrl_cmp;
			ActDisp = CtrlCompend.AllDisp;
			ActAssosiate = CtrlCompend.Assosiate;
		}
		

		//キャラデータ設定
		public void SetCharaData ( BD_Sqc bd_sqc )
		{
			if ( bd_sqc is null ) { return; }

			BD_Sqc = bd_sqc;

			//シークエンス個数が０のときダミー
			if ( 0 == bd_sqc.Count() )
			{
				bd_sqc.New ();
			}

			//アクションのときのみアクションカテゴリで分類
			if ( bd_sqc [ 0 ] is Action ) 
			{
				//カテゴリでツリー構築
				ClassficatinByCategory ( bd_sqc );

				//先頭を選択
				treeView1.SelectedNode = treeView1.Nodes[0].Nodes[0];
				string seq_name = treeView1.SelectedNode.Text;
//				EditCompend.SelectSequence ( seq_name );
			}
			//アクションではない(エフェクト)のとき
			else
			{
				//名前でツリー構築
				ClassficatinByName ( bd_sqc );

				//先頭を選択
				treeView1.SelectedNode = treeView1.Nodes[0];
				string seq_name = treeView1.SelectedNode.Text;
//				EditCompend.SelectSequence ( seq_name );
			}

			treeView1.ExpandAll ();
			SelectTop ();
		}


		//-----------------------------------------------------------------------------
		//名前でツリーを再構築
		private void ClassficatinByName ( BD_Sqc bd_sqc )
		{
			//既存をクリア
			treeView1.Nodes.Clear ();

			//ノードの数を先に確保
			TreeNode [] root = new TreeNode [ bd_sqc.Count () ];
			
			//名前で直接追加する
			int index = 0;
			foreach ( Sequence s in bd_sqc.GetEnumerable () )
			{
				root [ index ] = new TreeNode ( s.Name );
				++ index;
			}

			//複数追加
			treeView1.Nodes.AddRange ( root );
		}

		//カテゴリでツリーを再構築
		private void ClassficatinByCategory ( BD_Sqc bd_sqc )
		{
			//既存をクリア
			treeView1.Nodes.Clear ();

			//----------------------------------------------
			//カテゴリ名で木の１段目を作る

			//カテゴリを定数名で初期化
			string[] names = Enum.GetNames ( typeof ( ActionCategory ) );

			//木の根
			TreeNode[] root = new TreeNode [ names.Length ];

			//追加
			for ( int i = 0; i < names.Length; ++ i )
			{
				root [ i ] = new TreeNode ( names [ i ] );
			}

			//----------------------------------------------
			//各アクションを分類
			foreach ( Action a in bd_sqc.GetEnumerable () )
			{
				//カテゴリ名を取得
				string ctg_name = Enum.GetName ( typeof ( ActionCategory ), a.Category );

				//カテゴリからサーチ
				TreeNode tn = Array.Find ( root, n => n.Text == ctg_name );

				//追加
				tn.Nodes.Add ( a.Name );				
			}

			//----------------------------------------------
			//全追加
			treeView1.Nodes.AddRange ( root );

		}

		//ツリー選択後
		private void treeView1_AfterSelect ( object sender, TreeViewEventArgs e )
		{
			string name = treeView1.SelectedNode.Text;
			EditCompend.SelectSequence ( name );

			CtrlCompend.Assosiate ();
		}


		//先頭を選択
		public void SelectTop ()
		{
			if ( BD_Sqc is null ) { return; }

			//アクションのときのみアクションカテゴリで分類
			if ( BD_Sqc[0] is Action ) 
			{
				treeView1.SelectedNode = treeView1.Nodes[0].Nodes[0];
			}
			//アクションではない(エフェクト)のとき
			else
			{
				treeView1.SelectedNode = treeView1.Nodes[0];
			}

			treeView1.Focus ();
		}


		//==============================================================
		//コンテキストメニュ
		private ContextMenuStrip contextMenuStrip1;
		private System.ComponentModel.IContainer components;
		private ToolStripMenuItem すべて展開ToolStripMenuItem;
		private ToolStripMenuItem すべて閉鎖ToolStripMenuItem;

		private void すべて展開ToolStripMenuItem_Click ( object sender, EventArgs e )
		{
			treeView1.ExpandAll ();
		}

		private void すべて閉鎖ToolStripMenuItem_Click ( object sender, EventArgs e )
		{
			treeView1.CollapseAll ();
		}

	}
}
