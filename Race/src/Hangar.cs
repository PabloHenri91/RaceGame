using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGame.src
{
    class Hangar : State
    {
        public enum states { hangar, loading, supplyRoom, chooseSector, chooseMisson, saveGame };
        states state;
        states nextState;

        public Hangar()
            : base()
        {
            state = states.loading;
            nextState = states.hangar;

            //Hangar
            textures2Dlocations.Add("hangarBackground");
            textures2Dlocations.Add("buttonRace");
            textures2Dlocations.Add("buttonRacePressed");
            textures2Dlocations.Add("buttonSupplyRoom");
            textures2Dlocations.Add("buttonSupplyRoomPressed");
            textures2Dlocations.Add("buttonLevelUp");
            textures2Dlocations.Add("buttonLevelUpPressed");
            textures2Dlocations.Add("buttonPlus");
            textures2Dlocations.Add("buttonPlusPressed");
            textures2Dlocations.Add("buttonMinus");
            textures2Dlocations.Add("buttonMinusPressed");

            //Choose Sector
            textures2Dlocations.Add("background");
            textures2Dlocations.Add("sector");
            textures2Dlocations.Add("sectorLocked");
            textures2Dlocations.Add("sectorPressed");

            //Choose Mission
            //textures2Dlocations.Add("background");
            textures2Dlocations.Add("mission");
            textures2Dlocations.Add("missionLocked");
            textures2Dlocations.Add("missionPressed");

        }
        new internal bool load()
        {
            base.load();

            //Hangar
            textures2D["buttonRace"].setPosition(120, 83);
            textures2D["buttonRacePressed"].setPosition(120, 83);
            textures2D["buttonSupplyRoom"].setPosition(120, 143);
            textures2D["buttonSupplyRoomPressed"].setPosition(120, 143);
            textures2D["buttonLevelUp"].setPosition(342, 83);
            textures2D["buttonLevelUpPressed"].setPosition(342, 83);

            positions.Add("buttonMinusSpeed", new Vector2(757, 84));
            positions.Add("buttonPlusSpeed", new Vector2(827, 84));
            positions.Add("buttonMinusAcceleration", new Vector2(757, 144));
            positions.Add("buttonPlusAcceleration", new Vector2(827, 144));
            positions.Add("buttonMinusAgility", new Vector2(757, 204));
            positions.Add("buttonPlusAgility", new Vector2(827, 204));
            positions.Add("buttonMinusArmor", new Vector2(757, 264));
            positions.Add("buttonPlusArmor", new Vector2(827, 264));
            positions.Add("buttonMinusShieldPower", new Vector2(757, 324));
            positions.Add("buttonPlusShieldPower", new Vector2(827, 324));
            positions.Add("buttonMinusShieldRecharge", new Vector2(757, 384));
            positions.Add("buttonPlusShieldRecharge", new Vector2(827, 384));
            positions.Add("buttonMinusSuspension", new Vector2(757, 444));
            positions.Add("buttonPlusSuspension", new Vector2(827, 444));
            positions.Add("buttonMinusTires", new Vector2(757, 504));
            positions.Add("buttonPlusTires", new Vector2(827, 504));
            positions.Add("buttonMinusDownforce", new Vector2(757, 564));
            positions.Add("buttonPlusDownforce", new Vector2(827, 564));

            //Choose Sector
            for (int i = 0; i < 10; i++)
            {
                positions.Add("sector" + i, new Vector2(300 + (500 * i) + ((Game1.memoryCard.sectorIndex) * -500), 150));
            }

            //Choose Mission
            for (int i = 0; i < 10; i++)
            {
                if (i < 5)
                {
                    positions.Add("mission" + i, new Vector2(100 + (163 * i), 100));
                }
                else
                {
                    positions.Add("mission" + i, new Vector2(100 + (163 * (i - 5)), 275));
                }
            }

            return true;
        }

        internal void doLogic()
        {
            if (state == nextState)
            {
                switch (state)
                {
                    #region case states.hangar:
                    case states.hangar:
                        {
                            if (Game1.input.backButtonClick)
                            {
                                Game1.nextState = Game1.states.mainMenu;
                                return;
                            }

                            if (Game1.input.click0)
                            {
                                if (textures2D["buttonRace"].intersectsWithMouseClick())
                                {
                                    nextState = states.chooseSector;
                                    return;
                                }
                                if (textures2D["buttonSupplyRoom"].intersectsWithMouseClick())
                                {
                                    nextState = states.supplyRoom;
                                    return;
                                }
                                if (textures2D["buttonLevelUp"].intersectsWithMouseClick())
                                {
                                    return;
                                }
                            }
                        }
                        break;
                    #endregion
                    #region case states.supplyRoom:
                    case states.supplyRoom:
                        {
                            if (Game1.input.backButtonClick)
                            {
                                nextState = states.hangar;
                                return;
                            }
                        }
                        break;
                    #endregion
                    #region case states.chooseSector:
                    case states.chooseSector:
                        {
                            if (Game1.input.backButtonClick)
                            {
                                nextState = states.hangar;
                                return;
                            }
                        }
                        break;
                    #endregion
                    #region case states.chooseMisson:
                    case states.chooseMisson:
                        {
                            if (Game1.input.backButtonClick)
                            {
                                nextState = states.chooseSector;
                                return;
                            }
                        }
                        break;
                    #endregion
                    #region case states.saveGame:
                    case states.saveGame:
                        {
                            if (Game1.input.backButtonClick)
                            {
                                nextState = states.hangar;
                                return;
                            }
                        }
                        break;
                    #endregion
                }
            }
            else
            {
                //Reload nextState
                switch (nextState)
                {
                    #region case states.hangar:
                    case states.hangar:
                        {
                        }
                        break;
                    #endregion
                    #region case states.supplyRoom:
                    case states.supplyRoom:
                        {
                        }
                        break;
                    #endregion
                    #region case states.chooseSector:
                    case states.chooseSector:
                        {
                        }
                        break;
                    #endregion
                    #region case states.chooseMisson:
                    case states.chooseMisson:
                        {
                        }
                        break;
                    #endregion
                    #region case states.saveGame:
                    case states.saveGame:
                        {
                        }
                        break;
                    #endregion
                }
                state = nextState;
            }
        }

        internal void draw()
        {
            switch (state)
            {
                #region case states.hangar:
                case states.hangar:
                    {
                        Game1.graphicsDeviceManager.GraphicsDevice.Clear(Color.Black);
                        textures2D["hangarBackground"].drawOnScreen();

                        //Desenho dos botões
                        if (Game1.input.mouse0)
                        {
                            textures2D["buttonRace" + (textures2D["buttonRace"].intersectsWithMouseClick() ? "Pressed" : "")].drawOnScreen();
                            textures2D["buttonSupplyRoom" + (textures2D["buttonSupplyRoom"].intersectsWithMouseClick() ? "Pressed" : "")].drawOnScreen();
                            textures2D["buttonLevelUp" + (textures2D["buttonLevelUp"].intersectsWithMouseClick() ? "Pressed" : "")].drawOnScreen();

                            textures2D["buttonMinus" + (textures2D["buttonMinus"].intersectsWithMouseClick(positions["buttonMinusSpeed"]) ? "Pressed" : "")].drawOnScreen(positions["buttonMinusSpeed"]);
                            textures2D["buttonPlus" + (textures2D["buttonPlus"].intersectsWithMouseClick(positions["buttonPlusSpeed"]) ? "Pressed" : "")].drawOnScreen(positions["buttonPlusSpeed"]);
                            textures2D["buttonMinus" + (textures2D["buttonMinus"].intersectsWithMouseClick(positions["buttonMinusAcceleration"]) ? "Pressed" : "")].drawOnScreen(positions["buttonMinusAcceleration"]);
                            textures2D["buttonPlus" + (textures2D["buttonPlus"].intersectsWithMouseClick(positions["buttonPlusAcceleration"]) ? "Pressed" : "")].drawOnScreen(positions["buttonPlusAcceleration"]);
                            textures2D["buttonMinus" + (textures2D["buttonMinus"].intersectsWithMouseClick(positions["buttonMinusAgility"]) ? "Pressed" : "")].drawOnScreen(positions["buttonMinusAgility"]);
                            textures2D["buttonPlus" + (textures2D["buttonPlus"].intersectsWithMouseClick(positions["buttonPlusAgility"]) ? "Pressed" : "")].drawOnScreen(positions["buttonPlusAgility"]);
                            textures2D["buttonMinus" + (textures2D["buttonMinus"].intersectsWithMouseClick(positions["buttonMinusArmor"]) ? "Pressed" : "")].drawOnScreen(positions["buttonMinusArmor"]);
                            textures2D["buttonPlus" + (textures2D["buttonPlus"].intersectsWithMouseClick(positions["buttonPlusArmor"]) ? "Pressed" : "")].drawOnScreen(positions["buttonPlusArmor"]);
                            textures2D["buttonMinus" + (textures2D["buttonMinus"].intersectsWithMouseClick(positions["buttonMinusShieldPower"]) ? "Pressed" : "")].drawOnScreen(positions["buttonMinusShieldPower"]);
                            textures2D["buttonPlus" + (textures2D["buttonPlus"].intersectsWithMouseClick(positions["buttonPlusShieldPower"]) ? "Pressed" : "")].drawOnScreen(positions["buttonPlusShieldPower"]);
                            textures2D["buttonMinus" + (textures2D["buttonMinus"].intersectsWithMouseClick(positions["buttonMinusShieldRecharge"]) ? "Pressed" : "")].drawOnScreen(positions["buttonMinusShieldRecharge"]);
                            textures2D["buttonPlus" + (textures2D["buttonPlus"].intersectsWithMouseClick(positions["buttonPlusShieldRecharge"]) ? "Pressed" : "")].drawOnScreen(positions["buttonPlusShieldRecharge"]);
                            textures2D["buttonMinus" + (textures2D["buttonMinus"].intersectsWithMouseClick(positions["buttonMinusSuspension"]) ? "Pressed" : "")].drawOnScreen(positions["buttonMinusSuspension"]);
                            textures2D["buttonPlus" + (textures2D["buttonPlus"].intersectsWithMouseClick(positions["buttonPlusSuspension"]) ? "Pressed" : "")].drawOnScreen(positions["buttonPlusSuspension"]);
                            textures2D["buttonMinus" + (textures2D["buttonMinus"].intersectsWithMouseClick(positions["buttonMinusTires"]) ? "Pressed" : "")].drawOnScreen(positions["buttonMinusTires"]);
                            textures2D["buttonPlus" + (textures2D["buttonPlus"].intersectsWithMouseClick(positions["buttonPlusTires"]) ? "Pressed" : "")].drawOnScreen(positions["buttonPlusTires"]);
                            textures2D["buttonMinus" + (textures2D["buttonMinus"].intersectsWithMouseClick(positions["buttonMinusDownforce"]) ? "Pressed" : "")].drawOnScreen(positions["buttonMinusDownforce"]);
                            textures2D["buttonPlus" + (textures2D["buttonPlus"].intersectsWithMouseClick(positions["buttonPlusDownforce"]) ? "Pressed" : "")].drawOnScreen(positions["buttonPlusDownforce"]);
                        }
                        else
                        {
                            //Desenhar todos os botões
                            textures2D["buttonRace"].drawOnScreen();
                            textures2D["buttonSupplyRoom"].drawOnScreen();
                            textures2D["buttonLevelUp"].drawOnScreen();

                            textures2D["buttonMinus"].drawOnScreen(positions["buttonMinusSpeed"]);
                            textures2D["buttonPlus"].drawOnScreen(positions["buttonPlusSpeed"]);
                            textures2D["buttonMinus"].drawOnScreen(positions["buttonMinusAcceleration"]);
                            textures2D["buttonPlus"].drawOnScreen(positions["buttonPlusAcceleration"]);
                            textures2D["buttonMinus"].drawOnScreen(positions["buttonMinusAgility"]);
                            textures2D["buttonPlus"].drawOnScreen(positions["buttonPlusAgility"]);
                            textures2D["buttonMinus"].drawOnScreen(positions["buttonMinusArmor"]);
                            textures2D["buttonPlus"].drawOnScreen(positions["buttonPlusArmor"]);
                            textures2D["buttonMinus"].drawOnScreen(positions["buttonMinusShieldPower"]);
                            textures2D["buttonPlus"].drawOnScreen(positions["buttonPlusShieldPower"]);
                            textures2D["buttonMinus"].drawOnScreen(positions["buttonMinusShieldRecharge"]);
                            textures2D["buttonPlus"].drawOnScreen(positions["buttonPlusShieldRecharge"]);
                            textures2D["buttonMinus"].drawOnScreen(positions["buttonMinusSuspension"]);
                            textures2D["buttonPlus"].drawOnScreen(positions["buttonPlusSuspension"]);
                            textures2D["buttonMinus"].drawOnScreen(positions["buttonMinusTires"]);
                            textures2D["buttonPlus"].drawOnScreen(positions["buttonPlusTires"]);
                            textures2D["buttonMinus"].drawOnScreen(positions["buttonMinusDownforce"]);
                            textures2D["buttonPlus"].drawOnScreen(positions["buttonPlusDownforce"]);
                        }
                    }
                    break;
                #endregion
                #region case states.supplyRoom:
                case states.supplyRoom:
                    {
                    }
                    break;
                #endregion
                #region case states.chooseSector:
                case states.chooseSector:
                    {
                        Game1.graphicsDeviceManager.GraphicsDevice.Clear(Color.Black);
                        textures2D["background"].drawOnScreen();
                        for (int i = 0; i < 10; i++)
                        {
                            textures2D["sector"].drawOnScreen(positions["sector" + i]);
                        }
                        
                    }
                    break;
                #endregion
                #region case states.chooseMisson:
                case states.chooseMisson:
                    {
                        Game1.graphicsDeviceManager.GraphicsDevice.Clear(Color.Black);
                        textures2D["background"].drawOnScreen();
                        for (int i = 0; i < 10; i++)
                        {
                            textures2D["mission"].drawOnScreen(positions["mission" + i]);
                        }
                    }
                    break;
                #endregion
                #region case states.saveGame:
                case states.saveGame:
                    {
                    }
                    break;
                #endregion
            }
        }
    }
}
