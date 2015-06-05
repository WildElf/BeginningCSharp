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

using TeddyMineExplosion;

namespace ProgrammingAssignment5
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // constants for resolution
        const int WINDOW_WIDTH = 800;
        const int WINDOW_HEIGHT = 600;

        // mouse support
        MouseState oldmouse;

        // variables for game objects: mines, teddies, & explosions
        Mine mine;
        TeddyBear bear;
        Explosion boom;

        // sprites for game objects
        Texture2D mineSprite;
        Texture2D bearSprite;
        Texture2D explosionSprite;

        // lists for game assets
        List<Mine> mines = new List<Mine>();
        List<TeddyBear> bears = new List<TeddyBear>();
        List<Explosion> booms = new List<Explosion>();

        // random number generator
        Random rand = new Random();

        // timer support
        int elapsedSpawnTime;
        int bearSpawnDelay;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;

            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // todo: initialization logic

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

            // sprite loading
            mineSprite = Content.Load<Texture2D>("mine");
            bearSprite = Content.Load<Texture2D>("teddybear");
            explosionSprite = Content.Load<Texture2D>("explosion");

            // set bear spawn time
            bearSpawnDelay = 1000;
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

            // mouse support
            MouseState mouse = Mouse.GetState();

            // add a mine on left mouse button click
            if (mouse.LeftButton == ButtonState.Pressed &&
                oldmouse.LeftButton == ButtonState.Released)
            {
                mine = new Mine(mineSprite, mouse.X, mouse.Y);
                mines.Add(mine);
            }
            // randomization for bear creation
            float vectorX = rand.Next(100);
            float vectorY = rand.Next(100);
            vectorX = vectorX/100 - 0.5f;
            vectorY = vectorY/100 - 0.5f;

            Vector2 velocity = new Vector2(vectorX, vectorY);

            //Vector2 velocity = new Vector2((float)rand.NextDouble() - 0.5f);

            if (elapsedSpawnTime > bearSpawnDelay)
            {
                elapsedSpawnTime = 0;
                bear = new TeddyBear(bearSprite, velocity, WINDOW_WIDTH, WINDOW_HEIGHT);
                bears.Add(bear);
                bearSpawnDelay = rand.Next(1, 4) * 1000;
            }
            else
            {
                elapsedSpawnTime += gameTime.ElapsedGameTime.Milliseconds;
            }

            foreach (TeddyBear bear in bears)
            {
                bear.Update(gameTime);
                if (bear.Active)
                {
                    foreach (Mine mine in mines)
                    {
                        if (bear.CollisionRectangle.Intersects(mine.CollisionRectangle))
                        {
                            boom = new Explosion(explosionSprite, mine.CollisionRectangle.X, mine.CollisionRectangle.Y);
                            booms.Add(boom);
                            mine.Active = false;
                            bear.Active = false;
                        }
                    }
                }
            }

            foreach (Explosion boom in booms)
            {
                boom.Update(gameTime);
            }

            // remove dead things
            for (int i = mines.Count - 1; i >= 0; i--)
            {
                if (!mines[i].Active)
                {
                    mines.RemoveAt(i);
                }
            }

            for (int i = bears.Count - 1; i >= 0; i--)
            {
                if (!bears[i].Active)
                {
                    bears.RemoveAt(i);
                }
            }

            for (int i = booms.Count - 1; i >= 0; i--)
            {
                if (!booms[i].Playing)
                {
                    booms.RemoveAt(i);
                }
            }

            oldmouse = mouse;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SandyBrown);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            foreach (Mine mine in mines)
            {
                mine.Draw(spriteBatch);
            }
            foreach (TeddyBear bear in bears)
            {
                bear.Draw(spriteBatch);
            }
            foreach (Explosion boom in booms)
            {
                boom.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
