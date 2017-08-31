namespace ShaderForm.Graph
{
	partial class FormGraph
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
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGraph));
			this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.roundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.moveAxisYToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.valueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// chart1
			// 
			chartArea1.Name = "ChartArea1";
			this.chart1.ChartAreas.Add(chartArea1);
			this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.chart1.Location = new System.Drawing.Point(0, 28);
			this.chart1.Name = "chart1";
			series1.ChartArea = "ChartArea1";
			series1.Name = "Series1";
			this.chart1.Series.Add(series1);
			this.chart1.Size = new System.Drawing.Size(419, 440);
			this.chart1.TabIndex = 0;
			this.chart1.Text = "chart1";
			this.chart1.MouseHover += new System.EventHandler(this.chart1_MouseHover);
			// 
			// menuStrip1
			// 
			this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.roundToolStripMenuItem,
            this.moveAxisYToolStripMenuItem,
            this.valueToolStripMenuItem});
			this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.ShowItemToolTips = true;
			this.menuStrip1.Size = new System.Drawing.Size(419, 28);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.AutoToolTip = true;
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(47, 24);
			this.copyToolStripMenuItem.Text = "Copy";
			this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
			// 
			// pasteToolStripMenuItem
			// 
			this.pasteToolStripMenuItem.AutoToolTip = true;
			this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
			this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
			this.pasteToolStripMenuItem.Size = new System.Drawing.Size(47, 24);
			this.pasteToolStripMenuItem.Text = "Paste";
			this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
			// 
			// roundToolStripMenuItem
			// 
			this.roundToolStripMenuItem.Name = "roundToolStripMenuItem";
			this.roundToolStripMenuItem.Size = new System.Drawing.Size(54, 24);
			this.roundToolStripMenuItem.Text = "Round";
			this.roundToolStripMenuItem.Click += new System.EventHandler(this.roundToolStripMenuItem_Click);
			// 
			// moveAxisYToolStripMenuItem
			// 
			this.moveAxisYToolStripMenuItem.Checked = true;
			this.moveAxisYToolStripMenuItem.CheckOnClick = true;
			this.moveAxisYToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.moveAxisYToolStripMenuItem.Name = "moveAxisYToolStripMenuItem";
			this.moveAxisYToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
			this.moveAxisYToolStripMenuItem.Text = "Move Y";
			this.moveAxisYToolStripMenuItem.CheckedChanged += new System.EventHandler(this.moveAxisToolStripMenuItem_CheckedChanged);
			// 
			// valueToolStripMenuItem
			// 
			this.valueToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.valueToolStripMenuItem.AutoSize = false;
			this.valueToolStripMenuItem.Name = "valueToolStripMenuItem";
			this.valueToolStripMenuItem.Size = new System.Drawing.Size(117, 24);
			// 
			// FormGraph
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(419, 468);
			this.Controls.Add(this.chart1);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MainMenuStrip = this.menuStrip1;
			this.MinimizeBox = false;
			this.Name = "FormGraph";
			this.Text = "FormGraph";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormGraph_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem roundToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem moveAxisYToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem valueToolStripMenuItem;
	}
}