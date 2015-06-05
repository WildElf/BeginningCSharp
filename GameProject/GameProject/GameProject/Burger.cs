using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject
{
    /// <summary>
    /// A class for a burger
    /// </summary>
    public class Burger
    {
        #region Fields

        // graphic and drawing info
        Texture2D sprite;
        Rectangle drawRectangle;
        Texture2D deadBurger;

        //MouseState oldmouse;

        // burger stats
        int health = 100;

        // shooting support
        bool canShoot = true;
        int elapsedCooldownTime = 0;

        // sound effect
        SoundEffect shootSound;

        #endregion

        #region Constructors

        /// <summary>
        ///  Constructs a burger
        /// </summary>
        /// <param name="contentManager">the content manager for loading content</param>
        /// <param name="spriteName">the sprite name</param>
        /// <param name="x">the x location of the center of the burger</param>
        /// <param name="y">the y location of the center of the burger</param>
        /// <param name="shootSound">the sound the burger plays when shooting</param>
        public Burger(ContentManager contentManager, string spriteName, int x, int y,
            SoundEffect shootSound, string dead)
        {
            LoadContent(contentManager, spriteName, x, y, dead);
            this.shootSound = shootSound;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the collision rectangle for the burger
        /// </summary>
        public Rectangle CollisionRectangle
        {
            get { return drawRectangle; }
        }

        public int Health
        {
            get { return health; }
            set 
            {
                health = value;

                if (health > 100)
                {
                    health = 100;
                }
                else if (health <0)
                {
                    health = 0;
                }

            }
        }

        #endregion

        #region Private properties

        /// <summary>
        /// Gets and sets the x location of the center of the burger
        /// </summary>
        private int X
        {
            get { return drawRectangle.Center.X; }
            set
            {
                drawRectangle.X = value - drawRectangle.Width / 2;

                // clamp to keep in range
                if (drawRectangle.X < 0)
                {
                    drawRectangle.X = 0;
                }
                else if (drawRectangle.X > GameConstants.WINDOW_WIDTH - drawRectangle.Width)
                {
                    drawRectangle.X = GameConstants.WINDOW_WIDTH - drawRectangle.Width;
                }
            }
        }

        /// <summary>
        /// Gets and sets the y location of the center of the burger
        /// </summary>
        private int Y
        {
            get { return drawRectangle.Center.Y; }
            set
            {
                drawRectangle.Y = value - drawRectangle.Height / 2;

                // clamp to keep in range
                if (drawRectangle.Y < 0)
                {
                    drawRectangle.Y = 0;
                }
                else if (drawRectangle.Y > GameConstants.WINDOW_HEIGHT - drawRectangle.Height)
                {
                    drawRectangle.Y = GameConstants.WINDOW_HEIGHT - drawRectangle.Height;
                }
            }
        }

        #endregion

        #region Public methods

        public void Update(GameTime gameTime, KeyboardState keyboard)
        {
            // burger should only respond if alive
            if (health > 0)
            {

                // move burger using keyboard
                int move = GameConstants.BURGER_MOVEMENT_AMOUNT;
                if (keyboard.IsKeyDown(Keys.S))
                {
                    X -= move;

                }

                if (keyboard.IsKeyDown(Keys.F))
                {
                    X += move;
                }

                if (keyboard.IsKeyDown(Keys.E))
                {
                    Y -= move;
                }

                if (keyboard.IsKeyDown(Keys.D))
                {
                    Y += move;
                }

                // clamp burger movement
                if (X < 0 )
                {
                    X = 0;
                }
                else if (X > GameConstants.WINDOW_WIDTH)
                {
                    X = GameConstants.WINDOW_WIDTH;
                }

                if (Y < 0)
                {
                    Y = 0;
                }
                else if (Y > GameConstants.WINDOW_HEIGHT)
                {
                    Y = GameConstants.WINDOW_HEIGHT;
                }

                // checking if burger can shoot
                if (keyboard.IsKeyDown(Keys.Space) && canShoot)
                {
                    canShoot = false;

                    Projectile projectile = new Projectile(ProjectileType.FrenchFries,
                        Game1.GetProjectileSprite(ProjectileType.FrenchFries),
                        drawRectangle.X,
                        drawRectangle.Y + GameConstants.FRENCH_FRIES_PROJECTILE_OFFSET,
                        GameConstants.FRENCH_FRIES_PROJECTILE_SPEED);

                    Game1.AddProjectile(projectile);
                    shootSound.Play();

                }

                if (!canShoot)
                {
                    elapsedCooldownTime += gameTime.ElapsedGameTime.Milliseconds;
                }

                if (elapsedCooldownTime > GameConstants.BURGER_COOLDOWN_MILLISECONDS ||
                    keyboard.IsKeyUp(Keys.Space))
                {
                    canShoot = true;
                    elapsedCooldownTime = 0;
                }
            }
            else // change the burger sprite because it's dead
            {
                sprite = deadBurger;
            }

        }

        /// <summary>
        /// Updates the burger's location based on mouse. Also fires 
        /// french fries as appropriate
        /// </summary>
        /// <param name="gameTime">game time</param>
        /// <param name="mouse">the current state of the mouse</param>
        public void Update(GameTime gameTime, MouseState mouse)
        {
            // burger should only respond to input if it still has health
            if (health > 0)
            {
                // move burger using mouse
                X = mouse.X;
                Y = mouse.Y;


                // clamp burger in window
                //int greaterBoundsX = GameConstants.WINDOW_WIDTH - drawRectangle.Width;
                //int greaterBoundsY = GameConstants.WINDOW_HEIGHT - drawRectangle.Height;

                //if (drawRectangle.X < 0)
                //{
                //    drawRectangle.X = 0;
                //}
                //else if (drawRectangle.X > greaterBoundsX)
                //{
                //    drawRectangle.X = greaterBoundsX;
                //}

                //if (drawRectangle.Y < 0)
                //{
                //    drawRectangle.Y = 0;
                //}
                //else if (drawRectangle.Y > greaterBoundsY)
                //{
                //    drawRectangle.Y = greaterBoundsY;
                //}


                // update shooting allowed
                // timer concept (for animations) introduced in Chapter 7

                // shoot if appropriate
                if (mouse.LeftButton == ButtonState.Pressed && canShoot)
                {
                    canShoot = false;
                    //Game1.GetProjectileSprite(ProjectileType.FrenchFries);
                    Projectile projectile = new Projectile(ProjectileType.FrenchFries,
                        Game1.GetProjectileSprite(ProjectileType.FrenchFries),
                        drawRectangle.X,
                        drawRectangle.Y + GameConstants.FRENCH_FRIES_PROJECTILE_OFFSET,
                        GameConstants.FRENCH_FRIES_PROJECTILE_SPEED);

                    Game1.AddProjectile(projectile);
                    shootSound.Play();
                }

                if (!canShoot)
                {
                    elapsedCooldownTime += gameTime.ElapsedGameTime.Milliseconds;
                }

                if (elapsedCooldownTime > GameConstants.BURGER_COOLDOWN_MILLISECONDS ||
                    mouse.LeftButton == ButtonState.Released)
                {
                    canShoot = true;
                    elapsedCooldownTime = 0;
                }
            }
            else
            {
                sprite = deadBurger;
            }
        }

        /// <summary>
        /// Draws the burger
        /// </summary>
        /// <param name="spriteBatch">the sprite batch to use</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, drawRectangle, Color.White);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Loads the content for the burger
        /// </summary>
        /// <param name="contentManager">the content manager to use</param>
        /// <param name="spriteName">the name of the sprite for the burger</param>
        /// <param name="x">the x location of the center of the burger</param>
        /// <param name="y">the y location of the center of the burger</param>
        private void LoadContent(ContentManager contentManager, string spriteName,
            int x, int y, string dead)
        {
            // load content and set remainder of draw rectangle
            sprite = contentManager.Load<Texture2D>(spriteName);
            drawRectangle = new Rectangle(x - sprite.Width / 2,
                y - sprite.Height / 2, sprite.Width,
                sprite.Height);

            deadBurger = contentManager.Load<Texture2D>(dead);
        }

        #endregion
    }
}
