﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carcassonne.GameObjects;

namespace Carcassonne.Common
{
    struct Sector
    {
        public TerrainTypeEnum Terrain;
        public Pawn OccupiedBy;

        public Sector(TerrainTypeEnum terrainInit, Pawn pawnInit)
        {
            this.Terrain = terrainInit;
            this.OccupiedBy = pawnInit;
        }
    }
}
