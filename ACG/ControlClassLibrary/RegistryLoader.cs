using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace ControlClassLibrary
{
	public static class RegistryLoader
	{
		public static void LoadLayout(this Form form)
		{
			RegistryKey keyApp = Application.UserAppDataRegistry;
			if (ReferenceEquals(null,  keyApp)) return;
			var key = keyApp.CreateSubKey(form.Name);
			if (ReferenceEquals(null,  key)) return;
			form.WindowState = (FormWindowState)Convert.ToInt32(key.GetValue("WindowState", (int)form.WindowState));
			form.Visible = Convert.ToBoolean(key.GetValue("visible", form.Visible));
			form.Width = Convert.ToInt32(key.GetValue("Width", form.Width));
			form.Height = Convert.ToInt32(key.GetValue("Height", form.Height));
			form.Top = Convert.ToInt32(key.GetValue("Top", form.Top));
			form.Left = Convert.ToInt32(key.GetValue("Left", form.Left));
			form.TopMost = Convert.ToBoolean(key.GetValue("TopMost", form.TopMost));
		}

		public static void SaveLayout(this Form form)
		{
			RegistryKey keyApp = Application.UserAppDataRegistry;
			if (ReferenceEquals(null,  keyApp)) return;
			var key = keyApp.CreateSubKey(form.Name);
			if (ReferenceEquals(null,  key)) return;
			key.SetValue("WindowState", (int)form.WindowState);
			key.SetValue("visible", form.Visible);
			key.SetValue("Width", form.Width);
			key.SetValue("Height", form.Height);
			key.SetValue("Top", form.Top);
			key.SetValue("Left", form.Left);
			key.SetValue("TopMost", form.TopMost);
		}

		public static object LoadValue(string keyName, string name, object defaultValue)
		{
			RegistryKey keyApp = Application.UserAppDataRegistry;
			if (ReferenceEquals(null,  keyApp)) return null;
			var key = keyApp.CreateSubKey(keyName);
			if (ReferenceEquals(null,  key)) return null;
			return key.GetValue(name, defaultValue);
		}

		public static void SaveValue(string keyName, string name, object value)
		{
			RegistryKey keyApp = Application.UserAppDataRegistry;
			if (ReferenceEquals(null,  keyApp)) return;
			var key = keyApp.CreateSubKey(keyName);
			if (ReferenceEquals(null,  key)) return;
			key.SetValue(name, value);
		}
	}
}
