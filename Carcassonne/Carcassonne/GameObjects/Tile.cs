using Carcassonne.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private bool isVisited;
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
            this.isVisited = false;
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

        public bool IsVisited
        {
            get
            {
                return this.isVisited;
            }
            set
            {
                this.isVisited = value;
            }
        }

        public bool HasShield
        {
            get
            {
                return this.hasShield;
            }
            set
            {
                this.hasShield = value;
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
                    this.sectorsGrid[i, j] = new Sector(TerrainTypeEnum.Field, null);
                }
            }
            switch (type)
            {
                case TileType.Road:
                    InitializeVerticalRoad();
                    break;
                case TileType.Turn:
                    InitializeLeftTurn();
                    break;
                case TileType.TCrossroad:
                    InitializeTRoad();
                    break;
                case TileType.XCrossroad:
                    InitializeHorizontalRoad();
                    InitializeVerticalRoad();
                    this.SectorsGrid[GridSize / 2, GridSize / 2].Terrain = TerrainTypeEnum.Crossroad;
                    break;
                case TileType.Gate:
                    InitializeGate();
                    break;
                case TileType.GateShield:
                    InitializeGate();
                    this.hasShield = true;
                    break;
                case TileType.GatePlusRoad:
                    InitializeGatePlusRoad();
                    break;
                case TileType.GateShieldPlusRoad:
                    InitializeGatePlusRoad();
                    this.hasShield = true;
                    break;
                case TileType.DCastle:
                    InitializeDiogonalCastle();
                    break;
                case TileType.DCastleShield:
                    InitializeDiogonalCastle();
                    this.hasShield = true;
                    break;
                case TileType.DCastlePlusTurn:
                    InitializeDiogonalCastle();
                    InitializeRightTurn();
                    break;
                case TileType.DCastleShieldPlusTurn:
                    InitializeDiogonalCastle();
                    InitializeRightTurn();
                    this.hasShield = true;
                    break;
                case TileType.HCastle:
                    InitializeHalfCastleOnTop();
                    break;
                case TileType.HCastlePlusTurnRight:
                    InitializeHalfCastleOnTop();
                    InitializeRightTurn();
                    break;
                case TileType.HCastlePlusTurnLeft:
                    InitializeHalfCastleOnTop();
                    InitializeLeftTurn();
                    break;
                case TileType.HCastlePlusRoad:
                    InitializeHalfCastleOnTop();
                    InitializeHorizontalRoad();
                    break;
                case TileType.HCastlePlusTCrossorad:
                    InitializeHalfCastleOnTop();
                    InitializeTRoad();
                    break;
                case TileType.OHCastles:
                    InitializeHalfCastleOnLeft();
                    InitializeHalfCastleOnRight();
                    break;
                case TileType.NHCastles:
                    InitializeHalfCastleOnTop();
                    InitializeHalfCastleOnRight();
                    this.SectorsGrid[0, GridSize - 1].Terrain = TerrainTypeEnum.Castle;
                    break;
                case TileType.Bridge:
                    InitializeBridge();
                    break;
                case TileType.BridgeShield:
                    InitializeBridge();
                    this.hasShield = true;
                    break;
                case TileType.Square:
                    for (int i = 0; i < GridSize; i++)
                    {
                        for (int j = 0; j < GridSize; j++)
                        {
                            this.SectorsGrid[i, j].Terrain = TerrainTypeEnum.Castle;
                        }
                    }
                    break;
                case TileType.Monastery:
                    this.SectorsGrid[GridSize / 2, GridSize / 2].Terrain = TerrainTypeEnum.Monastery;
                    break;
                case TileType.MonasteryPlusRoad:
                    this.SectorsGrid[GridSize / 2, GridSize / 2].Terrain = TerrainTypeEnum.Monastery;
                    for (int i = GridSize / 2 + 1; i < GridSize; i++)
                    {
                        this.SectorsGrid[i, GridSize / 2].Terrain = TerrainTypeEnum.Road;
                    }
                    break;
                default:
                    throw new IndexOutOfRangeException("Illegal enum value");                    
            }
        }
       
        private void InitializeBridge()
        {
            for (int i = 1; i < GridSize - 1; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    this.SectorsGrid[i, j].Terrain = TerrainTypeEnum.Castle;
                }
            }
            this.SectorsGrid[0, 0].Terrain = TerrainTypeEnum.Wall;
            this.SectorsGrid[0, GridSize - 1].Terrain = TerrainTypeEnum.Wall;
            this.SectorsGrid[GridSize - 1, 0].Terrain = TerrainTypeEnum.Wall;
            this.SectorsGrid[GridSize - 1, GridSize - 1].Terrain = TerrainTypeEnum.Wall;
        }

        private void InitializeHalfCastleOnRight()
        {
            for (int i = 1; i < GridSize - 1; i++)
            {
                this.SectorsGrid[i, GridSize - 1].Terrain = TerrainTypeEnum.Castle;
            }
            this.SectorsGrid[0, GridSize - 1].Terrain = TerrainTypeEnum.Wall;
            this.SectorsGrid[GridSize - 1, GridSize - 1].Terrain = TerrainTypeEnum.Wall;
        }

        private void InitializeHalfCastleOnLeft()
        {
            for (int i = 1; i < GridSize - 1; i++)
            {
                this.SectorsGrid[i, 0].Terrain = TerrainTypeEnum.Castle;
            }
            this.SectorsGrid[0, 0].Terrain = TerrainTypeEnum.Wall;
            this.SectorsGrid[GridSize - 1, 0].Terrain = TerrainTypeEnum.Wall;
        }

        private void InitializeRightTurn()
        {
            for (int i = GridSize / 2; i < GridSize; i++)
            {
                this.SectorsGrid[GridSize / 2, i].Terrain = TerrainTypeEnum.Road;
            }
            for (int i = GridSize / 2 + 1; i < GridSize; i++)
            {
                this.SectorsGrid[i, GridSize / 2].Terrain = TerrainTypeEnum.Road;
            }
        }

        private void InitializeDiogonalCastle()
        {
            for (int i = 0; i < GridSize - 1; i++)
            {
                for (int j = 0; j < GridSize - i - 1; j++)
                {
                    this.SectorsGrid[i, j].Terrain = TerrainTypeEnum.Castle;
                }
            }
            this.SectorsGrid[GridSize - 1, 0].Terrain = TerrainTypeEnum.Wall;
            this.SectorsGrid[0, GridSize - 1].Terrain = TerrainTypeEnum.Wall;
        }

        private void InitializeGatePlusRoad()
        {
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
        }

        private void InitializeGate()
        {
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
        }

        private void InitializeHorizontalRoad()
        {
            for (int i = 0; i < GridSize; i++)
            {
                this.SectorsGrid[GridSize / 2, i].Terrain = TerrainTypeEnum.Road;
            }
        }

        private void InitializeVerticalRoad()
        {
            for (int i = 0; i < GridSize; i++)
            {
                this.SectorsGrid[i, GridSize / 2].Terrain = TerrainTypeEnum.Road;
            }
        }

        private void InitializeTRoad()
        {
            for (int i = 0; i < GridSize; i++)
            {
                if (i == GridSize / 2)
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
                this.SectorsGrid[i, GridSize / 2].Terrain = TerrainTypeEnum.Road;
            }
        }

        private void InitializeLeftTurn()
        {
            for (int i = 0; i < GridSize / 2; i++)
            {
                this.SectorsGrid[GridSize / 2, i].Terrain = TerrainTypeEnum.Road;
            }
            for (int i = GridSize / 2 + 1; i < GridSize; i++)
            {
                this.SectorsGrid[i, GridSize / 2].Terrain = TerrainTypeEnum.Road;
            }
        }

        private void InitializeHalfCastleOnTop()
        {
            for (int i = 1; i < GridSize - 1; i++)
            {
                this.SectorsGrid[0, i].Terrain = TerrainTypeEnum.Castle;
            }
            this.SectorsGrid[0, 0].Terrain = TerrainTypeEnum.Wall;
            this.SectorsGrid[0, GridSize - 1].Terrain = TerrainTypeEnum.Wall;
        }

        private List<Soldier> GetSoldiers(TerrainTypeEnum terrain)
        {
            List<Soldier> soldiers = new List<Soldier>();
            if (terrain == TerrainTypeEnum.Field)
            {
                AddSoldiersInList(soldiers);
            }
            else if (terrain == TerrainTypeEnum.Castle)
            {
                AddSoldiersInList(soldiers);
            }
            else if (terrain == TerrainTypeEnum.Road)
            {
                AddSoldiersInList(soldiers);
            }
            else if (terrain == TerrainTypeEnum.Monastery)
            {
                AddSoldiersInList(soldiers);
            }
            else
            {
                throw new InvalidOperationException("The soldier can only be placed in a field, road, castle or monastery");
            }
            return soldiers;
        }

        private void AddSoldiersInList(List<Soldier> soldiers)
        {
            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    if (this.SectorsGrid[i, j].OccupiedBy != null)
                    {
                        Soldier guard = new Soldier();
                        guard = this.SectorsGrid[i, j].OccupiedBy;
                        soldiers.Add(guard);
                    }
                }
            }
        }

        private bool PlaceSoldier(Soldier soldier, byte row, byte col)
        {
            if (this.SectorsGrid[row, col].OccupiedBy != null)
            {
                return false;
            }
            else if (this.SectorsGrid[row,col].Terrain == TerrainTypeEnum.Monastery || this.SectorsGrid[row, col].Terrain == TerrainTypeEnum.Road)
            {
                if (row == GridSize/2 && col == GridSize/2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }


            //TODO check if soldier can be placed
            return false;
        }
    }
}
