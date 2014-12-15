using Microsoft.Xna.Framework;
using System;

namespace MyGame.src
{
    class Display
    {
        internal int width;
        internal int height;
        internal int displayWidthOver2;
        internal int displayHeightOver2;
        internal float scale;
        internal Vector2 translate;
        internal Matrix camera;
        private GameWindow Window;

        public Display(GameWindow Window)
        {
            this.Window = Window;
            this.setup();
        }

        internal void setup()
        {
            var graphicsDeviceManager = Game1.graphicsDeviceManager;

            //Resolução virtual
            width = Config.displayWidth;
            height = Config.displayHeight;
            displayWidthOver2 = width / 2;
            displayHeightOver2 = height / 2;

#if WINDOWS_PHONE || ANDROID


            scale = Math.Min((graphicsDeviceManager.GraphicsDevice.PresentationParameters.BackBufferWidth  / (float)width),
                             (graphicsDeviceManager.GraphicsDevice.PresentationParameters.BackBufferHeight / (float)height));

            translate = new Vector2((graphicsDeviceManager.GraphicsDevice.PresentationParameters.BackBufferWidth - (width * scale)) / 2f,
                                    (graphicsDeviceManager.GraphicsDevice.PresentationParameters.BackBufferHeight - (height * scale)) / 2f);

            camera = Matrix.CreateRotationZ(MathHelper.ToRadians(0f)) *
                     Matrix.CreateScale(scale, scale, 1f) *
                     Matrix.CreateTranslation(new Vector3(translate.X, translate.Y, 0f));
#endif

#if WINDOWS
            if (Config.IsFullScreen)
            {
                graphicsDeviceManager.PreferredBackBufferWidth = graphicsDeviceManager.GraphicsDevice.DisplayMode.Width;
                graphicsDeviceManager.PreferredBackBufferHeight = graphicsDeviceManager.GraphicsDevice.DisplayMode.Height;

                OpenTK.GameWindow window = (OpenTK.GameWindow)typeof(OpenTKGameWindow).GetField("window", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(Window);
                window.X = 0;
                window.Y = 0;

                Window.IsBorderless = true;

                graphicsDeviceManager.ApplyChanges();

                scale = Math.Min((graphicsDeviceManager.GraphicsDevice.PresentationParameters.BackBufferWidth / (float)width),
                             (graphicsDeviceManager.GraphicsDevice.PresentationParameters.BackBufferHeight / (float)height));

                translate = new Vector2((graphicsDeviceManager.GraphicsDevice.PresentationParameters.BackBufferWidth - (width * scale)) / 2f,
                                        (graphicsDeviceManager.GraphicsDevice.PresentationParameters.BackBufferHeight - (height * scale)) / 2f);

                camera = new Matrix();
                camera = Matrix.CreateRotationZ(MathHelper.ToRadians(0f)) *
                         Matrix.CreateScale(scale, scale, 1f) *
                         Matrix.CreateTranslation(new Vector3(translate.X, translate.Y, 0f));

            }
            else
            {
                graphicsDeviceManager.PreferredBackBufferWidth = Config.displayWidth;
                graphicsDeviceManager.PreferredBackBufferHeight = Config.displayHeight;

                OpenTK.GameWindow window = (OpenTK.GameWindow)typeof(OpenTKGameWindow).GetField("window", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(Window);
                window.X = graphicsDeviceManager.GraphicsDevice.DisplayMode.Width / 2 - displayWidthOver2;
                window.Y = 0;// graphicsDeviceManager.GraphicsDevice.DisplayMode.Height / 2 - displayHeightOver2;

                Window.IsBorderless = false;

                graphicsDeviceManager.ApplyChanges();

                scale = Math.Min((graphicsDeviceManager.GraphicsDevice.PresentationParameters.BackBufferWidth / (float)width),
                             (graphicsDeviceManager.GraphicsDevice.PresentationParameters.BackBufferHeight / (float)height));

                translate = new Vector2((graphicsDeviceManager.GraphicsDevice.PresentationParameters.BackBufferWidth - (width * scale)) / 2f,
                                        (graphicsDeviceManager.GraphicsDevice.PresentationParameters.BackBufferHeight - (height * scale)) / 2f);

                camera = new Matrix();
                camera = Matrix.CreateRotationZ(MathHelper.ToRadians(0f)) *
                         Matrix.CreateScale(scale, scale, 1f) *
                         Matrix.CreateTranslation(new Vector3(translate.X, translate.Y, 0f));
#endif
            }
        }
    }
}

