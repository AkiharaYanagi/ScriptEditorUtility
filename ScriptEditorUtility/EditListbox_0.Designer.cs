namespace ScriptEditorUtility
{
	partial class EditListbox_0
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
			this.Btn_Down = new System.Windows.Forms.Button();
			this.Btn_Up = new System.Windows.Forms.Button();
			this.Btn_Del = new System.Windows.Forms.Button();
			this.Btn_Add = new System.Windows.Forms.Button();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// Btn_Down
			// 
			this.Btn_Down.Location = new System.Drawing.Point(162, 198);
			this.Btn_Down.Name = "Btn_Down";
			this.Btn_Down.Size = new System.Drawing.Size(30, 178);
			this.Btn_Down.TabIndex = 7;
			this.Btn_Down.Text = "↓";
			this.Btn_Down.UseVisualStyleBackColor = true;
			this.Btn_Down.Click += new System.EventHandler(this.Btn_Down_Click);
			// 
			// Btn_Up
			// 
			this.Btn_Up.Location = new System.Drawing.Point(162, 0);
			this.Btn_Up.Name = "Btn_Up";
			this.Btn_Up.Size = new System.Drawing.Size(30, 192);
			this.Btn_Up.TabIndex = 8;
			this.Btn_Up.Text = "↑";
			this.Btn_Up.UseVisualStyleBackColor = true;
			this.Btn_Up.Click += new System.EventHandler(this.Btn_Up_Click);
			// 
			// Btn_Del
			// 
			this.Btn_Del.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.Btn_Del.Location = new System.Drawing.Point(118, 382);
			this.Btn_Del.Name = "Btn_Del";
			this.Btn_Del.Size = new System.Drawing.Size(38, 34);
			this.Btn_Del.TabIndex = 6;
			this.Btn_Del.Text = "削除";
			this.Btn_Del.UseVisualStyleBackColor = false;
			this.Btn_Del.Click += new System.EventHandler(this.Btn_Del_Click);
			// 
			// Btn_Add
			// 
			this.Btn_Add.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.Btn_Add.Location = new System.Drawing.Point(0, 382);
			this.Btn_Add.Name = "Btn_Add";
			this.Btn_Add.Size = new System.Drawing.Size(112, 34);
			this.Btn_Add.TabIndex = 5;
			this.Btn_Add.Text = "追加";
			this.Btn_Add.UseVisualStyleBackColor = false;
			this.Btn_Add.Click += new System.EventHandler(this.Btn_Add_Click);
			// 
			// listBox1
			// 
			this.listBox1.FormattingEnabled = true;
			this.listBox1.ItemHeight = 12;
			this.listBox1.Location = new System.Drawing.Point(0, 0);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(156, 376);
			this.listBox1.TabIndex = 4;
			this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
			// 
			// EditListbox_0
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.Btn_Down);
			this.Controls.Add(this.Btn_Up);
			this.Controls.Add(this.Btn_Del);
			this.Controls.Add(this.Btn_Add);
			this.Controls.Add(this.listBox1);
			this.Name = "EditListbox_0";
			this.Size = new System.Drawing.Size(199, 424);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button Btn_Down;
		private System.Windows.Forms.Button Btn_Up;
		private System.Windows.Forms.Button Btn_Del;
		private System.Windows.Forms.Button Btn_Add;
		private System.Windows.Forms.ListBox listBox1;
	}
}
