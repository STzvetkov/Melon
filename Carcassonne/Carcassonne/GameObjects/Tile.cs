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
        public Sector[,] Rotate(Sector[,] oldGrid)
        {
            Sector[,] newGrid = new Sector[oldGrid.GetLength(1), oldGrid.GetLength(0)];
            int newColumn = 0;
            int newRow = 0;
            for (int oldColumn = oldGrid.GetLength(1) - 1; oldColumn >= 0; oldColumn--)
            {
                newColumn = 0;
                for (int oldRow = 0; oldRow < oldGrid.GetLength(0); oldRow++)
                {
                    newGrid[newRow, newColumn] = oldGrid[oldRow, oldColumn];
                    newColumn++;
                }
                newRow++;
            }
            return newGrid;
        }

        public void Rotate(int times)
        {
            for (int i = 0; i < times % 4; i++)
                Rotate(this.sectorsGrid);
        }


        public void Play(byte targetX, byte targetY)
        {
            Engine.CheckPosition(targetX, targetY, this);
        }

        

    }
}
