using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcassonne.Common
{
    public enum TileType
    {
        Road,
        Turn,
        TCrossroad,
        XCrossroad,
        Gate,
        GateShield,
        GatePlusRoad,
        GateShieldPlusRoad,
        DCastle,
        DCastleShield,
        DCastlePlusTurn,
        DCastleShieldPlusTurn,
        HCastle,
        HCastlePlusTurnRight,
        HCastlePlusTurnLeft,
        HCastlePlusRoad,
        HCastlePlusTCrossorad,
        OHCastles,
        NHCastles,
        Bridge,
        BridgeShield,
        Square,
        Monastery,
        MonasteryPlusRoad
    }
}
