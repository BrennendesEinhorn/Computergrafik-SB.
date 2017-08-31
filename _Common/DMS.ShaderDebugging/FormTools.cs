using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DMS.ShaderDebugging
{
	public static class FormTools
	{
		public static bool IsPartlyOnScreen(Rectangle bounds)
		{
			return Screen.AllScreens.Any(s => s.WorkingArea.IntersectsWith(bounds));
		}
		public static bool IsPointOnScreen(Point point)
		{
			return Screen.AllScreens.Any(s => s.WorkingArea.Contains(point));
		}
	}
}
