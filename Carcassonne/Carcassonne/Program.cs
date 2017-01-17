using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Carcassonne
{
#if WINDOWS || LINUX
    static class Program
    {
        ///<summary>
        ///The main entry point for the application.
        ///</summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Start the project through the other solution
            using (var game = UseOtherGameClass.Game)
            {
                //game.Run();
            }
        }
    }
#endif
}
