using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading;

namespace Raytracer
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

		private void FormMain_Load(object sender, EventArgs e)
		{
			try
			{
				Microsoft.Win32.RegistryKey keyApp = Application.UserAppDataRegistry;
				if (null == keyApp)
				{
					return;
				}
				WindowState = (FormWindowState)Convert.ToInt32(keyApp.GetValue("windowState", (int)WindowState));
				Width = Convert.ToInt32(keyApp.GetValue("width", Width));
				Height = Convert.ToInt32(keyApp.GetValue("height", Height));
				Top = Convert.ToInt32(keyApp.GetValue("top", Top));
				Left = Convert.ToInt32(keyApp.GetValue("left", Left));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				Microsoft.Win32.RegistryKey keyApp = Application.UserAppDataRegistry;
				if (null == keyApp)
				{
					return;
				}
				keyApp.SetValue("windowState", (int)WindowState);
				keyApp.SetValue("width", Width);
				keyApp.SetValue("height", Height);
				keyApp.SetValue("top", Top);
				keyApp.SetValue("left", Left);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void FormMain_Shown(object sender, EventArgs e)
		{
			menuItemTrace.PerformClick();
		}

		static private double Legalize(double d)
        {
            return d > 1 ? 1 : d;
        }

        static private System.Drawing.Color ToDrawingColor(Color c)
        {
            return System.Drawing.Color.FromArgb((int)(Legalize(c.R) * 255), (int)(Legalize(c.G) * 255), (int)(Legalize(c.B) * 255));
        }

		private int m_iCurRendering = 0;
		private Queue<Pixel> m_quePixel = new Queue<Pixel>();
		private List<Bitmap> m_lstBmp = new List<Bitmap>();

        private void backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
			Size s = m_lstBmp[m_iCurRendering].Size;
            Camera cam = new Camera(new Vector3(5, 2, 6), new Vector3(-1, .5, 0), s.Width, s.Height);
            long lastUpdate = 0;
			int cnt = 0;
            Stopwatch sw = Stopwatch.StartNew();
            backgroundWorker.ReportProgress(0);
			Visual vis = e.Argument as Visual;
			if (null == vis) return;
			vis.renderImage(InputScene.Scene1, cam, (int x, int y, Color color) =>
            {
				if (backgroundWorker.CancellationPending) return;
				lock (m_quePixel)
				{
					m_quePixel.Enqueue(new Pixel(x, y, ToDrawingColor(color)));
				}
				Interlocked.Increment(ref cnt);
                long time = sw.ElapsedMilliseconds;
                if (lastUpdate + 100 < time)
                {
                    lastUpdate = time;
					backgroundWorker.ReportProgress(cnt * 100 / (s.Width * s.Height));
				}
            }
			);
			backgroundWorker.ReportProgress(100, sw.ElapsedMilliseconds);
		}

		private Visual createVisual()
		{
			int multi = 1;
			if(!Int32.TryParse(comboBoxMulti.Text, out multi))
			{
				multi = 1;
			}
			return new Visual { 
				m_iMultiSamples = multi 
			};
		}

        private void backgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
			progressBar.Value = e.ProgressPercentage;
			Bitmap bitmap = pictureBox.Image as Bitmap;
			if (null == bitmap) return;

			int count = m_quePixel.Count;
			lock (m_quePixel) for (int i = 0; i < count; ++i)
			{
				Pixel p = m_quePixel.Dequeue();
				p.draw(bitmap);
			}
			pictureBox.Refresh();

			if (e.UserState is long)
			{
				menuItemTrace.Text = "Trace: " + ((long)e.UserState).ToString();
			}
		}

		private void menuItemNext_Click(object sender, EventArgs e)
		{
			++m_iCurRendering;
			if (m_lstBmp.Count < m_iCurRendering + 1) m_iCurRendering = m_lstBmp.Count - 1;
			pictureBox.Image = m_lstBmp[m_iCurRendering];
			Text = "Rendering: " + m_iCurRendering.ToString();
		}

		private void menuItemPrevious_Click(object sender, EventArgs e)
		{
			--m_iCurRendering;
			if(0 > m_iCurRendering) m_iCurRendering = 0;
			pictureBox.Image = m_lstBmp[m_iCurRendering];
			Text = "Rendering: " + m_iCurRendering.ToString();
		}

		private void menuItemSave_Click(object sender, EventArgs e)
		{
			try
			{
				Bitmap bmp = m_lstBmp[m_iCurRendering];
				saveFileDialog.FileName = "raytrace " 
					+ bmp.Width.ToString()+ 'x' + bmp.Height.ToString()
					+ m_iCurRendering.ToString();
				DialogResult res = saveFileDialog.ShowDialog();
				if (DialogResult.OK == res)
				{
					m_lstBmp[m_iCurRendering].Save(saveFileDialog.FileName);
				}
			}
			finally { }
		}

		private void menuItemTrace_Click(object sender, EventArgs e)
		{
			if (!backgroundWorker.IsBusy)
			{
				menuItemTrace.Text = "Traceing...";
				progressBar.Value = 0;
				updateImage();
				backgroundWorker.RunWorkerAsync(createVisual());
			}
		}

		private void updateImage()
		{
			Size s = pictureBox.ClientSize;
			Bitmap bitmap = new Bitmap(s.Width, s.Height);
			if (null == pictureBox.Image)
			{
				bitmap = new Bitmap(s.Width, s.Height);
			}
			else
			{
				bitmap = new Bitmap(pictureBox.Image as Bitmap, s.Width, s.Height);
			}
			pictureBox.Image = bitmap;
            m_iCurRendering = m_lstBmp.Count;
            Text = "Rendering: " + m_iCurRendering.ToString();
            m_lstBmp.Add(bitmap);
        }

		private void pictureBox_MouseClick(object sender, MouseEventArgs e)
		{
			updateImage();
			Bitmap bitmap = pictureBox.Image as  Bitmap;
			if (null == bitmap) return;
            Camera cam = new Camera(new Vector3(5, 2, 6), new Vector3(-1, .5, 0), bitmap.Width, bitmap.Height);
			Color color = createVisual().renderPixel(InputScene.Scene1, cam, e.X, e.Y);
			bitmap.SetPixel(e.X,e.Y, ToDrawingColor(color));
		}
    }
}
