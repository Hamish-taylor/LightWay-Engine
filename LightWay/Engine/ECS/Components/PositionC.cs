using FarseerPhysics;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay.Engine.ECS.Components
{
    public class PositionC
    {
      
        //If there is no body then this stores the position, Otherwise this just stores the initial position
        public Vector2 _position;

        //Always returns the position of this component
        public Vector2 position { get { return gPos(); } set { _position = value; } }
        public delegate Vector2 getPos();

        private getPos gPos;

        public Body body { get; }


        public static implicit operator Vector2(PositionC p) => p.position;

        public static explicit operator PositionC(Vector2 p) => new PositionC(p);
        public PositionC(Vector2 position)
        {
            gPos = new getPos(getPosFromPos);
            this._position = position;
        }

        public PositionC(float x, float y)
        {
            gPos = new getPos(getPosFromPos);
            this._position = new Vector2(x,y);
        }
        public PositionC(float x, float y, Body b)
        {
            body = b;
            if (b != null)
            {
                gPos = new getPos(getPosFromBody);

            }
            else
            {
                gPos = new getPos(getPosFromPos);
            }
            this._position = new Vector2(x, y);
        }

        private Vector2 getPosFromPos()
        {
            return _position;
        }

        private Vector2 getPosFromBody()
        {
            return ConvertUnits.ToDisplayUnits(body.Position);
        }

        private void SetPos(Vector2 pos)
        {
            _position = pos;
        }

        /// <summary>
        /// Method only works if this position component represents the position of a phyiscs body
        /// </summary>
        /// <param name="force"> The force to add</param>
        /// <returns></returns>
        public bool addForce(Vector2 force)
        {
            if (body == null) return false;
            body.ApplyLinearImpulse(force,body.WorldCenter);
            return true;
        }
    }
}
