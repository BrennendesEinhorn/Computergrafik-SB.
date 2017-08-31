using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace DMS.Base
{
	public static class PathTools
	{
		public static string GetFullPath(string fileName)
		{
			try
			{
				return Path.GetFullPath(fileName);
			}
			catch
			{
				return fileName;
			}
		}

		/// <summary>
		/// returns the relative path. if no relative path is valid, the absolut path is returned.
		/// </summary>
		/// <param name="fromPath">the path the result should be relative to</param>
		/// <param name="toPath">the path to be converted into relative form</param>
		/// <returns></returns>
		public static string GetRelativePath(string fromPath, string toPath)
		{
			if (string.IsNullOrEmpty(fromPath) || string.IsNullOrEmpty(toPath)) return toPath;
			try
			{
				int fromAttr = GetPathAttribute(fromPath);
				int toAttr = GetPathAttribute(toPath);

				StringBuilder path = new StringBuilder(5260); // todo: should we use MAX_PATH?
				if (0 == SafeNativeMethods.PathRelativePathTo(path, fromPath, fromAttr, toPath, toAttr))
				{
					return toPath;
				}
				return path.ToString();
			}
			catch
			{
				return toPath;
			}
		}

		public static string GetSourceFilePath([CallerFilePath] string doNotAssignCallerFilePath = "")
		{
			return doNotAssignCallerFilePath;
		}

		private const int FILE_ATTRIBUTE_DIRECTORY = 0x10;
		private const int FILE_ATTRIBUTE_NORMAL = 0x80;

		private static int GetPathAttribute(string path)
		{
			DirectoryInfo di = new DirectoryInfo(path);
			if (di.Exists)
			{
				return FILE_ATTRIBUTE_DIRECTORY;
			}

			FileInfo fi = new FileInfo(path);
			if (fi.Exists)
			{
				return FILE_ATTRIBUTE_NORMAL;
			}

			throw new FileNotFoundException();
		}

		internal static class SafeNativeMethods
		{
			[DllImport("shlwapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
			internal static extern int PathRelativePathTo(StringBuilder pszPath,
				string pszFrom, int dwAttrFrom, string pszTo, int dwAttrTo);
		}
	}
}
