namespace ScriptEditor
{
	partial class Menu_Board
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
			this.Btn_Del = new System.Windows.Forms.Button();
			this.Btn_Add = new System.Windows.Forms.Button();
			this.Btn_Ins = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.Btn_MltDel = new System.Windows.Forms.Button();
			this.Btn_MltAdd = new System.Windows.Forms.Button();
			this.Btn_MltIns = new System.Windows.Forms.Button();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.BtnUnify = new System.Windows.Forms.Button();
			this.BtnGrpDismantle = new System.Windows.Forms.Button();
			this.Btn_GrpMake = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.Btn_MltCpy = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.Btn_Del);
			this.groupBox1.Controls.Add(this.Btn_Add);
			this.groupBox1.Controls.Add(this.Btn_Ins);
			this.groupBox1.Location = new System.Drawing.Point(3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(186, 52);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "スクリプト単体";
			// 
			// Btn_Del
			// 
			this.Btn_Del.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.Btn_Del.Location = new System.Drawing.Point(122, 17);
			this.Btn_Del.Name = "Btn_Del";
			this.Btn_Del.Size = new System.Drawing.Size(48, 29);
			this.Btn_Del.TabIndex = 0;
			this.Btn_Del.Text = "削除";
			this.Btn_Del.UseVisualStyleBackColor = false;
			this.Btn_Del.Click += new System.EventHandler(this.Btn_Del_Click);
			// 
			// Btn_Add
			// 
			this.Btn_Add.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.Btn_Add.Location = new System.Drawing.Point(68, 17);
			this.Btn_Add.Name = "Btn_Add";
			this.Btn_Add.Size = new System.Drawing.Size(48, 29);
			this.Btn_Add.TabIndex = 0;
			this.Btn_Add.Text = "追加";
			this.Btn_Add.UseVisualStyleBackColor = false;
			this.Btn_Add.Click += new System.EventHandler(this.Btn_Add_Click);
			// 
			// Btn_Ins
			// 
			this.Btn_Ins.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.Btn_Ins.Location = new System.Drawing.Point(14, 17);
			this.Btn_Ins.Name = "Btn_Ins";
			this.Btn_Ins.Size = new System.Drawing.Size(48, 29);
			this.Btn_Ins.TabIndex = 0;
			this.Btn_Ins.Text = "挿入";
			this.Btn_Ins.UseVisualStyleBackColor = false;
			this.Btn_Ins.Click += new System.EventHandler(this.Btn_Ins_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.Btn_MltDel);
			this.groupBox2.Controls.Add(this.Btn_MltAdd);
			this.groupBox2.Controls.Add(this.Btn_MltCpy);
			this.groupBox2.Controls.Add(this.Btn_MltIns);
			this.groupBox2.Location = new System.Drawing.Point(195, 3);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(234, 52);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "複数スクリプト";
			// 
			// Btn_MltDel
			// 
			this.Btn_MltDel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.Btn_MltDel.Location = new System.Drawing.Point(176, 17);
			this.Btn_MltDel.Name = "Btn_MltDel";
			this.Btn_MltDel.Size = new System.Drawing.Size(48, 29);
			this.Btn_MltDel.TabIndex = 0;
			this.Btn_MltDel.Text = "削除";
			this.Btn_MltDel.UseVisualStyleBackColor = false;
			this.Btn_MltDel.Click += new System.EventHandler(this.Btn_MltDel_Click);
			// 
			// Btn_MltAdd
			// 
			this.Btn_MltAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.Btn_MltAdd.Location = new System.Drawing.Point(122, 17);
			this.Btn_MltAdd.Name = "Btn_MltAdd";
			this.Btn_MltAdd.Size = new System.Drawing.Size(48, 29);
			this.Btn_MltAdd.TabIndex = 0;
			this.Btn_MltAdd.Text = "追加";
			this.Btn_MltAdd.UseVisualStyleBackColor = false;
			this.Btn_MltAdd.Click += new System.EventHandler(this.Btn_MltAdd_Click);
			// 
			// Btn_MltIns
			// 
			this.Btn_MltIns.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.Btn_MltIns.Location = new System.Drawing.Point(14, 17);
			this.Btn_MltIns.Name = "Btn_MltIns";
			this.Btn_MltIns.Size = new System.Drawing.Size(48, 29);
			this.Btn_MltIns.TabIndex = 0;
			this.Btn_MltIns.Text = "挿入";
			this.Btn_MltIns.UseVisualStyleBackColor = false;
			this.Btn_MltIns.Click += new System.EventHandler(this.Btn_MltIns_Click);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.BtnUnify);
			this.groupBox3.Controls.Add(this.BtnGrpDismantle);
			this.groupBox3.Controls.Add(this.Btn_GrpMake);
			this.groupBox3.Location = new System.Drawing.Point(443, 3);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(186, 52);
			this.groupBox3.TabIndex = 0;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "グループ";
			// 
			// BtnUnify
			// 
			this.BtnUnify.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
			this.BtnUnify.Location = new System.Drawing.Point(122, 17);
			this.BtnUnify.Name = "BtnUnify";
			this.BtnUnify.Size = new System.Drawing.Size(58, 29);
			this.BtnUnify.TabIndex = 0;
			this.BtnUnify.Text = "同一化";
			this.BtnUnify.UseVisualStyleBackColor = false;
			this.BtnUnify.Click += new System.EventHandler(this.BtnUnify_Click);
			// 
			// BtnGrpDismantle
			// 
			this.BtnGrpDismantle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
			this.BtnGrpDismantle.Location = new System.Drawing.Point(68, 17);
			this.BtnGrpDismantle.Name = "BtnGrpDismantle";
			this.BtnGrpDismantle.Size = new System.Drawing.Size(48, 29);
			this.BtnGrpDismantle.TabIndex = 0;
			this.BtnGrpDismantle.Text = "解除";
			this.BtnGrpDismantle.UseVisualStyleBackColor = false;
			this.BtnGrpDismantle.Click += new System.EventHandler(this.BtnGrpDismantle_Click);
			// 
			// Btn_GrpMake
			// 
			this.Btn_GrpMake.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.Btn_GrpMake.Location = new System.Drawing.Point(14, 17);
			this.Btn_GrpMake.Name = "Btn_GrpMake";
			this.Btn_GrpMake.Size = new System.Drawing.Size(48, 29);
			this.Btn_GrpMake.TabIndex = 0;
			this.Btn_GrpMake.Text = "作成";
			this.Btn_GrpMake.UseVisualStyleBackColor = false;
			this.Btn_GrpMake.Click += new System.EventHandler(this.Btn_GrpMake_Click);
			// 
			// label1
			// 
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label1.Location = new System.Drawing.Point(435, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(2, 52);
			this.label1.TabIndex = 1;
			// 
			// Btn_MltCpy
			// 
			this.Btn_MltCpy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
			this.Btn_MltCpy.Location = new System.Drawing.Point(68, 17);
			this.Btn_MltCpy.Name = "Btn_MltCpy";
			this.Btn_MltCpy.Size = new System.Drawing.Size(48, 29);
			this.Btn_MltCpy.TabIndex = 0;
			this.Btn_MltCpy.Text = "複製";
			this.Btn_MltCpy.UseVisualStyleBackColor = false;
			this.Btn_MltCpy.Click += new System.EventHandler(this.Btn_MltCpy_Click);
			// 
			// Menu_Board
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.label1);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "Menu_Board";
			this.Size = new System.Drawing.Size(642, 62);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button Btn_Del;
		private System.Windows.Forms.Button Btn_Add;
		private System.Windows.Forms.Button Btn_Ins;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button Btn_MltDel;
		private System.Windows.Forms.Button Btn_MltAdd;
		private System.Windows.Forms.Button Btn_MltIns;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Button BtnUnify;
		private System.Windows.Forms.Button BtnGrpDismantle;
		private System.Windows.Forms.Button Btn_GrpMake;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button Btn_MltCpy;
	}
}
