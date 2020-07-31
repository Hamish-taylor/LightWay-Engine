using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using LightWay.Engine.ECS.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using LightWay.Engine.ECS.Tools;


namespace LightWay.Engine.ECS.Systems
{
    public class ChunkSystem
    {        
        private int diameter;
        private int numBlocks;
        private int numComponentsPerBlock = 1;
        SpriteBatch chunkSpriteBatch;
        private GraphicsDevice graphicsDevice;
        public static Texture2D texture;
        EntityController entityController;

        Dictionary<Vector2,ChunkC> chunks = new Dictionary<Vector2,ChunkC>();

        ChunkC chunkBeingProcessed;
        public ChunkSystem(GraphicsDevice graphicsDevice, EntityController entityController, int diameter)
        {
            this.entityController = entityController;
            this.graphicsDevice = graphicsDevice;
            this.chunkSpriteBatch = new SpriteBatch(graphicsDevice);
            this.diameter = diameter;
            this.numBlocks = diameter * diameter;
            Entity e = new Entity(entityController.GetFreeEntityId(), null);
            entityController.AddComponent(e, GenerateChunk(0, 0));
        }


        public void Update(GameTime gameTime)
        {
            List<Entity> players = entityController.EntitesThatContainComponents(entityController.GetAllEntityWithComponent<TransformC>(), typeof(ControllableC));

            CameraC camera = entityController.GetAllComponent<CameraC>()[0];
            chunkSpriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.matrix);
            Vector2 position = players[0].GetComponent<TransformC>();
            
            //The current chunk pos
            int cX = (int)((position.X / (diameter * Grid.gridPixelSize)));
            int cY = (int)((position.Y / (diameter * Grid.gridPixelSize)));
            int x = cX * diameter * Grid.gridPixelSize;
            int y = cY * diameter * Grid.gridPixelSize;
            
            Rectangle r = new Rectangle(x, y, diameter * Grid.gridPixelSize, diameter * Grid.gridPixelSize);
            if (x >= 0 && y >=0 && r.Contains(position))  {
                ChunkC c = null;
                //Console.WriteLine(cX + " " + cY);
                chunks.TryGetValue(new Vector2(cX,cY), out c);
                if (c == null) c = GenerateChunk(x, y);
                chunkBeingProcessed = c;
                ProcessEntity();
            }

            cX = (int)((position.X / (diameter * Grid.gridPixelSize)))+1;
            cY = (int)((position.Y / (diameter * Grid.gridPixelSize)))+1;
            x = (cX) * diameter * Grid.gridPixelSize;
            y = (cY) * diameter * Grid.gridPixelSize;
            r = new Rectangle(x, y, diameter * Grid.gridPixelSize, diameter * Grid.gridPixelSize);
            if (x >= 0 && y >= 0 && r.Contains(new Vector2((int)(position.X+graphicsDevice.Viewport.Width/2), (int)(position.Y + graphicsDevice.Viewport.Height / 2)))) {
                ChunkC c = null;
                chunks.TryGetValue(new Vector2(cX, cY), out c);
                if (c == null) c = GenerateChunk(x, y);
                chunkBeingProcessed = c;
                ProcessEntity();
            }

            cX = (int)((position.X / (diameter * Grid.gridPixelSize))) - 1;
            cY = (int)((position.Y / (diameter * Grid.gridPixelSize))) - 1;
            x = (cX) * diameter * Grid.gridPixelSize;
            y = (cY) * diameter * Grid.gridPixelSize;
            r = new Rectangle(x, y, diameter * Grid.gridPixelSize, diameter * Grid.gridPixelSize);
            if (x >= 0 && y >= 0 && r.Contains(new Vector2((int)(position.X - graphicsDevice.Viewport.Width / 2), (int)(position.Y - graphicsDevice.Viewport.Height / 2))))
            {
                ChunkC c = null;
                chunks.TryGetValue(new Vector2(cX, cY), out c);
                if (c == null) c = GenerateChunk(x, y);
                chunkBeingProcessed = c;
                ProcessEntity();
            }

