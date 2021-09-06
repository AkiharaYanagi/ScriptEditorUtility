namespace ScriptEditor
{
	partial class Ctrl_Route
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

		#region コンポーネント デザイナーで生成されたコード

		/// <summary> 
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent ()
		{
			this.Lbl_Route = new System.Windows.Forms.Label();
			this.Tb_Summary = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.CB_Branch = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// Lbl_Route
			// 
			this.Lbl_Route.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Lbl_Route.AutoSize = true;
			this.Lbl_Route.Location = new System.Drawing.Point(3, 3);
			this.Lbl_Route.Name = "Lbl_Route";
			this.Lbl_Route.Size = new System.Drawing.Size(33, 12);
			this.Lbl_Route.TabIndex = 0;
			this.Lbl_Route.Text = "ルート";
			// 
			// Tb_Summary
			// 
			this.Tb_Summary.Location = new System.Drawing.Point(5, 18);
			this.Tb_Summary.Multiline = true;
			this.Tb_Summary.Name = "Tb_Summary";
			this.Tb_Summary.Size = new System.Drawing.Size(208, 47);
			this.Tb_Summary.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(302, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "ブランチ";
			// 
			// CB_Branch
			// 
			this.CB_Branch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CB_Branch.FormattingEnabled = true;
			this.CB_Branch.Location = new System.Drawing.Point(558, 86);
			this.CB_Branch.Name = "CB_Branch";
			this.CB_Branch.Size = new System.Drawing.Size(184, 20);
			this.CB_Branch.TabIndex = 3;
			this.CB_Branch.SelectionChangeCommitted += new System.EventHandler(this.CB_Branch_SelectionChangeCommitted);
			// 
			// Ctrl_Route
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.CB_Branch);
			this.Controls.Add(this.Tb_Summary);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.Lbl_Route);
			this.Name = "Ctrl_Route";
			this.Size = new System.Drawing.Size(893, 553);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label Lbl_Route;
		private System.Windows.Forms.TextBox Tb_Summary;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox CB_Branch;
	}
}
