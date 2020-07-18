using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay.Engine.ECS.Components
{
    public class PhysicsBodyC : IComponent
    {
        public Type type { get; } = typeof(PhysicsBodyC);
        public BodyType bodytype { get; private set; }
        /// <summary>
        /// This is only for initialization purpose and does not reflect the position of the body.
        /// For that use <c> body.position </c>
        /// </summary>
        public Vector2 initialPosition { get; private set; }
        public Body body { get; set; }
        public PhysicsBodyC(BodyType bodyType, Vector2 initialPosition )
        {
            this.bodytype = bodyType;
            this.initialPosition = initialPosition;
        }


    }




}
