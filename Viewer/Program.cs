using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeometricAlgorithms.Viewer
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

            //if in debug the exceptions should be cought by the IDE, not by the program
            if (IsDebug)
            {
                Run();
            }
            else
            {
                try
                {
                    Run();
                }
                catch (Exception e)
                {
                    MessageBox.Show($"An Error occured: {e.Message}");
                }
            }

        }

        static void Run()
        {
            Application.Run(new MainWindow());
        }

        static bool IsDebug
        {
            get
            {
#if DEBUG
                return true;
#else
            return false;
#endif
            }
        }

    }
}
