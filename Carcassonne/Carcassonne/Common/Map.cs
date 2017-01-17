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

        private Tile[,] tiles = new Tile[Size, Size];


        public Map()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    this.tiles[i, j] = null;
                }
            }
        }


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

        public void ResetVisited()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (this.tiles[i, j] != null)
                    {
                        this.tiles[i, j].IsVisited = false;
                    }                  
                }
            }
        }
    }
}
