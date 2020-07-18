using LightWay.Engine.ECS.Components;

namespace LightWay.Engine.ECS.Systems
{
    class GravitySystemDEPRECATED : System
    {
        public GravitySystemDEPRECATED()
        {
            components.Add(typeof(VelocityC));

            Init();
        }
        public override void ProcessEntity()
        {
            ((VelocityC)workingEntity[typeof(VelocityC)]).velocity += ((GravityCDEPRECATED)workingEntity[typeof(GravityCDEPRECATED)]).gravity;
        }
    }
}
