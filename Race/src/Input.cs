using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

#if WINDOWS_PHONE
using Microsoft.Xna.Framework.Input.Touch;
#endif

namespace MyGame.src
{
    class Input
    {
        private bool backButtonPressed, lastBackButtonPressed, mouseStartMooving;
        public bool mouse0, click0, backButtonClick;
        private int last0Touch, lastMouseX, lastMouseY;
        public int onScreenMouseX, onScreenMouseY, mouseX, mouseY, dx, dy, totalDx, totalDy;
        public Rectangle mouseRectangle;

#if WINDOWS
        private MouseState mouseState, lastMouseState;

        private KeyboardState keyboardState;
        private KeyboardState lastKeyboardState;
        public bool key_left, key_down, key_right, key_up;
#endif

#if WINDOWS_PHONE
        private int maximumTouchCount;
        TouchPanelCapabilities touchPanelCapabilities;
        TouchCollection touchCollection;
        private bool click1; //TODO precisa ser ajustado para multitoque
        private int last1Touch;
#endif

        public void setup()
        {
            mouseRectangle = new Rectangle(0, 0, 1, 1);

#if WINDOWS_PHONE
            touchPanelCapabilities = TouchPanel.GetCapabilities();
            if (touchPanelCapabilities.IsConnected)
            {
                maximumTouchCount = touchPanelCapabilities.MaximumTouchCount;
            }
#endif
        }

        internal void update()
        {
#if WINDOWS_PHONE
            #region buttons
            lastBackButtonPressed = backButtonPressed;
            backButtonPressed = GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed;
            backButtonClick = !backButtonPressed && lastBackButtonPressed;
            #endregion

            #region touch
            touchCollection = TouchPanel.GetState();

            switch (touchCollection.Count)
            {
                case 0:
                    {
                        click0 = false;

                        totalDx = 0;
                        totalDy = 0;
                        dx = 0;
                        dy = 0;
                        last0Touch = Game1.frameCount;
                        mouseStartMooving = true;

                        last0Touch = Game1.frameCount;
                    }
                    break;
                case 1:
                    {
                        click1 = false;
                        last1Touch = Game1.frameCount;

                        lastMouseX = mouseX;
                        lastMouseY = mouseY;
                        updateMousePosition();

                        mouse0 = ((touchCollection[0].State == TouchLocationState.Pressed) || (touchCollection[0].State == TouchLocationState.Moved));

                        if (mouseStartMooving)
                        {
                            mouseStartMooving = false;
                        }
                        else
                        {
                            dx = mouseX - lastMouseX;
                            dy = mouseY - lastMouseY;
                            totalDx = totalDx + dx;
                            totalDy = totalDy + dy;
                        }

                        if (touchCollection[0].State == TouchLocationState.Released)
                        {
                            click0 = (Game1.frameCount - last0Touch < Config.clickInterval);
                        }
                        else
                        {
                            click0 = (touchCollection[0].State == TouchLocationState.Released);
                        }
                    }
                    break;
                case 2:
                    {
                        if (touchCollection[0].State == TouchLocationState.Released)
                        {
                            click1 = true;
                            updateMousePosition();
                            mouse0 = false;
                        }
                    }
                    break;
                default:
                    break;
            }

            #endregion
#endif

#if WINDOWS
            #region buttons
            lastKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            lastBackButtonPressed = backButtonPressed;
            backButtonPressed = Keyboard.GetState().IsKeyDown(Keys.Escape);
            backButtonClick = !backButtonPressed && lastBackButtonPressed;

            key_left = keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left);
            key_down = keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down);
            key_right = keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right);
            key_up = keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up);

            if ((keyboardState.IsKeyDown(Keys.LeftAlt) || keyboardState.IsKeyDown(Keys.RightAlt)) && (lastKeyboardState.IsKeyDown(Keys.Enter) && keyboardState.IsKeyUp(Keys.Enter)))
            {
                Config.IsFullScreen = !Config.IsFullScreen;
                Game1.display.setup();
                switch (Game1.state)
                {
                    default:
                        break;
                }
            }
            #endregion

            #region mouse
            lastMouseState = mouseState;
            mouseState = Mouse.GetState();

            click0 = ((lastMouseState.LeftButton == ButtonState.Pressed) && (mouseState.LeftButton == ButtonState.Released));

            if (click0)
            {
                click0 = (Game1.frameCount - last0Touch < Config.fps / 2);
                mouse0 = false;
                updateMousePosition();
            }
            else
            {
                mouse0 = mouseState.LeftButton == ButtonState.Pressed;

                if (mouse0)
                {
                    lastMouseX = mouseX;
                    lastMouseY = mouseY;
                    updateMousePosition();

                    if (mouseStartMooving)
                    {
                        mouseStartMooving = false;
                    }
                    else
                    {
                        dx = mouseX - lastMouseX;
                        dy = mouseY - lastMouseY;
                        totalDx = totalDx + dx;
                        totalDy = totalDy + dy;
                    }
                }
                else
                {
                    totalDx = 0;
                    totalDy = 0;
                    dx = 0;
                    dy = 0;
                    last0Touch = Game1.frameCount;
                    mouseStartMooving = true;
                }
            }
            #endregion
#endif
        }

        private void updateMousePosition()
        {
#if WINDOWS_PHONE
            float auxX = touchCollection[0].Position.X;
            float auxY = touchCollection[0].Position.Y;
#endif

#if WINDOWS
            float auxX = mouseState.X;
            float auxY = mouseState.Y;
#endif

            mouseX = (int)(((auxX / Game1.display.scale) - Game1.display.displayWidthOver2 + -Game1.display.translate.X) - Game1.matrix.X + Game1.display.displayWidthOver2);
            mouseY = (int)(-((auxY / Game1.display.scale) - Game1.display.displayHeightOver2 + -Game1.display.translate.Y) - Game1.matrix.Y - Game1.display.displayHeightOver2);

            onScreenMouseX = (int)((auxX / Game1.display.scale) + -Game1.display.translate.X);
            onScreenMouseY = (int)((auxY / Game1.display.scale) + -Game1.display.translate.Y);

            mouseRectangle.X = onScreenMouseX;
            mouseRectangle.Y = onScreenMouseY;
        }
    }
}
