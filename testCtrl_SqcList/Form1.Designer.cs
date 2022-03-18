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
			this.ctrl_SqcList1 = new ScriptEditor.Ctrl_SqcList();
			this.SuspendLayout();
			// 
			// ctrl_SqcList1
			// 
			this.ctrl_SqcList1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ctrl_SqcList1.Location = new System.Drawing.Point(0, 0);
			this.ctrl_SqcList1.Name = "ctrl_SqcList1";
			this.ctrl_SqcList1.Size = new System.Drawing.Size(806, 562);
			this.ctrl_SqcList1.TabIndex = 0;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(806, 562);
			this.Controls.Add(this.ctrl_SqcList1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}

		#endregion

		private Ctrl_SqcList ctrl_SqcList1;
	}
}

