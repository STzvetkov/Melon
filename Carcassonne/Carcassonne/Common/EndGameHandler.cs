﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Carcassonne.Common
{
    public class EndGameHandler
    {
        public static void HandleEvent(object sender, EndGameArguments e)
        {
            MessageBox.Show(e.Message);
        }
    }
}
