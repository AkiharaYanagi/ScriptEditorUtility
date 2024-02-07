namespace ScriptEditor
{
	partial class RB_ScriptTarget
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

		#region コンポーネント デザイナーで生成されたコード

		/// <summary> 
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent ()
		{
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.RB_TRG_SGL = new System.Windows.Forms.RadioButton();
			this.RB_TRG_GRP = new System.Windows.Forms.RadioButton();
			this.RB_TRG_ALL = new System.Windows.Forms.RadioButton();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.RB_TRG_SGL);
			this.groupBox1.Controls.Add(this.RB_TRG_GRP);
			this.groupBox1.Controls.Add(this.RB_TRG_ALL);
			this.groupBox1.Location = new System.Drawing.Point(3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(246, 46);
			this.groupBox1.TabIndex = 28;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "対象";
			// 
			// RB_TRG_SGL
			// 
			this.RB_TRG_SGL.AutoSize = true;
			this.RB_TRG_SGL.Checked = true;
			this.RB_TRG_SGL.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.RB_TRG_SGL.Location = new System.Drawing.Point(170, 18);
			this.RB_TRG_SGL.Name = "RB_TRG_SGL";
			this.RB_TRG_SGL.Size = new System.Drawing.Size(58, 20);
			this.RB_TRG_SGL.TabIndex = 25;
			this.RB_TRG_SGL.TabStop = true;
			this.RB_TRG_SGL.Text = "単体";
			this.RB_TRG_SGL.UseVisualStyleBackColor = true;
			// 
			// RB_TRG_GRP
			// 
			this.RB_TRG_GRP.AutoSize = true;
			this.RB_TRG_GRP.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.RB_TRG_GRP.Location = new System.Drawing.Point(88, 18);
			this.RB_TRG_GRP.Name = "RB_TRG_GRP";
			this.RB_TRG_GRP.Size = new System.Drawing.Size(76, 20);
			this.RB_TRG_GRP.TabIndex = 24;
			this.RB_TRG_GRP.Text = "グループ";
			this.RB_TRG_GRP.UseVisualStyleBackColor = true;
			// 
			// RB_TRG_ALL
			// 
			this.RB_TRG_ALL.AutoSize = true;
			this.RB_TRG_ALL.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.RB_TRG_ALL.Location = new System.Drawing.Point(23, 18);
			this.RB_TRG_ALL.Name = "RB_TRG_ALL";
			this.RB_TRG_ALL.Size = new System.Drawing.Size(58, 20);
			this.RB_TRG_ALL.TabIndex = 23;
			this.RB_TRG_ALL.Text = "全体";
			this.RB_TRG_ALL.UseVisualStyleBackColor = false;
			// 
			// RB_ScriptTarget
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox1);
			this.Name = "RB_ScriptTarget";
			this.Size = new System.Drawing.Size(257, 58);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton RB_TRG_SGL;
		private System.Windows.Forms.RadioButton RB_TRG_GRP;
		private System.Windows.Forms.RadioButton RB_TRG_ALL;
	}
}
