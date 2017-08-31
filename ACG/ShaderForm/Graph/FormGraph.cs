using ControlClassLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ShaderForm.Graph
{
	/// <summary>
	/// a line graph form with points with x coordinate as key (function)
	/// </summary>
	public partial class FormGraph : Form
	{
		public delegate void ChangedPointsHandler(IEnumerable<KeyValuePair<float, float>> points);
		public event ChangedPointsHandler ChangedPoints;
		public delegate void ChangedPositionHandler(double position);
		public event ChangedPositionHandler ChangedPosition;
		public event EventHandler CopyCommand;
		public event EventHandler PasteCommand;

		public FormGraph(string name)
		{
			InitializeComponent();
			Name = "FormGraph_" + name;
			Text = name + " (graph)";

			chart1.MouseDown += chart1_MouseDown;
			chart1.MouseUp += chart1_MouseUp;
			chart1.MouseMove += chart1_MouseMove;
			chart1.MouseWheel += Chart1_MouseWheel;

			chart1.Legends.Clear();
			var area = chart1.ChartAreas.First();
			area.AxisX.IsStartedFromZero = true;
			area.CursorX.IsUserEnabled = false;
			area.CursorY.IsUserEnabled = false;
			area.CursorX.IsUserSelectionEnabled = false;
			area.CursorY.IsUserSelectionEnabled = false;
			area.CursorX.AutoScroll = false;
			area.CursorY.AutoScroll = false;
			area.CursorX.Position = 0.0;
			//area.CursorX.Interval = 5.5;
			area.CursorX.LineWidth = 5;
			area.CursorX.LineColor = Color.Salmon;
			//area.AxisX.ScaleView.Zoomable = true;
			//area.AxisY.ScaleView.Zoomable = true;
			area.AxisX.LabelStyle.Format = "#####0.##";
			area.AxisY.LabelStyle.Format = "###0.####";

			var series = chart1.Series.First();
			series.ChartType = SeriesChartType.Line;
			series.IsValueShownAsLabel = true;
			series.LabelBackColor = Color.LightSkyBlue;
			series.LabelFormat = "###0.####";
			series.BorderWidth = 4;
			series.MarkerStyle = MarkerStyle.Circle;
			series.MarkerSize = 15;
			series.MarkerColor = Color.LightSkyBlue;
			this.LoadLayout();
		}

		public void SetPoints(IEnumerable<KeyValuePair<float, float>> points)
		{
			var series = chart1.Series.First();
			series.Points.Clear();
			if (ReferenceEquals(null,  points)) return;
			foreach (var p in points)
			{
				series.Points.AddXY(p.Key, p.Value);
			}
		}

		public void UpdatePosition(float position)
		{
			var area = chart1.ChartAreas.First();
			area.CursorX.Position = position;
		}

		private int selectedPoint = -1;
		private bool dragingCursor = false;

		private void FormGraph_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;
			Visible = false;
		}

		private void Chart1_MouseWheel(object sender, MouseEventArgs e)
		{
			//if (0 < e.Delta) return; //no zoom out
			var area = chart1.ChartAreas.First();
			//area.AxisX.ScaleView.Zoom(-10.0, 10.0);
			double factor = 0 < e.Delta ? e.Delta / 50.0 : 50.0 / Math.Abs(e.Delta);

			double deltaX = area.AxisX.ScaleView.ViewMaximum - area.AxisX.ScaleView.ViewMinimum;
			double deltaY = area.AxisY.ScaleView.ViewMaximum - area.AxisY.ScaleView.ViewMinimum;

			double x = area.AxisX.PixelPositionToValue(e.X);
			double y = area.AxisY.PixelPositionToValue(e.Y);

			double posXStart = x - deltaX * factor;
			double posXFinish = x + deltaX * factor;
			double posYStart = y - deltaY * factor;
			double posYFinish = y + deltaY * factor;

			area.AxisX.ScaleView.Zoom(posXStart, posXFinish);
			area.AxisY.ScaleView.Zoom(posYStart, posYFinish);
		}

		private void chart1_MouseDown(object sender, MouseEventArgs e)
		{
			var result = chart1.HitTest(e.X, e.Y);
			switch (result.ChartElementType)
			{
				case ChartElementType.DataPoint:
					switch (e.Button)
					{
						case MouseButtons.Left: selectedPoint = result.PointIndex; break;
						case MouseButtons.Right: RemovePoint(result.PointIndex); break;
					}
					break;
				case ChartElementType.DataPointLabel: EditDataPoint(result.PointIndex); break;
				default:
					switch (e.Button)
					{
						case MouseButtons.Left:
							SetCursorPositionFromMouse(e.X);
							dragingCursor = true;
							break;
					}
					break;
			}
		}

		private void RemovePoint(int pointIndex)
		{
			var series = chart1.Series.First();
			series.Points.RemoveAt(pointIndex);
			CallOnChangePoint(series.Points);
		}

		private void EditDataPoint(int pointIndex)
		{
			var points = chart1.Series.First().Points;
			var point = points[pointIndex];
			var value = point.YValues[0];
			point.YValues[0] = InputBox.Show((float)value);
			ZoomFit();
			CallOnChangePoint(points);
		}

		//private void AddPointFromMouse(int mouseX, int mouseY)
		//{
		//	double x = ConvertX(mouseX);
		//	double y = ConvertY(mouseY);
		//	var series = chart1.Series.First();
		//	series.Points.AddXY(x, y);
		//	series.Sort(PointSortOrder.Ascending, "X");
		//	CallOnChangePoint(series.Points);
		//}

		private void SetCursorPositionFromMouse(int mouseX)
		{
			double x = ConvertX(mouseX);
			var area = chart1.ChartAreas.First();
			area.CursorX.Position = x;
			ChangedPosition?.Invoke(x);
		}

		private void chart1_MouseMove(object sender, MouseEventArgs e)
		{
			if (-1 != selectedPoint)
			{
				var points = chart1.Series.First().Points;
				var point = points[selectedPoint];

				if (moveAxisYToolStripMenuItem.Checked)
				{
					double valueY = ConvertY(e.Y);
					point.YValues[0] = valueY;
				}
				else
				{
					double valueX = ConvertX(e.X);
					point.XValue = valueX;
					valueToolStripMenuItem.Text = "Time:" + valueX.ToString();
				}
				chart1.Invalidate();
				CallOnChangePoint(points);
			}
			if (dragingCursor)
			{
				SetCursorPositionFromMouse(e.X);
			}
		}

		private void CallOnChangePoint(DataPointCollection points)
		{
			if (!ReferenceEquals(null,  ChangedPoints))
			{
				var lst = new List<KeyValuePair<float, float>>();
				foreach (var point in points)
				{
					lst.Add(new KeyValuePair<float, float>((float)point.XValue, (float)point.YValues[0]));
				}
				ChangedPoints(lst);
			}
		}

		private void chart1_MouseUp(object sender, MouseEventArgs e)
		{
			selectedPoint = -1;
			dragingCursor = false;
			if (MouseButtons.Middle == e.Button)
			{
				ZoomFit();
			}
		}

		private void ZoomFit()
		{
			var area = chart1.ChartAreas.First();
			area.AxisX.ScaleView.ZoomReset();
			area.AxisY.ScaleView.ZoomReset();
			area.RecalculateAxesScale();
		}

		private double ConvertX(int x)
		{
			return Math.Round(chart1.ChartAreas.First().AxisX.PixelPositionToValue(
				Math.Max(Math.Min(x, chart1.Size.Width - 1), 0)), 2);
		}

		private double ConvertY(int y)
		{
			return chart1.ChartAreas.First().AxisY.PixelPositionToValue(
				Math.Max(Math.Min(y, chart1.Size.Height - 1), 0));
		}

		private void chart1_MouseHover(object sender, EventArgs e)
		{
			//var pos = chart1.PointToClient(MousePosition);
			//var result = chart1.HitTest(pos.X, pos.Y);
			//this.Text = result.ChartElementType.ToString();
			//if (ChartElementType.DataPoint == result.ChartElementType)
			//{
			//	var points = chart1.Series.First().Points;
			//	var point = points[result.PointIndex];
			//	point.Color = Color.Salmon;
			//	chart1.Invalidate();
			//}
		}

		private void copyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CopyCommand?.Invoke(sender, e);
		}

		private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			PasteCommand?.Invoke(sender, e);
		}

		private void roundToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var points = chart1.Series.First().Points;
			for (int i = 0; i < points.Count; ++i)
			{
				var point = points[i];
				point.YValues[0] = Math.Round(point.YValues[0]);
			}
			chart1.Invalidate();
			CallOnChangePoint(points);
		}

		private void moveAxisToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
		{
			if (moveAxisYToolStripMenuItem.Checked)
				moveAxisYToolStripMenuItem.Text = "Move Y";
			else
				moveAxisYToolStripMenuItem.Text = "Move X";
		}
	}
}
