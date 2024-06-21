namespace ScriptEditor
{
	partial class Ctrl_ScriptList
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Btn_Load = new System.Windows.Forms.Button();
            this.Btn_Save = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Btn_Ins = new System.Windows.Forms.Button();
            this.Btn_Add = new System.Windows.Forms.Button();
            this.Btn_Replace = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Btn_AllCpy = new System.Windows.Forms.Button();
            this.Btn_Copy = new System.Windows.Forms.Button();
            this.Btn_GrpAdd = new System.Windows.Forms.Button();
            this.Btn_GrpDel = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Btn_Load);
            this.groupBox2.Controls.Add(this.Btn_Save);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(290, 74);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "スクリプトリスト を テキストファイル に書出";
            // 
            // Btn_Load
            // 
            this.Btn_Load.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Btn_Load.Location = new System.Drawing.Point(153, 24);
            this.Btn_Load.Name = "Btn_Load";
            this.Btn_Load.Size = new System.Drawing.Size(119, 31);
            this.Btn_Load.TabIndex = 1;
            this.Btn_Load.Text = "読込";
            this.Btn_Load.UseVisualStyleBackColor = false;
            this.Btn_Load.Click += new System.EventHandler(this.Btn_Load_Click);
            // 
            // Btn_Save
            // 
            this.Btn_Save.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Btn_Save.Location = new System.Drawing.Point(16, 24);
            this.Btn_Save.Name = "Btn_Save";
            this.Btn_Save.Size = new System.Drawing.Size(131, 31);
            this.Btn_Save.TabIndex = 1;
            this.Btn_Save.Text = "保存";
            this.Btn_Save.UseVisualStyleBackColor = false;
            this.Btn_Save.Click += new System.EventHandler(this.Btn_Save_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Btn_Ins);
            this.groupBox1.Controls.Add(this.Btn_Add);
            this.groupBox1.Controls.Add(this.Btn_Replace);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.Btn_AllCpy);
            this.groupBox1.Controls.Add(this.Btn_Copy);
            this.groupBox1.Location = new System.Drawing.Point(3, 83);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(290, 233);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "複製";
            // 
            // Btn_Ins
            // 
            this.Btn_Ins.Location = new System.Drawing.Point(160, 167);
            this.Btn_Ins.Name = "Btn_Ins";
            this.Btn_Ins.Size = new System.Drawing.Size(119, 32);
            this.Btn_Ins.TabIndex = 4;
            this.Btn_Ins.Text = "挿入";
            this.Btn_Ins.UseVisualStyleBackColor = true;
            this.Btn_Ins.Click += new System.EventHandler(this.Btn_Ins_Click);
            // 
            // Btn_Add
            // 
            this.Btn_Add.Location = new System.Drawing.Point(160, 129);
            this.Btn_Add.Name = "Btn_Add";
            this.Btn_Add.Size = new System.Drawing.Size(119, 32);
            this.Btn_Add.TabIndex = 4;
            this.Btn_Add.Text = "追加";
            this.Btn_Add.UseVisualStyleBackColor = true;
            this.Btn_Add.Click += new System.EventHandler(this.Btn_Add_Click);
            // 
            // Btn_Replace
            // 
            this.Btn_Replace.Location = new System.Drawing.Point(16, 129);
            this.Btn_Replace.Name = "Btn_Replace";
            this.Btn_Replace.Size = new System.Drawing.Size(119, 32);
            this.Btn_Replace.TabIndex = 3;
            this.Btn_Replace.Text = "全部を置換";
            this.Btn_Replace.UseVisualStyleBackColor = true;
            this.Btn_Replace.Click += new System.EventHandler(this.Btn_Replace_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(16, 76);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(263, 19);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "Name[Num]";
            // 
            // Btn_AllCpy
            // 
            this.Btn_AllCpy.Location = new System.Drawing.Point(160, 23);
            this.Btn_AllCpy.Name = "Btn_AllCpy";
            this.Btn_AllCpy.Size = new System.Drawing.Size(119, 31);
            this.Btn_AllCpy.TabIndex = 1;
            this.Btn_AllCpy.Text = "全部をコピー↓";
            this.Btn_AllCpy.UseVisualStyleBackColor = true;
            this.Btn_AllCpy.Click += new System.EventHandler(this.Btn_AllCpy_Click);
            // 
            // Btn_Copy
            // 
            this.Btn_Copy.Location = new System.Drawing.Point(16, 23);
            this.Btn_Copy.Name = "Btn_Copy";
            this.Btn_Copy.Size = new System.Drawing.Size(119, 31);
            this.Btn_Copy.TabIndex = 1;
            this.Btn_Copy.Text = "選択部分をコピー↓";
            this.Btn_Copy.UseVisualStyleBackColor = true;
            this.Btn_Copy.Click += new System.EventHandler(this.Btn_Copy_Click);
            // 
            // Btn_GrpAdd
            // 
            this.Btn_GrpAdd.Location = new System.Drawing.Point(312, 12);
            this.Btn_GrpAdd.Name = "Btn_GrpAdd";
            this.Btn_GrpAdd.Size = new System.Drawing.Size(103, 30);
            this.Btn_GrpAdd.TabIndex = 7;
            this.Btn_GrpAdd.Text = "各グループに＋１";
            this.Btn_GrpAdd.UseVisualStyleBackColor = true;
            this.Btn_GrpAdd.Click += new System.EventHandler(this.Btn_GrpAdd_Click);
            // 
            // Btn_GrpDel
            // 
            this.Btn_GrpDel.Location = new System.Drawing.Point(312, 48);
            this.Btn_GrpDel.Name = "Btn_GrpDel";
            this.Btn_GrpDel.Size = new System.Drawing.Size(103, 30);
            this.Btn_GrpDel.TabIndex = 7;
            this.Btn_GrpDel.Text = "各グループに-１";
            this.Btn_GrpDel.UseVisualStyleBackColor = true;
            this.Btn_GrpDel.Click += new System.EventHandler(this.Btn_GrpDel_Click);
            // 
            // Ctrl_ScriptList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Btn_GrpDel);
            this.Controls.Add(this.Btn_GrpAdd);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Ctrl_ScriptList";
            this.Size = new System.Drawing.Size(422, 321);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button Btn_Load;
		private System.Windows.Forms.Button Btn_Save;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button Btn_Ins;
		private System.Windows.Forms.Button Btn_Add;
		private System.Windows.Forms.Button Btn_Replace;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button Btn_AllCpy;
		private System.Windows.Forms.Button Btn_Copy;
		private System.Windows.Forms.Button Btn_GrpAdd;
		private System.Windows.Forms.Button Btn_GrpDel;
	}
}
