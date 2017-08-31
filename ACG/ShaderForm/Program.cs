using OpenTK;
using System;
using System.Windows.Forms;

namespace ShaderForm
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
			Toolkit.Init(); //todo: check if newer version of glcontrol fixes this issue
			Application.Run(new FormMain());
        }
    }
}
