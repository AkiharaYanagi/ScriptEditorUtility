namespace ScriptEditor
{
	partial class Ctrl_Image
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
			if (disposing && (components != null))
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
            this.Btn_ArchiveOK = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.pbArchiveImage = new System.Windows.Forms.PictureBox();
            this.Lb_Image = new System.Windows.Forms.ListBox();
            this.Btn_Stream = new System.Windows.Forms.Button();
            this.Btn_MakeStream = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbArchiveImage)).BeginInit();
            this.SuspendLayout();
            // 
            // Btn_ArchiveOK
            // 
            this.Btn_ArchiveOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Btn_ArchiveOK.Location = new System.Drawing.Point(389, 50);
            this.Btn_ArchiveOK.Name = "Btn_ArchiveOK";
            this.Btn_ArchiveOK.Size = new System.Drawing.Size(236, 27);
            this.Btn_ArchiveOK.TabIndex = 8;
            this.Btn_ArchiveOK.Text = "OK";
            this.Btn_ArchiveOK.UseVisualStyleBackColor = true;
            this.Btn_ArchiveOK.Click += new System.EventHandler(this.Btn_ArchiveOK_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(194, 52);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(189, 25);
            this.btn_Cancel.TabIndex = 10;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
            // 
            // pbArchiveImage
            // 
            this.pbArchiveImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbArchiveImage.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pbArchiveImage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbArchiveImage.Location = new System.Drawing.Point(194, 83);
            this.pbArchiveImage.Name = "pbArchiveImage";
            this.pbArchiveImage.Size = new System.Drawing.Size(431, 453);
            this.pbArchiveImage.TabIndex = 9;
            this.pbArchiveImage.TabStop = false;
            // 
            // Lb_Image
            // 
            this.Lb_Image.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Lb_Image.FormattingEnabled = true;
            this.Lb_Image.ItemHeight = 12;
            this.Lb_Image.Location = new System.Drawing.Point(3, 52);
            this.Lb_Image.Name = "Lb_Image";
            this.Lb_Image.Size = new System.Drawing.Size(180, 484);
            this.Lb_Image.TabIndex = 7;
            this.Lb_Image.SelectedIndexChanged += new System.EventHandler(this.LB_SelectedIndexChanged);
            // 
            // Btn_Stream
            // 
            this.Btn_Stream.Location = new System.Drawing.Point(493, 9);
            this.Btn_Stream.Name = "Btn_Stream";
            this.Btn_Stream.Size = new System.Drawing.Size(132, 35);
            this.Btn_Stream.TabIndex = 11;
            this.Btn_Stream.Text = "流し込み";
            this.Btn_Stream.UseVisualStyleBackColor = true;
            this.Btn_Stream.Click += new System.EventHandler(this.Btn_Stream_Click);
            // 
            // Btn_MakeStream
            // 
            this.Btn_MakeStream.Location = new System.Drawing.Point(355, 9);
            this.Btn_MakeStream.Name = "Btn_MakeStream";
            this.Btn_MakeStream.Size = new System.Drawing.Size(132, 35);
            this.Btn_MakeStream.TabIndex = 11;
            this.Btn_MakeStream.Text = "同名作成";
            this.Btn_MakeStream.UseVisualStyleBackColor = true;
            this.Btn_MakeStream.Click += new System.EventHandler(this.Btn_MakeStream_Click);
            // 
            // Ctrl_Image
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Btn_MakeStream);
            this.Controls.Add(this.Btn_Stream);
            this.Controls.Add(this.Btn_ArchiveOK);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.pbArchiveImage);
            this.Controls.Add(this.Lb_Image);
            this.Name = "Ctrl_Image";
            this.Size = new System.Drawing.Size(637, 540);
            ((System.ComponentModel.ISupportInitialize)(this.pbArchiveImage)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button Btn_ArchiveOK;
		private System.Windows.Forms.Button btn_Cancel;
		private System.Windows.Forms.PictureBox pbArchiveImage;
		private System.Windows.Forms.ListBox Lb_Image;
		private System.Windows.Forms.Button Btn_Stream;
		private System.Windows.Forms.Button Btn_MakeStream;
	}
}
