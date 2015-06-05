using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Lab5
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        const int WINDOW_WIDTH = 1080;
        const int WINDOW_HEIGHT = 800;
        
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D peng0;
        Texture2D peng1;
        Texture2D peng2;

        Rectangle rectangle0;
        Rectangle rectangle1;
        Rectangle rectangle2;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
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

            // load penguin content

            peng0 = Content.Load<Texture2D>("peng0");
            peng1 = Content.Load<Texture2D>("peng1");
            peng2 = Content.Load<Texture2D>("peng2");

            // create draw rectangles

            rectangle0 = new Rectangle(100, 500, peng0.Width, peng0.Height);
            rectangle1 = new Rectangle(200, 100, peng1.Width, peng1.Height);
            rectangle2 = new Rectangle(700, 300, peng2.Width, peng2.Height);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // draw sprites

            spriteBatch.Begin();

            spriteBatch.Draw(peng0, rectangle0, Color.White);
            spriteBatch.Draw(peng1, rectangle1, Color.White);
            spriteBatch.Draw(peng2, rectangle2, Color.Blue);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
