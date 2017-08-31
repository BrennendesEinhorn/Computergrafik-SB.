using System.Windows.Forms;

namespace ShaderForm
{
	partial class FormMain
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
			this.glControl = new OpenTK.GLControl();
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.menuFps = new System.Windows.Forms.ToolStripMenuItem();
			this.menuSizeSetting = new System.Windows.Forms.ToolStripComboBox();
			this.menuDemo = new System.Windows.Forms.ToolStripMenuItem();
			this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuLoad = new System.Windows.Forms.ToolStripMenuItem();
			this.menuSave = new System.Windows.Forms.ToolStripMenuItem();
			this.menuShaders = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuShaderAdd = new System.Windows.Forms.ToolStripMenuItem();
			this.menuTextures = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuTextureAdd = new System.Windows.Forms.ToolStripMenuItem();
			this.menuUniforms = new System.Windows.Forms.ToolStripMenuItem();
			this.addCameraUniformsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.TextUniformAdd = new System.Windows.Forms.ToolStripTextBox();
			this.menuSound = new System.Windows.Forms.ToolStripMenuItem();
			this.menuWindow = new System.Windows.Forms.ToolStripMenuItem();
			this.menuScreenshot = new System.Windows.Forms.ToolStripMenuItem();
			this.menuFullscreen = new System.Windows.Forms.ToolStripMenuItem();
			this.menuCompact = new System.Windows.Forms.ToolStripMenuItem();
			this.menuOnTop = new System.Windows.Forms.ToolStripMenuItem();
			this.logToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cameraWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.panelSequence = new System.Windows.Forms.Panel();
			this.button1 = new System.Windows.Forms.Button();
			this.sequenceBar1 = new ControlClassLibrary.SequenceBar();
			this.soundPlayerBar1 = new ControlClassLibrary.SeekBar();
			this.textBoxLastMessage = new System.Windows.Forms.TextBox();
			this.menuStrip.SuspendLayout();
			this.panelSequence.SuspendLayout();
			this.SuspendLayout();
			// 
			// glControl
			// 
			this.glControl.AllowDrop = true;
			this.glControl.BackColor = System.Drawing.Color.Gold;
			this.glControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.glControl.Location = new System.Drawing.Point(0, 27);
			this.glControl.Margin = new System.Windows.Forms.Padding(4);
			this.glControl.Name = "glControl";
			this.glControl.Size = new System.Drawing.Size(688, 362);
			this.glControl.TabIndex = 0;
			this.glControl.VSync = false;
			this.glControl.Load += new System.EventHandler(this.GlControl_Load);
			this.glControl.DragDrop += new System.Windows.Forms.DragEventHandler(this.GlControl_DragDrop);
			this.glControl.DragOver += new System.Windows.Forms.DragEventHandler(this.GlControl_DragOver);
			this.glControl.Paint += new System.Windows.Forms.PaintEventHandler(this.GlControl_Paint);
			this.glControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GlControl_MouseDown);
			this.glControl.MouseEnter += new System.EventHandler(this.glControl_MouseEnter);
			this.glControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GlControl_MouseMove);
			this.glControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GlControl_MouseUp);
			// 
			// menuStrip
			// 
			this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuHelp,
            this.menuFps,
            this.menuSizeSetting,
            this.menuDemo,
            this.menuShaders,
            this.menuTextures,
            this.menuUniforms,
            this.menuSound,
            this.menuWindow});
			this.menuStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.ShowItemToolTips = true;
			this.menuStrip.Size = new System.Drawing.Size(688, 27);
			this.menuStrip.TabIndex = 2;
			this.menuStrip.Tag = "1";
			this.menuStrip.Text = "menuStrip1";
			// 
			// menuHelp
			// 
			this.menuHelp.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.menuHelp.Name = "menuHelp";
			this.menuHelp.ShortcutKeys = System.Windows.Forms.Keys.F1;
			this.menuHelp.Size = new System.Drawing.Size(24, 23);
			this.menuHelp.Text = "?";
			// 
			// menuFps
			// 
			this.menuFps.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.menuFps.CheckOnClick = true;
			this.menuFps.Name = "menuFps";
			this.menuFps.Size = new System.Drawing.Size(38, 23);
			this.menuFps.Text = "FPS";
			// 
			// menuSizeSetting
			// 
			this.menuSizeSetting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.menuSizeSetting.Items.AddRange(new object[] {
            "x0.25",
            "x0.5",
            "x1",
            "x2",
            "x4",
            "f128",
            "f256",
            "f512",
            "f1024",
            "f2048",
            "f4096",
            "f8192"});
			this.menuSizeSetting.Name = "menuSizeSetting";
			this.menuSizeSetting.Size = new System.Drawing.Size(75, 23);
			// 
			// menuDemo
			// 
			this.menuDemo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearToolStripMenuItem,
            this.menuLoad,
            this.menuSave});
			this.menuDemo.Name = "menuDemo";
			this.menuDemo.Size = new System.Drawing.Size(51, 23);
			this.menuDemo.Text = "Demo";
			// 
			// clearToolStripMenuItem
			// 
			this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
			this.clearToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
			this.clearToolStripMenuItem.Text = "Clear";
			this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
			// 
			// menuLoad
			// 
			this.menuLoad.Name = "menuLoad";
			this.menuLoad.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.menuLoad.Size = new System.Drawing.Size(154, 22);
			this.menuLoad.Text = "Load...";
			// 
			// menuSave
			// 
			this.menuSave.Name = "menuSave";
			this.menuSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.menuSave.Size = new System.Drawing.Size(154, 22);
			this.menuSave.Text = "Save...";
			// 
			// menuShaders
			// 
			this.menuShaders.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuShaderAdd});
			this.menuShaders.Name = "menuShaders";
			this.menuShaders.Size = new System.Drawing.Size(60, 23);
			this.menuShaders.Text = "Shaders";
			// 
			// MenuShaderAdd
			// 
			this.MenuShaderAdd.Name = "MenuShaderAdd";
			this.MenuShaderAdd.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
			this.MenuShaderAdd.Size = new System.Drawing.Size(150, 22);
			this.MenuShaderAdd.Text = "Add...";
			// 
			// menuTextures
			// 
			this.menuTextures.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuTextureAdd});
			this.menuTextures.Name = "menuTextures";
			this.menuTextures.Size = new System.Drawing.Size(62, 23);
			this.menuTextures.Text = "Textures";
			// 
			// MenuTextureAdd
			// 
			this.MenuTextureAdd.Name = "MenuTextureAdd";
			this.MenuTextureAdd.Size = new System.Drawing.Size(105, 22);
			this.MenuTextureAdd.Text = "Add...";
			// 
			// menuUniforms
			// 
			this.menuUniforms.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addCameraUniformsToolStripMenuItem,
            this.TextUniformAdd});
			this.menuUniforms.Name = "menuUniforms";
			this.menuUniforms.Size = new System.Drawing.Size(68, 23);
			this.menuUniforms.Text = "Uniforms";
			// 
			// addCameraUniformsToolStripMenuItem
			// 
			this.addCameraUniformsToolStripMenuItem.Name = "addCameraUniformsToolStripMenuItem";
			this.addCameraUniformsToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
			this.addCameraUniformsToolStripMenuItem.Text = "Add Camera Uniforms";
			this.addCameraUniformsToolStripMenuItem.ToolTipText = "C";
			this.addCameraUniformsToolStripMenuItem.Click += new System.EventHandler(this.addCameraUniformsToolStripMenuItem_Click);
			// 
			// TextUniformAdd
			// 
			this.TextUniformAdd.MaxLength = 40;
			this.TextUniformAdd.Name = "TextUniformAdd";
			this.TextUniformAdd.Size = new System.Drawing.Size(100, 23);
			this.TextUniformAdd.ToolTipText = "Enter uniform name";
			this.TextUniformAdd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextUniformAdd_KeyDown);
			this.TextUniformAdd.TextChanged += new System.EventHandler(this.TextUniformAdd_TextChanged);
			// 
			// menuSound
			// 
			this.menuSound.Name = "menuSound";
			this.menuSound.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
			this.menuSound.Size = new System.Drawing.Size(53, 23);
			this.menuSound.Text = "Sound";
			this.menuSound.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MenuSound_MouseDown);
			// 
			// menuWindow
			// 
			this.menuWindow.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuScreenshot,
            this.menuFullscreen,
            this.menuCompact,
            this.menuOnTop,
            this.logToolStripMenuItem,
            this.cameraWindowToolStripMenuItem});
			this.menuWindow.Name = "menuWindow";
			this.menuWindow.Size = new System.Drawing.Size(63, 23);
			this.menuWindow.Text = "Window";
			// 
			// menuScreenshot
			// 
			this.menuScreenshot.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.menuScreenshot.Name = "menuScreenshot";
			this.menuScreenshot.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
			this.menuScreenshot.Size = new System.Drawing.Size(223, 22);
			this.menuScreenshot.Text = "Save";
			// 
			// menuFullscreen
			// 
			this.menuFullscreen.AutoToolTip = true;
			this.menuFullscreen.CheckOnClick = true;
			this.menuFullscreen.Name = "menuFullscreen";
			this.menuFullscreen.ShortcutKeys = System.Windows.Forms.Keys.F11;
			this.menuFullscreen.Size = new System.Drawing.Size(223, 22);
			this.menuFullscreen.Text = "Fullscreen";
			this.menuFullscreen.ToolTipText = "F11";
			this.menuFullscreen.CheckedChanged += new System.EventHandler(this.MenuFullscreen_CheckedChanged);
			// 
			// menuCompact
			// 
			this.menuCompact.CheckOnClick = true;
			this.menuCompact.Name = "menuCompact";
			this.menuCompact.ShortcutKeys = System.Windows.Forms.Keys.F12;
			this.menuCompact.Size = new System.Drawing.Size(223, 22);
			this.menuCompact.Text = "Compact";
			this.menuCompact.CheckStateChanged += new System.EventHandler(this.menuCompact_CheckStateChanged);
			// 
			// menuOnTop
			// 
			this.menuOnTop.CheckOnClick = true;
			this.menuOnTop.Name = "menuOnTop";
			this.menuOnTop.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
			this.menuOnTop.Size = new System.Drawing.Size(223, 22);
			this.menuOnTop.Text = "OnTop";
			this.menuOnTop.CheckedChanged += new System.EventHandler(this.menuOnTop_CheckedChanged);
			// 
			// logToolStripMenuItem
			// 
			this.logToolStripMenuItem.Name = "logToolStripMenuItem";
			this.logToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
			this.logToolStripMenuItem.Text = "Log Window";
			this.logToolStripMenuItem.Click += new System.EventHandler(this.logToolStripMenuItem_Click);
			// 
			// cameraWindowToolStripMenuItem
			// 
			this.cameraWindowToolStripMenuItem.Name = "cameraWindowToolStripMenuItem";
			this.cameraWindowToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
			this.cameraWindowToolStripMenuItem.Text = "Camera Window";
			this.cameraWindowToolStripMenuItem.Click += new System.EventHandler(this.cameraWindowToolStripMenuItem_Click);
			// 
			// panelSequence
			// 
			this.panelSequence.Controls.Add(this.button1);
			this.panelSequence.Controls.Add(this.sequenceBar1);
			this.panelSequence.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelSequence.Location = new System.Drawing.Point(0, 389);
			this.panelSequence.Name = "panelSequence";
			this.panelSequence.Size = new System.Drawing.Size(688, 34);
			this.panelSequence.TabIndex = 5;
			// 
			// button1
			// 
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Image = global::ShaderForm.Properties.Resources.Restart_6322;
			this.button1.Location = new System.Drawing.Point(0, 0);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(29, 34);
			this.button1.TabIndex = 6;
			this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Reload_Click);
			// 
			// sequenceBar1
			// 
			this.sequenceBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.sequenceBar1.Location = new System.Drawing.Point(29, 1);
			this.sequenceBar1.Margin = new System.Windows.Forms.Padding(4);
			this.sequenceBar1.Name = "sequenceBar1";
			this.sequenceBar1.Size = new System.Drawing.Size(658, 32);
			this.sequenceBar1.TabIndex = 5;
			this.sequenceBar1.Changed += new System.EventHandler(this.sequenceBar1_OnChanged);
			// 
			// soundPlayerBar1
			// 
			this.soundPlayerBar1.AllowDrop = true;
			this.soundPlayerBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.soundPlayerBar1.Location = new System.Drawing.Point(0, 423);
			this.soundPlayerBar1.Margin = new System.Windows.Forms.Padding(4);
			this.soundPlayerBar1.Name = "soundPlayerBar1";
			this.soundPlayerBar1.Playing = false;
			this.soundPlayerBar1.Position = 0F;
			this.soundPlayerBar1.Size = new System.Drawing.Size(688, 29);
			this.soundPlayerBar1.TabIndex = 3;
			this.soundPlayerBar1.TabStop = false;
			// 
			// textBoxLastMessage
			// 
			this.textBoxLastMessage.AllowDrop = true;
			this.textBoxLastMessage.BackColor = System.Drawing.SystemColors.Control;
			this.textBoxLastMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBoxLastMessage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxLastMessage.Font = new System.Drawing.Font("Lucida Console", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBoxLastMessage.Location = new System.Drawing.Point(0, 27);
			this.textBoxLastMessage.Multiline = true;
			this.textBoxLastMessage.Name = "textBoxLastMessage";
			this.textBoxLastMessage.ReadOnly = true;
			this.textBoxLastMessage.Size = new System.Drawing.Size(688, 362);
			this.textBoxLastMessage.TabIndex = 6;
			this.textBoxLastMessage.DragDrop += new System.Windows.Forms.DragEventHandler(this.GlControl_DragDrop);
			this.textBoxLastMessage.DragOver += new System.Windows.Forms.DragEventHandler(this.GlControl_DragOver);
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(688, 452);
			this.Controls.Add(this.textBoxLastMessage);
			this.Controls.Add(this.glControl);
			this.Controls.Add(this.panelSequence);
			this.Controls.Add(this.soundPlayerBar1);
			this.Controls.Add(this.menuStrip);
			this.DoubleBuffered = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MainMenuStrip = this.menuStrip;
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "FormMain";
			this.Text = "ShaderForm";
			this.TopMost = true;
			this.Deactivate += new System.EventHandler(this.FormMain_Deactivate);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
			this.Load += new System.EventHandler(this.FormMain_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.panelSequence.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

        #endregion

        private OpenTK.GLControl glControl;
		private MenuStrip menuStrip;
		private ToolStripMenuItem menuFps;
		private ToolStripMenuItem menuTextures;
		private ToolStripMenuItem menuFullscreen;
		private ToolStripMenuItem menuScreenshot;
		private ToolStripMenuItem menuSound;
		private ToolStripMenuItem MenuTextureAdd;
		private ToolStripMenuItem menuShaders;
		private ToolStripMenuItem MenuShaderAdd;
		private ToolStripMenuItem menuDemo;
		private ToolStripMenuItem menuLoad;
		private ToolStripMenuItem menuSave;
		private ToolStripMenuItem menuUniforms;
		private ToolStripMenuItem menuHelp;
		private ToolStripMenuItem clearToolStripMenuItem;
		private ToolStripMenuItem addCameraUniformsToolStripMenuItem;
		private ToolStripMenuItem menuWindow;
		private ToolStripComboBox menuSizeSetting;
		private ToolStripTextBox TextUniformAdd;
		private Panel panelSequence;
		private ControlClassLibrary.SequenceBar sequenceBar1;
		private ControlClassLibrary.SeekBar soundPlayerBar1;
		private Button button1;
		private ToolStripMenuItem menuOnTop;
		private TextBox textBoxLastMessage;
		private ToolStripMenuItem logToolStripMenuItem;
		private ToolStripMenuItem cameraWindowToolStripMenuItem;
		private ToolStripMenuItem menuCompact;
	}
}

