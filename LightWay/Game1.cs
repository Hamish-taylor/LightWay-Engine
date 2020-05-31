using LightWay;
using LightWay.Engine.ECS.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace LightWay
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        PlayerDEPRICATED player;
        EntityController entityController;
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
            player = new PlayerDEPRICATED(Content.Load<Texture2D>("graphics/Bombsquad Black"),graphics);
            entityController.CreateEntity(new Position(new Vector2(100,100)), new Texture((Content.Load<Texture2D>("graphics/Bombsquad Black"))));
            entityController.CreateEntity(new Position(new Vector2(50, 50)), new Controllable(), new Texture((Content.Load<Texture2D>("graphics/Bombsquad Black"))),new VelocityC(),new GravityC(new Vector2(0, 1)));
            player.scale = 0.5f;

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

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == Input.getGamePadKey() || Input.getKeyBoardKey(Keys.Escape))
                Exit();
            entityController.GeneralUpdate(gameTime);
            
            player.Do();
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
            spriteBatch.Begin();
            spriteBatch.Draw(player.Texture,new Rectangle((int)player.position.X, (int)player.position.Y, (int)player.width, (int)player.height),Color.White);
            spriteBatch.End();
            entityController.RenderingUpdate(gameTime);
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
