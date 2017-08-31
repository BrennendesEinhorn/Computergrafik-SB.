using DMS.Geometry;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LevelEditor
{
	public static class EditorTools
	{
		public static string ResolveName(string name, string parentName)
		{
			return string.IsNullOrWhiteSpace(name) ? parentName : name;
		}

		public static System.Drawing.Bitmap ToBitmap(this ImageSource imageSource)
		{
			MemoryStream ms = new MemoryStream();
			var encoder = new System.Windows.Media.Imaging.PngBitmapEncoder();
			encoder.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(imageSource as System.Windows.Media.Imaging.BitmapSource));
			encoder.Save(ms);
			return new System.Drawing.Bitmap(ms);
		}

		/// <summary>
		/// Transforms elements coordinates relative to given canvas system into [0,1]² with y-axis heading upwards
		/// </summary>
		/// <param name="element">Input UIElement to transform relative to canvas</param>
		/// <param name="canvas">Input canvas for realative transformation</param>
		public static Box2D ConvertBounds(this UIElement element, Canvas canvas)
		{
			var p00 = new Point(0, 0);
			var p11 = p00 + (Vector)element.RenderSize;
			var leftTop = (Vector)element.TranslatePoint(p00, canvas);
			var rightBottom = (Vector)element.TranslatePoint(p11, canvas);
			//flip y coordinate
			return Box2dExtensions.CreateFromMinMax(
				(float)leftTop.X, (float)(canvas.ActualHeight - rightBottom.Y),
				(float)rightBottom.X, (float)(canvas.ActualHeight - leftTop.Y));
		}
	}
}
