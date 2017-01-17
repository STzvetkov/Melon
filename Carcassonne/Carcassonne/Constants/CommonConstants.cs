using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcassonne.Constants
{
    public class CommonConstants
    {
        //Game window size
        public static readonly int windowWidth = 800;
        public static readonly int windowHeight = 600;
        public static readonly int tileSize = 90;
        public static readonly Rectangle gameDimensions = new Rectangle(0, 0, windowWidth, windowHeight);

        //about menu offset
        public static readonly int aboutXOffset = 50; 
        public static readonly int aboutYOffset = 50;
        public static readonly int aboutTextYOffset = 40;

        //Messages
        public static readonly string aboutMessage = $"This project was created for CSharp OOP course 2016-autumn Teamwork.";
        public static readonly string aboutMessageNote = "This project aims no financial benefits, it is for educational purpose only.";
        public static readonly string aboutMessageUI = $@"All media is used with educational aim only.{Environment.NewLine}All credit goes to their respective owners.{Environment.NewLine}{Environment.NewLine}Developed by Team ""Melon""";
    }
}
