namespace ControlClassLibrary
{
	partial class SequenceBar
	{
		/// <summary> 
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Verwendete Ressourcen bereinigen.
		/// </summary>
		/// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Vom Komponenten-Designer generierter Code

		/// <summary> 
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.SuspendLayout();
			// 
			// menuStrip
			// 
			this.menuStrip.AllowItemReorder = true;
			this.menuStrip.BackColor = System.Drawing.Color.Silver;
			this.menuStrip.Dock = System.Windows.Forms.DockStyle.Fill;
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Padding = new System.Windows.Forms.Padding(0);
			this.menuStrip.ShowItemToolTips = true;
			this.menuStrip.Size = new System.Drawing.Size(559, 30);
			this.menuStrip.TabIndex = 0;
			this.menuStrip.Paint += new System.Windows.Forms.PaintEventHandler(this.menuStrip_Paint);
			this.menuStrip.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MenuItem_MouseMove);
			this.menuStrip.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MenuItem_MouseUp);
			this.menuStrip.Resize += new System.EventHandler(this.menuStrip_Resize);
			// 
			// SequenceBar
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.menuStrip);
			this.Name = "SequenceBar";
			this.Size = new System.Drawing.Size(559, 30);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip;
	}
}
