using FarseerPhysics.Collision;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using LightWay.Engine.ECS.Components;
using LightWay.Engine.ECS.Systems;
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
    /// A manager class the handles creating Entitys and executing Systems
    /// </summary>
    public class EntityController
    {
        public int entityCount { get; private set; } = 0;
        private GraphicsDevice graphicsDevice { get; set; }

        public ChunkC currentRenderedChunks = null;

        public World physicsWorld = new World(new Vector2(0,5f));

        public List<Entity> stagedEntitys = new List<Entity>();

        public EntityController(GraphicsDevice graphicsDevice)
        {
            // physicsWorld.
            this.graphicsDevice = graphicsDevice;
            InitCIP();
            InitSystems();
        }


        public List<Entity> entitys { get; set; } = new List<Entity>();
         
        public ComponentIndexPool CIP { get; private set; }


        //SYSTEMS
        /*
             generalSystems.Add(new PlayerSystem());
             generalSystems.Add(new CameraFollowSystem(graphicsDevice));*/

        //RENDERING
        BackGroundSystem backGroundSystem;
        ChunkSystem chunkSystem;
        RenderSystem renderSystem;
        UIRenderSystem UIRenderSystem;

        //GENERAL
        PlayerSystem playerSystem;
        CameraFollowSystem cameraFollowSystem;






        /// <summary>
        /// Updates any non rendering based Systems
        /// </summary>
        /// <param name="gameTime">The games <c>GameTime</c></param>
        public void GeneralUpdate(GameTime gameTime)
        {
            physicsWorld.Step(0.0166f);
            playerSystem.Update(gameTime);
            cameraFollowSystem.Update(gameTime, CIP);
        }
        /// <summary>
        /// Updates rendering based systems
        /// </summary>
        /// <param name="gameTime">The games <c>GameTime</c></param>
        public void RenderingUpdate(GameTime gameTime)
        {
            InsertStagedEntitys();
            backGroundSystem.Update(gameTime, CIP);
            chunkSystem.Update(gameTime, CIP);
            renderSystem.Update(gameTime, CIP);
            UIRenderSystem.Update(gameTime, CIP);

        }

        private void InsertStagedEntitys()
        {
            foreach(var entity in stagedEntitys)
            {           
                foreach (object component in entity.components)
                {
                    CIP.InsertComponent(component, entity);
                }
                entitys.Add(entity);
            }
            stagedEntitys.Clear();
        }

        /// <summary>
        /// Where systems are added to the EntiyController.
        /// Currently you have to add them here manually 
        /// </summary>
        private void InitSystems()
        {
            //generalSystems.Add(new PhysicsSystem(CIP,this));
            backGroundSystem = new BackGroundSystem(graphicsDevice,this);
            chunkSystem = new ChunkSystem(graphicsDevice, this,CIP, 100);
            playerSystem = new PlayerSystem(this);
            cameraFollowSystem = new CameraFollowSystem(graphicsDevice,this);
            renderSystem = new RenderSystem(new SpriteBatch(graphicsDevice), graphicsDevice, this);
            UIRenderSystem = new UIRenderSystem(new SpriteBatch(graphicsDevice),this);
        }
        /// <summary>
        /// Creates a ComponentIndexPool.
        /// Every EntityController needs a CIP.
        /// </summary>
        private void InitCIP()
        {
            this.CIP = new ComponentIndexPool();
        }
        /// <summary>
        /// Creates an entity out of passed in Components
        /// </summary>
        /// <param name="components">Your entitys components</param>
        public Entity CreateEntity(params object[] components)
        {
            Entity entity = new Entity(entityCount,this);
            foreach (object component in components)
            {
                CIP.InsertComponent(component, entity);

                entity.components.Add(component.GetType());
            }
            entitys.Add(entity);
            entityCount++;
            return entity;
        }
        /// <summary>
        /// Creates an entity out of passed in Components
        /// </summary>
        /// <param name="components">Your entitys components</param>
        public int CreateEntityDelayed(params object[] components)
        {
            Entity entity = new Entity(entityCount,this);
            foreach (object component in components)
            {
                entity.components.Add(component.GetType());
            }
            entitys.Add(entity);
            entityCount++;
            stagedEntitys.Add(entity);
            return entity.id;           
        }
        public int GetFreeEntityId()
        {
            return ++entityCount;
        }

       


        //New entity methods
        public bool TryGetComponent<T>(Entity entity, out T component)
        {
            component = CIP.Get<T>(entity);
            if (component is T) return true;
            return false;
        }
        public bool HasComponent<T>(Entity entity)
        {            
            if (CIP.Get<T>(entity) is T) return true;
            return false;
        }
        public void AddComponent<T>(Entity entity,T component)
        {
            CIP.InsertComponent(component, entity);
        }
        public List<Entity> GetAllEntityWithComponent<T>()
        {
           return CIP.GetAllEntityWithComponent<T>();
        }

        /// <summary>
        /// COME UP WITH A BETTER NAME
        /// </summary>
        /// <param name="entities">The list of entitys to search through</param>
        /// <param name="components">The components to search for</param>
        /// <returns></returns>
        public List<Entity> EntitesThatContainComponents(List<Entity> entities, params Type[] components)
        {
            List<Entity> output = new List<Entity>(entities);

            foreach (Entity e in entities)
            {
                foreach (var o in components)
                {

                    if (!e.components.Contains(o))
                    {
                        output.Remove(e);
                        break;
                    }
                }
            }
            return output;
        }
    }
}
