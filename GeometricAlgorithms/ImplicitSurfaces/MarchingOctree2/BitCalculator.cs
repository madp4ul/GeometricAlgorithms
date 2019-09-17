using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometricAlgorithms.ImplicitSurfaces.MarchingOctree2
{
    static class BitCalculator
    {
        public static int ToggleBit(int integer, int bitPosition)
        {
            int toggleBit = 1 << bitPosition;
            int toggleBitInverse = ~toggleBit;

            bool bitIsOn = (integer & toggleBit) > 0;

            return bitIsOn ? (integer & toggleBitInverse) : (integer | toggleBit);
        }

        public static bool IsOn(int integer, int bitPosition)
        {
            int testBit = 1 << bitPosition;

            return (integer & testBit) > 0;
        }

        public static int TurnOn(int integer, int bitPosition)
        {
            int testBit = 1 << bitPosition;

            return (integer | testBit);
        }
    }
}
