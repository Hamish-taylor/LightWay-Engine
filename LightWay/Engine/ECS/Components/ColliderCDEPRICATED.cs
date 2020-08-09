using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay.Engine.ECS.Components
{
    public class ColliderCDEPRICATED 
    {
        private Rectangle collider_ = new Rectangle();
        public Rectangle collider { get { return collider_; } private set { collider_ = value; } }
        public Point[] verticies { get; private set; } = new Point[4];
        public ColliderCDEPRICATED(Rectangle collider)
        {
            this.collider = collider;
        }
        public ColliderCDEPRICATED(int x,int y,int width, int height)
        {
            this.collider = new Rectangle(x,y,width,height);
            GenerateVerticies();
        }

        public void displace(float x,float y)
        {
            collider.Offset(x, y);
            GenerateVerticies();
        }

        public void setPos(Point p)
        {
            collider_.Location = new Point(p.X, p.Y);
            GenerateVerticies();
        }
        private void GenerateVerticies()
        {
            verticies[0] = new Point(collider.X, collider.Y);
            verticies[1] = new Point(collider.X + collider.Width, collider.Y);
            verticies[2] = new Point(collider.X, collider.Y + collider.Height);
            verticies[3] = new Point(collider.X + collider.Width, collider.Y + collider.Height);
        }
    }
}
