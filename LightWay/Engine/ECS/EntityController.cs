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

        private Dictionary<Type, Dictionary<Entity, object>> pool { get; set; } = new Dictionary<Type, Dictionary<Entity, object>>();
        public int EntityCount { get; private set; } = 0;
        private GraphicsDevice graphicsDevice { get; set; }

        public World physicsWorld = new World(new Vector2(0, 5f));

        public List<Entity> stagedEntitys = new List<Entity>();


        public EntityController(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            InitSystems();
        }


        public List<Entity> entitys { get; set; } = new List<Entity>();

        //SYSTEMS
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
            cameraFollowSystem.Update(gameTime);
        }
        /// <summary>
        /// Updates rendering based systems
        /// </summary>
        /// <param name="gameTime">The games <c>GameTime</c></param>
        public void RenderingUpdate(GameTime gameTime)
        {
            InsertStagedEntitys();
            backGroundSystem.Update(gameTime);
            chunkSystem.Update(gameTime);
            renderSystem.Update(gameTime);
            UIRenderSystem.Update(gameTime);
        }

        private void InsertStagedEntitys()
        {
            foreach (var entity in stagedEntitys)
            {
                foreach (object component in entity.components)
                {
                    AddComponent(entity, component);
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
            backGroundSystem = new BackGroundSystem(graphicsDevice, this);
            chunkSystem = new ChunkSystem(graphicsDevice, this, 100);
            playerSystem = new PlayerSystem(this);
            cameraFollowSystem = new CameraFollowSystem(graphicsDevice, this);
            renderSystem = new RenderSystem(new SpriteBatch(graphicsDevice), graphicsDevice, this);
            UIRenderSystem = new UIRenderSystem(new SpriteBatch(graphicsDevice), this);
        }

        /// <summary>
        /// Creates an entity out of passed in Components
        /// </summary>
        /// <param name="components">Your entitys components</param>
        public Entity CreateEntity(params object[] components)
        {
            Entity entity = new Entity(EntityCount, this);
            foreach (object component in components)
            {
                AddComponent(entity, component);
                entity.components.Add(component);
            }
            entitys.Add(entity);
            EntityCount++;
            return entity;
        }

        /// <summary>
        /// Creates an entity out of passed in Components
        /// </summary>
        /// <param name="components">Your entitys components</param>
        public int CreateEntityDelayed(params object[] components)
        {
            Entity entity = new Entity(EntityCount, this);
            foreach (object component in components)
            {
                entity.components.Add(component);
            }
            //entitys.Add(entity);
            EntityCount++;
            stagedEntitys.Add(entity);
            return entity;
        }
        public int GetFreeEntityId()
        {
            return ++EntityCount;
        }

        //New entity methods
        public bool TryGetComponent<T>(Entity entity, out T component)
        {
            if(pool.ContainsKey(typeof(T)) && pool[typeof(T)].ContainsKey(entity))
            {
                component = (T)pool[typeof(T)][entity];
                return true;
            }
            component = default;
            return false;
        }
        public bool HasComponent<T>(Entity entity)
        {
            if (TryGetComponent<T>(entity,out T component)) return true;
            return false;
        }
        public void AddComponent<T>(Entity entity, T component)
        {
            Type type = component.GetType();
            if (pool.ContainsKey(type))
            {
                if (pool[type].ContainsKey(entity)) pool[type][entity] = component;
                else
                {
                    pool[type].Add(entity, component);
                }
            }
            else
            {
                Dictionary<Entity, object> d = new Dictionary<Entity, object>();
                d.Add(entity, component);
                pool.Add(type, d);
            }
        }
        public List<Entity> GetAllEntityWithComponent<T>()
        {
            return pool[typeof(T)].Keys.ToList();
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
                    if (!e.ContainsComponentType(o))
                    {
                        output.Remove(e);
                        break;
                    }
                }
            }
            return output;
        }

        public T[] GetAllComponent<T>()
        {
            return Array.ConvertAll(pool[typeof(T)].Values.ToArray(), item => (T)item);
        }
    }
}
