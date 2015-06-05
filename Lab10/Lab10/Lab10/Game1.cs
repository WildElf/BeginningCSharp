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
using ExplodingTeddies;

namespace Lab10
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        const int WINDOW_WIDTH = 800;
        const int WINDOW_HEIGHT = 600;
        
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        TeddyBear teddy0;
        TeddyBear teddy1;

        Rectangle rectangle0;
        Rectangle rectangle1;
        Rectangle explosionRectangle;

        Explosion explosion;

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

            // load the teddies
            teddy0 = new TeddyBear(Content, WINDOW_WIDTH, WINDOW_HEIGHT, "teddybear0", 200, 100, new Vector2(1,0));
            teddy0.Active = true;
            teddy1 = new TeddyBear(Content, WINDOW_WIDTH, WINDOW_HEIGHT, "teddybear1", 600, 100, new Vector2(-1,0));
            teddy1.Active = true;

            // creating explosion
            explosion = new Explosion(Content);
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
            teddy0.Update();
            teddy1.Update();

            if (teddy0.Active && teddy1.Active)
            {
                rectangle0 = new Rectangle(teddy0.DrawRectangle.X, teddy0.DrawRectangle.Y, teddy0.DrawRectangle.Width, teddy0.DrawRectangle.Height);
                rectangle1 = new Rectangle(teddy1.DrawRectangle.X, teddy1.DrawRectangle.Y, teddy1.DrawRectangle.Width, teddy1.DrawRectangle.Height);
            
                if (rectangle0.Intersects(rectangle1))
                {
                    explosionRectangle = Rectangle.Intersect(rectangle0,rectangle1);
                    explosion.Play(explosionRectangle.X, explosionRectangle.Y);
                    teddy0.Active = false;
                    teddy1.Active = false;
                }
            }
            explosion.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            teddy0.Draw(spriteBatch);
            teddy1.Draw(spriteBatch);

            explosion.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
