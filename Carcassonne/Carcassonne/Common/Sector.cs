using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carcassonne.GameObjects;

namespace Carcassonne.Common
{
    public struct Sector
    {
        public TerrainTypeEnum Terrain;
        public Soldier OccupiedBy;

        public Sector(TerrainTypeEnum terrainInit, Soldier pawnInit)
        {
            this.Terrain = terrainInit;
            this.OccupiedBy = pawnInit;
        }
    }
}
