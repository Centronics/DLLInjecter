using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace Loader
{
    static class Program
    {
        public static bool ProgramFinished = false;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (MainFrm fr = new MainFrm())
            {
                Application.Run();
            }
        }
    }
}