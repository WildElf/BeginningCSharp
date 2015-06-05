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

namespace Lab11
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public const int WINDOW_WIDTH = 1000;
        public const int WINDOW_HEIGHT = 750;
        
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        TeddyBear teddy;
        Explosion boom;
        Texture2D teddy0;
        Texture2D teddy1;

        Random rand = new Random();

        float randX;
        float randY;
        Vector2 randVector;

        KeyboardState oldKeyboard;


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

            // random numbers for teddy's velocity
            randX = rand.Next(-2, 3);
            randY = rand.Next(-2, 3);
            randVector = new Vector2(randX, randY);

            // create a teddy and an explosion
            teddy = new TeddyBear(Content, WINDOW_WIDTH, WINDOW_HEIGHT, "teddybear0", 400, 300, randVector);

            boom = new Explosion(Content);

            // load backup teddys
            teddy0 = Content.Load<Texture2D>("teddybear0");
            teddy1 = Content.Load<Texture2D>("teddybear1");
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

            // is the mouse over teddy and the left button clicked?
            MouseState mouse = Mouse.GetState();
            bool mouseInBoxX;
            bool mouseInBoxY;

            if (mouse.X >= teddy.DrawRectangle.X && mouse.X <= teddy.DrawRectangle.X + teddy.DrawRectangle.Width)
            {
                mouseInBoxX = true;
            }
            else
            {
                mouseInBoxX = false;
            }

            if (mouse.Y >= teddy.DrawRectangle.Y && mouse.Y <= teddy.DrawRectangle.Y + teddy.DrawRectangle.Height)
            {
                mouseInBoxY = true;
            }
            else
            {
                mouseInBoxY = false;
            }

            if (mouseInBoxX && mouseInBoxY)
            {
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    teddy.Active = false;
                    boom.Play(teddy.DrawRectangle.X + teddy.DrawRectangle.Width / 2,
                        teddy.DrawRectangle.Y + teddy.DrawRectangle.Height / 2);
                }
            }

            // create a new teddy if A key is pressed on keyboard
            KeyboardState keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.A) && !oldKeyboard.IsKeyDown(Keys.A))
            {
                // new random velocity vector
                
                randX = rand.Next(-2, 3);
                randY = rand.Next(-2, 3);
                randVector = new Vector2(randX, randY);

                // random position and teddy
                int posX = rand.Next(100, WINDOW_WIDTH - 99);
                int posY = rand.Next(100, WINDOW_HEIGHT - 99);

                int spritePick = rand.Next(0, 2);

                // meet your new teddy

                if (spritePick == 0)
                {
                         teddy = new TeddyBear(Content, WINDOW_WIDTH, WINDOW_HEIGHT, "teddybear0", posX, posY, randVector);
                }
                else
                {
                        teddy = new TeddyBear(Content, WINDOW_WIDTH, WINDOW_HEIGHT, "teddybear1", posX, posY, randVector);
                }

                // simply make a non-random teddy
                //teddy = new TeddyBear(Content, WINDOW_WIDTH, WINDOW_HEIGHT, "teddybear1", posX, posY, randVector);
            }


            // B key blows up the bear
            if (keyboard.IsKeyDown(Keys.B) && !oldKeyboard.IsKeyDown(Keys.B))
            {
                // blow up teddy

                teddy.Active = false;
                boom.Play(teddy.DrawRectangle.X + teddy.DrawRectangle.Width / 2,
                    teddy.DrawRectangle.Y + teddy.DrawRectangle.Height / 2);

                // new random velocity vector

                randX = rand.Next(-2, 3);
                randY = rand.Next(-2, 3);
                randVector = new Vector2(randX, randY);

                // random position and teddy
                int posX = rand.Next(100, WINDOW_WIDTH - 99);
                int posY = rand.Next(100, WINDOW_HEIGHT - 99);

                int spritePick = rand.Next(0, 2);

                // the new teddy
                if (spritePick == 0)
                {
                    teddy = new TeddyBear(Content, WINDOW_WIDTH, WINDOW_HEIGHT, "teddybear0", posX, posY, randVector);
                }
                else
                {
                    teddy = new TeddyBear(Content, WINDOW_WIDTH, WINDOW_HEIGHT, "teddybear1", posX, posY, randVector);
                }


            }


            // TODO: Add your update logic here
            teddy.Update();
            boom.Update(gameTime);

            // save keyboard state
            oldKeyboard = keyboard;

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

            teddy.Draw(spriteBatch);
            boom.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
