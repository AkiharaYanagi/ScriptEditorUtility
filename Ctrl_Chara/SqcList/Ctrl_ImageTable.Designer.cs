namespace ScriptEditor
{
	partial class Ctrl_ImageTable
	{
		/// <summary> 
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region コンポーネント デザイナーで生成されたコード

		/// <summary> 
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.panel1 = new System.Windows.Forms.Panel();
			this.pB_Sqc1 = new ScriptEditor.PB_Sqc();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pB_Sqc1)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.AutoScroll = true;
			this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
			this.panel1.Controls.Add(this.pB_Sqc1);
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(545, 460);
			this.panel1.TabIndex = 0;
			this.panel1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.panel1_Scroll);
			// 
			// pB_Sqc1
			// 
			this.pB_Sqc1.BackColor = System.Drawing.SystemColors.Control;
			this.pB_Sqc1.EditData = null;
			this.pB_Sqc1.ELB_Sqc = null;
			this.pB_Sqc1.Location = new System.Drawing.Point(0, 0);
			this.pB_Sqc1.Name = "pB_Sqc1";
			this.pB_Sqc1.Size = new System.Drawing.Size(529, 446);
			this.pB_Sqc1.TabIndex = 0;
			this.pB_Sqc1.TabStop = false;
			// 
			// Ctrl_ImageTable
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel1);
			this.Name = "Ctrl_ImageTable";
			this.Size = new System.Drawing.Size(548, 463);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Ctrl_ImageTable_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Ctrl_ImageTable_DragEnter);
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pB_Sqc1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private ScriptEditor.PB_Sqc pB_Sqc1;
	}
}
