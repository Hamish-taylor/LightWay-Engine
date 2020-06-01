using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace LightWay
{
    class RenderSystem : System
    {
        public SpriteBatch spriteBatch { get; private set; }

        private TextureC Texture;
        private PositionC Pos;
        public RenderSystem(SpriteBatch spriteBatch)
        {
            components.Add(typeof(PositionC));
            components.Add(typeof(TextureC));
            this.spriteBatch = spriteBatch;
            Init();
        }
        public override void update(GameTime gameTime, ComponentIndexPool CIP)
        {
            spriteBatch.Begin();
            base.update(gameTime, CIP);
            spriteBatch.End();
        }
        public override void ProcessEntity()
        {
            Texture = GetComponent<TextureC>();
            Pos = GetComponent<PositionC>();
            float width = Texture.texture.Width * (float)Texture.scale;
            float height = Texture.texture.Height * (float)Texture.scale;
            spriteBatch.Draw(Texture.texture, new Rectangle((int)Pos.position.X, (int)Pos.position.Y, (int)width, (int)height), Color.White);
        }
    }

}
