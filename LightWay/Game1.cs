﻿using LightWay;
using LightWay.Engine.ECS.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Threading;

namespace LightWay
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        PlayerDEPRICATED[] player = new PlayerDEPRICATED[1000];
        EntityController entityController;

        private double keyUpdateTimer = 0;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.

            spriteBatch = new SpriteBatch(GraphicsDevice);
            entityController.CreateEntity(new PositionC(new Vector2(50, 50)), new ControllableC(), new TextureC((Content.Load<Texture2D>("graphics/Bombsquad Black"))), new VelocityC(), new GravityC(new Vector2(0, 1)), new ColliderC());
            for (int i = 0; i < 20000; i++)
            {
                entityController.CreateEntity(new PositionC(new Vector2(50, 50)), new ControllableC(), new TextureC((Content.Load<Texture2D>("graphics/Bombsquad Black"))));
            }
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
        protected override void Update(GameTime gameTime)
        {
            keyUpdateTimer -= gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
            if(keyUpdateTimer < -.1f)
            {
                Input.UpdateKeys();
                keyUpdateTimer = 0;
            }
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
            GraphicsDevice.Clear(Color.Gray);
            entityController.RenderingUpdate(gameTime);
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
