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

        private int CalcRoad(Tile playedTile, ref byte startX, ref byte startY, ref byte endX, ref byte endY, ref List<Soldier> guardsLst)
        {
            Map map = GameClass.Game.Map;
            Tile nextTile = new Tile();
            Soldier guard = new Soldier();

            switch (playedTile.Type)
            {
                case TileType.Road:
                    // check for Soldier
                    guard = playedTile.SectorsGrid[Tile.GridSize / 2, Tile.GridSize / 2].OccupiedBy;
                    if (guard != null)
                    {
                        guardsLst.Add(guard);
                    }
                    // horizontal road
                    if (playedTile.SectorsGrid[Tile.GridSize / 2, 0].Terrain == TerrainTypeEnum.Road)
                    {              
                        // search to the left
                        if (playedTile.mapX>0)
                        {
                            nextTile = map[playedTile.mapY, (byte)(playedTile.mapX - 1)];
                            while (nextTile != null &&
                                   nextTile.SectorsGrid[Tile.GridSize / 2, Tile.GridSize - 1].Terrain == TerrainTypeEnum.Road)
                            {
                                return CalcRoad(nextTile, ref startX, ref startY, ref endX, ref endY, ref guardsLst);
                            }
                        }
                        // search to the right
                        if (playedTile.mapX < Map.Size-1)
                        {
                            nextTile = map[playedTile.mapY, (byte)(playedTile.mapX + 1)];
                            while (nextTile != null &&
                                   nextTile.SectorsGrid[Tile.GridSize / 2, 0].Terrain == TerrainTypeEnum.Road)
                            {
                                return CalcRoad(nextTile, ref startX, ref startY, ref endX, ref endY, ref guardsLst);
                            }
                        }
                    }
                    // vertical road
                    else
                    {
                        // search to the top
                        if (playedTile.mapY > 0)
                        {
                            nextTile = map[(byte)(playedTile.mapY-1), playedTile.mapX];
                            while (nextTile != null &&
                                   nextTile.SectorsGrid[Tile.GridSize-1, Tile.GridSize / 2].Terrain == TerrainTypeEnum.Road)
                            {
                                return CalcRoad(nextTile, ref startX, ref startY, ref endX, ref endY, ref guardsLst);
                            }
                        }
                        // search to the bottom
                        if (playedTile.mapY < Map.Size - 1)
                        {
                            nextTile = map[(byte)(playedTile.mapY+1), playedTile.mapX];
                            while (nextTile != null &&
                                   nextTile.SectorsGrid[0, Tile.GridSize / 2].Terrain == TerrainTypeEnum.Road)
                            {
                                return CalcRoad(nextTile, ref startX, ref startY, ref endX, ref endY, ref guardsLst);
                            }
                        }
                    }
                    break;
                case TileType.Turn:
                    guard = playedTile.SectorsGrid[Tile.GridSize / 2, Tile.GridSize / 2].OccupiedBy;
                    if (guard != null)
                    {
                        guardsLst.Add(guard);
                    }
                    // left turn
                    if (playedTile.SectorsGrid[Tile.GridSize / 2, 0].Terrain == TerrainTypeEnum.Road)
                    {

                    }
                    //right turn
                    else if (playedTile.SectorsGrid[Tile.GridSize / 2, 0].Terrain == TerrainTypeEnum.Road)
                    {

                    }
                    //top turn
                    else if (playedTile.SectorsGrid[Tile.GridSize / 2, 0].Terrain == TerrainTypeEnum.Road)
                    {

                    }
                    // bottom turn
                    else
                    {

                    }
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

            return 1;
        }

        private int CalcCastle(Tile playedTile)
        {

            return 0;
        }


    }
}
