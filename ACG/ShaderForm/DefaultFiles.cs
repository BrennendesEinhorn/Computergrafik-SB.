using System;
using System.IO;
using System.Windows.Forms;

namespace ShaderForm
{
    public static class DefaultFiles
    {
        public static string GetDemoExtension()
        {
            return fileExt;
        }

        public static string GetAutoSaveDemoFilePath()
        {

            return Path.GetDirectoryName(Application.ExecutablePath) + Path.DirectorySeparatorChar + fileName + GetDemoExtension();
        }

        public static void RenameAutoSaveDemoFile()
        {
            try
            {
                var autoSaveFileName = GetAutoSaveDemoFilePath();
                var dt = File.GetLastWriteTime(autoSaveFileName);
                var newFileName = Path.GetDirectoryName(Application.ExecutablePath) + Path.DirectorySeparatorChar 
                    + fileName + " " + DateTime.Now.ToString("yyyyMMdd HHmmss") + fileExt;
                File.Move(autoSaveFileName, newFileName);
            }
            catch { }
        }

        private const string fileName = "default_save";
        private const string fileExt = ".config.xml";
    }
}
