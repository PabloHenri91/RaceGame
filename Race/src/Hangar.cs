using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGame.src
{
    class Hangar : State
    {
        public enum states { hangar, loading, supplyRoom, chooseSector, chooseRace, saveGame };
        states state;
        states nextState;

        //Hangar
        private int carIndex;
        private List<int> levelReqPoints;
        private bool drawPlusButtons;
        private int bonus;
        private int health;
        private int energyShield;

        //Upgrade(Weight) Points
        private int maximumWeight;
        private int currentWeight;
        private int availableWeight;
        
        //Choose Sector
        private List<string> sectors;

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
            for (int i = 0;i < 6;i++)
            {
                textures2Dlocations.Add("nave" + i);
            }
            textures2Dlocations.Add("wheel");

            //Choose Sector
            textures2Dlocations.Add("background");
            textures2Dlocations.Add("sector");
            textures2Dlocations.Add("sectorLocked");
            textures2Dlocations.Add("sectorPressed");

            //Choose Race
            //textures2Dlocations.Add("background");
            textures2Dlocations.Add("race");
            textures2Dlocations.Add("raceLocked");
            textures2Dlocations.Add("racePressed");

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

            positions.Add("wheel1", new Vector2(400, 539));
            positions.Add("wheel2", new Vector2(449, 539));

            positions.Add("speed", new Vector2(568, 109));
            positions.Add("acceleration", new Vector2(568, 169));
            positions.Add("agility", new Vector2(568, 229));
            positions.Add("armor", new Vector2(568, 288));
            positions.Add("shieldPower", new Vector2(568, 348));
            positions.Add("shieldRecharge", new Vector2(568, 408));
            positions.Add("suspension", new Vector2(568, 468));
            positions.Add("tires", new Vector2(568, 527));
            positions.Add("downforce", new Vector2(568, 587));

            positions.Add("score", new Vector2(155, 25));
            positions.Add("level", new Vector2(382, 214));

            //TODO alinhar ao centro, calculado em tempo de execução
            positions.Add("maximumWeight", new Vector2(131, 279));
            positions.Add("currentWeight", new Vector2(149, 339));
            positions.Add("availableWeight", new Vector2(141, 399));

            carIndex = Game1.memoryCard.carIndex;
            textures2D["nave" + carIndex].setPosition(399, 489);

            positions.Add("levelReqPoints", new Vector2(397, 154));
            levelReqPoints = new List<int>();
            for (int i = 0;i < 900;i++)
            {
                levelReqPoints.Add(i * 500);
            }

            setCarAtributes();

            //Choose Sector
            for (int i = 0;i < 10;i++)
            {
                positions.Add("sector" + i, new Vector2(300 + (500 * i) + ((Game1.memoryCard.sectorIndex) * -500), 150));
            }

            sectors = new List<string>();
            sectors.Add("1. Sector A");
            sectors.Add("2. Sector B");
            sectors.Add("3. Sector C");
            sectors.Add("4. Sector D");
            sectors.Add("5. Sector E");
            sectors.Add("6. Sector F");
            sectors.Add("7. Sector G");
            sectors.Add("8. Sector H");
            sectors.Add("9. Sector I");
            sectors.Add("10. Sector J");

            //Choose Race
            for (int i = 0;i < 10;i++)
            {
                if (i < 5)
                {
                    positions.Add("race" + i, new Vector2(100 + (163 * i), 100));
                }
                else
                {
                    positions.Add("race" + i, new Vector2(100 + (163 * (i - 5)), 275));
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
                                    if (Game1.memoryCard.score >= levelReqPoints[Game1.memoryCard.level + 1])
                                    {
                                        Game1.memoryCard.score -= levelReqPoints[Game1.memoryCard.level + 1];
                                        Game1.memoryCard.level++;
                                        setCarAtributes();
                                    }
                                    return;
                                }

                                if (drawPlusButtons)
                                {
                                    if (Game1.memoryCard.speed < 100)
                                        if (textures2D["buttonPlus"].intersectsWithMouseClick(positions["buttonPlusSpeed"]))
                                        {
                                            Game1.memoryCard.speed++;
                                            setCarAtributes();
                                            return;
                                        }
                                    if (Game1.memoryCard.acceleration < 100)
                                        if (textures2D["buttonPlus"].intersectsWithMouseClick(positions["buttonPlusAcceleration"]))
                                        {
                                            Game1.memoryCard.acceleration++;
                                            setCarAtributes();
                                            return;
                                        }
                                    if (Game1.memoryCard.agility < 100)
                                        if (textures2D["buttonPlus"].intersectsWithMouseClick(positions["buttonPlusAgility"]))
                                        {
                                            Game1.memoryCard.agility++;
                                            setCarAtributes();
                                            return;
                                        }
                                    if (Game1.memoryCard.armor < 100)
                                        if (textures2D["buttonPlus"].intersectsWithMouseClick(positions["buttonPlusArmor"]))
                                        {
                                            Game1.memoryCard.armor++;
                                            setCarAtributes();
                                            return;
                                        }
                                    if (Game1.memoryCard.shieldPower < 100)
                                        if (textures2D["buttonPlus"].intersectsWithMouseClick(positions["buttonPlusShieldPower"]))
                                        {
                                            Game1.memoryCard.shieldPower++;
                                            setCarAtributes();
                                            return;
                                        }
                                    if (Game1.memoryCard.shieldRecharge < 100)
                                        if (textures2D["buttonPlus"].intersectsWithMouseClick(positions["buttonPlusShieldRecharge"]))
                                        {
                                            Game1.memoryCard.shieldRecharge++;
                                            setCarAtributes();
                                            return;
                                        }
                                    if (Game1.memoryCard.suspension < 100)
                                        if (textures2D["buttonPlus"].intersectsWithMouseClick(positions["buttonPlusSuspension"]))
                                        {
                                            Game1.memoryCard.suspension++;
                                            setCarAtributes();
                                            return;
                                        }
                                    if (Game1.memoryCard.tires < 100)
                                        if (textures2D["buttonPlus"].intersectsWithMouseClick(positions["buttonPlusTires"]))
                                        {
                                            Game1.memoryCard.tires++;
                                            setCarAtributes();
                                            return;
                                        }
                                    if (Game1.memoryCard.downforce < 100)
                                        if (textures2D["buttonPlus"].intersectsWithMouseClick(positions["buttonPlusDownforce"]))
                                        {
                                            Game1.memoryCard.downforce++;
                                            setCarAtributes();
                                            return;
                                        }
                                }

                                if (Game1.memoryCard.speed > 10 + Game1.players.playerTypes[Game1.memoryCard.carIndex].speedBonus)
                                    if (textures2D["buttonMinus"].intersectsWithMouseClick(positions["buttonMinusSpeed"]))
                                    {
                                        Game1.memoryCard.speed--;
                                        setCarAtributes();
                                        return;
                                    }
                                if (Game1.memoryCard.acceleration > 10 + Game1.players.playerTypes[Game1.memoryCard.carIndex].accelerationBonus)
                                    if (textures2D["buttonMinus"].intersectsWithMouseClick(positions["buttonMinusAcceleration"]))
                                    {
                                        Game1.memoryCard.acceleration--;
                                        setCarAtributes();
                                        return;
                                    }
                                if (Game1.memoryCard.agility > 10 + Game1.players.playerTypes[Game1.memoryCard.carIndex].agilityBonus)
                                    if (textures2D["buttonMinus"].intersectsWithMouseClick(positions["buttonMinusAgility"]))
                                    {
                                        Game1.memoryCard.agility--;
                                        setCarAtributes();
                                        return;
                                    }
                                if (Game1.memoryCard.armor > 10 + Game1.players.playerTypes[Game1.memoryCard.carIndex].armorBonus)
                                    if (textures2D["buttonMinus"].intersectsWithMouseClick(positions["buttonMinusArmor"]))
                                    {
                                        Game1.memoryCard.armor--;
                                        setCarAtributes();
                                        return;
                                    }
                                if (Game1.memoryCard.shieldPower > 10 + Game1.players.playerTypes[Game1.memoryCard.carIndex].shieldPowerBonus)
                                    if (textures2D["buttonMinus"].intersectsWithMouseClick(positions["buttonMinusShieldPower"]))
                                    {
                                        Game1.memoryCard.shieldPower--;
                                        setCarAtributes();
                                        return;
                                    }
                                if (Game1.memoryCard.shieldRecharge > 10 + Game1.players.playerTypes[Game1.memoryCard.carIndex].shieldRechargeBonus)
                                    if (textures2D["buttonMinus"].intersectsWithMouseClick(positions["buttonMinusShieldRecharge"]))
                                    {
                                        Game1.memoryCard.shieldRecharge--;
                                        setCarAtributes();
                                        return;
                                    }
                                if (Game1.memoryCard.suspension > 10 + Game1.players.playerTypes[Game1.memoryCard.carIndex].suspensionBonus)
                                    if (textures2D["buttonMinus"].intersectsWithMouseClick(positions["buttonMinusSuspension"]))
                                    {
                                        Game1.memoryCard.suspension--;
                                        setCarAtributes();
                                        return;
                                    }
                                if (Game1.memoryCard.tires > 10 + Game1.players.playerTypes[Game1.memoryCard.carIndex].tiresBonus)
                                    if (textures2D["buttonMinus"].intersectsWithMouseClick(positions["buttonMinusTires"]))
                                    {
                                        Game1.memoryCard.tires--;
                                        setCarAtributes();
                                        return;
                                    }
                                if (Game1.memoryCard.downforce > 10 + Game1.players.playerTypes[Game1.memoryCard.carIndex].downforceBonus)
                                    if (textures2D["buttonMinus"].intersectsWithMouseClick(positions["buttonMinusDownforce"]))
                                    {
                                        Game1.memoryCard.downforce--;
                                        setCarAtributes();
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

                            updateSectorPositions();

                            if (Game1.input.click0)
                            {
                                for (int i = 0;i < 10;i++)
                                {
                                    if (textures2D["sector"].intersectsWithMouseClick(positions["sector" + i]))
                                    {
                                        if ((Game1.memoryCard.raceUnlocked) / 10 >= i)
                                        {
                                            nextState = states.chooseRace;
                                            Game1.memoryCard.sectorIndex = i;
                                            return;
                                        }
                                    }
                                }
                            }
   
                        }
                        break;
                    #endregion
                    #region case states.chooseRace:
                    case states.chooseRace:
                        {
                            if (Game1.input.backButtonClick)
                            {
                                nextState = states.chooseSector;
                                return;
                            }

                            if (Game1.input.click0)
                            {
                                for (int i = 0;i < 10;i++)
                                {
                                    int aux = (Game1.memoryCard.sectorIndex * 10) + i;

                                    if (textures2D["race"].intersectsWithMouseClick(positions["race" + i]))
                                    {
                                        if (aux <= Game1.memoryCard.raceUnlocked)
                                        {
                                            Game1.memoryCard.race = aux;
                                            Game1.nextState = Game1.states.race;
                                            return;
                                        }
                                    }
                                }
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
                    #region case states.chooseRace:
                    case states.chooseRace:
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

                        Game1.spriteBatch.DrawString(Game1.verdana20, "$" + Game1.memoryCard.score, positions["score"], Color.White);
                        Game1.spriteBatch.DrawString(Game1.verdana20, "Level: " + Game1.memoryCard.level, positions["level"], Color.White);

                        Game1.spriteBatch.DrawString(Game1.verdana12, "Maximum: " + maximumWeight, positions["maximumWeight"], Color.White);
                        Game1.spriteBatch.DrawString(Game1.verdana12, "Current: " + currentWeight, positions["currentWeight"], Color.White);
                        Game1.spriteBatch.DrawString(Game1.verdana12, "Available: " + availableWeight, positions["availableWeight"], Color.White);

                        if (Game1.memoryCard.score >= levelReqPoints[Game1.memoryCard.level + 1])
                        {
                            Game1.spriteBatch.DrawString(Game1.verdana20, "$" + levelReqPoints[Game1.memoryCard.level + 1], positions["levelReqPoints"], Color.CornflowerBlue);
                        }
                        else
                        {
                            Game1.spriteBatch.DrawString(Game1.verdana20, "$" + levelReqPoints[Game1.memoryCard.level + 1], positions["levelReqPoints"], Color.Red * 0.75f);
                        }

                        textures2D["nave" + carIndex].drawOnScreen();
                        textures2D["wheel"].drawOnScreen(positions["wheel1"]);
                        textures2D["wheel"].drawOnScreen(positions["wheel2"]);
                        
                        drawAtributeBar(positions["speed"], Game1.memoryCard.speed, 100, "Lime");
                        drawAtributeBar(positions["acceleration"], Game1.memoryCard.acceleration + 10, 100, "Lime");
                        drawAtributeBar(positions["agility"], Game1.memoryCard.agility, 100, "Lime");
                        drawAtributeBar(positions["armor"], Game1.memoryCard.armor, 100, "Lime");
                        drawAtributeBar(positions["shieldPower"], Game1.memoryCard.shieldPower, 100, "Lime");
                        drawAtributeBar(positions["shieldRecharge"], Game1.memoryCard.shieldRecharge, 100, "Lime");
                        drawAtributeBar(positions["suspension"], Game1.memoryCard.suspension, 100, "Lime");
                        drawAtributeBar(positions["tires"], Game1.memoryCard.tires, 100, "Lime");
                        drawAtributeBar(positions["downforce"], Game1.memoryCard.downforce, 100, "Lime");

                        //Desenho dos botões
                        if (Game1.input.mouse0)
                        {
                            textures2D["buttonRace" + (textures2D["buttonRace"].intersectsWithMouseClick() ? "Pressed" : "")].drawOnScreen();
                            textures2D["buttonSupplyRoom" + (textures2D["buttonSupplyRoom"].intersectsWithMouseClick() ? "Pressed" : "")].drawOnScreen();
                            textures2D["buttonLevelUp" + (textures2D["buttonLevelUp"].intersectsWithMouseClick() ? "Pressed" : "")].drawOnScreen();

                            if (drawPlusButtons)
                            {
                                if (Game1.memoryCard.speed < 100)
                                    textures2D["buttonPlus" + (textures2D["buttonPlus"].intersectsWithMouseClick(positions["buttonPlusSpeed"]) ? "Pressed" : "")].drawOnScreen(positions["buttonPlusSpeed"]);
                                if (Game1.memoryCard.acceleration < 100)
                                    textures2D["buttonPlus" + (textures2D["buttonPlus"].intersectsWithMouseClick(positions["buttonPlusAcceleration"]) ? "Pressed" : "")].drawOnScreen(positions["buttonPlusAcceleration"]);
                                if (Game1.memoryCard.agility < 100)
                                    textures2D["buttonPlus" + (textures2D["buttonPlus"].intersectsWithMouseClick(positions["buttonPlusAgility"]) ? "Pressed" : "")].drawOnScreen(positions["buttonPlusAgility"]);
                                if (Game1.memoryCard.armor < 100)
                                    textures2D["buttonPlus" + (textures2D["buttonPlus"].intersectsWithMouseClick(positions["buttonPlusArmor"]) ? "Pressed" : "")].drawOnScreen(positions["buttonPlusArmor"]);
                                if (Game1.memoryCard.shieldPower < 100)
                                    textures2D["buttonPlus" + (textures2D["buttonPlus"].intersectsWithMouseClick(positions["buttonPlusShieldPower"]) ? "Pressed" : "")].drawOnScreen(positions["buttonPlusShieldPower"]);
                                if (Game1.memoryCard.shieldRecharge < 100)
                                    textures2D["buttonPlus" + (textures2D["buttonPlus"].intersectsWithMouseClick(positions["buttonPlusShieldRecharge"]) ? "Pressed" : "")].drawOnScreen(positions["buttonPlusShieldRecharge"]);
                                if (Game1.memoryCard.suspension < 100)
                                    textures2D["buttonPlus" + (textures2D["buttonPlus"].intersectsWithMouseClick(positions["buttonPlusSuspension"]) ? "Pressed" : "")].drawOnScreen(positions["buttonPlusSuspension"]);
                                if (Game1.memoryCard.tires < 100)
                                    textures2D["buttonPlus" + (textures2D["buttonPlus"].intersectsWithMouseClick(positions["buttonPlusTires"]) ? "Pressed" : "")].drawOnScreen(positions["buttonPlusTires"]);
                                if (Game1.memoryCard.downforce < 100)
                                    textures2D["buttonPlus" + (textures2D["buttonPlus"].intersectsWithMouseClick(positions["buttonPlusDownforce"]) ? "Pressed" : "")].drawOnScreen(positions["buttonPlusDownforce"]);
                            }

                            if (Game1.memoryCard.speed > 10 + Game1.players.playerTypes[Game1.memoryCard.carIndex].speedBonus)
                                textures2D["buttonMinus" + (textures2D["buttonMinus"].intersectsWithMouseClick(positions["buttonMinusSpeed"]) ? "Pressed" : "")].drawOnScreen(positions["buttonMinusSpeed"]);
                            if (Game1.memoryCard.acceleration > 10 + Game1.players.playerTypes[Game1.memoryCard.carIndex].accelerationBonus)
                                textures2D["buttonMinus" + (textures2D["buttonMinus"].intersectsWithMouseClick(positions["buttonMinusAcceleration"]) ? "Pressed" : "")].drawOnScreen(positions["buttonMinusAcceleration"]);
                            if (Game1.memoryCard.agility > 10 + Game1.players.playerTypes[Game1.memoryCard.carIndex].agilityBonus)
                                textures2D["buttonMinus" + (textures2D["buttonMinus"].intersectsWithMouseClick(positions["buttonMinusAgility"]) ? "Pressed" : "")].drawOnScreen(positions["buttonMinusAgility"]);
                            if (Game1.memoryCard.armor > 10 + Game1.players.playerTypes[Game1.memoryCard.carIndex].armorBonus)
                                textures2D["buttonMinus" + (textures2D["buttonMinus"].intersectsWithMouseClick(positions["buttonMinusArmor"]) ? "Pressed" : "")].drawOnScreen(positions["buttonMinusArmor"]);
                            if (Game1.memoryCard.shieldPower > 10 + Game1.players.playerTypes[Game1.memoryCard.carIndex].shieldPowerBonus)
                                textures2D["buttonMinus" + (textures2D["buttonMinus"].intersectsWithMouseClick(positions["buttonMinusShieldPower"]) ? "Pressed" : "")].drawOnScreen(positions["buttonMinusShieldPower"]);
                            if (Game1.memoryCard.shieldRecharge > 10 + Game1.players.playerTypes[Game1.memoryCard.carIndex].shieldRechargeBonus)
                                textures2D["buttonMinus" + (textures2D["buttonMinus"].intersectsWithMouseClick(positions["buttonMinusShieldRecharge"]) ? "Pressed" : "")].drawOnScreen(positions["buttonMinusShieldRecharge"]);
                            if (Game1.memoryCard.suspension > 10 + Game1.players.playerTypes[Game1.memoryCard.carIndex].suspensionBonus)
                                textures2D["buttonMinus" + (textures2D["buttonMinus"].intersectsWithMouseClick(positions["buttonMinusSuspension"]) ? "Pressed" : "")].drawOnScreen(positions["buttonMinusSuspension"]);
                            if (Game1.memoryCard.tires > 10 + Game1.players.playerTypes[Game1.memoryCard.carIndex].tiresBonus)
                                textures2D["buttonMinus" + (textures2D["buttonMinus"].intersectsWithMouseClick(positions["buttonMinusTires"]) ? "Pressed" : "")].drawOnScreen(positions["buttonMinusTires"]);
                            if (Game1.memoryCard.downforce > 10 + Game1.players.playerTypes[Game1.memoryCard.carIndex].downforceBonus)
                                textures2D["buttonMinus" + (textures2D["buttonMinus"].intersectsWithMouseClick(positions["buttonMinusDownforce"]) ? "Pressed" : "")].drawOnScreen(positions["buttonMinusDownforce"]);

                        }
                        else
                        {
                            //Desenhar todos os botões
                            textures2D["buttonRace"].drawOnScreen();
                            textures2D["buttonSupplyRoom"].drawOnScreen();
                            textures2D["buttonLevelUp"].drawOnScreen();

                            if (drawPlusButtons)
                            {
                                if (Game1.memoryCard.speed < 100)
                                    textures2D["buttonPlus"].drawOnScreen(positions["buttonPlusSpeed"]);
                                if (Game1.memoryCard.acceleration < 100)
                                    textures2D["buttonPlus"].drawOnScreen(positions["buttonPlusAcceleration"]);
                                if (Game1.memoryCard.agility < 100)
                                    textures2D["buttonPlus"].drawOnScreen(positions["buttonPlusAgility"]);
                                if (Game1.memoryCard.armor < 100)
                                    textures2D["buttonPlus"].drawOnScreen(positions["buttonPlusArmor"]);
                                if (Game1.memoryCard.shieldPower < 100)
                                    textures2D["buttonPlus"].drawOnScreen(positions["buttonPlusShieldPower"]);
                                if (Game1.memoryCard.shieldRecharge < 100)
                                    textures2D["buttonPlus"].drawOnScreen(positions["buttonPlusShieldRecharge"]);
                                if (Game1.memoryCard.suspension < 100)
                                    textures2D["buttonPlus"].drawOnScreen(positions["buttonPlusSuspension"]);
                                if (Game1.memoryCard.tires < 100)
                                    textures2D["buttonPlus"].drawOnScreen(positions["buttonPlusTires"]);
                                if (Game1.memoryCard.downforce < 100)
                                    textures2D["buttonPlus"].drawOnScreen(positions["buttonPlusDownforce"]);
                            }

                            if (Game1.memoryCard.speed > 10 + Game1.players.playerTypes[Game1.memoryCard.carIndex].speedBonus)
                                textures2D["buttonMinus"].drawOnScreen(positions["buttonMinusSpeed"]);
                            if (Game1.memoryCard.acceleration > 10 + Game1.players.playerTypes[Game1.memoryCard.carIndex].accelerationBonus)
                                textures2D["buttonMinus"].drawOnScreen(positions["buttonMinusAcceleration"]);
                            if (Game1.memoryCard.agility > 10 + Game1.players.playerTypes[Game1.memoryCard.carIndex].agilityBonus)
                                textures2D["buttonMinus"].drawOnScreen(positions["buttonMinusAgility"]);
                            if (Game1.memoryCard.armor > 10 + Game1.players.playerTypes[Game1.memoryCard.carIndex].armorBonus)
                                textures2D["buttonMinus"].drawOnScreen(positions["buttonMinusArmor"]);
                            if (Game1.memoryCard.shieldPower > 10 + Game1.players.playerTypes[Game1.memoryCard.carIndex].shieldPowerBonus)
                                textures2D["buttonMinus"].drawOnScreen(positions["buttonMinusShieldPower"]);
                            if (Game1.memoryCard.shieldRecharge > 10 + Game1.players.playerTypes[Game1.memoryCard.carIndex].shieldRechargeBonus)
                                textures2D["buttonMinus"].drawOnScreen(positions["buttonMinusShieldRecharge"]);
                            if (Game1.memoryCard.suspension > 10 + Game1.players.playerTypes[Game1.memoryCard.carIndex].suspensionBonus)
                                textures2D["buttonMinus"].drawOnScreen(positions["buttonMinusSuspension"]);
                            if (Game1.memoryCard.tires > 10 + Game1.players.playerTypes[Game1.memoryCard.carIndex].tiresBonus)
                                textures2D["buttonMinus"].drawOnScreen(positions["buttonMinusTires"]);
                            if (Game1.memoryCard.downforce > 10 + Game1.players.playerTypes[Game1.memoryCard.carIndex].downforceBonus)
                                textures2D["buttonMinus"].drawOnScreen(positions["buttonMinusDownforce"]);
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
                        for (int i = 0;i < 10;i++)
                        {
                            if ((Game1.memoryCard.raceUnlocked) / 10 >= i)
                            {
                                textures2D["sector"].drawOnScreen(positions["sector" + i]);
                                Game1.spriteBatch.DrawString(Game1.verdana20, sectors[i], new Vector2(positions["sector" + i].X + 10, positions["sector" + i].Y + 10), Color.White);
                            }
                            else
                            {
                                textures2D["sectorLocked"].drawOnScreen(positions["sector" + i]);
                                Game1.spriteBatch.DrawString(Game1.verdana20, sectors[i], new Vector2(positions["sector" + i].X + 10, positions["sector" + i].Y + 10), Color.Black);
                            }
                            
                        }
                        
                    }
                    break;
                #endregion
                #region case states.chooseRace:
                case states.chooseRace:
                    {
                        Game1.graphicsDeviceManager.GraphicsDevice.Clear(Color.Black);
                        textures2D["background"].drawOnScreen();
                        for (int i = 0;i < 10;i++)
                        {
                            int aux = (Game1.memoryCard.sectorIndex * 10) + i;

                            if (aux <= Game1.memoryCard.raceUnlocked)
                            {
                                textures2D["race"].drawOnScreen(positions["race" + i]);
                                Game1.spriteBatch.DrawString(Game1.verdana12, "" + (aux + 1), new Vector2(positions["race" + i].X + 10, positions["race" + i].Y + 10), Color.Black);
                            }
                            else
                            {
                                textures2D["raceLocked"].drawOnScreen(positions["race" + i]);
                                Game1.spriteBatch.DrawString(Game1.verdana12, "" + (aux + 1), new Vector2(positions["race" + i].X + 10, positions["race" + i].Y + 10), Color.White);
                            }
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

        //Choose Sector
        private void updateSectorPositions()
        {
            //Se moveu o mouse mais de 10 pixels
            if (Math.Abs(Game1.input.totalDx) > 10)
            {
                if (Game1.input.dx < 0)
                {//Moveu o mouse para a esquerda

                    if (positions["sector" + (10 - 1)].X + (Game1.input.dx * 2) > 300)
                    {
                        for (int i = 0;i < 10;i++)
                        {
                            positions["sector" + i] = new Vector2((int)(positions["sector" + i].X + Game1.input.dx * 2), 150);
                        }
                    }
                    else
                    {
                        int auxMove = (int)(Game1.display.width - textures2D["sector"].width - positions["sector" + (10 - 1)].X - 300);
                        for (int i = 0;i < 10;i++)
                        {
                            positions["sector" + i] = new Vector2((int)(positions["sector" + i].X + auxMove), 150);
                        }
                    }
                }
                else
                {//Moveu o mouse para a esquerda
                    //Game1.input.dx > 0

                    if (positions["sector" + 0].X + Game1.input.dx * 2 < 300)
                    {
                        for (int i = 0;i < 10;i++)
                        {
                            positions["sector" + i] = new Vector2((int)(positions["sector" + i].X + Game1.input.dx * 2), 150);
                        }
                    }
                    else
                    {
                        int auxMove = (int)(positions["sector" + 0].X - 300);
                        for (int i = 0;i < 10;i++)
                        {
                            positions["sector" + i] = new Vector2((int)(positions["sector" + i].X - auxMove), 150);
                        }
                    }
                }
            }
        }

        //Hangar
        private void setCarAtributes()
        {
            health = (Game1.memoryCard.level * Config.hpPerLevel) + ((Game1.memoryCard.armor - 10) * Config.hpPerArmor);
            energyShield = (Game1.memoryCard.level * Config.spPerLevel) + ((Game1.memoryCard.shieldPower - 9) * Config.spPerPower);

            bonus = 90;//9 atributos no total * 10 pontos no minimo em cada
            bonus += Game1.players.playerTypes[Game1.memoryCard.carIndex].speedBonus;
            bonus += Game1.players.playerTypes[Game1.memoryCard.carIndex].accelerationBonus;
            bonus += Game1.players.playerTypes[Game1.memoryCard.carIndex].agilityBonus;
            bonus += Game1.players.playerTypes[Game1.memoryCard.carIndex].armorBonus;
            bonus += Game1.players.playerTypes[Game1.memoryCard.carIndex].shieldPowerBonus;
            bonus += Game1.players.playerTypes[Game1.memoryCard.carIndex].shieldRechargeBonus;
            bonus += Game1.players.playerTypes[Game1.memoryCard.carIndex].suspensionBonus;
            bonus += Game1.players.playerTypes[Game1.memoryCard.carIndex].tiresBonus;
            bonus += Game1.players.playerTypes[Game1.memoryCard.carIndex].downforceBonus;

            maximumWeight = Config.baseWeitght + (Game1.memoryCard.level * 100);

            //TODO pesos diferentes para cada nave
            currentWeight = Config.baseWeitght + 100;//((textures2D["nave" + Game1.memoryCard.carIndex].width - 1) * (textures2D["nave" + Game1.memoryCard.carIndex].height - 1)) / 10;
            currentWeight += ((
                Game1.memoryCard.speed + 
                Game1.memoryCard.acceleration + 
                Game1.memoryCard.agility + 
                Game1.memoryCard.armor + 
                Game1.memoryCard.shieldPower + 
                Game1.memoryCard.shieldRecharge + 
                Game1.memoryCard.suspension + 
                Game1.memoryCard.tires + 
                Game1.memoryCard.downforce) - bonus) * 100;
            for (int i = 0;i < 10;i++)
            {
                //TODO: calcular o peso das armas
            }
            availableWeight = maximumWeight - currentWeight;
            drawPlusButtons = availableWeight >= 100;
        }

        private void drawAtributeBar(Vector2 position, int health, int maxHealth, string color)
        {
            //TODO Melhorar design das barrinhas dos atributos???
            int biggerSide = 162;
            Color healthColor = new Color();
            switch (color)
            {
                case "Lime":
                    healthColor = new Color(500 - 500 * health / maxHealth, 500 * health / maxHealth, 0);
                    break;
                case "Red":
                    healthColor = new Color(500 - 500 * (maxHealth - health) / maxHealth, 500 * (maxHealth - health) / maxHealth, 0);
                    break;
            }


            Game1.spriteBatch.Draw(Game1.voidTexture, new Rectangle((int)(position.X), (int)(position.Y), biggerSide + 2, 5), Color.White);
            Game1.spriteBatch.Draw(Game1.voidTexture, new Rectangle((int)(position.X) + 1, (int)(position.Y) + 1, biggerSide, 3), Color.Black);
            Game1.spriteBatch.Draw(Game1.voidTexture, new Rectangle((int)(position.X) + 1, (int)(position.Y) + 1, biggerSide * health / maxHealth, 3), healthColor);
        }

    }
}
