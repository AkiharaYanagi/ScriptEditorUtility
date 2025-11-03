namespace ScriptEditor
{
	partial class EditListbox < T > 
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
			if ( disposing && ( components != null ) )
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
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.Btn_Add = new System.Windows.Forms.Button();
			this.Btn_Del = new System.Windows.Forms.Button();
			this.Btn_Up = new System.Windows.Forms.Button();
			this.Btn_Down = new System.Windows.Forms.Button();
			this.Tb_Name = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.BTN_N_Up = new System.Windows.Forms.Button();
			this.BTN_N_Down = new System.Windows.Forms.Button();
			this.TB_N_Up = new System.Windows.Forms.TextBox();
			this.TB_N_Down = new System.Windows.Forms.TextBox();
			this.Btn_Tail = new System.Windows.Forms.Button();
			this.Btn_Top = new System.Windows.Forms.Button();
			this.Btn_SaveOne = new System.Windows.Forms.Button();
			this.Btn_LoadOne = new System.Windows.Forms.Button();
			this.Btn_SaveAll = new System.Windows.Forms.Button();
			this.Btn_LoadAll = new System.Windows.Forms.Button();
			this.Btn_Folder = new System.Windows.Forms.Button();
			this.Lbl_i_n = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// listBox1
			// 
			this.listBox1.FormattingEnabled = true;
			this.listBox1.ItemHeight = 12;
			this.listBox1.Location = new System.Drawing.Point(3, 7);
			this.listBox1.Name = "listBox1";
			this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listBox1.Size = new System.Drawing.Size(158, 352);
			this.listBox1.TabIndex = 0;
			this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
			// 
			// Btn_Add
			// 
			this.Btn_Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.Btn_Add.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.Btn_Add.Location = new System.Drawing.Point(0, 360);
			this.Btn_Add.Name = "Btn_Add";
			this.Btn_Add.Size = new System.Drawing.Size(112, 34);
			this.Btn_Add.TabIndex = 1;
			this.Btn_Add.Text = "追加";
			this.Btn_Add.UseVisualStyleBackColor = false;
			this.Btn_Add.Click += new System.EventHandler(this.Btn_Add_Click);
			// 
			// Btn_Del
			// 
			this.Btn_Del.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.Btn_Del.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.Btn_Del.Location = new System.Drawing.Point(118, 360);
			this.Btn_Del.Name = "Btn_Del";
			this.Btn_Del.Size = new System.Drawing.Size(43, 34);
			this.Btn_Del.TabIndex = 2;
			this.Btn_Del.Text = "削除";
			this.Btn_Del.UseVisualStyleBackColor = false;
			this.Btn_Del.Click += new System.EventHandler(this.Btn_Del_Click);
			// 
			// Btn_Up
			// 
			this.Btn_Up.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
			this.Btn_Up.Location = new System.Drawing.Point(167, 82);
			this.Btn_Up.Name = "Btn_Up";
			this.Btn_Up.Size = new System.Drawing.Size(30, 107);
			this.Btn_Up.TabIndex = 3;
			this.Btn_Up.Text = "↑";
			this.Btn_Up.UseVisualStyleBackColor = true;
			this.Btn_Up.Click += new System.EventHandler(this.Btn_Up_Click);
			// 
			// Btn_Down
			// 
			this.Btn_Down.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
			this.Btn_Down.Location = new System.Drawing.Point(167, 197);
			this.Btn_Down.Name = "Btn_Down";
			this.Btn_Down.Size = new System.Drawing.Size(30, 116);
			this.Btn_Down.TabIndex = 3;
			this.Btn_Down.Text = "↓";
			this.Btn_Down.UseVisualStyleBackColor = true;
			this.Btn_Down.Click += new System.EventHandler(this.Btn_Down_Click);
			// 
			// Tb_Name
			// 
			this.Tb_Name.Location = new System.Drawing.Point(3, 92);
			this.Tb_Name.Name = "Tb_Name";
			this.Tb_Name.Size = new System.Drawing.Size(194, 19);
			this.Tb_Name.TabIndex = 4;
			this.Tb_Name.Text = "Name";
			this.Tb_Name.TextChanged += new System.EventHandler(this.Tb_Name_TextChanged);
			this.Tb_Name.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Tb_Name_KeyPress);
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.Controls.Add(this.BTN_N_Up);
			this.panel1.Controls.Add(this.BTN_N_Down);
			this.panel1.Controls.Add(this.TB_N_Up);
			this.panel1.Controls.Add(this.TB_N_Down);
			this.panel1.Controls.Add(this.Btn_Tail);
			this.panel1.Controls.Add(this.Btn_Top);
			this.panel1.Controls.Add(this.Btn_Del);
			this.panel1.Controls.Add(this.listBox1);
			this.panel1.Controls.Add(this.Btn_Add);
			this.panel1.Controls.Add(this.Btn_Up);
			this.panel1.Controls.Add(this.Btn_Down);
			this.panel1.Location = new System.Drawing.Point(3, 131);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(203, 406);
			this.panel1.TabIndex = 5;
			// 
			// BTN_N_Up
			// 
			this.BTN_N_Up.Font = new System.Drawing.Font("MS UI Gothic", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.BTN_N_Up.Location = new System.Drawing.Point(167, 28);
			this.BTN_N_Up.Name = "BTN_N_Up";
			this.BTN_N_Up.Size = new System.Drawing.Size(30, 23);
			this.BTN_N_Up.TabIndex = 6;
			this.BTN_N_Up.Text = "n↑";
			this.BTN_N_Up.UseVisualStyleBackColor = true;
			this.BTN_N_Up.Click += new System.EventHandler(this.BTN_N_Up_Click);
			// 
			// BTN_N_Down
			// 
			this.BTN_N_Down.Font = new System.Drawing.Font("MS UI Gothic", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.BTN_N_Down.Location = new System.Drawing.Point(167, 346);
			this.BTN_N_Down.Name = "BTN_N_Down";
			this.BTN_N_Down.Size = new System.Drawing.Size(30, 23);
			this.BTN_N_Down.TabIndex = 6;
			this.BTN_N_Down.Text = "n↓";
			this.BTN_N_Down.UseVisualStyleBackColor = true;
			this.BTN_N_Down.Click += new System.EventHandler(this.BTN_N_Down_Click);
			// 
			// TB_N_Up
			// 
			this.TB_N_Up.Location = new System.Drawing.Point(167, 57);
			this.TB_N_Up.Name = "TB_N_Up";
			this.TB_N_Up.Size = new System.Drawing.Size(30, 19);
			this.TB_N_Up.TabIndex = 5;
			// 
			// TB_N_Down
			// 
			this.TB_N_Down.Location = new System.Drawing.Point(167, 323);
			this.TB_N_Down.Name = "TB_N_Down";
			this.TB_N_Down.Size = new System.Drawing.Size(30, 19);
			this.TB_N_Down.TabIndex = 5;
			// 
			// Btn_Tail
			// 
			this.Btn_Tail.Font = new System.Drawing.Font("MS UI Gothic", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Btn_Tail.Location = new System.Drawing.Point(167, 375);
			this.Btn_Tail.Name = "Btn_Tail";
			this.Btn_Tail.Size = new System.Drawing.Size(30, 15);
			this.Btn_Tail.TabIndex = 4;
			this.Btn_Tail.Text = "↓↓";
			this.Btn_Tail.UseVisualStyleBackColor = true;
			this.Btn_Tail.Click += new System.EventHandler(this.Btn_Tail_Click);
			// 
			// Btn_Top
			// 
			this.Btn_Top.Font = new System.Drawing.Font("MS UI Gothic", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Btn_Top.Location = new System.Drawing.Point(167, 7);
			this.Btn_Top.Name = "Btn_Top";
			this.Btn_Top.Size = new System.Drawing.Size(30, 15);
			this.Btn_Top.TabIndex = 4;
			this.Btn_Top.Text = "↑↑";
			this.Btn_Top.UseVisualStyleBackColor = true;
			this.Btn_Top.Click += new System.EventHandler(this.Btn_Top_Click);
			// 
			// Btn_SaveOne
			// 
			this.Btn_SaveOne.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
			this.Btn_SaveOne.Location = new System.Drawing.Point(3, 28);
			this.Btn_SaveOne.Name = "Btn_SaveOne";
			this.Btn_SaveOne.Size = new System.Drawing.Size(89, 26);
			this.Btn_SaveOne.TabIndex = 6;
			this.Btn_SaveOne.Text = "１項目書出";
			this.Btn_SaveOne.UseVisualStyleBackColor = false;
			this.Btn_SaveOne.Click += new System.EventHandler(this.Btn_SaveOne_Click);
			// 
			// Btn_LoadOne
			// 
			this.Btn_LoadOne.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.Btn_LoadOne.Location = new System.Drawing.Point(109, 27);
			this.Btn_LoadOne.Name = "Btn_LoadOne";
			this.Btn_LoadOne.Size = new System.Drawing.Size(88, 27);
			this.Btn_LoadOne.TabIndex = 7;
			this.Btn_LoadOne.Text = "１項目読込";
			this.Btn_LoadOne.UseVisualStyleBackColor = false;
			this.Btn_LoadOne.Click += new System.EventHandler(this.Btn_LoadOne_Click);
			// 
			// Btn_SaveAll
			// 
			this.Btn_SaveAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(232)))));
			this.Btn_SaveAll.Location = new System.Drawing.Point(3, 59);
			this.Btn_SaveAll.Name = "Btn_SaveAll";
			this.Btn_SaveAll.Size = new System.Drawing.Size(89, 26);
			this.Btn_SaveAll.TabIndex = 6;
			this.Btn_SaveAll.Text = "全項目書出";
			this.Btn_SaveAll.UseVisualStyleBackColor = false;
			this.Btn_SaveAll.Click += new System.EventHandler(this.Btn_SaveAll_Click);
			// 
			// Btn_LoadAll
			// 
			this.Btn_LoadAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.Btn_LoadAll.Location = new System.Drawing.Point(109, 61);
			this.Btn_LoadAll.Name = "Btn_LoadAll";
			this.Btn_LoadAll.Size = new System.Drawing.Size(88, 25);
			this.Btn_LoadAll.TabIndex = 7;
			this.Btn_LoadAll.Text = "全項目読込";
			this.Btn_LoadAll.UseVisualStyleBackColor = false;
			this.Btn_LoadAll.Click += new System.EventHandler(this.Btn_LoadAll_Click);
			// 
			// Btn_Folder
			// 
			this.Btn_Folder.Location = new System.Drawing.Point(3, 3);
			this.Btn_Folder.Name = "Btn_Folder";
			this.Btn_Folder.Size = new System.Drawing.Size(194, 20);
			this.Btn_Folder.TabIndex = 8;
			this.Btn_Folder.Text = "フォルダ";
			this.Btn_Folder.UseVisualStyleBackColor = true;
			this.Btn_Folder.Click += new System.EventHandler(this.Btn_Folder_Click);
			// 
			// Lbl_i_n
			// 
			this.Lbl_i_n.AutoSize = true;
			this.Lbl_i_n.Location = new System.Drawing.Point(4, 116);
			this.Lbl_i_n.Name = "Lbl_i_n";
			this.Lbl_i_n.Size = new System.Drawing.Size(59, 12);
			this.Lbl_i_n.TabIndex = 9;
			this.Lbl_i_n.Text = "index/num";
			// 
			// EditListbox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.Lbl_i_n);
			this.Controls.Add(this.Btn_Folder);
			this.Controls.Add(this.Btn_LoadAll);
			this.Controls.Add(this.Btn_SaveAll);
			this.Controls.Add(this.Btn_LoadOne);
			this.Controls.Add(this.Btn_SaveOne);
			this.Controls.Add(this.Tb_Name);
			this.Controls.Add(this.panel1);
			this.Name = "EditListbox";
			this.Size = new System.Drawing.Size(227, 540);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Button Btn_Add;
		private System.Windows.Forms.Button Btn_Del;
		private System.Windows.Forms.Button Btn_Up;
		private System.Windows.Forms.Button Btn_Down;
		private System.Windows.Forms.TextBox Tb_Name;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button Btn_SaveOne;
		private System.Windows.Forms.Button Btn_LoadOne;
		private System.Windows.Forms.Button Btn_SaveAll;
		private System.Windows.Forms.Button Btn_LoadAll;
		private System.Windows.Forms.Button Btn_Folder;
		private System.Windows.Forms.Button Btn_Tail;
		private System.Windows.Forms.Button Btn_Top;
		private System.Windows.Forms.Button BTN_N_Up;
		private System.Windows.Forms.Button BTN_N_Down;
		private System.Windows.Forms.TextBox TB_N_Up;
		private System.Windows.Forms.TextBox TB_N_Down;
		private System.Windows.Forms.Label Lbl_i_n;
	}
}
