namespace ScriptEditor
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
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.Cb_Action = new System.Windows.Forms.ComboBox();
			this.Cb_Command = new System.Windows.Forms.ComboBox();
			this.Cb_Condition = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.RB_Action = new System.Windows.Forms.RadioButton();
			this.RB_Effect = new System.Windows.Forms.RadioButton();
			this.Cb_Effect = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(302, 165);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(17, 12);
			this.label3.TabIndex = 20;
			this.label3.Text = "↓";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(264, 125);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(55, 12);
			this.label1.TabIndex = 18;
			this.label1.Text = "Command";
			// 
			// Cb_Action
			// 
			this.Cb_Action.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Cb_Action.FormattingEnabled = true;
			this.Cb_Action.Location = new System.Drawing.Point(325, 196);
			this.Cb_Action.Name = "Cb_Action";
			this.Cb_Action.Size = new System.Drawing.Size(144, 20);
			this.Cb_Action.TabIndex = 17;
			this.Cb_Action.SelectionChangeCommitted += new System.EventHandler(this.Cb_Action_SelectionChangeCommitted);
			// 
			// Cb_Command
			// 
			this.Cb_Command.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Cb_Command.FormattingEnabled = true;
			this.Cb_Command.Location = new System.Drawing.Point(265, 142);
			this.Cb_Command.Name = "Cb_Command";
			this.Cb_Command.Size = new System.Drawing.Size(199, 20);
			this.Cb_Command.TabIndex = 16;
			this.Cb_Command.SelectionChangeCommitted += new System.EventHandler(this.Cb_Command_SelectionChangeCommitted);
			// 
			// Cb_Condition
			// 
			this.Cb_Condition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Cb_Condition.FormattingEnabled = true;
			this.Cb_Condition.Location = new System.Drawing.Point(265, 73);
			this.Cb_Condition.Name = "Cb_Condition";
			this.Cb_Condition.Size = new System.Drawing.Size(178, 20);
			this.Cb_Condition.TabIndex = 21;
			this.Cb_Condition.SelectionChangeCommitted += new System.EventHandler(this.Cb_Condition_SelectionChangeCommitted);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(263, 58);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(53, 12);
			this.label4.TabIndex = 22;
			this.label4.Text = "Condition";
			// 
			// RB_Action
			// 
			this.RB_Action.AutoSize = true;
			this.RB_Action.Checked = true;
			this.RB_Action.Location = new System.Drawing.Point(253, 198);
			this.RB_Action.Name = "RB_Action";
			this.RB_Action.Size = new System.Drawing.Size(65, 16);
			this.RB_Action.TabIndex = 23;
			this.RB_Action.TabStop = true;
			this.RB_Action.Text = "アクション";
			this.RB_Action.UseVisualStyleBackColor = true;
			this.RB_Action.CheckedChanged += new System.EventHandler(this.RB_Action_CheckedChanged);
			// 
			// RB_Effect
			// 
			this.RB_Effect.AutoSize = true;
			this.RB_Effect.Location = new System.Drawing.Point(253, 236);
			this.RB_Effect.Name = "RB_Effect";
			this.RB_Effect.Size = new System.Drawing.Size(63, 16);
			this.RB_Effect.TabIndex = 23;
			this.RB_Effect.Text = "エフェクト";
			this.RB_Effect.UseVisualStyleBackColor = true;
			this.RB_Effect.CheckedChanged += new System.EventHandler(this.RB_Effect_CheckedChanged);
			// 
			// Cb_Effect
			// 
			this.Cb_Effect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Cb_Effect.FormattingEnabled = true;
			this.Cb_Effect.Location = new System.Drawing.Point(325, 234);
			this.Cb_Effect.Name = "Cb_Effect";
			this.Cb_Effect.Size = new System.Drawing.Size(144, 20);
			this.Cb_Effect.TabIndex = 17;
			this.Cb_Effect.SelectionChangeCommitted += new System.EventHandler(this.Cb_Effect_SelectionChangeCommitted);
			// 
			// Ctrl_Branch
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.RB_Effect);
			this.Controls.Add(this.RB_Action);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.Cb_Condition);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.Cb_Effect);
			this.Controls.Add(this.Cb_Action);
			this.Controls.Add(this.Cb_Command);
			this.Name = "Ctrl_Branch";
			this.Size = new System.Drawing.Size(501, 452);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox Cb_Action;
		private System.Windows.Forms.ComboBox Cb_Command;
		private System.Windows.Forms.ComboBox Cb_Condition;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.RadioButton RB_Action;
		private System.Windows.Forms.RadioButton RB_Effect;
		private System.Windows.Forms.ComboBox Cb_Effect;
	}
}
