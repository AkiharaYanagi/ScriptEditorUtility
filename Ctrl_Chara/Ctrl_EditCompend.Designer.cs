namespace Ctrl_Chara
{
	partial class Ctrl_EditCompend
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
            this.TB_SlctIndex = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TB_SlctGroup = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TB_SqcName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TB_GrpSpan = new System.Windows.Forms.TextBox();
            this.TB_SlctSpan = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TB_SlctIndex
            // 
            this.TB_SlctIndex.Location = new System.Drawing.Point(138, 61);
            this.TB_SlctIndex.Name = "TB_SlctIndex";
            this.TB_SlctIndex.Size = new System.Drawing.Size(43, 22);
            this.TB_SlctIndex.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "SelectedIndex";
            // 
            // TB_SlctGroup
            // 
            this.TB_SlctGroup.Location = new System.Drawing.Point(138, 100);
            this.TB_SlctGroup.Name = "TB_SlctGroup";
            this.TB_SlctGroup.Size = new System.Drawing.Size(43, 22);
            this.TB_SlctGroup.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "SelectedGroup";
            // 
            // TB_SqcName
            // 
            this.TB_SqcName.Location = new System.Drawing.Point(138, 17);
            this.TB_SqcName.Name = "TB_SqcName";
            this.TB_SqcName.Size = new System.Drawing.Size(170, 22);
            this.TB_SqcName.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "Sequence";
            // 
            // TB_GrpSpan
            // 
            this.TB_GrpSpan.Location = new System.Drawing.Point(342, 103);
            this.TB_GrpSpan.Name = "TB_GrpSpan";
            this.TB_GrpSpan.Size = new System.Drawing.Size(85, 22);
            this.TB_GrpSpan.TabIndex = 0;
            // 
            // TB_SlctSpan
            // 
            this.TB_SlctSpan.Location = new System.Drawing.Point(342, 61);
            this.TB_SlctSpan.Name = "TB_SlctSpan";
            this.TB_SlctSpan.Size = new System.Drawing.Size(85, 22);
            this.TB_SlctSpan.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(214, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 15);
            this.label4.TabIndex = 1;
            this.label4.Text = "SelectedSpan";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(214, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 15);
            this.label5.TabIndex = 1;
            this.label5.Text = "GroupSpan";
            // 
            // Ctrl_EditCompend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TB_SlctSpan);
            this.Controls.Add(this.TB_GrpSpan);
            this.Controls.Add(this.TB_SlctGroup);
            this.Controls.Add(this.TB_SqcName);
            this.Controls.Add(this.TB_SlctIndex);
            this.Name = "Ctrl_EditCompend";
            this.Size = new System.Drawing.Size(608, 455);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox TB_SlctIndex;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox TB_SlctGroup;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox TB_SqcName;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox TB_GrpSpan;
		private System.Windows.Forms.TextBox TB_SlctSpan;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
	}
}
