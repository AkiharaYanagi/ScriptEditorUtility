﻿namespace ScriptEditor
{
	partial class Ctrl_Branch
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ctrl_Branch));
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.Cb_Action = new System.Windows.Forms.ComboBox();
			this.Cb_Command = new System.Windows.Forms.ComboBox();
			this.Tb_Name = new System.Windows.Forms.TextBox();
			this.editListbox_01 = new ScriptEditorUtility.EditListbox_0();
			this.SuspendLayout();
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(628, 62);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(17, 12);
			this.label3.TabIndex = 20;
			this.label3.Text = "→";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(649, 42);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(38, 12);
			this.label2.TabIndex = 19;
			this.label2.Text = "Action";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(438, 42);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(55, 12);
			this.label1.TabIndex = 18;
			this.label1.Text = "Command";
			// 
			// Cb_Action
			// 
			this.Cb_Action.FormattingEnabled = true;
			this.Cb_Action.Location = new System.Drawing.Point(651, 59);
			this.Cb_Action.Name = "Cb_Action";
			this.Cb_Action.Size = new System.Drawing.Size(199, 20);
			this.Cb_Action.TabIndex = 17;
			// 
			// Cb_Command
			// 
			this.Cb_Command.FormattingEnabled = true;
			this.Cb_Command.Location = new System.Drawing.Point(440, 59);
			this.Cb_Command.Name = "Cb_Command";
			this.Cb_Command.Size = new System.Drawing.Size(182, 20);
			this.Cb_Command.TabIndex = 16;
			// 
			// Tb_Name
			// 
			this.Tb_Name.Location = new System.Drawing.Point(440, 3);
			this.Tb_Name.Name = "Tb_Name";
			this.Tb_Name.Size = new System.Drawing.Size(199, 19);
			this.Tb_Name.TabIndex = 15;
			this.Tb_Name.Text = "Name";
			this.Tb_Name.TextChanged += new System.EventHandler(this.Tb_Name_TextChanged);
			// 
			// editListbox_01
			// 
			this.editListbox_01.BL_T = ((System.ComponentModel.BindingList<object>)(resources.GetObject("editListbox_01.BL_T")));
			this.editListbox_01.Location = new System.Drawing.Point(233, 0);
			this.editListbox_01.Make = null;
			this.editListbox_01.Name = "editListbox_01";
			this.editListbox_01.SelectedIndexChanged = null;
			this.editListbox_01.Size = new System.Drawing.Size(199, 424);
			this.editListbox_01.TabIndex = 21;
			// 
			// Ctrl_Branch
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.editListbox_01);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.Cb_Action);
			this.Controls.Add(this.Cb_Command);
			this.Controls.Add(this.Tb_Name);
			this.Name = "Ctrl_Branch";
			this.Size = new System.Drawing.Size(873, 424);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox Cb_Action;
		private System.Windows.Forms.ComboBox Cb_Command;
		private System.Windows.Forms.TextBox Tb_Name;
		private ScriptEditorUtility.EditListbox_0 editListbox_01;
	}
}
