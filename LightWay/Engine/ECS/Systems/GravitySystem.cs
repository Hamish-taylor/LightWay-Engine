using LightWay.Engine.ECS.Components;

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
