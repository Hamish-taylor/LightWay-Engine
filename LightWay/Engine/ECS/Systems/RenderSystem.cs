using LightWay.Engine.ECS.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LightWay.Engine.ECS.Systems
{
    class RenderSystem : System
    {
        public SpriteBatch spriteBatch { get; private set; }

        private TextureC TextureC;
        private Texture2D Texture;
        private Vector2 Pos;
        GraphicsDevice graphicsDevice;
        public RenderSystem(SpriteBatch spriteBatch,GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            components.Add(typeof(PositionC));
            components.Add(typeof(TextureC));
            components.Add(typeof(ForgroundC));
            this.spriteBatch = spriteBatch;
            Init();
        }
        public override void update(GameTime gameTime, ComponentIndexPool CIP)
        {
            CameraC camera = ((CameraC)CIP.GetAll(typeof(CameraC)).First().Value);
            spriteBatch.Begin(SpriteSortMode.Texture,null,null,null,null,null,camera.matrix);
            base.update(gameTime, CIP);
            spriteBatch.End();
        }
        public override void ProcessEntity()
        {
            TextureC = (TextureC)workingEntity[typeof(TextureC)];
            Texture = TextureC;
            Pos = (PositionC)workingEntity[typeof(PositionC)];

            float width = Texture.Width * (float)TextureC.scale.X;
            float height = Texture.Height * (float)TextureC.scale.Y;
            spriteBatch.Draw(Texture, new Rectangle((int)Pos.X, (int)Pos.Y, (int)width, (int)height), Color.White);
        }
    }

}
