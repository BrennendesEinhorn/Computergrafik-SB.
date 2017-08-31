namespace ControlTest
{
	partial class Form1
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

		#region Vom Windows Form-Designer generierter Code

		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung.
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.shaderNodeControl1 = new ControlClassLibrary.ShaderNodeControl();
			this.movableControlsLayoutPanel1 = new ControlClassLibrary.MovableControlsLayoutPanel();
			this.shaderNodeControl3 = new ControlClassLibrary.ShaderNodeControl();
			this.shaderNodeControl2 = new ControlClassLibrary.ShaderNodeControl();
			this.shaderNodeControl4 = new ControlClassLibrary.ShaderNodeControl();
			this.shaderNodeControl5 = new ControlClassLibrary.ShaderNodeControl();
			this.movableControlsLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// shaderNodeControl1
			// 
			this.shaderNodeControl1.BackColor = System.Drawing.SystemColors.Control;
			this.shaderNodeControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.shaderNodeControl1.Location = new System.Drawing.Point(102, 45);
			this.shaderNodeControl1.Name = "shaderNodeControl1";
			this.shaderNodeControl1.Size = new System.Drawing.Size(150, 100);
			this.shaderNodeControl1.TabIndex = 0;
			// 
			// movableControlsLayoutPanel1
			// 
			this.movableControlsLayoutPanel1.AllowDrop = true;
			this.movableControlsLayoutPanel1.AllowOverlap = false;
			this.movableControlsLayoutPanel1.BackColor = System.Drawing.Color.White;
			this.movableControlsLayoutPanel1.Controls.Add(this.shaderNodeControl5);
			this.movableControlsLayoutPanel1.Controls.Add(this.shaderNodeControl4);
			this.movableControlsLayoutPanel1.Controls.Add(this.shaderNodeControl3);
			this.movableControlsLayoutPanel1.Controls.Add(this.shaderNodeControl2);
			this.movableControlsLayoutPanel1.Controls.Add(this.shaderNodeControl1);
			this.movableControlsLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.movableControlsLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.movableControlsLayoutPanel1.Name = "movableControlsLayoutPanel1";
			this.movableControlsLayoutPanel1.RestrictToPanel = true;
			this.movableControlsLayoutPanel1.Size = new System.Drawing.Size(848, 617);
			this.movableControlsLayoutPanel1.TabIndex = 25;
			this.movableControlsLayoutPanel1.ControlBoundsChanging += new System.EventHandler<ControlClassLibrary.NewControlBoundsArgs>(this.movableControlsLayoutPanel1_ControlBoundsChanging);
			this.movableControlsLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.movableControlsLayoutPanel1_Paint);
			// 
			// shaderNodeControl3
			// 
			this.shaderNodeControl3.BackColor = System.Drawing.SystemColors.Control;
			this.shaderNodeControl3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.shaderNodeControl3.Location = new System.Drawing.Point(349, 45);
			this.shaderNodeControl3.Name = "shaderNodeControl3";
			this.shaderNodeControl3.Size = new System.Drawing.Size(150, 100);
			this.shaderNodeControl3.TabIndex = 2;
			// 
			// shaderNodeControl2
			// 
			this.shaderNodeControl2.BackColor = System.Drawing.SystemColors.Control;
			this.shaderNodeControl2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.shaderNodeControl2.Location = new System.Drawing.Point(349, 258);
			this.shaderNodeControl2.Name = "shaderNodeControl2";
			this.shaderNodeControl2.Size = new System.Drawing.Size(150, 100);
			this.shaderNodeControl2.TabIndex = 1;
			// 
			// shaderNodeControl4
			// 
			this.shaderNodeControl4.BackColor = System.Drawing.SystemColors.Control;
			this.shaderNodeControl4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.shaderNodeControl4.Location = new System.Drawing.Point(550, 45);
			this.shaderNodeControl4.Name = "shaderNodeControl4";
			this.shaderNodeControl4.Size = new System.Drawing.Size(150, 100);
			this.shaderNodeControl4.TabIndex = 3;
			// 
			// shaderNodeControl5
			// 
			this.shaderNodeControl5.BackColor = System.Drawing.SystemColors.Control;
			this.shaderNodeControl5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.shaderNodeControl5.Location = new System.Drawing.Point(550, 258);
			this.shaderNodeControl5.Name = "shaderNodeControl5";
			this.shaderNodeControl5.Size = new System.Drawing.Size(150, 100);
			this.shaderNodeControl5.TabIndex = 4;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(848, 617);
			this.Controls.Add(this.movableControlsLayoutPanel1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.movableControlsLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private ControlClassLibrary.ShaderNodeControl shaderNodeControl1;
		private ControlClassLibrary.MovableControlsLayoutPanel movableControlsLayoutPanel1;
		private ControlClassLibrary.ShaderNodeControl shaderNodeControl3;
		private ControlClassLibrary.ShaderNodeControl shaderNodeControl2;
		private ControlClassLibrary.ShaderNodeControl shaderNodeControl5;
		private ControlClassLibrary.ShaderNodeControl shaderNodeControl4;
	}
}

