using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carcassonne.GameObjects;

namespace Carcassonne.Common
{
    public class Map
    {
        public const byte Size = 200;

        private Tile[,] tiles;




        public Tile this[byte x, byte y]
        {
            get
            {
                if (x >= Size || x < 0)
                {
                    throw new IndexOutOfRangeException(String.Format(
                        "Invalid index: {0}.", x));
                }
                if (y >= Size || y < 0)
                {
                    throw new IndexOutOfRangeException(String.Format(
                        "Invalid index: {0}.", y));
                }
                Tile result = this.tiles[x,y];
                return result;
            }
        }
    }
}
