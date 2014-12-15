using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MyGame.src
{
    public class Game1 : Game
    {
        //Display
        internal static Display display;
        internal static GraphicsDeviceManager graphicsDeviceManager;
        internal static SpriteBatch spriteBatch;
        public static bool needToDraw;
        internal static Vector2 matrix;

        //Input
        internal static Input input;

        //Content
        internal static ContentManager myContentManager;
        private bool contentIsEmpty;

        //Estados
        internal enum states { loading, mainMenu, race, quit, hangar };
        internal static states state;
        internal static states nextState;
        private MainMenu mainMenu;

        //Load
        private LoadScreen loadScreen;
        private bool loadScreenLoaded;

        //FPS
        private static int fps = Config.fps;
        internal static float frameDurationSeconds = 1f / (float)fps;
        public static int frameCount;

        //Fonts
        internal static SpriteFont verdana20;
        internal static SpriteFont verdana12;

        //Aux
        internal static Texture2D voidTexture;
        internal static MemoryCard memoryCard;

        //Sleep
#if XNA
        private int lastMilliseconds;
        private int elapsedMilliseconds;
        private int oversleep;
        private int last;
        private int elapsed;
#endif

        //Debug
#if DEBUG
        private int updateCount;
        private int drawCount;
        private float timeSinceLastDraw;
        private int dps;
        private float timeSinceLastUpdate;
        private int ups;
#endif

        public Game1()
            : base()
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            IsMouseVisible = true;

            //FPS
            IsFixedTimeStep = false;
            graphicsDeviceManager.SynchronizeWithVerticalRetrace = false;
            TargetElapsedTime = TimeSpan.FromTicks(10000000 / fps);

            //Extend battery life under lock.
            InactiveSleepTime = TimeSpan.FromMilliseconds(1000);
        }

        protected override void Initialize()
        {
            display = new Display(Window);
            input = new Input();

            //Definindo Estados
            state = states.loading;
            nextState = states.mainMenu;

            //Content
            myContentManager = new ContentManager(Services, "Content");

            //Translate
            matrix = Vector2.Zero;

            needToDraw = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            verdana20 = Content.Load<SpriteFont>("verdana20");
            verdana12 = Content.Load<SpriteFont>("verdana12");
            voidTexture = Content.Load<Texture2D>("void");
        }

        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime)
        {
#if XNA
            //Auxiliar da função Sleep
            lastMilliseconds = Environment.TickCount;
#endif
            input.update();

            if (state == nextState)
            {
                switch (state)
                {
                    case states.mainMenu:
                        mainMenu.doLogic();
                        break;
                }
            }
            else if (loadScreenLoaded)
            {
                needToDraw = true;
                unloadContent(state);
                switch (nextState)
                {
                    case states.mainMenu:
                        if (mainMenu == null)
                            mainMenu = new MainMenu();
                        if (mainMenu.load())
                            changeState();
                        break;
                }
            }
            else
            {
                unloadContent(state);
                if (loadScreen == null)
                    loadScreen = new LoadScreen();
                loadScreenLoaded = loadScreen.load(Content);
            }

            if (nextState == states.quit)
                this.Exit();

#if DEBUG
            countFPSUpdate(gameTime);
#endif
            frameCount++;

            if (state == nextState)
            {
                if (!needToDraw)
                {
#if !DEBUG
#if XNA
                    Sleep();
#endif
                    this.SuppressDraw();
#else
                    drawCount--;
#endif
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (needToDraw) { needToDraw = false; }
#if DEBUG
            GraphicsDevice.Clear(Color.CornflowerBlue);
#endif
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, display.camera);
            if (state == nextState)
            {
                switch (state)
                {
                    case states.mainMenu:
                        mainMenu.draw();
                        break;
                }
            }
#if DEBUG
            spriteBatch.DrawString(verdana12, "FPS: " + ups, new Vector2(11f, 11f), Color.Black);
            spriteBatch.DrawString(verdana12, "FPS: " + ups, new Vector2(10f, 10f), Color.White);
            spriteBatch.DrawString(verdana12, "DPS: " + dps, new Vector2(11f, 26f), Color.Black);
            spriteBatch.DrawString(verdana12, "DPS: " + dps, new Vector2(10f, 25f), Color.White);
#endif
            spriteBatch.End();

#if DEBUG
            countFPSDraw(gameTime);
#endif
            base.Draw(gameTime);
#if XNA
            if (state == nextState)
            {
                Sleep();
            }
#endif
        }

#if XNA
        private void Sleep()
        {
            //Resumo. Era pra funcionar assim mas precisou de vários ajustes
            //elapsedMilliseconds = Environment.TickCount - lastMilliseconds;
            //if (elapsedMilliseconds < frameDuration)
            //{
            //    System.Threading.Thread.Sleep(frameDuration - elapsedMilliseconds);
            //}

            elapsedMilliseconds = Environment.TickCount - lastMilliseconds;
            if (elapsedMilliseconds < TargetElapsedTime.TotalMilliseconds)//Need to sleep?
            {
                if (elapsedMilliseconds + oversleep < TargetElapsedTime.TotalMilliseconds)
                {
                    last = Environment.TickCount;
                    System.Threading.Thread.Sleep((int)TargetElapsedTime.TotalMilliseconds - (elapsedMilliseconds + oversleep));//Sleep
                    elapsed = Environment.TickCount - last;

                    if (elapsedMilliseconds + elapsed > TargetElapsedTime.TotalMilliseconds)
                    {
                        oversleep = (((elapsedMilliseconds + elapsed - (int)TargetElapsedTime.TotalMilliseconds) + oversleep) / 2);
                    }
                }
                else if (oversleep > 0)
                {
                    oversleep--;
                }
            }
        }
#endif

        private void changeState()
        {
            IsFixedTimeStep = true;
            graphicsDeviceManager.SynchronizeWithVerticalRetrace = true;

            state = nextState;
            Game1.needToDraw = true;
            contentIsEmpty = false;
            GC.Collect();
        }

        private void unloadContent(states state)
        {
            if (contentIsEmpty == false)
            {
                IsFixedTimeStep = false;
                graphicsDeviceManager.SynchronizeWithVerticalRetrace = false;

                myContentManager.Unload();
                contentIsEmpty = true;

                switch (state)
                {
                    case states.mainMenu:
                        mainMenu = null;
                        break;
                }
            }
        }

#if DEBUG
        private void countFPSUpdate(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timeSinceLastDraw += elapsed;

            if (timeSinceLastDraw > 1)
            {
                dps = drawCount;
                drawCount = 0;
                timeSinceLastDraw -= 1;
            }
            updateCount++;
        }

        private void countFPSDraw(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timeSinceLastUpdate += elapsed;

            if (timeSinceLastUpdate > 1)
            {
                ups = updateCount;
                updateCount = 0;
                timeSinceLastUpdate -= 1;
            }
            drawCount++;
        }
#endif
    }
}
