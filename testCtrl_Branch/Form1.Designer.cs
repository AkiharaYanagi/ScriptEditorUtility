﻿namespace ScriptEditor
{
	partial class Form1
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

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent ()
		{
			ScriptEditor.BindingDictionary<ScriptEditor.Branch> bindingDictionary_11 = new ScriptEditor.BindingDictionary<ScriptEditor.Branch>();
			ScriptEditor.BindingDictionary<ScriptEditor.Command> bindingDictionary_12 = new ScriptEditor.BindingDictionary<ScriptEditor.Command>();
			ScriptEditor.BindingDictionary<ScriptEditor.Sequence> bindingDictionary_13 = new ScriptEditor.BindingDictionary<ScriptEditor.Sequence>();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			ScriptEditor.Branch branch1 = new ScriptEditor.Branch();
			this.ctrl_Branch1 = new ScriptEditor.Ctrl_Branch();
			this.SuspendLayout();
			// 
			// ctrl_Branch1
			// 
			this.ctrl_Branch1.BD_Branch = bindingDictionary_11;
			this.ctrl_Branch1.BD_Command = bindingDictionary_12;
			this.ctrl_Branch1.BD_Sequence = bindingDictionary_13;
			this.ctrl_Branch1.Location = new System.Drawing.Point(-3, 0);
			this.ctrl_Branch1.Name = "ctrl_Branch1";
			branch1.Frame = 0;
			branch1.Name = "BrcName";
			branch1.NameSequence = "NameAction";
			branch1.NameCommand = "NameCommand";
			this.ctrl_Branch1.Size = new System.Drawing.Size(513, 446);
			this.ctrl_Branch1.TabIndex = 0;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(496, 455);
			this.Controls.Add(this.ctrl_Branch1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}

		#endregion

		private ScriptEditor.Ctrl_Branch ctrl_Branch1;
	}
}

