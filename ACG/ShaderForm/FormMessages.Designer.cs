namespace ShaderForm
{
	partial class FormMessages
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMessages));
			this.errorLog = new System.Windows.Forms.TextBox();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fontBiggerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fontSmallerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.checkBoxOnTop = new System.Windows.Forms.CheckBox();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// errorLog
			// 
			this.errorLog.AllowDrop = true;
			this.errorLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.errorLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.errorLog.Location = new System.Drawing.Point(0, 24);
			this.errorLog.Margin = new System.Windows.Forms.Padding(2);
			this.errorLog.Multiline = true;
			this.errorLog.Name = "errorLog";
			this.errorLog.ReadOnly = true;
			this.errorLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.errorLog.Size = new System.Drawing.Size(411, 288);
			this.errorLog.TabIndex = 2;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearToolStripMenuItem,
            this.fontBiggerToolStripMenuItem,
            this.fontSmallerToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.ShowItemToolTips = true;
			this.menuStrip1.Size = new System.Drawing.Size(411, 24);
			this.menuStrip1.TabIndex = 4;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// clearToolStripMenuItem
			// 
			this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
			this.clearToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
			this.clearToolStripMenuItem.Text = "Clear";
			this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
			// 
			// fontBiggerToolStripMenuItem
			// 
			this.fontBiggerToolStripMenuItem.AutoToolTip = true;
			this.fontBiggerToolStripMenuItem.Name = "fontBiggerToolStripMenuItem";
			this.fontBiggerToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
			this.fontBiggerToolStripMenuItem.Text = "Font bigger";
			this.fontBiggerToolStripMenuItem.ToolTipText = "CTRL+WHEEL_UP";
			this.fontBiggerToolStripMenuItem.Click += new System.EventHandler(this.fontBiggerToolStripMenuItem_Click);
			// 
			// fontSmallerToolStripMenuItem
			// 
			this.fontSmallerToolStripMenuItem.AutoToolTip = true;
			this.fontSmallerToolStripMenuItem.Name = "fontSmallerToolStripMenuItem";
			this.fontSmallerToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
			this.fontSmallerToolStripMenuItem.Text = "Font smaller";
			this.fontSmallerToolStripMenuItem.ToolTipText = "CTRL+WHEEL_UP";
			this.fontSmallerToolStripMenuItem.Click += new System.EventHandler(this.fontSmallerToolStripMenuItem_Click);
			// 
			// checkBoxOnTop
			// 
			this.checkBoxOnTop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkBoxOnTop.AutoSize = true;
			this.checkBoxOnTop.Location = new System.Drawing.Point(354, 2);
			this.checkBoxOnTop.Name = "checkBoxOnTop";
			this.checkBoxOnTop.Size = new System.Drawing.Size(57, 17);
			this.checkBoxOnTop.TabIndex = 5;
			this.checkBoxOnTop.Text = "onTop";
			this.checkBoxOnTop.UseVisualStyleBackColor = true;
			this.checkBoxOnTop.CheckedChanged += new System.EventHandler(this.checkBoxOnTop_CheckedChanged);
			// 
			// FormMessages
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(411, 312);
			this.Controls.Add(this.checkBoxOnTop);
			this.Controls.Add(this.errorLog);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "FormMessages";
			this.Text = "FormMessages";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMessages_FormClosing);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox errorLog;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fontBiggerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fontSmallerToolStripMenuItem;
		private System.Windows.Forms.CheckBox checkBoxOnTop;
	}
}