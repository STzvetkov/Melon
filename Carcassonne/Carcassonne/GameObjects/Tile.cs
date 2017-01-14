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
        private int orientataion;



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
                    this.sectorsGrid[i, j] = new Sector(TerrainTypeEnum.Field,null);
                }
            }
            this.orientataion = 0;
            this.hasShield = false;
            this.isPlayed = false;
            this.isRevealed = false;
        }

        public Tile(TileType type)
        {
            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    this.sectorsGrid[i, j] = new Sector(TerrainTypeEnum.Field, null);
                }
            }
            this.orientataion = 0;
            this.hasShield = false;
            this.isPlayed = false;
            this.isRevealed = false;
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
            get { return this.type; }           
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
            this.orientataion = (this.orientataion + 1) % 4;
            return newGrid;
        }       


        public void Play(byte targetX, byte targetY)
        {
            Engine.CheckPosition(targetX, targetY, this);
        }

        public int Orientation
        {
            get{ return this.orientataion; }
        }
        //Initializations 
        private void Init(TileType type)
        {
            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    this.sectorsGrid[i, j] = new Sector(0, null);
                }
            }
            switch (type)
            {
                case TileType.Road:                    
                    for (int i = 0; i < GridSize; i++)
                    {
                        this.SectorsGrid[i, GridSize / 2].Terrain = TerrainTypeEnum.Road;                      
                    }
                    break;

                case TileType.Turn:
                    for (int i = 0; i < GridSize / 2; i++)
                    {
                        this.SectorsGrid[GridSize / 2, i].Terrain = TerrainTypeEnum.Road;
                    }
                    for (int i = GridSize / 2 + 1; i < GridSize; i++)
                    {
                        this.SectorsGrid[i, GridSize / 2].Terrain = TerrainTypeEnum.Road;
                    }
                    break;
                case TileType.TCrossroad:
                    for (int i = 0; i < GridSize; i++)
                    {
                        if (i==GridSize/2)
                        {
                            this.SectorsGrid[GridSize / 2, i].Terrain = TerrainTypeEnum.Crossroad;
                        }
                        else
                        {
                            this.SectorsGrid[GridSize / 2, i].Terrain = TerrainTypeEnum.Road;
                        }
                    }
                    for (int i = GridSize / 2 + 1; i < GridSize; i++)
                    {
                        this.SectorsGrid[i, GridSize/2].Terrain = TerrainTypeEnum.Road;
                    }
                    break;
                case TileType.XCrossroad:
                    for (int i = 0; i < GridSize; i++)
                    {
                        this.SectorsGrid[GridSize / 2, i].Terrain = TerrainTypeEnum.Road;
                    }
                    for (int i = 0; i < GridSize; i++)
                    {
                        this.SectorsGrid[i, GridSize / 2].Terrain = TerrainTypeEnum.Road;
                    }
                    this.SectorsGrid[GridSize / 2, GridSize / 2].Terrain = TerrainTypeEnum.Crossroad;
                    break;

                case TileType.Gate:
                    for (int i = 0; i < GridSize; i++)
                    {
                        for (int j = 0; j < GridSize / 2 + 1; j++)
                        {
                            this.SectorsGrid[i, j].Terrain = TerrainTypeEnum.Castle;
                        }
                    }
                    for (int i = GridSize / 2 + 1; i < GridSize - 1; i++)
                    {
                        this.SectorsGrid[i, 0].Terrain = TerrainTypeEnum.Castle;
                        this.SectorsGrid[i, GridSize - 1].Terrain = TerrainTypeEnum.Castle;
                    }
                    this.SectorsGrid[GridSize - 1, 0].Terrain = TerrainTypeEnum.Wall;
                    this.SectorsGrid[GridSize - 1, GridSize - 1].Terrain = TerrainTypeEnum.Wall;
                    break;
                case TileType.GateShield:
                    for (int i = 0; i < GridSize; i++)
                    {
                        for (int j = 0; j < GridSize / 2 + 1; j++)
                        {
                            this.SectorsGrid[i, j].Terrain = TerrainTypeEnum.Castle;
                        }
                    }
                    for (int i = GridSize / 2 + 1; i < GridSize - 1; i++)
                    {
                        this.SectorsGrid[i, 0].Terrain = TerrainTypeEnum.Castle;
                        this.SectorsGrid[i, GridSize - 1].Terrain = TerrainTypeEnum.Castle;
                    }
                    this.SectorsGrid[GridSize - 1, 0].Terrain = TerrainTypeEnum.Wall;
                    this.SectorsGrid[GridSize - 1, GridSize - 1].Terrain = TerrainTypeEnum.Wall;
                    this.hasShield = true;
                    break;
                case TileType.GatePlusRoad:
                    for (int i = 0; i < GridSize; i++)
                    {
                        for (int j = 0; j < GridSize / 2 + 1; j++)
                        {
                            this.SectorsGrid[i, j].Terrain = TerrainTypeEnum.Castle;
                        }
                    }
                    for (int i = GridSize / 2 + 1; i < GridSize - 1; i++)
                    {
                        this.SectorsGrid[i, 0].Terrain = TerrainTypeEnum.Castle;
                        this.SectorsGrid[i, GridSize / 2].Terrain = TerrainTypeEnum.Road;
                        this.SectorsGrid[i, GridSize - 1].Terrain = TerrainTypeEnum.Castle;
                    }
                    this.SectorsGrid[GridSize - 1, 0].Terrain = TerrainTypeEnum.Wall;
                    this.SectorsGrid[GridSize - 1, GridSize / 2].Terrain = TerrainTypeEnum.Road;
                    this.SectorsGrid[GridSize - 1, GridSize - 1].Terrain = TerrainTypeEnum.Wall;
                    break;
                case TileType.GateShieldPlusRoad:
                    for (int i = 0; i < GridSize; i++)
                    {
                        for (int j = 0; j < GridSize / 2 + 1; j++)
                        {
                            this.SectorsGrid[i, j].Terrain = TerrainTypeEnum.Castle;
                        }
                    }
                    for (int i = GridSize / 2 + 1; i < GridSize - 1; i++)
                    {
                        this.SectorsGrid[i, 0].Terrain = TerrainTypeEnum.Castle;
                        this.SectorsGrid[i, GridSize / 2].Terrain = TerrainTypeEnum.Road;
                        this.SectorsGrid[i, GridSize - 1].Terrain = TerrainTypeEnum.Castle;
                    }
                    this.SectorsGrid[GridSize - 1, 0].Terrain = TerrainTypeEnum.Wall;
                    this.SectorsGrid[GridSize - 1, GridSize / 2].Terrain = TerrainTypeEnum.Road;
                    this.SectorsGrid[GridSize - 1, GridSize - 1].Terrain = TerrainTypeEnum.Wall;
                    this.hasShield = true;
                    break;
                case TileType.DCastle:
                    for (int i = 0; i < GridSize; i++)
                    {
                        for (int j = 0; j < GridSize - i; j++)
                        {
                            this.SectorsGrid[i, j].Terrain = TerrainTypeEnum.Castle;
                        }
                    }
                    break;
                case TileType.DCastleShield:
                    break;
                case TileType.DCastlePlusTurn:
                    break;
                case TileType.DCastleShieldPlusTurn:
                    break;
                case TileType.HCastle:
                    break;
                case TileType.HCastlePlusTurnRight:
                    break;
                case TileType.HCastlePlusTurnLeft:
                    break;
                case TileType.HCastlePlusRoad:
                    break;
                case TileType.HCastlePlusTCrossorad:
                    break;
                case TileType.OHCastles:
                    break;
                case TileType.NHCastles:
                    break;
                case TileType.Bridge:
                    break;
                case TileType.BridgeShield:
                    break;
                case TileType.Square:
                    break;
                case TileType.Monastery:
                    break;
                case TileType.MonasteryPlusRoad:
                    break;
                default:
                    break;
            }
        }
    }
}
