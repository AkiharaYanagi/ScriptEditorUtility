namespace ScriptEditor
{
	partial class Ctrl_ImageTable
	{
		/// <summary> 
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region コンポーネント デザイナーで生成されたコード

		/// <summary> 
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.panel1 = new System.Windows.Forms.Panel();
			this.pB_Sqc1 = new ScriptEditor.PB_Sqc();
			this.Btn_Clear = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.Btn_Load = new System.Windows.Forms.Button();
			this.Btn_Save = new System.Windows.Forms.Button();
			this.Btn_ImgDir = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pB_Sqc1)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.AutoScroll = true;
			this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
			this.panel1.Controls.Add(this.pB_Sqc1);
			this.panel1.Location = new System.Drawing.Point(0, 65);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(634, 496);
			this.panel1.TabIndex = 0;
			this.panel1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.panel1_Scroll);
			// 
			// pB_Sqc1
			// 
			this.pB_Sqc1.BackColor = System.Drawing.SystemColors.Control;
			this.pB_Sqc1.EditData = null;
			this.pB_Sqc1.ELB_Sqc = null;
			this.pB_Sqc1.Location = new System.Drawing.Point(3, 3);
			this.pB_Sqc1.Name = "pB_Sqc1";
			this.pB_Sqc1.Size = new System.Drawing.Size(590, 458);
			this.pB_Sqc1.TabIndex = 0;
			this.pB_Sqc1.TabStop = false;
			// 
			// Btn_Clear
			// 
			this.Btn_Clear.BackColor = System.Drawing.Color.White;
			this.Btn_Clear.Location = new System.Drawing.Point(2, 2);
			this.Btn_Clear.Name = "Btn_Clear";
			this.Btn_Clear.Size = new System.Drawing.Size(45, 45);
			this.Btn_Clear.TabIndex = 1;
			this.Btn_Clear.Text = "Clear";
			this.Btn_Clear.UseVisualStyleBackColor = false;
			this.Btn_Clear.Click += new System.EventHandler(this.Btn_Clear_Click);
			// 
			// textBox1
			// 
			this.textBox1.AllowDrop = true;
			this.textBox1.Location = new System.Drawing.Point(116, 3);
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(518, 19);
			this.textBox1.TabIndex = 2;
			this.textBox1.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox1_DragDrop);
			this.textBox1.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox1_DragEnter);
			// 
			// Btn_Load
			// 
			this.Btn_Load.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.Btn_Load.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Btn_Load.Location = new System.Drawing.Point(172, 26);
			this.Btn_Load.Name = "Btn_Load";
			this.Btn_Load.Size = new System.Drawing.Size(102, 21);
			this.Btn_Load.TabIndex = 4;
			this.Btn_Load.Text = "読込";
			this.Btn_Load.UseVisualStyleBackColor = false;
			this.Btn_Load.Click += new System.EventHandler(this.Btn_Load_Click);
			// 
			// Btn_Save
			// 
			this.Btn_Save.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
			this.Btn_Save.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Btn_Save.Location = new System.Drawing.Point(64, 26);
			this.Btn_Save.Name = "Btn_Save";
			this.Btn_Save.Size = new System.Drawing.Size(102, 21);
			this.Btn_Save.TabIndex = 4;
			this.Btn_Save.Text = "書出";
			this.Btn_Save.UseVisualStyleBackColor = false;
			this.Btn_Save.Click += new System.EventHandler(this.Btn_Save_Click);
			// 
			// Btn_ImgDir
			// 
			this.Btn_ImgDir.Location = new System.Drawing.Point(64, 3);
			this.Btn_ImgDir.Name = "Btn_ImgDir";
			this.Btn_ImgDir.Size = new System.Drawing.Size(34, 19);
			this.Btn_ImgDir.TabIndex = 5;
			this.Btn_ImgDir.Text = "Dir";
			this.Btn_ImgDir.UseVisualStyleBackColor = true;
			this.Btn_ImgDir.Click += new System.EventHandler(this.Btn_ImgDir_Click);
			// 
			// Ctrl_ImageTable
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.Btn_ImgDir);
			this.Controls.Add(this.Btn_Save);
			this.Controls.Add(this.Btn_Load);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.Btn_Clear);
			this.Controls.Add(this.panel1);
			this.Name = "Ctrl_ImageTable";
			this.Size = new System.Drawing.Size(637, 549);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Ctrl_ImageTable_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Ctrl_ImageTable_DragEnter);
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pB_Sqc1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private ScriptEditor.PB_Sqc pB_Sqc1;
		private System.Windows.Forms.Button Btn_Clear;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button Btn_Load;
		private System.Windows.Forms.Button Btn_Save;
		private System.Windows.Forms.Button Btn_ImgDir;
	}
}
