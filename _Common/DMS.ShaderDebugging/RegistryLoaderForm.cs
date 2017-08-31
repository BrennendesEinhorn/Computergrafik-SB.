using Microsoft.Win32;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DMS.ShaderDebugging
{
	public static class RegistryLoaderForm
	{
		public static RegistryKey GetAppKey()
		{
			return Application.UserAppDataRegistry;
		}

		public static void LoadLayout(this Form form)
		{
			RegistryKey keyApp = GetAppKey();
			if (ReferenceEquals(null, keyApp)) return;
			var key = keyApp.CreateSubKey(form.Name);
			if (ReferenceEquals(null, key)) return;
			form.WindowState = (FormWindowState)Convert.ToInt32(key.GetValue("WindowState", (int)form.WindowState));
			form.Visible = Convert.ToBoolean(key.GetValue("visible", form.Visible));
			form.Width = Convert.ToInt32(key.GetValue("Width", form.Width));
			form.Height = Convert.ToInt32(key.GetValue("Height", form.Height));
			var top = Convert.ToInt32(key.GetValue("Top", form.Top));
			var left = Convert.ToInt32(key.GetValue("Left", form.Left));
			if (FormTools.IsPartlyOnScreen(new Rectangle(left + 10, top + 10, 200, 10))) //check if part of the windows title bar is visible
			{
				form.Top = top;
				form.Left = left;
			}
		}

		public static void SaveLayout(this Form form)
		{
			RegistryKey keyApp = GetAppKey();
			if (ReferenceEquals(null, keyApp)) return;
			var key = keyApp.CreateSubKey(form.Name);
			if (ReferenceEquals(null, key)) return;
			key.SetValue("WindowState", (int)form.WindowState);
			key.SetValue("visible", form.Visible);
			key.SetValue("Width", form.Width);
			key.SetValue("Height", form.Height);
			key.SetValue("Top", form.Top);
			key.SetValue("Left", form.Left);
		}
	}
}
