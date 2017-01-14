using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carcassonne.GameObjects;

namespace Carcassonne.Common
{
    public class Engine
    {

        public static bool CheckPosition(byte targetX, byte targetY, Tile playedTile)
        {
            byte checkedWithX, checkedWithY;
            Tile checkedWithTile;
            Map map = GameClass.Game.Map;
            //Check top tile            
            if (targetY > 0)
            {
                checkedWithX = targetX;
                checkedWithY = (byte)(targetY - 1);
                if (map[checkedWithX, checkedWithY] != null)
                {
                    checkedWithTile = map[checkedWithX, checkedWithY];
                    for (int i = 0; i < Tile.GridSize; i++)
                    {
                        if (playedTile.SectorsGrid[0, i].Terrain != checkedWithTile.SectorsGrid[Tile.GridSize - 1, i].Terrain &&
                            playedTile.SectorsGrid[0, i].Terrain != TerrainTypeEnum.Wall &&
                            checkedWithTile.SectorsGrid[Tile.GridSize - 1, i].Terrain != TerrainTypeEnum.Wall)
                        {
                            return false;
                        }
                    }
                }
            }

            //Check bottom tile            
            if (targetY < Map.Size - 1)
            {
                checkedWithX = targetX;
                checkedWithY = (byte)(targetY + 1);
                if (map[checkedWithX, checkedWithY] != null)
                {
                    checkedWithTile = map[checkedWithX, checkedWithY];
                    for (int i = 0; i < Tile.GridSize; i++)
                    {
                        if (playedTile.SectorsGrid[Tile.GridSize - 1, i].Terrain != checkedWithTile.SectorsGrid[0, i].Terrain &&
                            playedTile.SectorsGrid[Tile.GridSize - 1, i].Terrain != TerrainTypeEnum.Wall &&
                            checkedWithTile.SectorsGrid[0, i].Terrain != TerrainTypeEnum.Wall)
                        {
                            return false;
                        }
                    }
                }
            }

            //Check left tile            
            if (targetX > 0)
            {
                checkedWithX = (byte)(targetX - 1);
                checkedWithY = targetY;
                if (map[checkedWithX, checkedWithY] != null)
                {
                    checkedWithTile = map[checkedWithX, checkedWithY];
                    for (int i = 0; i < 3; i++)
                    {
                        if (playedTile.SectorsGrid[i, 0].Terrain != checkedWithTile.SectorsGrid[i, Tile.GridSize - 1].Terrain &&
                            playedTile.SectorsGrid[i, 0].Terrain != TerrainTypeEnum.Wall &&
                            checkedWithTile.SectorsGrid[i, Tile.GridSize - 1].Terrain != TerrainTypeEnum.Wall)
                        {
                            return false;
                        }
                    }
                }
            }

            //Check bottom tile            
            if (targetX < Map.Size - 1)
            {
                checkedWithX = (byte)(targetX + 1);
                checkedWithY = targetY;
                if (map[checkedWithX, checkedWithY] != null)
                {
                    checkedWithTile = map[checkedWithX, checkedWithY];
                    for (int i = 0; i < 3; i++)
                    {
                        if (playedTile.SectorsGrid[i, Tile.GridSize - 1].Terrain != checkedWithTile.SectorsGrid[i, 0].Terrain &&
                            playedTile.SectorsGrid[i, Tile.GridSize - 1].Terrain != TerrainTypeEnum.Wall &&
                            checkedWithTile.SectorsGrid[i, 0].Terrain != TerrainTypeEnum.Wall)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public int UpdateResult(Tile playedTile)
        {
            return 0;
        }

        public int CalculateFinalResult()
        {
            return 0;
        }

        private int CalcMonastery(Tile playedTile, Tile monasteryTile)
        {
            if (playedTile.mapX == monasteryTile.mapX && playedTile.mapY == monasteryTile.mapY)
            {

            }
            return 0;
        }

        private void CalcRoad(Tile playedTile, ref byte startX, ref byte startY, ref List<Soldier> guardsLst, ref int result)
        {
            Map map = GameClass.Game.Map;
            Tile nextTile = new Tile();
            Soldier guard = new Soldier();

            switch (playedTile.Type)
            {
                case TileType.Road:
                    // horizontal road
                    if (playedTile.SectorsGrid[Tile.GridSize/2, 0].Terrain==TerrainTypeEnum.Road)
                    {
                        // search to the left
                        nextTile = map[playedTile.mapX, playedTile.mapY];
                        while (nextTile!=null &&
                               nextTile.SectorsGrid[Tile.GridSize / 2, Tile.GridSize-1].Terrain==TerrainTypeEnum.Road)
                        {
                            guard = nextTile.SectorsGrid[Tile.GridSize / 2, Tile.GridSize - 1].OccupiedBy;
                            if (guard!=null)
                            {
                                guardsLst.Add(guard);
                            }
                        }
                    }
                    // vertical road
                    else
                    {

                    }
                    break;
                case TileType.Turn:
                    break;
                case TileType.TCrossroad:
                    break;
                case TileType.XCrossroad:
                    break;
                case TileType.Gate:
                    break;
                case TileType.GateShield:
                    break;
                case TileType.GatePlusRoad:
                    break;
                case TileType.GateShieldPlusRoad:
                    break;
                case TileType.DCastle:
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
            // check top
            if (playedTile.SectorsGrid[0, Tile.GridSize/2].Terrain==TerrainTypeEnum.Road)                 
            {

            }
        }

        private int CalcCastle(Tile playedTile)
        {

            return 0;
        }


    }
}
