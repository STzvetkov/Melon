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
            Map map = UseOtherGameClass.Game.Map;
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

        private int CalcMonastery(Tile playedTile)
        {
            Map map = UseOtherGameClass.Game.Map;
            Tile neighbour = new Tile();
            int accumulatedResult = 0;
            if (playedTile.mapX == monasteryTile.mapX && playedTile.mapY == monasteryTile.mapY)
            {
                for (int i = -1; i < 1; i++)
                {
                    for (int j = -1; j < 1; j++)
                    {
                        if (playedTile.mapX + j>=0 && playedTile.mapX+j<Map.Size-1 &&
                            playedTile.mapY + i >= 0 && playedTile.mapX + i < Map.Size - 1 &&
                            (i!=0 ||j!=0))
                        {
                            neighbour = map[(byte)(playedTile.mapY + i), (byte)(playedTile.mapX + j)];
                            if (neighbour!=null)
                            {
                                accumulatedResult++;
                            }
                        }
                    }
                }
            }
            return accumulatedResult;
        }

        private int CalcRoad(Tile playedTile, string direction, ref byte startX, ref byte startY, ref byte endX, ref byte endY, ref List<Soldier> guardsLst)
        {
            Map map = UseOtherGameClass.Game.Map;
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
                case TileType.XCrossroad:
                    // check left
                    switch (direction)
                    {
                        case "left":
                            return SearchRoadLeft(playedTile, map, ref startX, ref startY, ref endX, ref endY, ref guardsLst);
                        case "right":
                            return SearchRoadRight(playedTile, map, ref startX, ref startY, ref endX, ref endY, ref guardsLst);
                        case "top":
                            return SearchRoadTop(playedTile, map, ref startX, ref startY, ref endX, ref endY, ref guardsLst);
                        case "bottom":
                            return SearchRoadBottom(playedTile, map, ref startX, ref startY, ref endX, ref endY, ref guardsLst);
                    }
                    break;
                case TileType.GatePlusRoad:
                case TileType.GateShieldPlusRoad:
                case TileType.MonasteryPlusRoad:
                    switch (playedTile.Orientation)
                    {
                        case 0:
                            return SearchRoadBottom(playedTile, map, ref startX, ref startY, ref endX, ref endY, ref guardsLst);
                        case 1:
                            return SearchRoadLeft(playedTile, map, ref startX, ref startY, ref endX, ref endY, ref guardsLst);
                        case 2:
                            return SearchRoadTop(playedTile, map, ref startX, ref startY, ref endX, ref endY, ref guardsLst);
                        case 3:
                            return SearchRoadRight(playedTile, map, ref startX, ref startY, ref endX, ref endY, ref guardsLst);
                    }
                    break;
            }

            return 1;
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

        private int CalcCastle(Tile playedTile, string direction, ref List<Soldier> guardsLst)
        {
            Map map = UseOtherGameClass.Game.Map;
            Soldier guard = new Soldier();

            map[playedTile.mapY, playedTile.mapX].IsVisited = true; // mark tiles we have already been to

            switch (playedTile.Type)
            {

                case TileType.Gate:
                case TileType.GateShield:
                case TileType.GatePlusRoad:
                case TileType.GateShieldPlusRoad:
                case TileType.Bridge:
                case TileType.BridgeShield:
                    guard = playedTile.SectorsGrid[Tile.GridSize / 2, Tile.GridSize / 2].OccupiedBy;
                    if (guard != null)
                    {
                        guardsLst.Add(guard);
                    }
                    int accumulatedValue = 0;
                    if (playedTile.HasShield)
                    {
                        accumulatedValue = 2;
                    }
                    else
                    {
                        accumulatedValue = 4;
                    }
                    // check left
                    if (playedTile.mapX > 0 && direction!="right" )  //avoid going back to previous tile
                    {
                        Tile nextTile = new Tile();
                        nextTile = map[playedTile.mapY, (byte)(playedTile.mapX - 1)];
                        if (nextTile != null && !nextTile.IsVisited &&
                               nextTile.SectorsGrid[Tile.GridSize / 2, Tile.GridSize - 1].Terrain == TerrainTypeEnum.Castle)
                        {
                            accumulatedValue += CalcCastle(nextTile, "left", ref guardsLst);
                        }
                    }
                    //check right
                    if (playedTile.mapX < Map.Size - 1 && direction != "left")
                    {
                        Tile nextTile = new Tile();
                        nextTile = map[playedTile.mapY, (byte)(playedTile.mapX + 1)];
                        if (nextTile != null && !nextTile.IsVisited &&
                               nextTile.SectorsGrid[Tile.GridSize / 2, 0].Terrain == TerrainTypeEnum.Castle)
                        {
                            accumulatedValue += CalcCastle(nextTile, "right", ref guardsLst);
                        }
                    }
                    // check top
                    if (playedTile.mapY > 0 && direction != "bottom")
                    {
                        Tile nextTile = new Tile();
                        nextTile = map[(byte)(playedTile.mapY - 1), playedTile.mapX];
                        if (nextTile != null && !nextTile.IsVisited &&
                               nextTile.SectorsGrid[Tile.GridSize -1 , Tile.GridSize/2].Terrain == TerrainTypeEnum.Castle)
                        {
                            accumulatedValue += CalcCastle(nextTile, "top", ref guardsLst);
                        }
                    }
                    // check bottom
                    if (playedTile.mapY > 0 && direction != "top")
                    {
                        Tile nextTile = new Tile();
                        nextTile = map[(byte)(playedTile.mapY - 1), playedTile.mapX];
                        if (nextTile != null && !nextTile.IsVisited &&
                               nextTile.SectorsGrid[0, Tile.GridSize / 2].Terrain == TerrainTypeEnum.Castle)
                        {
                            accumulatedValue += CalcCastle(nextTile, "bottom", ref guardsLst);
                        }
                    }
                    return accumulatedValue;
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

                case TileType.Square:
                    break;
            }

            return 0;
        }
    }


}

