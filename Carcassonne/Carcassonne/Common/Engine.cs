using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carcassonne.GameObjects;

namespace Carcassonne.Common
{
    class Engine
    {

        public bool CheckPosition(byte targetX, byte targetY, Tile playedTile, Map map)
        {
            byte checkedWithX, checkedWithY;
            Tile checkedWithTile;
            //Check top tile            
            if (targetY>0)
            {
                checkedWithX = targetX;
                checkedWithY = (byte)(targetY - 1);
                if (map[checkedWithX, checkedWithY]!=null)
                {
                    checkedWithTile = map[checkedWithX, checkedWithY];
                    for (int i = 0; i < 3; i++)
                    {
                        /*if (playedTile.sectorsGrid[0,i]!=checkedWithTile.sectorsGrid[0, i])
                        {
                            return false;
                        }*/
                    }
                }                
            }

            //Check bottom tile            
            if (targetY < Map.Size-1)
            {
                checkedWithX = targetX;
                checkedWithY = (byte)(targetY + 1);
                if (map[checkedWithX, checkedWithY] != null)
                {
                    checkedWithTile = map[checkedWithX, checkedWithY];
                    for (int i = 0; i < 3; i++)
                    {
                        /*if (playedTile.sectorsGrid[2,i]!=checkedWithTile.sectorsGrid[2, i])
                        {
                            return false;
                        }*/
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
                        /*if (playedTile.sectorsGrid[i,0]!=checkedWithTile.sectorsGrid[i,0])
                        {
                            return false;
                        }*/
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
                        /*if (playedTile.sectorsGrid[i,2]!=checkedWithTile.sectorsGrid[i,2])
                        {
                            return false;
                        }*/
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
            /*if (playedTile.X == monasteryTile.X && playedTile.Y == monasteryTile.Y)
            {

            }*/
            return 0;
        }

        private int CalcRoad(Tile playedTile)
        {

            return 0;

        }

        private int CalcCastle(Tile playedTile)
        {

            return 0;
        }


    }
}
