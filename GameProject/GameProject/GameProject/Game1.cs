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

namespace GameProject
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // game objects. Using inheritance would make this
        // easier, but inheritance isn't a GDD 1200 topic
        Burger burger;
        List<TeddyBear> bears = new List<TeddyBear>();
        static List<Projectile> projectiles = new List<Projectile>();
        List<Explosion> explosions = new List<Explosion>();

        // projectile and explosion sprites. Saved so they don't have to
        // be loaded every time projectiles or explosions are created
        static Texture2D frenchFriesSprite;
        static Texture2D teddyBearProjectileSprite;
        static Texture2D explosionSpriteStrip;

        // support for two extra explosions
        static Texture2D explosionSmallSpriteStrip;
        static Texture2D explosionBigSpriteStrip;

        // scoring support
        int score = 0;
        string scoreString = GameConstants.SCORE_PREFIX + 0;

        // health support
        string healthString = GameConstants.HEALTH_PREFIX + 
            GameConstants.BURGER_INITIAL_HEALTH;
        bool burgerDead = false;

        // text display support
        SpriteFont font;

        // sound effects
        SoundEffect burgerDamage;
        SoundEffect burgerDeath;
        SoundEffect burgerShot;
        SoundEffect explosion;
        SoundEffect teddyBounce;
        SoundEffect teddyShot;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // set resolution
            graphics.PreferredBackBufferWidth = GameConstants.WINDOW_WIDTH;
            graphics.PreferredBackBufferHeight = GameConstants.WINDOW_HEIGHT;

            // show mouse cursor
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
            RandomNumberGenerator.Initialize();

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

            // load audio content
            burgerShot = Content.Load<SoundEffect>("Sounds/BurgerShot");
            burgerDamage = Content.Load<SoundEffect>("Sounds/BurgerDamage");
            burgerDeath = Content.Load<SoundEffect>("Sounds/BurgerDeath");
            explosion = Content.Load<SoundEffect>("Sounds/Explosion");
            teddyBounce = Content.Load<SoundEffect>("Sounds/TeddyBounce");
            teddyShot = Content.Load<SoundEffect>("Sounds/TeddyShot");
            
            // load sprite font
            font = Content.Load<SpriteFont>("Arial20");

            // load projectile and explosion sprites
            // additional explosions (resized content) for burger hits and death
            teddyBearProjectileSprite = Content.Load<Texture2D>("teddybearprojectile");
            frenchFriesSprite = Content.Load<Texture2D>("frenchfries");
            explosionSpriteStrip = Content.Load<Texture2D>("explosion");
            explosionSmallSpriteStrip = Content.Load<Texture2D>("explosionsmall");
            explosionBigSpriteStrip = Content.Load<Texture2D>("explosionbig");


            // add initial game objects
            // added 2nd sprite string to change burger at death
            burger = new Burger(Content,"burger",
                GameConstants.WINDOW_WIDTH / 2,GameConstants.WINDOW_HEIGHT- (GameConstants.WINDOW_HEIGHT /8), 
                burgerShot, "burgerdead");

            for (int i = 0; i < GameConstants.MAX_BEARS; i++)
            {
                SpawnBear();
            }
            // set initial health and score strings
            healthString = GameConstants.HEALTH_PREFIX + burger.Health;

            scoreString = GameConstants.SCORE_PREFIX + score;
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            //// get current mouse state and update burger
            //MouseState mouse = Mouse.GetState();
            //burger.Update(gameTime, mouse);

            // get Keyboard state and update burger
            KeyboardState keyboard = Keyboard.GetState();
            burger.Update(gameTime, keyboard);

            // update other game objects
            foreach (TeddyBear bear in bears)
            {
                bear.Update(gameTime, burgerDead);
            }
            foreach (Projectile projectile in projectiles)
            {
                projectile.Update(gameTime);
            }
            foreach (Explosion explosion in explosions)
            {
                explosion.Update(gameTime);
            }

            // check and resolve collisions between teddy bears
            for (int i = bears.Count - 1; i >= 0; i--)
            {
                for (int b = i + 1; b < bears.Count; b++)
                {
                    if (bears[i].Active && bears[b].Active)
                    {
                        CollisionResolutionInfo collisionCheck = CollisionUtils.CheckCollision(gameTime.ElapsedGameTime.Milliseconds,
                            GameConstants.WINDOW_WIDTH, GameConstants.WINDOW_HEIGHT,
                            bears[i].Velocity, bears[i].DrawRectangle,
                            bears[b].Velocity, bears[b].DrawRectangle);
                        {
                            // support to exchange velocities for a smooth bounce
                            Vector2 bear1 = bears[i].Velocity;
                            Vector2 bear2 = bears[b].Velocity;

                            if (collisionCheck != null)
                            {
                                if (collisionCheck.FirstOutOfBounds)
                                {
                                    bears[i].Active = false;
                                }
                                else
                                {
                                    bears[i].Velocity = bear2;
                                }

                                if (collisionCheck.SecondOutOfBounds)
                                {
                                    bears[b].Active = false;
                                }
                                else
                                {
                                    bears[b].Velocity = bear1;
                                }
                            }
                        }
                    }
                }
            }

            // check and resolve collisions between burger and teddy bears
            // unless burger is dead
            if (!burgerDead)
            {
                foreach (TeddyBear bear in bears)
                {
                    if (bear.CollisionRectangle.Intersects(burger.CollisionRectangle))
                    {
                        burger.Health -= GameConstants.BEAR_DAMAGE;
                        burgerDamage.Play();
                        bear.Active = false;
                        Explosion crash = new Explosion(explosionSpriteStrip,
                            bear.Location.X, bear.Location.Y, explosion);
                        explosions.Add(crash);

                        // update score for kills by collision
                        score += GameConstants.BEAR_POINTS;
                        scoreString = GameConstants.SCORE_PREFIX + score;

                        // check if burger is still alive
                        CheckBurgerKill();

                        // update health display
                        healthString = GameConstants.HEALTH_PREFIX + burger.Health;

                    }
                }


                // check and resolve collisions between burger and (teddy) projectiles
                foreach (Projectile projectile in projectiles)
                {
                    if (burger.CollisionRectangle.Intersects(projectile.CollisionRectangle) &&
                        projectile.Type == ProjectileType.TeddyBear)
                    {
                        burger.Health -= GameConstants.TEDDY_BEAR_PROJECTILE_DAMAGE;
                        burgerDamage.Play();
                        projectile.Active = false;
                        Explosion boom = new Explosion(explosionSmallSpriteStrip,
                            projectile.Location.X, projectile.Location.Y, explosion);
                        explosions.Add(boom);

                        // check if burger is still alive
                        CheckBurgerKill();

                        // update health display
                        healthString = GameConstants.HEALTH_PREFIX + burger.Health;
                    }
                }
            }


            // check and resolve collisions between teddy bears and (burger) projectiles
            foreach (TeddyBear bear in bears)
            {
                foreach (Projectile projectile in projectiles)
                {
                    if (bear.CollisionRectangle.Intersects(projectile.CollisionRectangle) &&
                        projectile.Type == ProjectileType.FrenchFries)
                    {
                        bear.Active = false;
                        projectile.Active = false;
                        Explosion newBoom = new Explosion(explosionSpriteStrip,
                            bear.Location.X, bear.Location.Y, explosion);
                        explosions.Add(newBoom);

                        // update score
                        score += GameConstants.BEAR_POINTS;
                        scoreString = GameConstants.SCORE_PREFIX + score;
                    }
                }
            }

            // clean out inactive teddy bears and add new ones as necessary
            for (int i = bears.Count -1; i >= 0; i--)
            {
                if (!bears[i].Active)
                {
                    bears.RemoveAt(i);
                }
            }

            // clean out inactive projectiles
            for (int i = projectiles.Count -1; i >= 0; i--)
            {
                if (!projectiles[i].Active)
                {
                    projectiles.RemoveAt(i);
                }
            }

            // clean out finished explosions
            for (int i = explosions.Count - 1; i >= 0; i--)
            {
                if (explosions[i].Finished)
                {
                    explosions.RemoveAt(i);
                }
            }

            // spawn new bears
            while (bears.Count < GameConstants.MAX_BEARS)
            {
                SpawnBear();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Snow);

            spriteBatch.Begin();

            // draw game objects
            burger.Draw(spriteBatch);
            foreach (TeddyBear bear in bears)
            {
                bear.Draw(spriteBatch);
            }
            foreach (Projectile projectile in projectiles)
            {
                projectile.Draw(spriteBatch);
            }
            foreach (Explosion explosion in explosions)
            {
                explosion.Draw(spriteBatch);
            }

            // draw score and health
            spriteBatch.DrawString(font, healthString, GameConstants.HEALTH_LOCATION, Color.Black);
            spriteBatch.DrawString(font, scoreString, GameConstants.SCORE_LOCATION, Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        #region Public methods

        /// <summary>
        /// Gets the projectile sprite for the given projectile type
        /// </summary>
        /// <param name="type">the projectile type</param>
        /// <returns>the projectile sprite for the type</returns>
        public static Texture2D GetProjectileSprite(ProjectileType type)
        {
                        // replace with code to return correct projectile sprite based on projectile type
            if (type == ProjectileType.FrenchFries)
            {
                return frenchFriesSprite;
            }
            else if (type == ProjectileType.TeddyBear)
            {
                return teddyBearProjectileSprite;
            }
            else return null;
        }

        /// <summary>
        /// Adds the given projectile to the game
        /// </summary>
        /// <param name="projectile">the projectile to add</param>
        public static void AddProjectile(Projectile projectile)
        {
            projectiles.Add(projectile);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Spawns a new teddy bear at a random location
        /// </summary>
        private void SpawnBear()
        {
            // generate random location
            int x = GetRandomLocation(GameConstants.SPAWN_BORDER_SIZE, 
                GameConstants.WINDOW_WIDTH - (2 * GameConstants.SPAWN_BORDER_SIZE));
            int y = GetRandomLocation(GameConstants.SPAWN_BORDER_SIZE, 
                GameConstants.WINDOW_HEIGHT - (2 *GameConstants.SPAWN_BORDER_SIZE));

            // generate random velocity
            float speed = GameConstants.MIN_BEAR_SPEED + RandomNumberGenerator.NextFloat(GameConstants.BEAR_SPEED_RANGE);
            float angle = (RandomNumberGenerator.NextFloat(360) * (float)Math.PI / 180);
            Vector2 velocity = new Vector2((float)Math.Cos(angle) * speed, 
                -1 * (float)Math.Sin(angle) * speed);

            // create new bear
            TeddyBear newBear = new TeddyBear(Content, "teddybear", x, y, velocity, teddyBounce, teddyShot);

            // make sure we don't spawn into a collision

            List<Rectangle> collisionRectangles = new List<Rectangle>(GetCollisionRectangles());
            while (!CollisionUtils.IsCollisionFree(newBear.CollisionRectangle, collisionRectangles))
            {
                // generate another random location
                x = GetRandomLocation(GameConstants.SPAWN_BORDER_SIZE,
                    GameConstants.WINDOW_WIDTH - (2 * GameConstants.SPAWN_BORDER_SIZE));
                y = GetRandomLocation(GameConstants.SPAWN_BORDER_SIZE,
                    GameConstants.WINDOW_HEIGHT - (2 * GameConstants.SPAWN_BORDER_SIZE));

                // create another new bear
                newBear = new TeddyBear(Content, "teddybear", x, y, velocity, teddyBounce, teddyShot);

           }

            // add new bear to list
            bears.Add(newBear);

        }

        /// <summary>
        /// Gets a random location using the given min and range
        /// </summary>
        /// <param name="min">the minimum</param>
        /// <param name="range">the range</param>
        /// <returns>the random location</returns>
        private int GetRandomLocation(int min, int range)
        {
            return min + RandomNumberGenerator.Next(range);
        }

        /// <summary>
        /// Gets a list of collision rectangles for all the objects in the game world
        /// </summary>
        /// <returns>the list of collision rectangles</returns>
        private List<Rectangle> GetCollisionRectangles()
        {
            List<Rectangle> collisionRectangles = new List<Rectangle>();
            collisionRectangles.Add(burger.CollisionRectangle);
            foreach (TeddyBear bear in bears)
            {
                collisionRectangles.Add(bear.CollisionRectangle);
            }
            foreach (Projectile projectile in projectiles)
            {
                collisionRectangles.Add(projectile.CollisionRectangle);
            }
            foreach (Explosion explosion in explosions)
            {
                collisionRectangles.Add(explosion.CollisionRectangle);
            }
            return collisionRectangles;
        }

        /// <summary>
        /// Checks to see if the burger has just been killed
        /// </summary>
        private void CheckBurgerKill()
        {
            if (burger.Health == 0 && !burgerDead)
            {
                burgerDead = true;
                explosions.Add(new Explosion(explosionBigSpriteStrip,burger.CollisionRectangle.Center.X,
                    burger.CollisionRectangle.Center.Y,explosion));

                // play losing sound
                burgerDeath.Play();
         
            }
        }

        #endregion
    }
}
