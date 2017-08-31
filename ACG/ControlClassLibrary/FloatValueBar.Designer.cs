namespace ControlClassLibrary
{
	partial class FloatValueBar
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
			this.SuspendLayout();
			// 
			// MarkerBar
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Cursor = System.Windows.Forms.Cursors.VSplit;
			this.Name = "MarkerBar";
			this.Size = new System.Drawing.Size(148, 148);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MarkerBar_MouseDown);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MarkerBar_MouseMove);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MarkerBar_MouseUp);
			this.ResumeLayout(false);

		}

		#endregion
	}
}
