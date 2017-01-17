using System;
using System.Windows.Forms;

namespace GameClassFix
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Startup
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Singleton design pattern
            using (var game = GameClass.Game)
            {
                game.Run();
            }
        }
    }
#endif
}
