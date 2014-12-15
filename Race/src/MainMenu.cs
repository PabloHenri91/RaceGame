using Microsoft.Xna.Framework;

namespace MyGame.src
{
    class MainMenu : State
    {
        public enum states { mainMenu, loading, newGame, loadGame, options, help, credits };
        states state;
        states nextState;
        public MainMenu()
            : base()
        {
            state = states.loading;
            nextState = states.mainMenu;

            //Main Menu
            textures2Dlocations.Add("mainMenuBackground");
            textures2Dlocations.Add("buttonPlay");
            textures2Dlocations.Add("buttonPlayPressed");
            textures2Dlocations.Add("buttonOptions");
            textures2Dlocations.Add("buttonOptionsPressed");
            textures2Dlocations.Add("buttonHelp");
            textures2Dlocations.Add("buttonHelpPressed");
            textures2Dlocations.Add("buttonCredits");
            textures2Dlocations.Add("buttonCreditsPressed");

            //New Game
            textures2Dlocations.Add("newGameBackground");
            textures2Dlocations.Add("buttonOk");
            textures2Dlocations.Add("buttonOkPressed");
            textures2Dlocations.Add("buttonCancel");
            textures2Dlocations.Add("buttonCancelPressed");
            textures2Dlocations.Add("buttonLeft");
            textures2Dlocations.Add("buttonLeftPressed");
            textures2Dlocations.Add("buttonRight");
            textures2Dlocations.Add("buttonRightPressed");
        }

        new internal bool load()
        {
            base.load();

            //Main Menu
            textures2D["buttonPlay"].setPosition(425, 319);
            textures2D["buttonPlayPressed"].setPosition(425, 319);
            textures2D["buttonOptions"].setPosition(425, 379);
            textures2D["buttonOptionsPressed"].setPosition(425, 379);
            textures2D["buttonHelp"].setPosition(425, 439);
            textures2D["buttonHelpPressed"].setPosition(425, 439);
            textures2D["buttonCredits"].setPosition(425, 499);
            textures2D["buttonCreditsPressed"].setPosition(425, 499);

            //New Game
            textures2D["buttonOk"].setPosition(459, 405);
            textures2D["buttonOkPressed"].setPosition(459, 405);
            textures2D["buttonCancel"].setPosition(459, 523);
            textures2D["buttonCancelPressed"].setPosition(459, 523);
            textures2D["buttonLeft"].setPosition(619, 405);
            textures2D["buttonLeftPressed"].setPosition(619, 405);
            textures2D["buttonRight"].setPosition(688, 405);
            textures2D["buttonRightPressed"].setPosition(688, 405);

            return true;
        }
        internal void doLogic()
        {
            if (state == nextState)
            {
                switch (state)
                {
                    #region case states.mainMenu:
                    case states.mainMenu:
                        {
                            if (Game1.input.backButtonClick)
                            {
                                Game1.nextState = Game1.states.quit;
                                return;
                            }

                            if (Game1.input.click0)
                            {
                                if (textures2D["buttonPlay"].intersectsWithMouseClick())
                                {
                                    Game1.memoryCard = new MemoryCard();
                                    if (Game1.memoryCard.loadGame())
                                    {
                                        Game1.nextState = Game1.states.hangar;
                                    }
                                    else
                                    {
                                        nextState = states.newGame;
                                    }
                                    return;
                                }
                                if (textures2D["buttonOptions"].intersectsWithMouseClick())
                                {
                                    nextState = states.options;
                                    return;
                                }
                                if (textures2D["buttonHelp"].intersectsWithMouseClick())
                                {
                                    nextState = states.help;
                                    return;
                                }
                                if (textures2D["buttonCredits"].intersectsWithMouseClick())
                                {
                                    nextState = states.credits;
                                    return;
                                }
                            }
                        }
                        break;
                    #endregion
                    #region case states.newGame:
                    case states.newGame: 
                        {
                            if (Game1.input.click0)
                            {
                                nextState = states.mainMenu;
                            }
                        }
                        break;
                    #endregion
                    #region case states.loadGame:
                    case states.loadGame: 
                        {
                            if (Game1.input.click0)
                            {
                                nextState = states.mainMenu;
                            }
                        }
                        break;
                    #endregion
                    #region case states.options:
                    case states.options: 
                        {
                            if (Game1.input.click0)
                            {
                                nextState = states.mainMenu;
                            }
                        }
                        break;
                    #endregion
                    #region case states.help:
                    case states.help:
                        {
                            if (Game1.input.click0)
                            {
                                nextState = states.mainMenu;
                            }
                        }
                        break;
                    #endregion
                    #region case states.credits:
                    case states.credits:
                        {
                            if (Game1.input.click0)
                            {
                                nextState = states.mainMenu;
                            }
                        }
                        break;
                    #endregion
                }
            }
            else
            {
                //Reload nextState
                positions.Clear();

                switch (nextState)
                {
                    #region case states.mainMenu:
                    case states.mainMenu:
                        {
                        }
                        break;
                    #endregion
                    #region case states.newGame:
                    case states.newGame:
                        {
                        }
                        break;
                    #endregion
                    #region case states.loadGame:
                    case states.loadGame:
                        {
                        }
                        break;
                    #endregion
                    #region case states.options:
                    case states.options:
                        {
                        }
                        break;
                    #endregion
                    #region case states.help:
                    case states.help:
                        {
                        }
                        break;
                    #endregion
                    #region case states.credits:
                    case states.credits:
                        {
                        }
                        break;
                    #endregion
                }
                state = nextState;
                Game1.needToDraw = true;
            }
        }

