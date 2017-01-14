using Carcassonne.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcassonne.GameObjects
{
    public class Tile:MapObjects
    {
        public const int GridSize = 5;

        private Sector[,] sectorsGrid;
        private TileType type;
        private bool isRevealed;
        private bool isPlayed;

        public Tile()
        {
            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    this.sectorsGrid[i, j] = new Sector(0,null);
                }
            }
        }

        public Sector[,] SectorsGrid
        {
            get
            {
                return this.sectorsGrid;
            }
            set
            {
                this.sectorsGrid = value;
            }
        }

        public TileType Type
        {
            get;
            set;
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
