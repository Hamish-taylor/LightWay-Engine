using LightWay.Engine.ECS.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay
{
    class GravitySystem : System
    {
        public GravitySystem()
        {
            components.Add(typeof(VelocityC));
            components.Add(typeof(GravityC));
            Init();
        }
        public override void ProcessEntity()
        { 
            ((VelocityC)workingEntity[typeof(VelocityC)]).velocity += ((GravityC)workingEntity[typeof(GravityC)]).gravity;
        }
    }
}
