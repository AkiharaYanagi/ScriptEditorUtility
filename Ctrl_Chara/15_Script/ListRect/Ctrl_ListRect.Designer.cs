namespace ScriptEditor
{
	partial class Ctrl_ListRect
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
			this.Btn_Del = new System.Windows.Forms.Button();
			this.Btn_Add = new System.Windows.Forms.Button();
			this.Lbl_H = new System.Windows.Forms.Label();
			this.Lbl_W = new System.Windows.Forms.Label();
			this.Lbl_Y = new System.Windows.Forms.Label();
			this.Lbl_X = new System.Windows.Forms.Label();
			this.Tbn_H = new ScriptEditor.TB_Number();
			this.Tbn_Y = new ScriptEditor.TB_Number();
			this.Tbn_W = new ScriptEditor.TB_Number();
			this.Tbn_X = new ScriptEditor.TB_Number();
			this.Lbl_RectName = new System.Windows.Forms.Label();
			this.Pb_Num = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.Pb_Num)).BeginInit();
			this.SuspendLayout();
			// 
			// Btn_Del
			// 
			this.Btn_Del.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.Btn_Del.Location = new System.Drawing.Point(67, 5);
			this.Btn_Del.Name = "Btn_Del";
			this.Btn_Del.Size = new System.Drawing.Size(44, 22);
			this.Btn_Del.TabIndex = 16;
			this.Btn_Del.Text = "削除";
			this.Btn_Del.UseVisualStyleBackColor = false;
			this.Btn_Del.Click += new System.EventHandler(this.Btn_Del_Click);
			// 
			// Btn_Add
			// 
			this.Btn_Add.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.Btn_Add.Location = new System.Drawing.Point(283, 5);
			this.Btn_Add.Name = "Btn_Add";
			this.Btn_Add.Size = new System.Drawing.Size(44, 22);
			this.Btn_Add.TabIndex = 17;
			this.Btn_Add.Text = "追加";
			this.Btn_Add.UseVisualStyleBackColor = false;
			this.Btn_Add.Click += new System.EventHandler(this.Btn_Add_Click);
			// 
			// Lbl_H
			// 
			this.Lbl_H.AutoSize = true;
			this.Lbl_H.Location = new System.Drawing.Point(275, 36);
			this.Lbl_H.Name = "Lbl_H";
			this.Lbl_H.Size = new System.Drawing.Size(13, 12);
			this.Lbl_H.TabIndex = 12;
			this.Lbl_H.Text = "H";
			// 
			// Lbl_W
			// 
			this.Lbl_W.AutoSize = true;
			this.Lbl_W.Location = new System.Drawing.Point(192, 36);
			this.Lbl_W.Name = "Lbl_W";
			this.Lbl_W.Size = new System.Drawing.Size(14, 12);
			this.Lbl_W.TabIndex = 13;
			this.Lbl_W.Text = "W";
			// 
			// Lbl_Y
			// 
			this.Lbl_Y.AutoSize = true;
			this.Lbl_Y.Location = new System.Drawing.Point(96, 37);
			this.Lbl_Y.Name = "Lbl_Y";
			this.Lbl_Y.Size = new System.Drawing.Size(12, 12);
			this.Lbl_Y.TabIndex = 14;
			this.Lbl_Y.Text = "Y";
			// 
			// Lbl_X
			// 
			this.Lbl_X.AutoSize = true;
			this.Lbl_X.Location = new System.Drawing.Point(4, 37);
			this.Lbl_X.Name = "Lbl_X";
			this.Lbl_X.Size = new System.Drawing.Size(12, 12);
			this.Lbl_X.TabIndex = 15;
			this.Lbl_X.Text = "X";
			// 
			// Tbn_H
			// 
			this.Tbn_H.GetFunc = null;
			this.Tbn_H.Location = new System.Drawing.Point(295, 33);
			this.Tbn_H.Name = "Tbn_H";
			this.Tbn_H.SetFunc = null;
			this.Tbn_H.Size = new System.Drawing.Size(49, 19);
			this.Tbn_H.TabIndex = 8;
			this.Tbn_H.Text = "0";
			// 
			// Tbn_Y
			// 
			this.Tbn_Y.GetFunc = null;
			this.Tbn_Y.Location = new System.Drawing.Point(114, 34);
			this.Tbn_Y.Name = "Tbn_Y";
			this.Tbn_Y.SetFunc = null;
			this.Tbn_Y.Size = new System.Drawing.Size(53, 19);
			this.Tbn_Y.TabIndex = 9;
			this.Tbn_Y.Text = "0";
			// 
			// Tbn_W
			// 
			this.Tbn_W.GetFunc = null;
			this.Tbn_W.Location = new System.Drawing.Point(212, 33);
			this.Tbn_W.Name = "Tbn_W";
			this.Tbn_W.SetFunc = null;
			this.Tbn_W.Size = new System.Drawing.Size(49, 19);
			this.Tbn_W.TabIndex = 10;
			this.Tbn_W.Text = "0";
			// 
			// Tbn_X
			// 
			this.Tbn_X.GetFunc = null;
			this.Tbn_X.Location = new System.Drawing.Point(22, 34);
			this.Tbn_X.Name = "Tbn_X";
			this.Tbn_X.SetFunc = null;
			this.Tbn_X.Size = new System.Drawing.Size(53, 19);
			this.Tbn_X.TabIndex = 11;
			this.Tbn_X.Text = "0";
			// 
			// Lbl_RectName
			// 
			this.Lbl_RectName.AutoSize = true;
			this.Lbl_RectName.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Lbl_RectName.Location = new System.Drawing.Point(3, 10);
			this.Lbl_RectName.Name = "Lbl_RectName";
			this.Lbl_RectName.Size = new System.Drawing.Size(40, 16);
			this.Lbl_RectName.TabIndex = 7;
			this.Lbl_RectName.Text = "Rect";
			// 
			// Pb_Num
			// 
			this.Pb_Num.BackColor = System.Drawing.SystemColors.ControlDark;
			this.Pb_Num.Location = new System.Drawing.Point(117, 5);
			this.Pb_Num.Name = "Pb_Num";
			this.Pb_Num.Size = new System.Drawing.Size(160, 20);
			this.Pb_Num.TabIndex = 18;
			this.Pb_Num.TabStop = false;
			this.Pb_Num.Paint += new System.Windows.Forms.PaintEventHandler(this.Pb_Num_Paint);
			this.Pb_Num.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Pb_Num_MouseClick);
			// 
			// Ctrl_ListRect
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.Pb_Num);
			this.Controls.Add(this.Btn_Del);
			this.Controls.Add(this.Btn_Add);
			this.Controls.Add(this.Lbl_H);
			this.Controls.Add(this.Lbl_W);
			this.Controls.Add(this.Lbl_Y);
			this.Controls.Add(this.Lbl_X);
			this.Controls.Add(this.Tbn_H);
			this.Controls.Add(this.Tbn_Y);
			this.Controls.Add(this.Tbn_W);
			this.Controls.Add(this.Tbn_X);
			this.Controls.Add(this.Lbl_RectName);
			this.Name = "Ctrl_ListRect";
			this.Size = new System.Drawing.Size(354, 57);
			this.Click += new System.EventHandler(this.Ctrl_ListRect_Click);
			((System.ComponentModel.ISupportInitialize)(this.Pb_Num)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button Btn_Del;
		private System.Windows.Forms.Button Btn_Add;
		private System.Windows.Forms.Label Lbl_H;
		private System.Windows.Forms.Label Lbl_W;
		private System.Windows.Forms.Label Lbl_Y;
		private System.Windows.Forms.Label Lbl_X;
		private ScriptEditor.TB_Number Tbn_H;
		private ScriptEditor.TB_Number Tbn_Y;
		private ScriptEditor.TB_Number Tbn_W;
		private ScriptEditor.TB_Number Tbn_X;
		private System.Windows.Forms.Label Lbl_RectName;
		private System.Windows.Forms.PictureBox Pb_Num;
	}
}