            cX = (int)((position.X / (diameter * Grid.gridPixelSize))) - 1;
            cY = (int)((position.Y / (diameter * Grid.gridPixelSize))) + 1;
            x = (cX) * diameter * Grid.gridPixelSize;
            y = (cY) * diameter * Grid.gridPixelSize;
            r = new Rectangle(x, y, diameter * Grid.gridPixelSize, diameter * Grid.gridPixelSize);
            if (x >= 0 && y >= 0 && r.Contains(new Vector2((int)(position.X - graphicsDevice.Viewport.Width / 2), (int)(position.Y + graphicsDevice.Viewport.Height / 2))))
            {
                ChunkC c = null;
                chunks.TryGetValue(new Vector2(cX, cY), out c);
                if (c == null) c = GenerateChunk(x, y);
                chunkBeingProcessed = c;
                ProcessEntity();
            }

            cX = (int)((position.X / (diameter * Grid.gridPixelSize))) + 1;
            cY = (int)((position.Y / (diameter * Grid.gridPixelSize))) - 1;
            x = (cX) * diameter * Grid.gridPixelSize;
            y = (cY) * diameter * Grid.gridPixelSize;
            r = new Rectangle(x, y, diameter * Grid.gridPixelSize, diameter * Grid.gridPixelSize);
            if (x >= 0 && y >= 0 && r.Contains(new Vector2((int)(position.X - graphicsDevice.Viewport.Width / 2), (int)(position.Y + graphicsDevice.Viewport.Height / 2)))) {
                ChunkC c = null;
                chunks.TryGetValue(new Vector2(cX, cY), out c);
                if (c == null) c = GenerateChunk(x, y);
                chunkBeingProcessed = c;
                ProcessEntity();
            }

            cX = (int)((position.X / (diameter * Grid.gridPixelSize)));
            cY = (int)((position.Y / (diameter * Grid.gridPixelSize))) + 1;
            x = (cX) * diameter * Grid.gridPixelSize;
            y = (cY) * diameter * Grid.gridPixelSize;
            r = new Rectangle(x, y, diameter * Grid.gridPixelSize, diameter * Grid.gridPixelSize);
            if (x >= 0 && y >= 0 && r.Contains(new Vector2(position.X, position.Y + graphicsDevice.Viewport.Height / 2)))
            {
                ChunkC c = null;
                chunks.TryGetValue(new Vector2(cX, cY), out c);
                if (c == null) c = GenerateChunk(x, y);
                chunkBeingProcessed = c;
                ProcessEntity();
            }

            cX = (int)((position.X / (diameter * Grid.gridPixelSize)));
            cY = (int)((position.Y / (diameter * Grid.gridPixelSize))) - 1;
            x = (cX) * diameter * Grid.gridPixelSize;
            y = (cY) * diameter * Grid.gridPixelSize;
            r = new Rectangle(x, y, diameter * Grid.gridPixelSize, diameter * Grid.gridPixelSize);
            if (x >= 0 && y >= 0 && r.Contains(new Vector2((int)(position.X), (int)(position.Y - graphicsDevice.Viewport.Height / 2))))
            {
                ChunkC c = null;
                chunks.TryGetValue(new Vector2(cX, cY), out c);
                if (c == null) c = GenerateChunk(x, y);
                chunkBeingProcessed = c;
                ProcessEntity();
            }

