using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;

namespace LightWay
{
    public class PlayerDEPRICATED
    {
        GraphicsDeviceManager graphics { get; set; }
        public float width { get; set; }
        public float height { get; set; }

        public Texture2D Texture { get; set; }

        private Vector2 _velocity = Vector2.Zero;
        public Vector2 velocity { get { return _velocity; } set { _velocity = value; } }
        public Vector2 _position = Vector2.Zero;
        public Vector2 position { get{return _position; } set {_position = value; } }
        public Vector2 gravity { get; set; } = Vector2.Zero;

        public float drawingScale { get; set; }
        public Rectangle _collider;
        public Rectangle collider { get { reScale(); return _collider; } set { _collider = value; } }
        private float _scale = 0.2f;
        public float scale { get { return _scale; } set { _scale = value; reScale(); } }
        public PlayerDEPRICATED(Texture2D texture, GraphicsDeviceManager graphics)
        {
            this.Texture = texture;
            this.drawingScale = (float)texture.Height / texture.Width;
            reScale();
            this.graphics = graphics;
            this.gravity = new Vector2(0, 1);
        }


        public void Do() {
            Keys[] keys = Input.getKeyBoardKeys();
            if (keys.Contains(Keys.D)) velocity += new Vector2(1, 0);
            if (keys.Contains(Keys.A)) velocity -= new Vector2(1, 0);
            if (keys.Contains(Keys.S)) velocity += new Vector2(0, 1);
            if (keys.Contains(Keys.Space) && velocity.Y == 0) velocity -= new Vector2(0, 20);

            _velocity += gravity;
            CheckBoundsCollision();
            _position += _velocity;
            
            _velocity -= velocity / 13;

        }

        public void reScale()
        {
            this.width = (Texture.Width * scale);
            this.height = (Texture.Height * scale);
            collider = new Rectangle((int)position.X, (int)position.Y, (int)width, (int)height);
        }

        private void CheckBoundsCollision()
        {
     /*       //left
            if (collider.X + collider.Width > graphics.GraphicsDevice.Viewport.Width && _velocity.X > 0)
            {
                _velocity.X = 0;
                if(_position.X + collider.Width > graphics.GraphicsDevice.Viewport.Width + 1 || (_position.X + collider.Width < graphics.GraphicsDevice.Viewport.Width - 1))
                _position.X +=  graphics.GraphicsDevice.Viewport.Width - position.X - collider.Width;
            }

            //right
            if (collider.X < 0 && _velocity.X < 0)
            {
                if (_position.X > 1 || (_position.X  < - 1))
                    _position.X = 0;
            }

            //top
            if (collider.Y < 0 && _velocity.Y < 0)
            {
                if (_position.Y > 1 || (_position.Y < -1))
                    _position.Y = 0;
            }*/

            //bottom
            if (collider.Y + collider.Height > graphics.GraphicsDevice.Viewport.Height && _velocity.Y > 0)
            {
                _velocity.Y = 0;
                if (_position.Y + collider.Height > graphics.GraphicsDevice.Viewport.Height + 1 || (_position.Y + collider.Height < graphics.GraphicsDevice.Viewport.Height - 1))
                    _position.Y += graphics.GraphicsDevice.Viewport.Height - position.Y - collider.Height;
            }
        }
    }
}
