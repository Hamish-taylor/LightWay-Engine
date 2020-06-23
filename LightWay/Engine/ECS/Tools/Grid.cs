using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay
{
    class Grid
    {
        public static int gridPixelSize = 50;

        public static int ratio = 10;
        public static Vector2 WorldToGridPos(Vector2 worldPos)
        {
            return new Vector2(worldPos.X / gridPixelSize, worldPos.Y / gridPixelSize);
        }

        public static int WorldToGridInt(int worldInt)
        {
            return worldInt/gridPixelSize;
        }
    }
}
