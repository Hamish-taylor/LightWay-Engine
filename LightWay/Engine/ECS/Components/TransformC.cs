using FarseerPhysics;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

namespace LightWay.Engine.ECS.Components
{
    public class TransformC
    {

        //If there is no body then this stores the position, Otherwise this just stores the initial position
        private Vector2 _position = new Vector2(0,0);

        private Vector2 _scale = new Vector2(0, 0);

        private Vector2 _rotation = new Vector2(0, 0);

        //Always returns the position of this component
        public Vector2 Position { get { return gPos(); } set { _position = value; } }

        public Vector2 Scale { get { return _scale; } set { _scale = value; } }

        public Vector2 Rotation { get { return _rotation; } set { _rotation = value; } }

        public delegate Vector2 getPos();

        private getPos gPos;

        public Body body { get; }

        /// <summary>
        /// returns the position of the transform
        /// </summary>
        /// <param name="p">The transform</param>
        public static implicit operator Vector2(TransformC p) => p.Position;

        public static explicit operator TransformC(Vector2 p) => new TransformC(p);

        public static explicit operator TransformC((Vector2 p, Vector2 s) t) => new TransformC(t.p, t.s);

        public static explicit operator TransformC((Vector2 p, Vector2 s,Vector2 r) t) => new TransformC(t.p, t.s,t.r);
        public TransformC(Vector2 position)
        {
            gPos = new getPos(getPosFromPos);
            Position = position;
        }

        public TransformC(Vector2 position, Vector2 scale)
        {
            gPos = new getPos(getPosFromPos);
            Scale = scale;
            Position = position;
        }
        public TransformC(Vector2 position, Vector2 scale,Vector2 rotation)
        {
            gPos = new getPos(getPosFromPos);
            Position = position;
            Scale = scale;         
            Rotation = rotation;
        }

        public TransformC(Vector2 pos, Body b)
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
            Position = pos;
        }

        public TransformC(Vector2 pos, Vector2 scale, Body b)
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
            Position = pos;
            Scale = scale;
        }

        private Vector2 getPosFromPos()
        {
            return _position;
        }

        private Vector2 getPosFromBody()
        {
            return ConvertUnits.ToDisplayUnits(body.Position);
        }

        /// <summary>
        /// Method only works if this position component represents the position of a phyiscs body
        /// </summary>
        /// <param name="force"> The force to add</param>
        /// <returns></returns>
        public bool addForce(Vector2 force)
        {
            if (body == null) return false;
            body.ApplyLinearImpulse(force, body.WorldCenter);
            return true;
        }
    }
}
