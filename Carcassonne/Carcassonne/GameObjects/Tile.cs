using Carcassonne.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcassonne.GameObjects
{
    public class Tile
    {
        private Sector[,] sectorsGrid;
        private TileType type;
        private bool isRevealed;
        private bool isPlayed;

        public Tile()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    this.sectorsGrid[i, j] = new Sector(0,null);
                }
            }
        }
        //TODO:constructor with propeties
        public void Rotate()
        {
            //TODO
            // rotation++;
            // rotation = rotation % 4;
        }

        public void Rotate(int times)
        {
            for (int i = 0; i < times % 4; i++)
                Rotate();
        }


        public void Play(byte targetX, byte targetY)
        {
            Engine.CheckPosition(targetX, targetY, this);
        }

        

    }
}
