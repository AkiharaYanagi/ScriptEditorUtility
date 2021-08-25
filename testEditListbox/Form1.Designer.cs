namespace ScriptEditor
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.tB_Number1 = new ScriptEditor.TB_Number();
			this.editListbox_01 = new ScriptEditorUtility.EditListbox_0();
			this.SuspendLayout();
			// 
			// tB_Number1
			// 
			this.tB_Number1.GetFunc = null;
			this.tB_Number1.GroupSetter = null;
			this.tB_Number1.Location = new System.Drawing.Point(628, 14);
			this.tB_Number1.Name = "tB_Number1";
			this.tB_Number1.SetFunc = null;
			this.tB_Number1.Size = new System.Drawing.Size(160, 19);
			this.tB_Number1.TabIndex = 0;
			this.tB_Number1.Text = "0";
			// 
			// editListbox_01
			// 
			this.editListbox_01.BL_T = ((System.ComponentModel.BindingList<object>)(resources.GetObject("editListbox_01.BL_T")));
			this.editListbox_01.Location = new System.Drawing.Point(246, 14);
			this.editListbox_01.Make = null;
			this.editListbox_01.Name = "editListbox_01";
			this.editListbox_01.Size = new System.Drawing.Size(199, 424);
			this.editListbox_01.TabIndex = 1;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.editListbox_01);
			this.Controls.Add(this.tB_Number1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private TB_Number tB_Number1;
		private ScriptEditorUtility.EditListbox_0 editListbox_01;
	}
}

