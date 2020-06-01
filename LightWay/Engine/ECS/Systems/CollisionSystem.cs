using LightWay.Engine.ECS.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay.Engine.ECS.Systems
{
    class CollisionSystem : System
    {
        public CollisionSystem()
        {
            components.Add(typeof(ColliderC));
            components.Add(typeof(PositionC));
            components.Add(typeof(VelocityC));
            Init();
        }
        public override void ProcessEntity()
        {
            if (((PositionC)workingEntity[typeof(PositionC)]).position.Y > 200) ((VelocityC)workingEntity[typeof(VelocityC)]).velocity.Y = 0;
        }
    }
}
