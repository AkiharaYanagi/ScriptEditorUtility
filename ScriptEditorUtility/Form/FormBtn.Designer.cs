namespace ScriptEditorUtility
{
	partial class FormBtn
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
			this.Btn_Hide = new System.Windows.Forms.Button();
			this.Btn_SubForm = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// Btn_Hide
			// 
			this.Btn_Hide.Location = new System.Drawing.Point(5, 1);
			this.Btn_Hide.Margin = new System.Windows.Forms.Padding(4);
			this.Btn_Hide.Name = "Btn_Hide";
			this.Btn_Hide.Size = new System.Drawing.Size(28, 39);
			this.Btn_Hide.TabIndex = 27;
			this.Btn_Hide.Text = "▼";
			this.Btn_Hide.UseVisualStyleBackColor = true;
			this.Btn_Hide.Click += new System.EventHandler(this.Btn_Hide_Click);
			// 
			// Btn_SubForm
			// 
			this.Btn_SubForm.Location = new System.Drawing.Point(41, 0);
			this.Btn_SubForm.Margin = new System.Windows.Forms.Padding(4);
			this.Btn_SubForm.Name = "Btn_SubForm";
			this.Btn_SubForm.Size = new System.Drawing.Size(132, 39);
			this.Btn_SubForm.TabIndex = 26;
			this.Btn_SubForm.Text = "サブフォーム";
			this.Btn_SubForm.UseVisualStyleBackColor = true;
			this.Btn_SubForm.Click += new System.EventHandler(this.Btn_SubForm_Click);
			// 
			// FormBtn
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.Btn_Hide);
			this.Controls.Add(this.Btn_SubForm);
			this.Name = "FormBtn";
			this.Size = new System.Drawing.Size(177, 44);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button Btn_Hide;
		private System.Windows.Forms.Button Btn_SubForm;
	}
}
