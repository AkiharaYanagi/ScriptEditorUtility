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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.tB_Setter1 = new ScriptEditor.TB_Setter();
			this.tB_Number1 = new ScriptEditor.TB_Number();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(20, 63);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(39, 12);
			this.label1.TabIndex = 2;
			this.label1.Text = "カテゴリ";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(20, 107);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(70, 12);
			this.label2.TabIndex = 3;
			this.label2.Text = "スクリプト個数";
			// 
			// comboBox1
			// 
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(101, 55);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(121, 23);
			this.comboBox1.TabIndex = 6;
			this.comboBox1.SelectionChangeCommitted += new System.EventHandler(this.comboBox1_SelectionChangeCommitted);
			// 
			// tB_Setter1
			// 
			this.tB_Setter1.GetFunc = null;
			this.tB_Setter1.Location = new System.Drawing.Point(22, 12);
			this.tB_Setter1.Name = "tB_Setter1";
			this.tB_Setter1.SetFunc = null;
			this.tB_Setter1.Size = new System.Drawing.Size(159, 19);
			this.tB_Setter1.TabIndex = 7;
			this.tB_Setter1.Text = "0";
			// 
			// tB_Number1
			// 
			this.tB_Number1.GetFunc = null;
			this.tB_Number1.Location = new System.Drawing.Point(110, 100);
			this.tB_Number1.Name = "tB_Number1";
			this.tB_Number1.SetFunc = null;
			this.tB_Number1.Size = new System.Drawing.Size(112, 19);
			this.tB_Number1.TabIndex = 8;
			this.tB_Number1.Text = "0";
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.button1.Location = new System.Drawing.Point(259, 218);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(118, 50);
			this.button1.TabIndex = 9;
			this.button1.Text = "OK";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.button2.Location = new System.Drawing.Point(150, 242);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(88, 25);
			this.button2.TabIndex = 10;
			this.button2.Text = "Cancel";
			this.button2.UseVisualStyleBackColor = false;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// Form_Action
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(386, 280);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.tB_Number1);
			this.Controls.Add(this.tB_Setter1);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "Form_Action";
			this.Text = "シークエンス詳細";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboBox1;
		public TB_Setter tB_Setter1;
		public TB_Number tB_Number1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
	}
}