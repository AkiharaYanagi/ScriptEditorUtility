namespace ScriptEditor
{
	partial class Form_Script
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
			this.ctrl_Script1 = new ScriptEditor._Ctrl_Script();
			this.SuspendLayout();
			// 
			// ctrl_Script1
			// 
			this.ctrl_Script1.Location = new System.Drawing.Point(12, 12);
			this.ctrl_Script1.Name = "ctrl_Script1";
			this.ctrl_Script1.Size = new System.Drawing.Size(500, 447);
			this.ctrl_Script1.TabIndex = 0;
			// 
			// Form_Script
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(549, 480);
			this.Controls.Add(this.ctrl_Script1);
			this.Name = "Form_Script";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}

		#endregion

		private _Ctrl_Script ctrl_Script1;
	}
}

