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
        private const int RoadCalcCorrection = 1;

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

        private int CalcRoad(Tile playedTile, string direction, ref byte startX, ref byte startY, ref byte endX, ref byte endY, ref List<Soldier> guardsLst)
        {
            Map map = GameClass.Game.Map;
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
                        switch (direction)
                        {
                            case "left":
                                return SearchRoadLeft(playedTile, map, ref startX, ref startY, ref endX, ref endY, ref guardsLst);
                            case "right":
                                return SearchRoadRight(playedTile, map, ref startX, ref startY, ref endX, ref endY, ref guardsLst);
                            default:
                                return SearchRoadLeft(playedTile, map, ref startX, ref startY, ref endX, ref endY, ref guardsLst) +
                                       SearchRoadRight(playedTile, map, ref startX, ref startY, ref endX, ref endY, ref guardsLst) - RoadCalcCorrection;
                        }
                    }
                    // vertical road                                                                                         
                    else
                    {
                        switch (direction)
                        {
                            case "top":
                                return SearchRoadTop(playedTile, map, ref startX, ref startY, ref endX, ref endY, ref guardsLst);
                            case "bottom":
                                return SearchRoadTop(playedTile, map, ref startX, ref startY, ref endX, ref endY, ref guardsLst);
                            default:
                                return SearchRoadTop(playedTile, map, ref startX, ref startY, ref endX, ref endY, ref guardsLst) +
                                       SearchRoadBottom(playedTile, map, ref startX, ref startY, ref endX, ref endY, ref guardsLst) - RoadCalcCorrection;
                        }
                    }
                case TileType.Turn:
                case TileType.HCastlePlusTurnLeft:
                case TileType.HCastlePlusTurnRight:
                case TileType.DCastlePlusTurn:
                case TileType.DCastleShieldPlusTurn:
                    guard = playedTile.SectorsGrid[Tile.GridSize / 2, Tile.GridSize / 2].OccupiedBy;
                    if (guard != null)
                    {
                        guardsLst.Add(guard);
                    }
                    // left turn is indeed a rotated right one
                    int turnOrientation;
                    if (playedTile.Type == TileType.Turn || playedTile.Type == TileType.HCastlePlusTurnLeft)
                    {
                        turnOrientation = playedTile.Orientation;
                    }
                    else
                    {
                        turnOrientation = (playedTile.Orientation + 3) % 4;
                    }
                    switch (turnOrientation)
                    {
                        case 0:  //bottom left turn
                            switch (direction)
                            {
                                case "right":
                                    return SearchRoadBottom(playedTile, map, ref startX, ref startY, ref endX, ref endY, ref guardsLst);
                                case "top":
                                    return SearchRoadLeft(playedTile, map, ref startX, ref startY, ref endX, ref endY, ref guardsLst);
                                default:
                                    return SearchRoadLeft(playedTile, map, ref startX, ref startY, ref endX, ref endY, ref guardsLst) +
                                                SearchRoadBottom(playedTile, map, ref startX, ref startY, ref endX, ref endY, ref guardsLst) - RoadCalcCorrection;
                            }
                        case 1: // top left turn
                            switch (direction)
                            {
                                case "right":
                                    return SearchRoadTop(playedTile, map, ref startX, ref startY, ref endX, ref endY, ref guardsLst);
                                case "bottom":
                                    return SearchRoadLeft(playedTile, map, ref startX, ref startY, ref endX, ref endY, ref guardsLst);
                                default:
                                    return SearchRoadLeft(playedTile, map, ref startX, ref startY, ref endX, ref endY, ref guardsLst) +
                                           SearchRoadTop(playedTile, map, ref startX, ref startY, ref endX, ref endY, ref guardsLst) - RoadCalcCorrection;
                            }
                        case 2:  //top right turn
                            switch (direction)
                            {
                                case "left":
                                    return SearchRoadTop(playedTile, map, ref startX, ref startY, ref endX, ref endY, ref guardsLst);
                                case "bottom":
                                    return SearchRoadRight(playedTile, map, ref startX, ref startY, ref endX, ref endY, ref guardsLst);
                                default:
                                    return SearchRoadRight(playedTile, map, ref startX, ref startY, ref endX, ref endY, ref guardsLst) +
                                           SearchRoadTop(playedTile, map, ref startX, ref startY, ref endX, ref endY, ref guardsLst) - RoadCalcCorrection;
                            }
                        case 3: // bottom right turn
                            switch (direction)
                            {
                                case "left":
                                    return SearchRoadBottom(playedTile, map, ref startX, ref startY, ref endX, ref endY, ref guardsLst);
                                case "top":
                                    return SearchRoadRight(playedTile, map, ref startX, ref startY, ref endX, ref endY, ref guardsLst);
                                default:
                                    return SearchRoadRight(playedTile, map, ref startX, ref startY, ref endX, ref endY, ref guardsLst) +
                                           SearchRoadBottom(playedTile, map, ref startX, ref startY, ref endX, ref endY, ref guardsLst) - RoadCalcCorrection;
                            }
                    }
                    break;
                case TileType.TCrossroad:
                    // check left

                    break;
                case TileType.XCrossroad:
                    // check left
                    guard = playedTile.SectorsGrid[Tile.GridSize / 2, 0].OccupiedBy;
                    if (guard != null)
                    {
                        guardsLst.Add(guard);
                    }
                    break;
                case TileType.GatePlusRoad:
                case TileType.GateShieldPlusRoad:


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


        private int SearchRoadLeft(Tile playedTile, Map map, ref byte startX, ref byte startY, ref byte endX, ref byte endY, ref List<Soldier> guardsLst)
        {
            if (playedTile.mapX > 0)
            {
                Tile nextTile = new Tile();
                nextTile = map[playedTile.mapY, (byte)(playedTile.mapX - 1)];
                if (nextTile != null &&
                       nextTile.SectorsGrid[Tile.GridSize / 2, Tile.GridSize - 1].Terrain == TerrainTypeEnum.Road)
                {
                    return CheckCenterSector(nextTile, "left", ref startX, ref startY, ref endX, ref endY, ref startX, ref startY, ref guardsLst);
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        private int SearchRoadRight(Tile playedTile, Map map, ref byte startX, ref byte startY, ref byte endX, ref byte endY, ref List<Soldier> guardsLst)
        {
            if (playedTile.mapX < Map.Size - 1)
            {
                Tile nextTile = new Tile();
                nextTile = map[playedTile.mapY, (byte)(playedTile.mapX + 1)];
                if (nextTile != null &&
                       nextTile.SectorsGrid[Tile.GridSize / 2, 0].Terrain == TerrainTypeEnum.Road)
                {
                    return CheckCenterSector(nextTile, "right", ref startX, ref startY, ref endX, ref endY, ref endX, ref endY, ref guardsLst);
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        private int SearchRoadTop(Tile playedTile, Map map, ref byte startX, ref byte startY, ref byte endX, ref byte endY, ref List<Soldier> guardsLst)
        {
            if (playedTile.mapY > 0)
            {
                Tile nextTile = new Tile();
                nextTile = map[(byte)(playedTile.mapY - 1), playedTile.mapX];
                if (nextTile != null &&
                       nextTile.SectorsGrid[Tile.GridSize / 2, Tile.GridSize - 1].Terrain == TerrainTypeEnum.Road)
                {
                    return CheckCenterSector(nextTile, "top", ref startX, ref startY, ref endX, ref endY, ref startX, ref startY, ref guardsLst);
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        private int SearchRoadBottom(Tile playedTile, Map map, ref byte startX, ref byte startY, ref byte endX, ref byte endY, ref List<Soldier> guardsLst)
        {
            if (playedTile.mapY < Tile.GridSize - 1)
            {
                Tile nextTile = new Tile();
                nextTile = map[(byte)(playedTile.mapY + 1), playedTile.mapX];
                if (nextTile != null &&
                       nextTile.SectorsGrid[Tile.GridSize / 2, Tile.GridSize - 1].Terrain == TerrainTypeEnum.Road)
                {
                    return CheckCenterSector(nextTile, "bottom", ref startX, ref startY, ref endX, ref endY, ref endX, ref endY, ref guardsLst);
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        private int CheckCenterSector(Tile nextTile, string direction, ref byte startX, ref byte startY, ref byte endX, ref byte endY, ref byte pX, ref byte pY,
                                      ref List<Soldier> guardsLst)
        {
            if (nextTile.SectorsGrid[Tile.GridSize / 2, Tile.GridSize / 2].Terrain == TerrainTypeEnum.Road)
            {
                return CalcRoad(nextTile, direction, ref startX, ref startY, ref endX, ref endY, ref guardsLst);
            }
            else
            // end of road
            {
                pX = nextTile.mapX;
                pY = nextTile.mapY;
                return 1;
            }
        }
    }


}

