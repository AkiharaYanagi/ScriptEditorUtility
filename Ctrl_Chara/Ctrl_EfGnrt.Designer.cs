namespace ScriptEditor
{
	partial class _Ctrl_EfGnrt
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
			this.Tbn_x = new ScriptEditor.TB_Number();
			this.Tbn_y = new ScriptEditor.TB_Number();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.Tbn_z = new ScriptEditor.TB_Number();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.Cbx_gnrt = new System.Windows.Forms.CheckBox();
			this.Cbx_loop = new System.Windows.Forms.CheckBox();
			this.Cbx_sync = new System.Windows.Forms.CheckBox();
			this.label6 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// Tbn_x
			// 
			this.Tbn_x.Font = new System.Drawing.Font("MS UI Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Tbn_x.GetFunc = null;
			this.Tbn_x.Location = new System.Drawing.Point(248, 126);
			this.Tbn_x.Name = "Tbn_x";
			this.Tbn_x.SetFunc = null;
			this.Tbn_x.Size = new System.Drawing.Size(100, 26);
			this.Tbn_x.TabIndex = 0;
			this.Tbn_x.Text = "0";
			// 
			// Tbn_y
			// 
			this.Tbn_y.Font = new System.Drawing.Font("MS UI Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Tbn_y.GetFunc = null;
			this.Tbn_y.Location = new System.Drawing.Point(404, 126);
			this.Tbn_y.Name = "Tbn_y";
			this.Tbn_y.SetFunc = null;
			this.Tbn_y.Size = new System.Drawing.Size(100, 26);
			this.Tbn_y.TabIndex = 1;
			this.Tbn_y.Text = "0";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("MS UI Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label1.Location = new System.Drawing.Point(216, 128);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(18, 19);
			this.label1.TabIndex = 2;
			this.label1.Text = "x";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("MS UI Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label2.Location = new System.Drawing.Point(375, 126);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(18, 19);
			this.label2.TabIndex = 2;
			this.label2.Text = "y";
			// 
			// Tbn_z
			// 
			this.Tbn_z.Font = new System.Drawing.Font("MS UI Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Tbn_z.GetFunc = null;
			this.Tbn_z.Location = new System.Drawing.Point(547, 126);
			this.Tbn_z.Name = "Tbn_z";
			this.Tbn_z.SetFunc = null;
			this.Tbn_z.Size = new System.Drawing.Size(100, 26);
			this.Tbn_z.TabIndex = 1;
			this.Tbn_z.Text = "0";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("MS UI Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label3.Location = new System.Drawing.Point(518, 126);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(18, 19);
			this.label3.TabIndex = 2;
			this.label3.Text = "z";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label4.Location = new System.Drawing.Point(544, 195);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(122, 14);
			this.label4.TabIndex = 13;
			this.label4.Text = "0.10<-前 | 後->0.90";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label5.Location = new System.Drawing.Point(544, 171);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(131, 14);
			this.label5.TabIndex = 14;
			this.label5.Text = "実効Z値(float)は/100";
			// 
			// Cbx_gnrt
			// 
			this.Cbx_gnrt.AutoSize = true;
			this.Cbx_gnrt.Font = new System.Drawing.Font("MS UI Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Cbx_gnrt.Location = new System.Drawing.Point(261, 230);
			this.Cbx_gnrt.Name = "Cbx_gnrt";
			this.Cbx_gnrt.Size = new System.Drawing.Size(66, 23);
			this.Cbx_gnrt.TabIndex = 15;
			this.Cbx_gnrt.Text = "生成";
			this.Cbx_gnrt.UseVisualStyleBackColor = true;
			// 
			// Cbx_loop
			// 
			this.Cbx_loop.AutoSize = true;
			this.Cbx_loop.Font = new System.Drawing.Font("MS UI Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Cbx_loop.Location = new System.Drawing.Point(261, 270);
			this.Cbx_loop.Name = "Cbx_loop";
			this.Cbx_loop.Size = new System.Drawing.Size(74, 23);
			this.Cbx_loop.TabIndex = 15;
			this.Cbx_loop.Text = "ループ";
			this.Cbx_loop.UseVisualStyleBackColor = true;
			// 
			// Cbx_sync
			// 
			this.Cbx_sync.AutoSize = true;
			this.Cbx_sync.Font = new System.Drawing.Font("MS UI Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Cbx_sync.Location = new System.Drawing.Point(261, 310);
			this.Cbx_sync.Name = "Cbx_sync";
			this.Cbx_sync.Size = new System.Drawing.Size(66, 23);
			this.Cbx_sync.TabIndex = 15;
			this.Cbx_sync.Text = "同期";
			this.Cbx_sync.UseVisualStyleBackColor = true;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label6.Location = new System.Drawing.Point(566, 220);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(80, 14);
			this.label6.TabIndex = 13;
			this.label6.Text = "(0.50 : キャラ)";
			// 
			// _Ctrl_EfGnrt
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.Cbx_sync);
			this.Controls.Add(this.Cbx_loop);
			this.Controls.Add(this.Cbx_gnrt);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.Tbn_z);
			this.Controls.Add(this.Tbn_y);
			this.Controls.Add(this.Tbn_x);
			this.Name = "_Ctrl_EfGnrt";
			this.Size = new System.Drawing.Size(699, 564);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private TB_Number Tbn_x;
		private TB_Number Tbn_y;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private TB_Number Tbn_z;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox Cbx_gnrt;
		private System.Windows.Forms.CheckBox Cbx_loop;
		private System.Windows.Forms.CheckBox Cbx_sync;
		private System.Windows.Forms.Label label6;
	}
}
