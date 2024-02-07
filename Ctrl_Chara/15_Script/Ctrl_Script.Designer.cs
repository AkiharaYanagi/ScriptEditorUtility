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
			this.Tb_Img = new System.Windows.Forms.TextBox();
			this.Lbl_Img = new System.Windows.Forms.Label();
			this.Tb_Frame = new System.Windows.Forms.TextBox();
			this.Lbl_Frame = new System.Windows.Forms.Label();
			this.Lbl_clc = new System.Windows.Forms.Label();
			this.Lbl_Vel = new System.Windows.Forms.Label();
			this.Lbl_Power = new System.Windows.Forms.Label();
			this.Lbl_Acc = new System.Windows.Forms.Label();
			this.Lbl_Warp = new System.Windows.Forms.Label();
			this.Lbl_Recoil = new System.Windows.Forms.Label();
			this.Lbl_Balance = new System.Windows.Forms.Label();
			this.Lbl_Rotate = new System.Windows.Forms.Label();
			this.Lbl_Center = new System.Windows.Forms.Label();
			this.Lbl_BlackOut = new System.Windows.Forms.Label();
			this.Lbl_Vib = new System.Windows.Forms.Label();
			this.Lbl_Stop = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// Lbl_Pos
			// 
			this.Lbl_Pos.AutoSize = true;
			this.Lbl_Pos.Location = new System.Drawing.Point(3, 100);
			this.Lbl_Pos.Name = "Lbl_Pos";
			this.Lbl_Pos.Size = new System.Drawing.Size(48, 12);
			this.Lbl_Pos.TabIndex = 1;
			this.Lbl_Pos.Text = "Pos(X,Y)";
			// 
			// Tb_Img
			// 
			this.Tb_Img.Location = new System.Drawing.Point(263, 68);
			this.Tb_Img.Name = "Tb_Img";
			this.Tb_Img.ReadOnly = true;
			this.Tb_Img.Size = new System.Drawing.Size(90, 19);
			this.Tb_Img.TabIndex = 31;
			// 
			// Lbl_Img
			// 
			this.Lbl_Img.AutoSize = true;
			this.Lbl_Img.Location = new System.Drawing.Point(223, 71);
			this.Lbl_Img.Name = "Lbl_Img";
			this.Lbl_Img.Size = new System.Drawing.Size(23, 12);
			this.Lbl_Img.TabIndex = 30;
			this.Lbl_Img.Text = "Img";
			// 
			// Tb_Frame
			// 
			this.Tb_Frame.BackColor = System.Drawing.SystemColors.Control;
			this.Tb_Frame.Cursor = System.Windows.Forms.Cursors.Default;
			this.Tb_Frame.Location = new System.Drawing.Point(54, 64);
			this.Tb_Frame.Margin = new System.Windows.Forms.Padding(2);
			this.Tb_Frame.Name = "Tb_Frame";
			this.Tb_Frame.ReadOnly = true;
			this.Tb_Frame.Size = new System.Drawing.Size(30, 19);
			this.Tb_Frame.TabIndex = 29;
			// 
			// Lbl_Frame
			// 
			this.Lbl_Frame.AutoSize = true;
			this.Lbl_Frame.Location = new System.Drawing.Point(3, 67);
			this.Lbl_Frame.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.Lbl_Frame.Name = "Lbl_Frame";
			this.Lbl_Frame.Size = new System.Drawing.Size(37, 12);
			this.Lbl_Frame.TabIndex = 28;
			this.Lbl_Frame.Text = "Frame";
			// 
			// Lbl_clc
			// 
			this.Lbl_clc.AutoSize = true;
			this.Lbl_clc.Location = new System.Drawing.Point(223, 104);
			this.Lbl_clc.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.Lbl_clc.Name = "Lbl_clc";
			this.Lbl_clc.Size = new System.Drawing.Size(33, 12);
			this.Lbl_clc.TabIndex = 32;
			this.Lbl_clc.Text = "ClcSt";
			// 
			// Lbl_Vel
			// 
			this.Lbl_Vel.AutoSize = true;
			this.Lbl_Vel.Location = new System.Drawing.Point(3, 130);
			this.Lbl_Vel.Name = "Lbl_Vel";
			this.Lbl_Vel.Size = new System.Drawing.Size(68, 12);
			this.Lbl_Vel.TabIndex = 1;
			this.Lbl_Vel.Text = "Vel(X,Y)_0.1f";
			// 
			// Lbl_Power
			// 
			this.Lbl_Power.AutoSize = true;
			this.Lbl_Power.Location = new System.Drawing.Point(3, 190);
			this.Lbl_Power.Name = "Lbl_Power";
			this.Lbl_Power.Size = new System.Drawing.Size(36, 12);
			this.Lbl_Power.TabIndex = 1;
			this.Lbl_Power.Text = "Power";
			// 
			// Lbl_Acc
			// 
			this.Lbl_Acc.AutoSize = true;
			this.Lbl_Acc.Location = new System.Drawing.Point(3, 160);
			this.Lbl_Acc.Name = "Lbl_Acc";
			this.Lbl_Acc.Size = new System.Drawing.Size(71, 12);
			this.Lbl_Acc.TabIndex = 1;
			this.Lbl_Acc.Text = "Acc(X,Y)_0.1f";
			// 
			// Lbl_Warp
			// 
			this.Lbl_Warp.AutoSize = true;
			this.Lbl_Warp.Location = new System.Drawing.Point(3, 220);
			this.Lbl_Warp.Name = "Lbl_Warp";
			this.Lbl_Warp.Size = new System.Drawing.Size(30, 12);
			this.Lbl_Warp.TabIndex = 1;
			this.Lbl_Warp.Text = "Warp";
			// 
			// Lbl_Recoil
			// 
			this.Lbl_Recoil.AutoSize = true;
			this.Lbl_Recoil.Location = new System.Drawing.Point(3, 250);
			this.Lbl_Recoil.Name = "Lbl_Recoil";
			this.Lbl_Recoil.Size = new System.Drawing.Size(61, 12);
			this.Lbl_Recoil.TabIndex = 1;
			this.Lbl_Recoil.Text = "Recoil(I/E)";
			// 
			// Lbl_Balance
			// 
			this.Lbl_Balance.AutoSize = true;
			this.Lbl_Balance.Location = new System.Drawing.Point(3, 280);
			this.Lbl_Balance.Name = "Lbl_Balance";
			this.Lbl_Balance.Size = new System.Drawing.Size(70, 12);
			this.Lbl_Balance.TabIndex = 1;
			this.Lbl_Balance.Text = "Balance(I/E)";
			// 
			// Lbl_Rotate
			// 
			this.Lbl_Rotate.AutoSize = true;
			this.Lbl_Rotate.Location = new System.Drawing.Point(3, 310);
			this.Lbl_Rotate.Name = "Lbl_Rotate";
			this.Lbl_Rotate.Size = new System.Drawing.Size(39, 12);
			this.Lbl_Rotate.TabIndex = 33;
			this.Lbl_Rotate.Text = "Rotate";
			// 
			// Lbl_Center
			// 
			this.Lbl_Center.AutoSize = true;
			this.Lbl_Center.Location = new System.Drawing.Point(3, 340);
			this.Lbl_Center.Name = "Lbl_Center";
			this.Lbl_Center.Size = new System.Drawing.Size(63, 12);
			this.Lbl_Center.TabIndex = 33;
			this.Lbl_Center.Text = "Center(X,Y)";
			// 
			// Lbl_BlackOut
			// 
			this.Lbl_BlackOut.AutoSize = true;
			this.Lbl_BlackOut.Location = new System.Drawing.Point(209, 134);
			this.Lbl_BlackOut.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.Lbl_BlackOut.Name = "Lbl_BlackOut";
			this.Lbl_BlackOut.Size = new System.Drawing.Size(52, 12);
			this.Lbl_BlackOut.TabIndex = 32;
			this.Lbl_BlackOut.Text = "BlackOut";
			// 
			// Lbl_Vib
			// 
			this.Lbl_Vib.AutoSize = true;
			this.Lbl_Vib.Location = new System.Drawing.Point(234, 164);
			this.Lbl_Vib.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.Lbl_Vib.Name = "Lbl_Vib";
			this.Lbl_Vib.Size = new System.Drawing.Size(22, 12);
			this.Lbl_Vib.TabIndex = 32;
			this.Lbl_Vib.Text = "Vib";
			// 
			// Lbl_Stop
			// 
			this.Lbl_Stop.AutoSize = true;
			this.Lbl_Stop.Location = new System.Drawing.Point(234, 194);
			this.Lbl_Stop.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.Lbl_Stop.Name = "Lbl_Stop";
			this.Lbl_Stop.Size = new System.Drawing.Size(28, 12);
			this.Lbl_Stop.TabIndex = 32;
			this.Lbl_Stop.Text = "Stop";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(217, 224);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(39, 12);
			this.label1.TabIndex = 32;
			this.label1.Text = "AftImg";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(191, 254);
			this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(65, 12);
			this.label2.TabIndex = 32;
			this.label2.Text = "AftImgPitch";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(191, 284);
			this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(64, 12);
			this.label3.TabIndex = 32;
			this.label3.Text = "AftImgTime";
			// 
			// _Ctrl_Script
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.Lbl_Center);
			this.Controls.Add(this.Lbl_Rotate);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.Lbl_Stop);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.Lbl_Vib);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.Lbl_BlackOut);
			this.Controls.Add(this.Lbl_clc);
			this.Controls.Add(this.Tb_Img);
			this.Controls.Add(this.Lbl_Img);
			this.Controls.Add(this.Tb_Frame);
			this.Controls.Add(this.Lbl_Frame);
			this.Controls.Add(this.Lbl_Balance);
			this.Controls.Add(this.Lbl_Recoil);
			this.Controls.Add(this.Lbl_Warp);
			this.Controls.Add(this.Lbl_Acc);
			this.Controls.Add(this.Lbl_Power);
			this.Controls.Add(this.Lbl_Vel);
			this.Controls.Add(this.Lbl_Pos);
			this.Name = "_Ctrl_Script";
			this.Size = new System.Drawing.Size(429, 424);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label Lbl_Pos;
		private System.Windows.Forms.TextBox Tb_Img;
		private System.Windows.Forms.Label Lbl_Img;
		public System.Windows.Forms.TextBox Tb_Frame;
		private System.Windows.Forms.Label Lbl_Frame;
		private System.Windows.Forms.Label Lbl_clc;
		private System.Windows.Forms.Label Lbl_Vel;
		private System.Windows.Forms.Label Lbl_Power;
		private System.Windows.Forms.Label Lbl_Acc;
		private System.Windows.Forms.Label Lbl_Warp;
		private System.Windows.Forms.Label Lbl_Recoil;
		private System.Windows.Forms.Label Lbl_Balance;
		private System.Windows.Forms.Label Lbl_Rotate;
		private System.Windows.Forms.Label Lbl_Center;
		private System.Windows.Forms.Label Lbl_BlackOut;
		private System.Windows.Forms.Label Lbl_Vib;
		private System.Windows.Forms.Label Lbl_Stop;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
	}
}
