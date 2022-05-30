namespace ScriptEditor
{
	partial class Ctrl_AllRect
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
			this.Btn_Copy = new System.Windows.Forms.Button();
			this.Btn_Clear = new System.Windows.Forms.Button();
			this.Btn_PasteGroup = new System.Windows.Forms.Button();
			this.Btn_PasteSequence = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.TB_CopyName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.Btn_PasteSingle = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// Btn_Copy
			// 
			this.Btn_Copy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
			this.Btn_Copy.Location = new System.Drawing.Point(34, 21);
			this.Btn_Copy.Name = "Btn_Copy";
			this.Btn_Copy.Size = new System.Drawing.Size(87, 35);
			this.Btn_Copy.TabIndex = 1;
			this.Btn_Copy.Text = "コピー";
			this.Btn_Copy.UseVisualStyleBackColor = false;
			this.Btn_Copy.Click += new System.EventHandler(this.Btn_Copy_Click);
			// 
			// Btn_Clear
			// 
			this.Btn_Clear.BackColor = System.Drawing.Color.White;
			this.Btn_Clear.Location = new System.Drawing.Point(175, 21);
			this.Btn_Clear.Name = "Btn_Clear";
			this.Btn_Clear.Size = new System.Drawing.Size(42, 35);
			this.Btn_Clear.TabIndex = 2;
			this.Btn_Clear.Text = "クリア";
			this.Btn_Clear.UseVisualStyleBackColor = false;
			this.Btn_Clear.Click += new System.EventHandler(this.Btn_Clear_Click);
			// 
			// Btn_PasteGroup
			// 
			this.Btn_PasteGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.Btn_PasteGroup.Location = new System.Drawing.Point(94, 134);
			this.Btn_PasteGroup.Name = "Btn_PasteGroup";
			this.Btn_PasteGroup.Size = new System.Drawing.Size(75, 33);
			this.Btn_PasteGroup.TabIndex = 3;
			this.Btn_PasteGroup.Text = "グループ";
			this.Btn_PasteGroup.UseVisualStyleBackColor = false;
			this.Btn_PasteGroup.Click += new System.EventHandler(this.Btn_PasteGroup_Click);
			// 
			// Btn_PasteSequence
			// 
			this.Btn_PasteSequence.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.Btn_PasteSequence.Location = new System.Drawing.Point(175, 133);
			this.Btn_PasteSequence.Name = "Btn_PasteSequence";
			this.Btn_PasteSequence.Size = new System.Drawing.Size(70, 34);
			this.Btn_PasteSequence.TabIndex = 4;
			this.Btn_PasteSequence.Text = "シークエンス";
			this.Btn_PasteSequence.UseVisualStyleBackColor = false;
			this.Btn_PasteSequence.Click += new System.EventHandler(this.Btn_PasteSequence_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.Btn_Copy);
			this.groupBox1.Controls.Add(this.TB_CopyName);
			this.groupBox1.Controls.Add(this.Btn_Clear);
			this.groupBox1.Controls.Add(this.Btn_PasteSequence);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.Btn_PasteSingle);
			this.groupBox1.Controls.Add(this.Btn_PasteGroup);
			this.groupBox1.Location = new System.Drawing.Point(421, 25);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(258, 182);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "複製";
			// 
			// TB_CopyName
			// 
			this.TB_CopyName.Location = new System.Drawing.Point(13, 72);
			this.TB_CopyName.Name = "TB_CopyName";
			this.TB_CopyName.Size = new System.Drawing.Size(204, 19);
			this.TB_CopyName.TabIndex = 5;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(95, 108);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(54, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "↓ペースト";
			// 
			// Btn_PasteSingle
			// 
			this.Btn_PasteSingle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.Btn_PasteSingle.Location = new System.Drawing.Point(13, 134);
			this.Btn_PasteSingle.Name = "Btn_PasteSingle";
			this.Btn_PasteSingle.Size = new System.Drawing.Size(75, 33);
			this.Btn_PasteSingle.TabIndex = 3;
			this.Btn_PasteSingle.Text = "シングル";
			this.Btn_PasteSingle.UseVisualStyleBackColor = false;
			this.Btn_PasteSingle.Click += new System.EventHandler(this.Btn_PasteSingle_Click);
			// 
			// Ctrl_AllRect
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox1);
			this.Name = "Ctrl_AllRect";
			this.Size = new System.Drawing.Size(697, 420);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.Ctrl_AllRect_Paint);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Button Btn_Copy;
		private System.Windows.Forms.Button Btn_Clear;
		private System.Windows.Forms.Button Btn_PasteGroup;
		private System.Windows.Forms.Button Btn_PasteSequence;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox TB_CopyName;
		private System.Windows.Forms.Button Btn_PasteSingle;
	}
}
