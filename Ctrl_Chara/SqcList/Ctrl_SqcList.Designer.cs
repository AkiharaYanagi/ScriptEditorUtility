namespace ScriptEditor
{
	partial class Ctrl_SqcList
	{
		/// <summary> 
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose ( bool disposing )
		{
			if ( disposing && (components != null) )
			{
				components.Dispose ();
			}
			base.Dispose ( disposing );
		}

		#region コンポーネント デザイナーで生成されたコード

		/// <summary> 
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent ()
		{
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.フォルダToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.上書保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.別名保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.読込ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.button1 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.ctrl_ImageTable1 = new ScriptEditor.Ctrl_ImageTable();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.フォルダToolStripMenuItem,
            this.上書保存ToolStripMenuItem,
            this.別名保存ToolStripMenuItem,
            this.読込ToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(770, 24);
			this.menuStrip1.TabIndex = 2;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// フォルダToolStripMenuItem
			// 
			this.フォルダToolStripMenuItem.Name = "フォルダToolStripMenuItem";
			this.フォルダToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
			this.フォルダToolStripMenuItem.Text = "フォルダ(&F)";
			this.フォルダToolStripMenuItem.Click += new System.EventHandler(this.フォルダToolStripMenuItem_Click);
			// 
			// 上書保存ToolStripMenuItem
			// 
			this.上書保存ToolStripMenuItem.Name = "上書保存ToolStripMenuItem";
			this.上書保存ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.上書保存ToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
			this.上書保存ToolStripMenuItem.Text = "上書保存(&S)";
			// 
			// 別名保存ToolStripMenuItem
			// 
			this.別名保存ToolStripMenuItem.Name = "別名保存ToolStripMenuItem";
			this.別名保存ToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
			this.別名保存ToolStripMenuItem.Text = "別名保存(&A)";
			// 
			// 読込ToolStripMenuItem
			// 
			this.読込ToolStripMenuItem.Name = "読込ToolStripMenuItem";
			this.読込ToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
			this.読込ToolStripMenuItem.Text = "読込(&R)";
			this.読込ToolStripMenuItem.Click += new System.EventHandler(this.読込ToolStripMenuItem_Click);
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.button1.Location = new System.Drawing.Point(227, 27);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(98, 19);
			this.button1.TabIndex = 9;
			this.button1.Text = "イメージディレクトリ";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// textBox1
			// 
			this.textBox1.AllowDrop = true;
			this.textBox1.Location = new System.Drawing.Point(331, 27);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(437, 19);
			this.textBox1.TabIndex = 8;
			// 
			// ctrl_ImageTable1
			// 
			this.ctrl_ImageTable1.AllowDrop = true;
			this.ctrl_ImageTable1.EditData = null;
			this.ctrl_ImageTable1.ELB_Sqc = null;
			this.ctrl_ImageTable1.Location = new System.Drawing.Point(227, 52);
			this.ctrl_ImageTable1.Name = "ctrl_ImageTable1";
			this.ctrl_ImageTable1.Size = new System.Drawing.Size(540, 472);
			this.ctrl_ImageTable1.TabIndex = 10;
			// 
			// Ctrl_SqcList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.ctrl_ImageTable1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.menuStrip1);
			this.Name = "Ctrl_SqcList";
			this.Size = new System.Drawing.Size(770, 563);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem フォルダToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 上書保存ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 別名保存ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 読込ToolStripMenuItem;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox textBox1;
		private Ctrl_ImageTable ctrl_ImageTable1;
	}
}
