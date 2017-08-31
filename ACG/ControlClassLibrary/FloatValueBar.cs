using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ControlClassLibrary
{
	[DefaultEvent("ValueChanged")]
	[DefaultBindingProperty("Value")]
	public partial class FloatValueBar : UserControl
	{
		[Description("Called after Value has changed.")]
		public event EventHandler ValueChanged;

		public FloatValueBar()
		{
			ForeColor = Color.White;
			BackColor = Color.Gray;
			InitializeComponent();
			DoubleBuffered = true;
			ResizeRedraw = true;
		}

		public float Value { 
			get { return m_fValue; } 
			set 
			{
				if (value == this.m_fValue) return;
				this.m_fValue = Math.Min(Math.Max(value, this.m_fMin), this.m_fMax);
				ValueChanged?.Invoke(this, null);
				Invalidate();
			} 
		}
		
		public float Min { 
			get { return m_fMin; } 
			set 
			{
				m_fMin = Math.Min(value, Max);
				Value = Math.Max(Min, Value);
			} 
		}
		
		public float Max { 
			get { return m_fMax; } 
			set 
			{ 
				m_fMax = Math.Max(value, Min);
				Value = Math.Min(Max, Value);
			}
		}

		public byte Decimals { get { return m_decimals; } set { m_decimals = value; Invalidate(); } }
		
		/// <summary>
		/// set all interdependent values at once; is usefull if consecutive updates to max, value and min fail or event code changes inputs
		/// </summary>
		/// <param name="value_"></param>
		/// <param name="min_"></param>
		/// <param name="max_"></param>
		public void Set(float value_, float min_, float max_)
		{
			if (max_ < min_) throw new ArgumentException("Maximum smaller than minimum!");
			if (value_ < min_ || max_ < value_) throw new ArgumentException("Value outside [min, max]!");
			m_fMin = min_;
			m_fMax = max_;
			Value = value_;
		}

		[Browsable(true)]
		public bool ShowText { get { return m_bShowText; } set { m_bShowText = value; Invalidate(); } }

		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Bindable(true)]
		public override string Text { get { return m_sText; } set { m_sText = value; Invalidate(); } }

		[Browsable(false)]
		public bool Interacting { get { return m_bMarking; } } 

		public Color BarColor { get { return m_barColor; } set { m_barColor = value; Invalidate(); } }

		private bool m_bMarking;
		private float m_fValue = 0;
		private float m_fMin = 0;
		private float m_fMax = 1;
		private Color m_barColor = Color.Green;
		private bool m_bShowText;
		private string m_sText;
		private byte m_decimals = 4;

		/// <summary>
		/// Redraws the control in response to a WinForms paint message.
		/// </summary>
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			e.Graphics.Clear(this.BackColor);
			using (Brush brush = new SolidBrush(this.BarColor))
			{
				float diff = Max - Min;
				if (0 == diff) diff = 1.0f;
				float normValue = (Value - Min) / diff;
				int iValue = (int)(normValue * ClientRectangle.Width);
				e.Graphics.FillRectangle(brush, new Rectangle(Math.Max(iValue - 1, 0), ClientRectangle.Y, 2, ClientRectangle.Height));
			}
			using (Brush brush = new SolidBrush(this.ForeColor))
			{
				using (StringFormat format = new StringFormat())
				{
					format.Alignment = StringAlignment.Center;
					format.LineAlignment = StringAlignment.Center;
					string sMsg = "";
					if (ShowText)
					{
						sMsg += this.Text;
					}
					sMsg += Math.Round(Value, Decimals).ToString();
					e.Graphics.DrawString(sMsg, Font, brush, ClientRectangle, format);
				}
			}
		}
		private void MarkerBar_MouseDown(object sender, MouseEventArgs e)
		{
			m_bMarking = true;
			MarkerBar_MouseMove(null, e);
		}

		private void MarkerBar_MouseMove(object sender, MouseEventArgs e)
		{
			if (m_bMarking)
			{
				Point pt = Clip(e.Location, ClientRectangle);
				float normValue = pt.X / ((float)ClientRectangle.Width - 2);
				Value = Min + (Max - Min) * normValue;
				Invalidate();
			}
		}

		private void MarkerBar_MouseUp(object sender, MouseEventArgs e)
		{
			m_bMarking = false;
			ValueChanged?.Invoke(this, null);
		}
		public Point Clip(Point point_, Rectangle rect_)
		{
			if (point_.X < rect_.Left)
			{
				point_.X = rect_.Left;
			}
			else if (point_.X > rect_.Right)
			{
				point_.X = rect_.Right;
			}
			if (point_.Y < rect_.Top)
			{
				point_.Y = rect_.Top;
			}
			else if (point_.Y > rect_.Bottom)
			{
				point_.Y = rect_.Bottom;
			}
			return point_;
		}
	}
}
