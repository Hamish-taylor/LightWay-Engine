using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay.Engine.ECS.Components
{
    public class BackGroundC
    {
        public bool leftNeighbour { get; set; } = false;

        /// <summary>
        /// The percentage of the players movement that the background will move
        /// </summary>
        public float moveRatio { get; set; } = 1;

        public BackGroundC(float moveRatio)
        {
            this.moveRatio = moveRatio;
        }
    }
}