        internal void draw()
        {
            switch (state)
            {
                #region case states.mainMenu:
                case states.mainMenu:
                    {
                        Game1.graphicsDeviceManager.GraphicsDevice.Clear(Color.Black);
                        textures2D["mainMenuBackground"].drawOnScreen();

                        //Desenho dos botões
                        if (Game1.input.mouse0)
                        {
                            textures2D["buttonPlay" + (textures2D["buttonPlay"].intersectsWithMouseClick() ? "Pressed" : "")].drawOnScreen();
                            textures2D["buttonOptions" + (textures2D["buttonOptions"].intersectsWithMouseClick() ? "Pressed" : "")].drawOnScreen();
                            textures2D["buttonHelp" + (textures2D["buttonHelp"].intersectsWithMouseClick() ? "Pressed" : "")].drawOnScreen();
                            textures2D["buttonCredits" + (textures2D["buttonCredits"].intersectsWithMouseClick() ? "Pressed" : "")].drawOnScreen();
                            
                        }
                        else
                        {
                            //Desenhar todos os botões
                            textures2D["buttonPlay"].drawOnScreen();
                            textures2D["buttonOptions"].drawOnScreen();
                            textures2D["buttonHelp"].drawOnScreen();
                            textures2D["buttonCredits"].drawOnScreen();
                        }
                    }
                    break;
                #endregion
                #region case states.newGame:
                case states.newGame:
                    {
                        Game1.graphicsDeviceManager.GraphicsDevice.Clear(Color.Black);
                        textures2D["newGameBackground"].drawOnScreen();

                        //Desenho dos botões
                        if (Game1.input.mouse0)
                        {
                            textures2D["buttonOk" + (textures2D["buttonOk"].intersectsWithMouseClick() ? "Pressed" : "")].drawOnScreen();
                            textures2D["buttonCancel" + (textures2D["buttonCancel"].intersectsWithMouseClick() ? "Pressed" : "")].drawOnScreen();
                            textures2D["buttonLeft" + (textures2D["buttonLeft"].intersectsWithMouseClick() ? "Pressed" : "")].drawOnScreen();
                            textures2D["buttonRight" + (textures2D["buttonRight"].intersectsWithMouseClick() ? "Pressed" : "")].drawOnScreen();

                        }
                        else
                        {
                            //Desenhar todos os botões
                            textures2D["buttonOk"].drawOnScreen();
                            textures2D["buttonCancel"].drawOnScreen();
                            textures2D["buttonLeft"].drawOnScreen();
                            textures2D["buttonRight"].drawOnScreen();
                        }
                    }
                    break;
                #endregion
                #region case states.loadGame:
                case states.loadGame:
                    {
                        Game1.graphicsDeviceManager.GraphicsDevice.Clear(Color.Black);
                    }
                    break;
                #endregion
                #region case states.options:
                case states.options:
                    {
                        Game1.graphicsDeviceManager.GraphicsDevice.Clear(Color.Black);
                    }
                    break;
                #endregion
                #region case states.help:
                case states.help:
                    {
                        Game1.graphicsDeviceManager.GraphicsDevice.Clear(Color.Black);
                    }
                    break;
                #endregion
                #region case states.credits:
                case states.credits:
                    {
                        Game1.graphicsDeviceManager.GraphicsDevice.Clear(Color.Black);
                    }
                    break;
                #endregion
            }
        }
    }
}
