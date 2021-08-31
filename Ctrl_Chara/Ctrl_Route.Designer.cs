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
			this.Lbl_Branch = new System.Windows.Forms.Label();
			this.Tb_Summary = new System.Windows.Forms.TextBox();
			this.Cb_Branch = new System.Windows.Forms.ComboBox();
			this.Cb_Command = new System.Windows.Forms.ComboBox();
			this.Cb_Action = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
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
			// Lbl_Branch
			// 
			this.Lbl_Branch.AutoSize = true;
			this.Lbl_Branch.Location = new System.Drawing.Point(303, 3);
			this.Lbl_Branch.Name = "Lbl_Branch";
			this.Lbl_Branch.Size = new System.Drawing.Size(40, 12);
			this.Lbl_Branch.TabIndex = 1;
			this.Lbl_Branch.Text = "ブランチ";
			// 
			// Tb_Summary
			// 
			this.Tb_Summary.Location = new System.Drawing.Point(5, 18);
			this.Tb_Summary.Multiline = true;
			this.Tb_Summary.Name = "Tb_Summary";
			this.Tb_Summary.Size = new System.Drawing.Size(208, 47);
			this.Tb_Summary.TabIndex = 2;
			// 
			// Cb_Branch
			// 
			this.Cb_Branch.FormattingEnabled = true;
			this.Cb_Branch.Location = new System.Drawing.Point(303, 18);
			this.Cb_Branch.Name = "Cb_Branch";
			this.Cb_Branch.Size = new System.Drawing.Size(182, 20);
			this.Cb_Branch.TabIndex = 3;
			// 
			// Cb_Command
			// 
			this.Cb_Command.FormattingEnabled = true;
			this.Cb_Command.Location = new System.Drawing.Point(520, 88);
			this.Cb_Command.Name = "Cb_Command";
			this.Cb_Command.Size = new System.Drawing.Size(129, 20);
			this.Cb_Command.TabIndex = 4;
			// 
			// Cb_Action
			// 
			this.Cb_Action.FormattingEnabled = true;
			this.Cb_Action.Location = new System.Drawing.Point(520, 148);
			this.Cb_Action.Name = "Cb_Action";
			this.Cb_Action.Size = new System.Drawing.Size(129, 20);
			this.Cb_Action.TabIndex = 4;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(518, 73);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 12);
			this.label1.TabIndex = 5;
			this.label1.Text = "コマンド";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(518, 133);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(47, 12);
			this.label2.TabIndex = 5;
			this.label2.Text = "アクション";
			// 
			// Ctrl_Route
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.Cb_Action);
			this.Controls.Add(this.Cb_Command);
			this.Controls.Add(this.Cb_Branch);
			this.Controls.Add(this.Tb_Summary);
			this.Controls.Add(this.Lbl_Branch);
			this.Controls.Add(this.Lbl_Route);
			this.Name = "Ctrl_Route";
			this.Size = new System.Drawing.Size(806, 553);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label Lbl_Route;
		private System.Windows.Forms.Label Lbl_Branch;
		private System.Windows.Forms.TextBox Tb_Summary;
		private System.Windows.Forms.ComboBox Cb_Branch;
		private System.Windows.Forms.ComboBox Cb_Command;
		private System.Windows.Forms.ComboBox Cb_Action;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
	}
}
