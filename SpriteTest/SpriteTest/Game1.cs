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

namespace SpriteTest
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D t2alucard;
        MobileSprite alucardRunning;
        
        Background background;


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
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.ApplyChanges();
            this.IsMouseVisible = true;
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
            t2alucard = Content.Load<Texture2D>(@"Textures\alucard_colors");
            //crouching = new AnimatedSprite(Content.Load<Texture2D>(@"Textures\alucard_colors"), 19,56,51,46,14);
            //crouching.X = 0;
            //crouching.Y = 0;

            //running = new AnimatedSprite(Content.Load<Texture2D>(@"Textures\alucard_colors"), 32,271,39,46,16);
            //running.X = 0;
            //running.Y = 0;

            alucardRunning = new MobileSprite(t2alucard);
            alucardRunning.Sprite.AddAnimation("leftstop", 243, 160, 32, 64, 1, 0.1f);
            alucardRunning.Sprite.AddAnimation("left", 243, 160, 32, 64, 4, 0.1f);            
            alucardRunning.Sprite.AddAnimation("rightstop", 243, 160, 35, 48, 7, 0.1f);
            alucardRunning.Sprite.AddAnimation("right", 243, 160, 34, 49, 16, 0.1f);
            alucardRunning.Sprite.CurrentAnimation = "rightstop";
            alucardRunning.Position = new Vector2(551, 346);
            //alucardRunning.Position = new Vector2(100, 300);
            alucardRunning.Sprite.AutoRotate = false;
            alucardRunning.IsPathing = false;
            alucardRunning.IsMoving = false;
            
            // TODO: use this.Content to load your game content here
            //background = new Background(Content, @"Textures\background");
            background = new Background(Content, @"Textures\fullbackground-1");
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
            //crouching.Update(gameTime);
            //running.Update(gameTime);
            MouseState ms = Mouse.GetState();
            KeyboardState ks = Keyboard.GetState();

            bool leftKey = ks.IsKeyDown(Keys.Left);
            bool rightKey = ks.IsKeyDown(Keys.Right);

            if (leftKey)
            {
                if (alucardRunning.Sprite.CurrentAnimation != "left")
                {
                    alucardRunning.Sprite.CurrentAnimation = "left";
                }
                alucardRunning.Sprite.MoveBy(-2, 0);
            }
            if (rightKey)
            {
                if (alucardRunning.Sprite.CurrentAnimation != "right")
                {
                    alucardRunning.Sprite.CurrentAnimation = "right";
                }
                alucardRunning.Sprite.MoveBy(2,0);
            }
            if (!leftKey && !rightKey)
            {
                if (alucardRunning.Sprite.CurrentAnimation == "left")
                {
                    alucardRunning.Sprite.CurrentAnimation = "leftstop";
                }
                if (alucardRunning.Sprite.CurrentAnimation == "right")
                {
                    alucardRunning.Sprite.CurrentAnimation = "rightstop";
                }
            }
            alucardRunning.Update(gameTime);
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
            background.Draw(spriteBatch);
            alucardRunning.Draw(spriteBatch);
            //crouching.Draw(spriteBatch, 0, 120, false);
            //running.Draw(spriteBatch, 551, 346, false);            
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
