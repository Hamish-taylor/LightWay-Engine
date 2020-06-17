using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LightWay
{
    class RenderSystem : System
    {
        public SpriteBatch spriteBatch { get; private set; }

        private TextureC Texture;
        private PositionC Pos;
        GraphicsDevice graphicsDevice;
        public RenderSystem(SpriteBatch spriteBatch,GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            components.Add(typeof(PositionC));
            components.Add(typeof(TextureC));
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
            Texture = (TextureC)workingEntity[typeof(TextureC)];
            Pos = (PositionC)workingEntity[typeof(PositionC)];
            float width = Texture.texture.Width * (float)Texture.scale.X;
            float height = Texture.texture.Height * (float)Texture.scale.Y;
            spriteBatch.Draw(Texture.texture, new Rectangle((int)Pos.position.X, (int)Pos.position.Y, (int)width, (int)height), Color.White);
        }
    }

}
