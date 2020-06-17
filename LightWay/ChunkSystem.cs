using LightWay.Engine.ECS.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightWay
{
    /// <summary>
    /// Defines a group of chunks
    /// </summary>
    class ChunkSystem : System
    {        
        private int diameter = 50;
        private int numBlocks;
        private int numComponentsPerBlock = 3;
        SpriteBatch chunkSpriteBatch;
        private GraphicsDevice graphicsDevice;
        EntityController EntityController;  
        public override void update(GameTime gameTime, ComponentIndexPool CIP)
        {
            IComponent first;
            //Looping through a key set of position components
            foreach (var p in CIP.GetAll(components[0]))
            {
                //get the first component
                first = p.Value;
                //get its entity id
                int id = p.Key;
                //see if the other components in components also contain a components for this entity
                bool foundAll = true;
                for (int i = 1; i < components.Count; i++)
                {
                    IComponent c = CIP.Get(components[i], id);
                    if (c == null)
                    {
                        foundAll = false;
                        break;
                    }
                    //if the component is attached to the entity add it to the working entity 
                    workingEntity[components[i]] = c;
                }
                if (foundAll)
                {
                    workingEntity[components[0]] = first;
                }
            }

            CameraC camera = ((CameraC)CIP.GetAll(typeof(CameraC)).First().Value);
            chunkSpriteBatch.Begin(SpriteSortMode.Texture, null, null, null, null, null, camera.matrix);
            Vector2 position = ((PositionC)workingEntity[typeof(PositionC)]).position;
            bool rendered = false;
            foreach (var item in CIP.GetAll(typeof(ChunkC)))
            {
                if(((ChunkC)item.Value).bounds.Contains(position))
                {
                    EntityController.currentRenderedChunk = (ChunkC)item.Value;
                    rendered = true;
                    workingEntity[typeof(ChunkC)] = item.Value;
                    ProcessEntity();
                }              
            }

            if (!rendered )
            {
                int x = (int)((position.X / (diameter * Grid.gridPixelSize))) * diameter * Grid.gridPixelSize;
                int y = (int)((position.Y / (diameter * Grid.gridPixelSize))) * diameter * Grid.gridPixelSize;
                if(!(x <= 0 || y <= 0))
                CIP.InsertComponent(GenerateChunk(x,y), EntityController.GetFreeEntityId());
            }
            chunkSpriteBatch.End();
            //get the current rendered chunks
            //loop through their shit 
        }
        public ChunkSystem(GraphicsDevice graphicsDevice,EntityController entityController, ComponentIndexPool CIP, int diameter)
        {
            components.Add(typeof(PositionC));
            components.Add(typeof(ControllableC));
            workingEntity.Add(typeof(ChunkC),null);
            this.EntityController = entityController;
            this.graphicsDevice = graphicsDevice;
            this.chunkSpriteBatch = new SpriteBatch(graphicsDevice);
            this.diameter = diameter;
            this.numBlocks = diameter * diameter;
            CIP.InsertComponent(GenerateChunk(0, 0), EntityController.GetFreeEntityId());
        }

        public ChunkC GenerateChunk(int x,int y)
        {
            ChunkC chunk = new ChunkC(x,y,diameter,numComponentsPerBlock);
            IComponent[] comp = new IComponent[numBlocks*numComponentsPerBlock];
            var rand = new Random();
            float increament = 0.01f;
            float decemalW = (x/(diameter*Grid.gridPixelSize)) * ((diameter-1) +(diameter*increament));
            float decemalH = (y / (diameter * Grid.gridPixelSize)) * ((diameter - 1) + (diameter * increament));
            Console.WriteLine(decemalW);
            
            for (int h = 0; h < diameter; h++)
            {
                decemalH += increament;
                for (int w = 0; w < diameter; w++)
                {
                    decemalW += increament;
                    Double n = PerlinNoise.OctavePerlin((decemalW) *5 ,(decemalH)*5, 0, 1, 1) * 255;;
                    if (n < 150 && h > 5)
                    {
                        comp[((w + (h * diameter)) * numComponentsPerBlock)] = new TextureC(graphicsDevice, Grid.gridPixelSize, Grid.gridPixelSize, Color.FromNonPremultiplied((int)n, (int)n, (int)n, 255));
                        comp[((w + (h * diameter)) * numComponentsPerBlock) + 2] = new ColliderC(new Rectangle(w * Grid.gridPixelSize + chunk.bounds.X, h * Grid.gridPixelSize + chunk.bounds.Y, Grid.gridPixelSize, Grid.gridPixelSize));
                    }
                    else
                    {
                        comp[((w + (h * diameter)) * numComponentsPerBlock)] = new TextureC(graphicsDevice, Grid.gridPixelSize, Grid.gridPixelSize, Color.FromNonPremultiplied((int)n, (int)n, (int)n, 0));
                    }
                    comp[((w + (h * diameter)) * numComponentsPerBlock) + 1] = new PositionC(w * Grid.gridPixelSize + chunk.bounds.X, h * Grid.gridPixelSize + chunk.bounds.Y);                    
                }
                decemalW = (x / (diameter * Grid.gridPixelSize)) * ((diameter - 1) + (diameter * increament));
            }
            chunk.Blocks = comp;
            Console.WriteLine(decemalW + ":" + decemalH);
            return chunk;
        }

        public override void ProcessEntity()
        {
            IComponent[] components = ((ChunkC)workingEntity[typeof(ChunkC)]).Blocks;
            for (int i = 0; i < components.Length; i+=numComponentsPerBlock)
            {
                TextureC texture = ((TextureC)components[i]);
                Vector2 position = ((PositionC)components[i+1]).position;           

                chunkSpriteBatch.Draw(texture.texture, new Rectangle((int)position.X, (int)position.Y, (int)texture.scale.X, (int)texture.scale.Y), Color.White);
            }   
        }
    }
}
