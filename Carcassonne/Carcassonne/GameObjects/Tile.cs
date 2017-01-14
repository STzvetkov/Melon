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
        private bool hasShield;

        public static Dictionary<TileType, byte> numberOfTileType = new Dictionary<TileType, byte>
        {
            { TileType.Road, 8 },
            { TileType.Turn, 9},
            { TileType.TCrossroad, 4},
            { TileType.XCrossroad, 1},
            { TileType.Gate, 3},
            { TileType.GateShield, 1},
            { TileType.GatePlusRoad, 1},
            { TileType.GateShieldPlusRoad, 2},
            { TileType.DCastle, 3 },
            { TileType.DCastleShield, 2},
            { TileType.DCastlePlusTurn, 3},
            { TileType.DCastleShieldPlusTurn, 2},
            { TileType.HCastle, 5},
            { TileType.HCastlePlusTurnRight, 3},
            { TileType.HCastlePlusTurnLeft, 3},
            { TileType.HCastlePlusRoad, 4},
            { TileType.HCastlePlusTCrossorad, 3},
            { TileType.OHCastles, 3},
            { TileType.NHCastles, 2},
            { TileType.Bridge, 1},
            { TileType.BridgeShield, 2},
            { TileType.Square, 1},
            { TileType.Monastery, 4},
            { TileType.MonasteryPlusRoad, 2}
        };

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


        public void Play(byte targetX, byte targetY)
        {
            Engine.CheckPosition(targetX, targetY, this);
        }

        

    }
}
