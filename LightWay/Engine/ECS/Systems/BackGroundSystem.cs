using LightWay.Engine.ECS.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay.Engine.ECS.Systems
{
    class BackGroundSystem
    {
        SpriteBatch spriteBatch;
        CameraC camera;
        GraphicsDevice graphicsDevice;
        EntityController entityController;

        List<Entity> compatableEntitys = new List<Entity>();
        public BackGroundSystem(GraphicsDevice graphicsDevice,EntityController entityController)
        {
            this.graphicsDevice = graphicsDevice;
            this.entityController = entityController;
            spriteBatch = new SpriteBatch(graphicsDevice);
        }

        public void Update(GameTime gameTime, ComponentIndexPool CIP)
        {
            compatableEntitys = entityController.GetAllEntityWithComponent<BackGroundC>();
            camera = ((CameraC)CIP.GetAll(typeof(CameraC)).First().Value);
            spriteBatch.Begin(SpriteSortMode.Texture, null, SamplerState.PointWrap, null, null, null, camera.matrix);
            ProcessEntity();
            spriteBatch.End();
        }
        public void ProcessEntity()
        {
            //Console.WriteLine(compatableEntitys.Count);
            foreach (Entity e in compatableEntitys)
            {
                TextureC Texture = e.GetComponent<TextureC>();
                TransformC Transform = e.GetComponent<TransformC>();
                BackGroundC backGround = e.GetComponent<BackGroundC>();

                Vector2 Position = new Vector2(Transform.Position.X + (camera.matrix.Translation.X * backGround.moveRatio), Transform.Position.Y);

                float width = Texture.Texture.Width * Transform.Scale.X;
                float height = Texture.Texture.Height * Transform.Scale.Y;
                if (graphicsDevice.Viewport.Bounds.Contains(Transform.Position.X + (camera.matrix.Translation.X * -backGround.moveRatio) + width + 50, 0) && !backGround.leftNeighbour)
                {
                    entityController.CreateEntityDelayed(Texture, new TransformC(new Vector2(Transform.Position.X + width, Transform.Position.Y)), new BackGroundC(backGround.moveRatio));
                    backGround.leftNeighbour = true;
                    Console.WriteLine("generating background");
                }
                spriteBatch.Draw(Texture.Texture, new Rectangle((int)Position.X, (int)Position.Y, (int)width, (int)height), Color.White);
            }

        }
    }
}
