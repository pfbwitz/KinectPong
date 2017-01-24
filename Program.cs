using System;
using System.Windows.Forms;

namespace KinectPong
{
    static class Program
    {
        public static bool IsTwoPlayerMode = false;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PlayForm());
        }
    }
}