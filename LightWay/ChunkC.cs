using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay
{
    class ChunkC : IComponent
    {
        public Type type { get; set; } = typeof(ChunkC);

        public int diameter { get; private set; }
        public int numComponentsPerBlock { get; private set; }
        public int numBlocks { get; private set; }
        public IComponent[] Blocks { get; set; }
        public Rectangle bounds { get; private set; } 

        public ChunkC(int x,int y,int diameter, int numComponentsPerBlock)
        {
            this.diameter = diameter;
            this.bounds = new Rectangle(x,y,diameter * Grid.gridPixelSize,diameter * Grid.gridPixelSize);
            this.numComponentsPerBlock = numComponentsPerBlock;
            this.numBlocks = diameter * diameter;
            Blocks = new IComponent[numBlocks * numComponentsPerBlock];
        }
    }
}
