using GameLinker.Forms;
using GameLinker.Helpers;
using GameLinker.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameLinker
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (Settings.Default.MaxUploadThreads == -1)
            {
                Settings.Default.MaxUploadThreads = Environment.ProcessorCount - 1;
                Settings.Default.Save();
            }
            Application.Run(new Library());
        }
    }
}
