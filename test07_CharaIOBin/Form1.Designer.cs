namespace ScriptEditor
{
	partial class Form1
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

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent ()
		{
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.LBL_MSG = new System.Windows.Forms.Label();
			this.button3 = new System.Windows.Forms.Button();
			this.BTN_SAVE_DOC = new System.Windows.Forms.Button();
			this.BTN_LOAD_DOC = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.button1.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.button1.Location = new System.Drawing.Point(317, 97);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(145, 89);
			this.button1.TabIndex = 0;
			this.button1.Text = "Save Binary";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(21, 17);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(96, 38);
			this.button2.TabIndex = 1;
			this.button2.Text = "フォルダ";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// LBL_MSG
			// 
			this.LBL_MSG.AutoSize = true;
			this.LBL_MSG.Location = new System.Drawing.Point(29, 332);
			this.LBL_MSG.Name = "LBL_MSG";
			this.LBL_MSG.Size = new System.Drawing.Size(35, 12);
			this.LBL_MSG.TabIndex = 2;
			this.LBL_MSG.Text = "label1";
			// 
			// button3
			// 
			this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.button3.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.button3.Location = new System.Drawing.Point(317, 207);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(145, 100);
			this.button3.TabIndex = 3;
			this.button3.Text = "Load Binary";
			this.button3.UseVisualStyleBackColor = false;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// BTN_SAVE_DOC
			// 
			this.BTN_SAVE_DOC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.BTN_SAVE_DOC.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.BTN_SAVE_DOC.Location = new System.Drawing.Point(136, 97);
			this.BTN_SAVE_DOC.Name = "BTN_SAVE_DOC";
			this.BTN_SAVE_DOC.Size = new System.Drawing.Size(119, 89);
			this.BTN_SAVE_DOC.TabIndex = 0;
			this.BTN_SAVE_DOC.Text = "Save Document";
			this.BTN_SAVE_DOC.UseVisualStyleBackColor = false;
			this.BTN_SAVE_DOC.Click += new System.EventHandler(this.BTN_SAVE_DOC_Click);
			// 
			// BTN_LOAD_DOC
			// 
			this.BTN_LOAD_DOC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.BTN_LOAD_DOC.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.BTN_LOAD_DOC.Location = new System.Drawing.Point(136, 207);
			this.BTN_LOAD_DOC.Name = "BTN_LOAD_DOC";
			this.BTN_LOAD_DOC.Size = new System.Drawing.Size(119, 100);
			this.BTN_LOAD_DOC.TabIndex = 3;
			this.BTN_LOAD_DOC.Text = "Load Document";
			this.BTN_LOAD_DOC.UseVisualStyleBackColor = false;
			this.BTN_LOAD_DOC.Click += new System.EventHandler(this.BTN_LOAD_DOC_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(484, 383);
			this.Controls.Add(this.BTN_LOAD_DOC);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.LBL_MSG);
			this.Controls.Add(this.BTN_SAVE_DOC);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label LBL_MSG;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button BTN_SAVE_DOC;
		private System.Windows.Forms.Button BTN_LOAD_DOC;
	}
}

