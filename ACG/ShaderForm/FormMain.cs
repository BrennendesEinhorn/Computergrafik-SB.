using ControlClassLibrary;
using ShaderForm.Camera;
using ShaderForm.Demo;
using ShaderForm.Graph;
using ShaderForm.Interfaces;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ShaderForm
{
	public partial class FormMain : Form
	{
		private DemoModel demo;
		private int mouseButton = 0;
		private Point mousePos;
		//private int painting = 0;
		private MultiGraph multiGraph = new MultiGraph();
		private FacadeFormMessages log = new FacadeFormMessages();
		private FacadeCamera camera = new FacadeCamera();
		private string lastMessage;

		public FormMain()
		{
			InitializeComponent();

			string demoFilter = DefaultFiles.GetDemoExtension() + " (*" + DefaultFiles.GetDemoExtension() + ")|*" + DefaultFiles.GetDemoExtension();
			menuSizeSetting.SelectedIndexChanged += (sender, e) => glControl.Invalidate();
			multiGraph.ChangedPosition += (pos) => soundPlayerBar1.Position = pos;
			multiGraph.KeyDown += FormMain_KeyDown;
			camera.Redraw += (position) => glControl.Invalidate();
			soundPlayerBar1.PositionChanged += (position) => glControl.Invalidate();
			soundPlayerBar1.PositionChanged += (position) => multiGraph.UpdatePosition(position);
			soundPlayerBar1.PositionChanged += (position) => camera.UpdateFromUniforms(demo.Uniforms, position);

			menuHelp.Click += (sender, e) => Dialogs.Help();
			menuLoad.Click += (sender, e) => Dialogs.OpenFile(demoFilter
				, (fileName) => LoadDemo(fileName));
			menuSound.Click += (sender, e) => Dialogs.OpenFile("sound (*.*)|*.*", (fileName) => demo.TimeSource.Load(DemoTimeSource.FromMediaFile(fileName), fileName));
			MenuShaderAdd.Click += (sender, e) => Dialogs.OpenFile("glsl (*.glsl)|*.glsl", (fileName) => AddShader(fileName));
			MenuTextureAdd.Click += (sender, e) => Dialogs.OpenFile("texture (*.*)|*.*", (fileName) => demo.Textures.AddUpdate(fileName));
			menuSave.Click += (sender, e) => Dialogs.SaveFile(demoFilter, (fileName) =>
				{
					try
					{
						DemoLoader.SaveToFile(demo, fileName);
					}
					catch (Exception ex)
					{
						log.Append(ex.Message);
					}
				});
			menuScreenshot.Click += (sender, e) => Dialogs.SaveFile("png (*.png)|*.png", (fileName) => { glControl.Invalidate(); demo.SaveBuffer(fileName); });

			KeyDown += (sender, e) => { camera.KeyChange(e.KeyCode, true); };
			KeyUp += (sender, e) => { camera.KeyChange(e.KeyCode, false); };
		}

		private void AddShader(string fileName)
		{
			demo.Shaders.AddUpdateShader(fileName);
			//put new shader at cursor position
			float time = demo.TimeSource.Position;
			demo.ShaderKeyframes.AddUpdate(time, fileName);
		}

		private void LoadDemo(string fileName)
		{
			try
			{
				camera.Reset();
				DemoLoader.LoadFromFile(demo, fileName, (obj, args) => log.Append(args.Message) );
			}
			catch(Exception e)
			{
				log.Append("No valid demo file found with exception '" + e.Message + "'");
			};
		}

		private void GlControl_DragOver(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				//test if control key was pressed -> add stuff
				e.Effect = (8 == (e.KeyState & 8)) ? DragDropEffects.Copy : DragDropEffects.Move;
			}
		}

		private void GlControl_DragDrop(object sender, DragEventArgs e)
		{
			string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
			foreach (string file in files)
			{
				//check if texture
				if (!demo.Textures.AddUpdate(file))
				{
					//check if sound file
					var sound = DemoTimeSource.FromMediaFile(file);
					if (!ReferenceEquals(null,  sound))
					{
						demo.TimeSource.Load(sound, file);
					}
					else
					{
						//test if control key was pressed
						if (8 == (e.KeyState & 8))
						{
							//add stuff
							AddShader(file);
						}
						else
						{
							//replace stuff
							camera.Reset();
							demo.Shaders.Clear();
							demo.ShaderKeyframes.Clear();
							AddShader(file);
						}
					}
				}
			}
		}

		private void GlControl_Paint(object sender, PaintEventArgs e)
		{
			//todo: paint in other thread?
			//if (1 == painting) return;
			//System.Threading.Interlocked.Exchange(ref painting, 1);

			//normalized mouse pos
			float mouseX = this.mousePos.X / (float)glControl.Width;
			float mouseY = (glControl.Height - this.mousePos.Y) / (float)glControl.Height;
			float factor = 1.0f;
			try
			{
				factor = float.Parse(menuSizeSetting.Text.Substring(1), CultureInfo.InvariantCulture);
			}
			catch (Exception) { };

			int width = glControl.Width;
			int height = glControl.Height;

			if ('x' == menuSizeSetting.Text[0])
			{
				width = (int)Math.Round(glControl.Width * factor);
				height = (int)Math.Round(glControl.Height * factor);
			}
			else
			{
				width = (int)factor;
				height = (int)factor;
			}
			//todo: update not in main thread -> make async;
			//glControl.Context.MakeCurrent(null);
			//await Task.Run(() =>
			//{
			//	glControl.MakeCurrent();
			camera.Update(mouseX * width, mouseY * height, 1 == mouseButton);
			try
			{
				if (!demo.UpdateBuffer((int)Math.Round(mouseX * width), (int)Math.Round(mouseY * height), mouseButton, width, height))
				{
					textBoxLastMessage.Text = lastMessage;
					textBoxLastMessage.Visible = true;
				}
				else
				{
					textBoxLastMessage.Visible = false;
				}
			}
			catch { /* We do not care about errors at this level */ }
			//	glControl.Context.MakeCurrent(null);
			//});
			//glControl.MakeCurrent();
			demo.Draw(glControl.Width, glControl.Height);
			glControl.SwapBuffers();

			menuFps.Text = menuFps.Checked ? string.Format("{0:0.00}FPS ", 1 / demo.UpdateTime) : string.Format("{0:0.0}MSec ", demo.UpdateTime * 1e3f);
			//System.Threading.Interlocked.Exchange(ref painting, 0);
			if (camera.IsActive)
			{
				glControl.Invalidate();
			}
		}

		private void GlControl_Load(object sender, EventArgs eArgs)
		{
			try
			{
				demo = DemoModelFactory.DemoModelFactory.Create(this);
				//make for valid time source even if no new demo is loaded afterwards (when starting with shader cmd line argument)
				Demo_OnTimeSourceLoaded(null, EventArgs.Empty);
				demo.SetCustomUniforms += Demo_OnSetCustomUniforms;
				demo.TimeSource.Loaded += Demo_OnTimeSourceLoaded;
				demo.Uniforms.UniformAdded += Uniforms_OnAdd;
				demo.Uniforms.UniformRemoved += Uniforms_OnRemove;
				demo.Uniforms.UniformAdded += multiGraph.Uniforms_OnAdd;
				demo.Uniforms.UniformRemoved += multiGraph.Uniforms_OnRemove;
				demo.Uniforms.ChangedKeyframes += multiGraph.Uniforms_OnChange;
				demo.Uniforms.ChangedKeyframes += (s, a) => camera.UpdateFromUniforms(demo.Uniforms, demo.TimeSource.Position);
				demo.Uniforms.ChangedKeyframes += (s, a) => glControl.Invalidate();
				demo.Shaders.Changed += Shaders_OnChange;
				demo.ShaderKeyframes.Changed += ShaderKeframes_OnChange;
				demo.Textures.Changed += Textures_OnChange;
			}
			catch (Exception e)
			{
				log.Append(e.Message);
			}
		}

		private void Demo_OnSetCustomUniforms(ISetUniform visualContext)
		{
			if (demo.TimeSource.IsRunning)
			{
				if (camera.UpdateFromUniforms(demo.Uniforms, demo.TimeSource.Position)) return;
			}
			camera.SetUniforms(visualContext);
		}

		private void Demo_OnTimeSourceLoaded(object sender, EventArgs e)
		{
			soundPlayerBar1.TimeSource = demo.TimeSource;
			Text = Path.GetFileNameWithoutExtension(demo.TimeSource.SoundFileName) + " ShaderForm";
		}

		private void Shaders_OnChange(object sender, string message)
		{
			Text = "ShaderForm"; //initial window caption
			while (menuShaders.DropDownItems.Count > 1) menuShaders.DropDownItems.RemoveAt(1); //recreate shader menus
			foreach (var shaderPath in demo.Shaders)
			{
				var menu = new ToolStripMenuItem();
				menu.Text = shaderPath;
				menu.ToolTipText = "Right click removes shader";
				menu.MouseDown += MenuShader_MouseDown;
				menuShaders.DropDownItems.Add(menu);
				Text = Path.GetFileNameWithoutExtension(shaderPath); //set name of last loaded shader as window caption
			}

			//todo: if errors disappear we would like to clear the log....
			log.Append(message);
			lastMessage = message;
			glControl.Invalidate();
		}

		private void ShaderKeframes_OnChange(object sender, EventArgs e)
		{
			//recreate sequence bar
			sequenceBar1.Clear();
			foreach (var shaderRatio in demo.ShaderKeyframes.CalculateRatios(soundPlayerBar1.Length))
			{
				//shader sequence
				sequenceBar1.AddItem(new SequenceBarItem(
					Path.GetFileNameWithoutExtension(shaderRatio.Item2), shaderRatio.Item2, shaderRatio.Item1));
			}
			sequenceBar1.CorrectSizes();
		}

		private void Textures_OnChange(object sender, EventArgs e)
		{
			//recreate texture menus
			while (menuTextures.DropDownItems.Count > 1) menuTextures.DropDownItems.RemoveAt(1);
			foreach (var textureName in demo.Textures)
			{
				var menu = new ToolStripMenuItem();
				menu.Text = textureName;
				menu.ToolTipText = "Right click removes shader";
				menu.MouseDown += MenuTexture_MouseDown;
				menuTextures.DropDownItems.Add(menu);
			}
			glControl.Invalidate();
		}

		private void Uniforms_OnAdd(object sender, string uniformName)
		{
			var menu = new ToolStripMenuItem();
			menu.Name = uniformName;
			menu.Text = uniformName;
			menu.ToolTipText = "Right click removes uniform";
			menu.MouseDown += MenuUniform_MouseDown;
			menuUniforms.DropDownItems.Add(menu);
			var menuCopy = new ToolStripMenuItem();
			menuCopy.Text = "Copy";
			menuCopy.ToolTipText = "copy keyframe data to clipboard";
			menuCopy.Click += (snder, args) => demo.Uniforms.GetKeyFrames(uniformName).CopyKeyframesToClipboard();
			menu.DropDownItems.Add(menuCopy);
			var menuPaste = new ToolStripMenuItem();
			menuPaste.Text = "Paste";
			menuPaste.ToolTipText = "paste keyframe data from clipboard";
			menuPaste.Click += (snder, args) => demo.Uniforms.GetKeyFrames(uniformName).PasteKeyframesFromClipboard();
			menu.DropDownItems.Add(menuPaste);
		}

		private void Uniforms_OnRemove(object sender, string uniformName)
		{
			menuUniforms.DropDownItems.RemoveByKey(uniformName);
		}

		private void FormMain_Load(object sender, EventArgs e)
		{
			try
			{
				this.LoadLayout();
				string granularity = Convert.ToString(RegistryLoader.LoadValue(Name, "granularity", menuSizeSetting.Text));
				menuSizeSetting.SelectedIndex = menuSizeSetting.FindString(granularity);
				menuFps.Checked = Convert.ToBoolean(RegistryLoader.LoadValue(Name, "showFPS", false));
				menuCompact.Checked = Convert.ToBoolean(RegistryLoader.LoadValue(Name, "compact", false));
				menuOnTop.Checked = TopMost;

				String[] arguments = Environment.GetCommandLineArgs();
				if (arguments.Length > 1)
				{
					AddShader(arguments[1]);
				}
				else
				{
					//no cmd arguments
					LoadDemo(DefaultFiles.GetAutoSaveDemoFilePath());
					soundPlayerBar1.Position = (float)Convert.ToDouble(RegistryLoader.LoadValue(Name, "time", 0.0));
				}
				soundPlayerBar1.Playing = Convert.ToBoolean(RegistryLoader.LoadValue(Name, "play", false));
			}
			catch (Exception ex)
			{
				log.Append(ex.Message);
			}
		}

		private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				this.SaveLayout();
				RegistryLoader.SaveValue(Name, "play", soundPlayerBar1.Playing);
				RegistryLoader.SaveValue(Name, "granularity", menuSizeSetting.Text);
				RegistryLoader.SaveValue(Name, "time", soundPlayerBar1.Position);
				RegistryLoader.SaveValue(Name, "showFPS", menuFps.Checked);
				RegistryLoader.SaveValue(Name, "compact", menuCompact.Checked);

				multiGraph.SaveLayout();
				log.SaveLayout();
				camera.SaveLayout();
				// rename old
				DefaultFiles.RenameAutoSaveDemoFile();
				// save new
				DemoLoader.SaveToFile(demo, DefaultFiles.GetAutoSaveDemoFilePath());
				demo.Dispose();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void FormMain_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Handled) return;
			switch (e.KeyCode)
			{
				case Keys.Escape: Close(); return;
				case Keys.C: camera.AddKeyFrames(soundPlayerBar1.Position, demo.Uniforms); break;
				case Keys.K:
					if (e.Control)
					{
						multiGraph.AddInterpolatedKeyframeTo(sender, soundPlayerBar1.Position);
					}
					else
					{
						multiGraph.AddInterpolatedKeyframeToVisible(soundPlayerBar1.Position);
					}
					break;
				case Keys.R:
					if (e.Control)
					{
						camera.Reset();
					}
					break;
				case Keys.Space:
					soundPlayerBar1.Playing = !soundPlayerBar1.Playing;
					e.Handled = true;
					break;
				case Keys.Left: soundPlayerBar1.Position -= e.Alt ? 0.1f : 0.5f; break;
				case Keys.Right: soundPlayerBar1.Position += e.Alt ? 0.1f : 0.5f; break;
				case Keys.PageDown: soundPlayerBar1.Position += e.Alt ? 2.0f : 5.0f; break;
				case Keys.PageUp: soundPlayerBar1.Position -= e.Alt ? 2.0f : 5.0f; break;
				case Keys.Home: soundPlayerBar1.Position = 0.0f; break;
				case Keys.End: soundPlayerBar1.Position = soundPlayerBar1.Length; break;
			}
		}

		private void GlControl_MouseDown(object sender, MouseEventArgs e)
		{
			switch(e.Button)
			{
				case MouseButtons.Left: mouseButton = 1; break;
				case MouseButtons.Middle: mouseButton = 2; break;
				case MouseButtons.Right: mouseButton = 3; break;
			}
			glControl.Invalidate();
		}

		private void GlControl_MouseMove(object sender, MouseEventArgs e)
		{
			mousePos = e.Location;
			if (!soundPlayerBar1.Playing) glControl.Invalidate(); //todo: otherwise time stops during update?!
		}

		private void GlControl_MouseUp(object sender, MouseEventArgs e)
		{
			mouseButton = 0;
			glControl.Invalidate();
		}

		private void Reload_Click(object sender, EventArgs e)
		{
			soundPlayerBar1.Position = 0.0f;
			//todo: reload
		}

		private void MenuTexture_MouseDown(object sender, MouseEventArgs e)
		{
			if (MouseButtons.Right != e.Button) return;
			var menu = sender as ToolStripMenuItem;
			if (ReferenceEquals(null,  menu)) return;
			demo.Textures.Remove(menu.Text);
		}

		private void MenuShader_MouseDown(object sender, MouseEventArgs e)
		{
			var menu = sender as ToolStripMenuItem;
			if (ReferenceEquals(null,  menu)) return;
			switch (e.Button)
			{
				case MouseButtons.Left:
					AddShader(menu.Text);
					break;
				case MouseButtons.Right:
					demo.Shaders.RemoveShader(menu.Text);
					demo.ShaderKeyframes.RemoveAllWithName(menu.Text);
					break;
			}
		}

		private void MenuUniform_MouseDown(object sender, MouseEventArgs e)
		{
			var menu = sender as ToolStripMenuItem;
			if (ReferenceEquals(null,  menu)) return;
			switch (e.Button)
			{
				case MouseButtons.Left: ShowUniformGraph(menu.Text); break;
				case MouseButtons.Right: demo.Uniforms.Remove(menu.Text); break;
			}
		}

		private void MenuSound_MouseDown(object sender, MouseEventArgs e)
		{
			if (MouseButtons.Right != e.Button) return;
			demo.TimeSource.Clear();
		}

		private void ShowUniformGraph(string uniformName)
		{
			multiGraph.Show(uniformName);
		}

		private void MenuFullscreen_CheckedChanged(object sender, EventArgs e)
		{
			if (menuFullscreen.Checked)
			{
				//ordering important
				this.TopMost = true;
				this.FormBorderStyle = FormBorderStyle.None;
				this.WindowState = FormWindowState.Maximized;
				this.menuStrip.Visible = false;
				this.panelSequence.Visible = false;
				this.soundPlayerBar1.Visible = false;
			}
			else
			{
				this.TopMost = this.menuOnTop.Checked;
				this.FormBorderStyle = FormBorderStyle.Sizable;
				this.WindowState = FormWindowState.Normal;
				this.menuStrip.Visible = true;
				this.panelSequence.Visible = true;
				this.soundPlayerBar1.Visible = true;
			}
		}

		private void sequenceBar1_OnChanged(object sender, EventArgs e)
		{
			//no udpate events during loading
			demo.ShaderKeyframes.Changed -= ShaderKeframes_OnChange;
			demo.ShaderKeyframes.Clear();
			var ratios = sequenceBar1.Items.Select((item) => new Tuple<float, string>(item.Ratio, item.Data as string));
			var keyframes = ShaderKeyframes.CalculatePosFromRatios(ratios, soundPlayerBar1.Length);
			foreach (var kf in keyframes)
			{
				demo.ShaderKeyframes.AddUpdate(kf.Item1, kf.Item2);
			}
			demo.ShaderKeyframes.Changed += ShaderKeframes_OnChange;
			glControl.Invalidate();
		}

		private void TextUniformAdd_KeyDown(object sender, KeyEventArgs e)
		{
			if (Keys.Enter == e.KeyCode)
			{
				string text = TextUniformAdd.Text;
				if (demo.Uniforms.Add(text))
				{
					var kfs = demo.Uniforms.GetKeyFrames(text);
					TextUniformAdd.Text = string.Empty;
					if (ReferenceEquals(null,  kfs)) return;
					kfs.AddUpdate(0.0f, 0.0f);
					kfs.AddUpdate(soundPlayerBar1.Length, soundPlayerBar1.Length);
					ShowUniformGraph(text);
				}
			}
		}

		private void TextUniformAdd_TextChanged(object sender, EventArgs e)
		{
			TextUniformAdd.BackColor = UniformHelper.IsNameValid(TextUniformAdd.Text) ? Color.PaleGreen : Color.LightSalmon;
		}

		private void clearToolStripMenuItem_Click(object sender, EventArgs e)
		{
			demo.Clear();
			camera.Reset();
		}

		private void addCameraUniformsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			camera.AddKeyFrames(demo.TimeSource.Position, demo.Uniforms);
		}

		private void menuOnTop_CheckedChanged(object sender, EventArgs e)
		{
			TopMost = menuOnTop.Checked;
		}

		private void logToolStripMenuItem_Click(object sender, EventArgs e)
		{
			log.Show();
		}

		private void cameraWindowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			camera.Show();
		}

		private void menuCompact_CheckStateChanged(object sender, EventArgs e)
		{
			if (menuCompact.Checked)
			{
				CompactView();
			}
			else
			{
				NormalView();
			}
		}

		private void NormalView()
		{
			FormBorderStyle = FormBorderStyle.Sizable;
			this.menuStrip.Visible = true;
			this.panelSequence.Visible = true;
			this.soundPlayerBar1.Visible = true;
		}

		private void CompactView()
		{
			//ordering important
			this.FormBorderStyle = FormBorderStyle.None;
			var bounds = Bounds;
			this.menuStrip.Visible = false;
			this.panelSequence.Visible = false;
			this.soundPlayerBar1.Visible = false;
			this.Bounds = bounds;
		}

		private void glControl_MouseEnter(object sender, EventArgs e)
		{
			//if (menuCompact.Checked)
			//{
			//	NormalView();
			//	Activate();
			//}
		}

		private void FormMain_Deactivate(object sender, EventArgs e)
		{
			if (menuCompact.Checked) CompactView();
		}
	}
}
