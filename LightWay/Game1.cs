using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Threading;
using LightWay.Engine.ECS.Tools;
using LightWay.Engine.ECS.Systems;
using LightWay.Engine.ECS.Components;
using Test;
using System.Runtime.InteropServices;
using LightWay.Engine.Tools;

namespace LightWay
{
    
    public class Game1 : Game
    {
        public static GraphicsDevice graphicsDevice = null;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        EntityController entityController;
        Texture2D texture;
        private double keyUpdateTimer = 0;
        public EntityGroup mainMenu = null;
        public Game1()
        {
            
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.AllowUserResizing = true;
            IsMouseVisible = true;
            // Window.ClientSizeChanged += OnResize;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            entityController = new EntityController(GraphicsDevice);
            graphicsDevice = GraphicsDevice;
            base.Initialize();
            Imui.Init();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            ChunkSystem.texture = Content.Load<Texture2D>("Dirt_1");
            // Create a new SpriteBatch, which can be used to draw textures
            spriteBatch = new SpriteBatch(GraphicsDevice);
            entityController.CreateEntity(new CameraC());
            Body b = BodyFactory.CreateCircle(entityController.physicsWorld, ConvertUnits.ToSimUnits((Grid.gridPixelSize)/2), 20f, new Vector2(0, 0), BodyType.Dynamic);
            b.FixedRotation = true;
            b.Friction = 0.1f;
            b.Restitution = 0f;

            entityController.CreateEntity(new TransformC(new Vector2(0, 200), new Vector2(Grid.gridPixelSize, Grid.gridPixelSize), b), new ControllableC(), new TextureC(GraphicsDevice, Color.Black), new VelocityC(), new ForgroundC());

            //entityController.CreateEntity(new TransformC(50, 50, BodyFactory.CreateBody(entityController.physicsWorld, new Vector2(0, 0), 0, BodyType.Static)), new TextureC(GraphicsDevice, new Vector2(50, 50), Color.Black));
            // entityController.CreateEntity(new TransformC(new Vector2(0, 300)), new TextureC(GraphicsDevice, new Vector2(300, 100),Color.Green),new ColliderC(new Rectangle(0,300,300,100)));
            //entityController.CreateEntity(new TransformC(new Vector2(300, 200)), new TextureC(GraphicsDevice, new Vector2(100, 300), Color.Green), new ColliderC(new Rectangle(300, 200, 100, 300)));

            entityController.CreateEntityDelayed(new TextureC(Content.Load<Texture2D>("Background_Mountain_1")),new TransformC(new Vector2(0, 60), new Vector2(3,3)), new BackGroundC(-.9f));
            entityController.CreateEntity(new TextureC(Content.Load<Texture2D>("Background_trees_3")), new TransformC(new Vector2(0, 20), new Vector2(3, 3)), new BackGroundC(-.8f));
            entityController.CreateEntity(new TextureC(Content.Load<Texture2D>("Background_trees_2")), new TransformC(new Vector2(0, 35), new Vector2(3, 3)), new BackGroundC(-.7f));
            entityController.CreateEntity(new TextureC(Content.Load<Texture2D>("Background_trees_1")), new TransformC(new Vector2(0, 50), new Vector2(3, 3)), new BackGroundC(-.6f));

            TextHelper.CreateFont(Content.Load<Texture2D>("Font_1"), "default", GraphicsDevice);

           /* UIBuilder.Begin(entityController);
            UIBuilder.AttachTexture(Content.Load<Texture2D>("Background_Mountain_1"), new Vector2(300, 300), new Vector2(1, 1));
            UIBuilder.AttachText("FUCK YOU,          BRAH", "default",Color.Blue, new Vector2(300, 300), new Vector2(10, 10));
            UIBuilder.AttachButton(Content.Load<Texture2D>("Background_Mountain_1"),new Vector2(500,500),new Vector2(1,1));
            mainMenu = UIBuilder.Complete();*/
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 
        protected override void Update(GameTime gameTime)
        {
            Console.WriteLine(TestClass.Number());
            if (States.MainMenue == States.Dead) Console.WriteLine("Hello");
            else Console.WriteLine("Goodbye");
            keyUpdateTimer -= gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
            if (keyUpdateTimer < -.1f)
            {
                Input.UpdateKeys();
                keyUpdateTimer = 0;
            }
            Input.CheckMouse();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == Input.getGamePadKey() || Input.getKeyBoardKey(Keys.Escape))
                Exit();
            entityController.GeneralUpdate(gameTime);

            //base.IsMouseVisible = true;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {         
            GraphicsDevice.Clear(Color.CornflowerBlue);
            entityController.RenderingUpdate(gameTime);
            // TODO: Add your drawing code here
            
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointWrap, null, null, null, Matrix.CreateScale(new Vector3(10, 10, 0)));
            spriteBatch.End();

            Texture2D texture = new Texture2D(GraphicsDevice, 1, 1);
            Color[] c = new Color[1];
            c[0] = Color.Black;
            texture.SetData(c);
            spriteBatch.Begin();

            spriteBatch.End();
            base.Draw(gameTime);
            //Drawing ui
            Imui.Begin();
            if (Imui.Button(10, 10, "PRESS ME", 10, 10, 1, 1)) Imui.Text(100, 100, "THANKS", 10, 10);
            Imui.End();
        }
    }

}

