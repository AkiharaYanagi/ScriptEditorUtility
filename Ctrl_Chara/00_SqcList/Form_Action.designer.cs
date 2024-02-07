namespace ScriptEditor
{
	partial class Form_Action
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Btn_OK = new System.Windows.Forms.Button();
			this.Btn_Cancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// Btn_OK
			// 
			this.Btn_OK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.Btn_OK.Location = new System.Drawing.Point(298, 315);
			this.Btn_OK.Name = "Btn_OK";
			this.Btn_OK.Size = new System.Drawing.Size(118, 50);
			this.Btn_OK.TabIndex = 9;
			this.Btn_OK.Text = "OK";
			this.Btn_OK.UseVisualStyleBackColor = false;
			this.Btn_OK.Click += new System.EventHandler(this.Btn_OK_Click);
			// 
			// Btn_Cancel
			// 
			this.Btn_Cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.Btn_Cancel.Location = new System.Drawing.Point(189, 339);
			this.Btn_Cancel.Name = "Btn_Cancel";
			this.Btn_Cancel.Size = new System.Drawing.Size(88, 25);
			this.Btn_Cancel.TabIndex = 10;
			this.Btn_Cancel.Text = "Cancel";
			this.Btn_Cancel.UseVisualStyleBackColor = false;
			this.Btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
			// 
			// Form_Action
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(425, 370);
			this.Controls.Add(this.Btn_Cancel);
			this.Controls.Add(this.Btn_OK);
			this.Name = "Form_Action";
			this.Text = "シークエンス詳細";
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Button Btn_OK;
		private System.Windows.Forms.Button Btn_Cancel;
	}
}