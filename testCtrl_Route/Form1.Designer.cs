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
			if ( disposing && ( components != null ) )
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
			ScriptEditor.BindingDictionary<ScriptEditor.Branch> bindingDictionary_11 = new ScriptEditor.BindingDictionary<ScriptEditor.Branch>();
			ScriptEditor.BindingDictionary<ScriptEditor.Route> bindingDictionary_12 = new ScriptEditor.BindingDictionary<ScriptEditor.Route>();
			this.ctrl_Route1 = new ScriptEditor.Ctrl_Route();
			this.SuspendLayout();
			// 
			// ctrl_Route1
			// 
			this.ctrl_Route1.Location = new System.Drawing.Point(0, 0);
			this.ctrl_Route1.Name = "ctrl_Route1";
			this.ctrl_Route1.Size = new System.Drawing.Size(813, 528);
			this.ctrl_Route1.TabIndex = 0;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(811, 528);
			this.Controls.Add(this.ctrl_Route1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}

		#endregion

		private Ctrl_Route ctrl_Route1;
	}
}

