using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ControlClassLibrary
{
	public partial class SequenceBar : UserControl
	{
		public SequenceBar()
		{
			InitializeComponent();
			menuStrip.Cursor = Cursors.VSplit;
			Clear();
		}

		public void AddItem(SequenceBarItem item)
		{
			var menu = new ToolStripMenuItem(item.Label);
			menu.ToolTipText = item.Label;
			menu.AutoSize = false;
			menu.Height = menuStrip.Height;
			menu.BackColor = NewColor();
			menu.MouseDown += MenuItem_MouseDown;
			menu.MouseUp += MenuItem_MouseUp;
			menu.MouseMove += MenuItem_MouseMove;
			menu.Tag = item;
			UpdateWidthFromRatio(menu);
			menuStrip.Items.Add(menu);
		}

		public void Clear()
		{
			menuStrip.Items.Clear();
			rnd = new Random(12);
		}

		public void CorrectSizes()
		{
			if (2 > menuStrip.Items.Count) return;
			//two or more items -> no item should cover complete bar
			foreach (ToolStripMenuItem menu in menuStrip.Items)
			{
				if (menu.Width == menuStrip.Width)
				{
					menu.Width = menuStrip.Width / 2;
					UpdateRatioFromWidth(menu);
				}
				if (menu.Width < 10)
				{
					menu.Width = 10;
					UpdateRatioFromWidth(menu);
				}
			}
			menuStrip_Paint(null, null);
		}

		public event EventHandler Changed; 
		public IEnumerable<SequenceBarItem> Items { get { return new SequenceBarItemEnumerator(menuStrip.Items.GetEnumerator()); } }

		private Random rnd;
		private ToolStripMenuItem resizingMenu;

		private Color NewColor()
		{
			return Color.FromArgb(130 + rnd.Next(125), 130 + rnd.Next(125), 130 + rnd.Next(125));
		}

		private void UpdateRatioFromWidth(ToolStripMenuItem menu)
		{
			var item = menu.Tag as SequenceBarItem;
			if (ReferenceEquals(null,  item)) throw new Exception("menu.tag is no SequenceBarItem");
			item.Ratio = menu.Width / (float)menuStrip.Width;
		}

		private void UpdateWidthFromRatio(ToolStripMenuItem menu)
		{
			var item = menu.Tag as SequenceBarItem;
			if (ReferenceEquals(null,  item)) throw new Exception("menu.tag is no SequenceBarItem");
			menu.Width = (int)Math.Round(menuStrip.Width * item.Ratio);
		}

		private void MenuItem_MouseUp(object sender, MouseEventArgs e)
		{
			if (MouseButtons.Right == e.Button && !ReferenceEquals(null,  resizingMenu))
			{
				menuStrip.Items.Remove(resizingMenu);
				CorrectSizes();
			}
			resizingMenu = null;
		}

		private void MenuItem_MouseMove(object sender, MouseEventArgs e)
		{
			if (!ReferenceEquals(null,  resizingMenu))
			{
				int x = menuStrip.PointToClient(MousePosition).X;
				resizingMenu.Width = Math.Max(10, x - resizingMenu.Bounds.Left);
				UpdateRatioFromWidth(resizingMenu);
			}
		}

		private void MenuItem_MouseDown(object sender, MouseEventArgs e)
		{
			resizingMenu = sender as ToolStripMenuItem;
		}

		private void menuStrip_Paint(object sender, PaintEventArgs e)
		{
			if (0 == menuStrip.Items.Count) return;
			var menu = menuStrip.Items[menuStrip.Items.Count - 1] as ToolStripMenuItem;
			if (ReferenceEquals(null,  menu)) return;
			menu.Width = menuStrip.Width - menu.Bounds.X;
			UpdateRatioFromWidth(menu);
			Changed?.Invoke(this, new EventArgs());
		}

		private void menuStrip_Resize(object sender, EventArgs e)
		{
			foreach (ToolStripMenuItem menu in menuStrip.Items)
			{
				UpdateWidthFromRatio(menu);
			}
			menuStrip_Paint(null, null);
		}
	}
}