            cX = (int)((position.X / (diameter * Grid.gridPixelSize))) - 1;
            cY = (int)((position.Y / (diameter * Grid.gridPixelSize)));
            x = (cX) * diameter * Grid.gridPixelSize;
            y = (cY) * diameter * Grid.gridPixelSize;
            r = new Rectangle(x, y, diameter * Grid.gridPixelSize, diameter * Grid.gridPixelSize);
            if (x >= 0 && y >= 0 && r.Contains(new Vector2((int)(position.X - graphicsDevice.Viewport.Width / 2), (int)(position.Y))))
            {
                ChunkC c = null;
                chunks.TryGetValue(new Vector2(cX, cY), out c);
                if (c == null) c = GenerateChunk(x, y);
                chunkBeingProcessed = c;
                ProcessEntity();
            }

            cX = (int)((position.X / (diameter * Grid.gridPixelSize))) + 1;
            cY = (int)((position.Y / (diameter * Grid.gridPixelSize)));
            x = (cX) * diameter * Grid.gridPixelSize;
            y = (cY) * diameter * Grid.gridPixelSize;
            r = new Rectangle(x, y, diameter * Grid.gridPixelSize, diameter * Grid.gridPixelSize);
            if (x >= 0 && y >= 0 && r.Contains(new Vector2((int)(position.X + graphicsDevice.Viewport.Width / 2), (int)(position.Y)))) {
                ChunkC c = null;
                chunks.TryGetValue(new Vector2(cX,cY), out c);
                if (c == null) c = GenerateChunk(x, y);
                chunkBeingProcessed = c;
                ProcessEntity();
            }

            chunkSpriteBatch.End();

        }     

        public ChunkC GenerateChunk(int x,int y)
        {
            ChunkC chunk = new ChunkC(x,y,diameter,numComponentsPerBlock);
            object[] comp = new object[numBlocks*numComponentsPerBlock];
            var rand = new Random();
            float increament = 0.01f;
            float decemalW = (x/(diameter*Grid.gridPixelSize)) * ((diameter-1) +(diameter*increament));
            float decemalH = (y / (diameter * Grid.gridPixelSize)) * ((diameter - 1) + (diameter * increament));
            
            for (int h = 0; h < diameter; h++)
            {
                decemalH += increament;
                for (int w = 0; w < diameter; w++)
                {
                    decemalW += increament;
                    Double n = PerlinNoise.OctavePerlin((decemalW) *5 ,(decemalH)*5, 0, 1, 1) * 255;;
                   
                    if (n < 150 && h > 5)
                    {
                        Body b = BodyFactory.CreateRectangle(entityController.physicsWorld, ConvertUnits.ToSimUnits(Grid.gridPixelSize), ConvertUnits.ToSimUnits(Grid.gridPixelSize), 1,ConvertUnits.ToSimUnits(new Vector2(w * Grid.gridPixelSize + chunk.bounds.X, h * Grid.gridPixelSize + chunk.bounds.Y)), 0, BodyType.Static);
                        b.Friction = 0.5f;
                        b.Restitution = 0;
                        comp[((w + (h * diameter)) * numComponentsPerBlock)] = new TextureC(ChunkSystem.texture);
                    }                             
                }
                decemalW = (x / (diameter * Grid.gridPixelSize)) * ((diameter - 1) + (diameter * increament));
            }
            chunk.Blocks = comp;
            int cX = (int)((x / (diameter * Grid.gridPixelSize)));
            int cY = (int)((y / (diameter * Grid.gridPixelSize)));
            chunks.Add(new Vector2(cX, cY),chunk);
            return chunk;
        }

        public  void ProcessEntity()
        {
            object[] components = chunkBeingProcessed.Blocks;

            for (int h = 0; h < diameter; h++)
            {
                for (int w = 0; w < diameter; w++)
                {
                    TextureC texture = ((TextureC)components[w + (h*diameter)]);
                   if(texture != null) chunkSpriteBatch.Draw(ChunkSystem.texture, new Rectangle(w * Grid.gridPixelSize + chunkBeingProcessed.bounds.X, h * Grid.gridPixelSize + chunkBeingProcessed.bounds.Y, Grid.gridPixelSize, Grid.gridPixelSize), Color.White);
                }
            }
        }
    }
}
