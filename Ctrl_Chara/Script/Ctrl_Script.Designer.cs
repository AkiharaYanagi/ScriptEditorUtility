namespace ScriptEditor
{
	partial class _Ctrl_Script
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
			this.Lbl_Pos = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.RB_TRG_SGL = new System.Windows.Forms.RadioButton();
			this.RB_TRG_GRP = new System.Windows.Forms.RadioButton();
			this.RB_TRG_ALL = new System.Windows.Forms.RadioButton();
			this.Tb_Img = new System.Windows.Forms.TextBox();
			this.Lbl_Img = new System.Windows.Forms.Label();
			this.Tb_Frame = new System.Windows.Forms.TextBox();
			this.Lbl_Frame = new System.Windows.Forms.Label();
			this.lbl_clc = new System.Windows.Forms.Label();
			this.Lbl_Vel = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// Lbl_Pos
			// 
			this.Lbl_Pos.AutoSize = true;
			this.Lbl_Pos.Location = new System.Drawing.Point(15, 120);
			this.Lbl_Pos.Name = "Lbl_Pos";
			this.Lbl_Pos.Size = new System.Drawing.Size(48, 12);
			this.Lbl_Pos.TabIndex = 1;
			this.Lbl_Pos.Text = "Pos(X,Y)";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.RB_TRG_SGL);
			this.groupBox1.Controls.Add(this.RB_TRG_GRP);
			this.groupBox1.Controls.Add(this.RB_TRG_ALL);
			this.groupBox1.Location = new System.Drawing.Point(17, 15);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(246, 46);
			this.groupBox1.TabIndex = 27;
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
			this.RB_TRG_SGL.CheckedChanged += new System.EventHandler(this.RB_TRG_SGL_CheckedChanged);
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
			this.RB_TRG_GRP.CheckedChanged += new System.EventHandler(this.RB_TRG_GRP_CheckedChanged);
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
			this.RB_TRG_ALL.CheckedChanged += new System.EventHandler(this.RB_TRG_ALL_CheckedChanged);
			// 
			// Tb_Img
			// 
			this.Tb_Img.Location = new System.Drawing.Point(211, 81);
			this.Tb_Img.Name = "Tb_Img";
			this.Tb_Img.ReadOnly = true;
			this.Tb_Img.Size = new System.Drawing.Size(90, 19);
			this.Tb_Img.TabIndex = 31;
			// 
			// Lbl_Img
			// 
			this.Lbl_Img.AutoSize = true;
			this.Lbl_Img.Location = new System.Drawing.Point(174, 84);
			this.Lbl_Img.Name = "Lbl_Img";
			this.Lbl_Img.Size = new System.Drawing.Size(23, 12);
			this.Lbl_Img.TabIndex = 30;
			this.Lbl_Img.Text = "Img";
			// 
			// Tb_Frame
			// 
			this.Tb_Frame.BackColor = System.Drawing.SystemColors.Control;
			this.Tb_Frame.Cursor = System.Windows.Forms.Cursors.Default;
			this.Tb_Frame.Location = new System.Drawing.Point(78, 81);
			this.Tb_Frame.Margin = new System.Windows.Forms.Padding(2);
			this.Tb_Frame.Name = "Tb_Frame";
			this.Tb_Frame.ReadOnly = true;
			this.Tb_Frame.Size = new System.Drawing.Size(30, 19);
			this.Tb_Frame.TabIndex = 29;
			// 
			// Lbl_Frame
			// 
			this.Lbl_Frame.AutoSize = true;
			this.Lbl_Frame.Location = new System.Drawing.Point(15, 84);
			this.Lbl_Frame.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.Lbl_Frame.Name = "Lbl_Frame";
			this.Lbl_Frame.Size = new System.Drawing.Size(37, 12);
			this.Lbl_Frame.TabIndex = 28;
			this.Lbl_Frame.Text = "Frame";
			// 
			// lbl_clc
			// 
			this.lbl_clc.AutoSize = true;
			this.lbl_clc.Location = new System.Drawing.Point(174, 117);
			this.lbl_clc.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lbl_clc.Name = "lbl_clc";
			this.lbl_clc.Size = new System.Drawing.Size(33, 12);
			this.lbl_clc.TabIndex = 32;
			this.lbl_clc.Text = "ClcSt";
			// 
			// Lbl_Vel
			// 
			this.Lbl_Vel.AutoSize = true;
			this.Lbl_Vel.Location = new System.Drawing.Point(15, 150);
			this.Lbl_Vel.Name = "Lbl_Vel";
			this.Lbl_Vel.Size = new System.Drawing.Size(46, 12);
			this.Lbl_Vel.TabIndex = 1;
			this.Lbl_Vel.Text = "Vel(X,Y)";
			// 
			// _Ctrl_Script
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lbl_clc);
			this.Controls.Add(this.Tb_Img);
			this.Controls.Add(this.Lbl_Img);
			this.Controls.Add(this.Tb_Frame);
			this.Controls.Add(this.Lbl_Frame);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.Lbl_Vel);
			this.Controls.Add(this.Lbl_Pos);
			this.Name = "_Ctrl_Script";
			this.Size = new System.Drawing.Size(429, 424);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label Lbl_Pos;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton RB_TRG_SGL;
		private System.Windows.Forms.RadioButton RB_TRG_GRP;
		private System.Windows.Forms.RadioButton RB_TRG_ALL;
		private System.Windows.Forms.TextBox Tb_Img;
		private System.Windows.Forms.Label Lbl_Img;
		public System.Windows.Forms.TextBox Tb_Frame;
		private System.Windows.Forms.Label Lbl_Frame;
		private System.Windows.Forms.Label lbl_clc;
		private System.Windows.Forms.Label Lbl_Vel;
	}
}
