using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ControlClassLibrary
{
	public partial class TrackView : UserControl
	{
		public TrackView()
		{
			InitializeComponent();
			Clear();
			Length = 100f;
			for (int i = 0; i < 10; ++i)
			{
				AddItem("t" + i.ToString(), (float)i * 10, 10f, i % 5);
			}
		}

		public void Clear()
		{
			Controls.Clear();
			rnd = new Random(12);
			trackItems.Clear();
		}

		public float Length { get; set; }

		public int GetTrackHeight()
		{
			return Height / trackCount;
		}

		public const int trackCount = 5;

		public void Update(IEnumerable<ITrackItem> trackItems)
		{
			foreach (var item in trackItems)
			{
			}
		}

		private Random rnd;
		
		private void AddItem(string label, float start, float length, int track)
		{
			trackItems.Add(new TrackItemVisual(this, new TrackItem(label, start, length, track)));
		}

		public Color NewColor()
		{
			return Color.FromArgb(130 + rnd.Next(125), 130 + rnd.Next(125), 130 + rnd.Next(125));
		}

		private List<TrackItemVisual> trackItems = new List<TrackItemVisual>();

		private void TrackView_Resize(object sender, EventArgs e)
		{
			foreach(var item in trackItems)
			{

			}
		}
	}
}
