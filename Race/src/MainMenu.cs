using FarseerPhysics;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FarseerPhysics.Common.Decomposition;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MyGame.src
{
    class MainMenu : State
    {
        public enum states { mainMenu, loading, newGame, loadGame, options, help, credits };
        states state;
        states nextState;

        //New Game
        int newCarIndex;
        private Players players;

        //Física
        World world;
        Body ground;
        Body car, wheel1, wheel2;

        //Auxiliares Física
        private Vector2 scale;
        private uint[] data;
        private Vertices vertices;
        private float zeta;
        private float hz;
        private WheelJoint spring1;
        private WheelJoint spring2;
        private CircleShape circle;
        private Vector2 axis;
        private PolygonShape polygonShape;

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
            for (int i = 0; i < 6; i++)//Carregar texturas dos carros que o jogador vai poder escolher para começar o jogo.
            {
                textures2Dlocations.Add("nave" + i);
            }
            textures2Dlocations.Add("wheel");
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

            //Física
            world = new World(new Vector2(0, -9.807f)); // 9,807 = gravidade da Terra =P

            //Chão
            vertices = new Vertices();
            vertices.Add(new Vector2(ConvertUnits.ToSimUnits(0), -ConvertUnits.ToSimUnits(0)));
            vertices.Add(new Vector2(ConvertUnits.ToSimUnits(299), -ConvertUnits.ToSimUnits(0)));
            vertices.Add(new Vector2(ConvertUnits.ToSimUnits(299), -ConvertUnits.ToSimUnits(10)));
            vertices.Add(new Vector2(ConvertUnits.ToSimUnits(0), -ConvertUnits.ToSimUnits(10)));
            polygonShape = new PolygonShape(vertices, 1f);
            ground = new Body(world);
            ground.BodyType = BodyType.Static;
            ground.Position = new Vector2(ConvertUnits.ToSimUnits(449), -ConvertUnits.ToSimUnits(375));
            ground.CreateFixture(polygonShape);

            //Carros
            players = new Players();
            //Auxiliares
            scale = new Vector2(ConvertUnits.ToSimUnits(1), -ConvertUnits.ToSimUnits(1));
            hz = 5f;//4.0f;//suspensão
            zeta = 0.1f;//0.6f;//suspensão
            circle = new CircleShape(ConvertUnits.ToSimUnits(25f / 2f), 2f);
            axis = new Vector2(0.0f, -1.0f);
            newCarIndex = 0;
            loadCar();

            positions.Add("speed", new Vector2(266, 321));
            positions.Add("acceleration", new Vector2(266, 352));
            positions.Add("agility", new Vector2(266, 383));
            positions.Add("armor", new Vector2(266, 413));
            positions.Add("shieldPower", new Vector2(266, 444));
            positions.Add("shieldRecharge", new Vector2(266, 475));
            positions.Add("suspension", new Vector2(266, 506));
            positions.Add("tires", new Vector2(266, 536));
            positions.Add("downforce", new Vector2(266, 567));

            positions.Add("score", new Vector2(638, 476));
            positions.Add("level", new Vector2(636, 536));
            
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
                            world.Step(1f / Config.fps);

                            if (Game1.input.backButtonClick)
                            {
                                nextState = states.mainMenu;
                                return;
                            }

                            if (Game1.input.click0)
                            {
                                if (textures2D["buttonCancel"].intersectsWithMouseClick())
                                {
                                    nextState = states.mainMenu;
                                    return;
                                }
                                if (textures2D["buttonOk"].intersectsWithMouseClick())
                                {
                                    Game1.memoryCard = new MemoryCard();
                                    Game1.memoryCard.newGame(newCarIndex);
                                    Game1.nextState = Game1.states.hangar;
                                    return;
                                }
                                if (textures2D["buttonLeft"].intersectsWithMouseClick())
                                {
                                    newCarIndex--;
                                    if (newCarIndex < 0) newCarIndex = 5;
                                    loadCar();
                                    return;
                                }
                                if (textures2D["buttonRight"].intersectsWithMouseClick())
                                {
                                    newCarIndex++;
                                    if (newCarIndex > 5) newCarIndex = 0;
                                    loadCar();
                                    return;
                                }
                            }

                            textures2D["nave" + newCarIndex].setPosition(ConvertUnits.ToDisplayUnits(car.Position.X), -ConvertUnits.ToDisplayUnits(car.Position.Y), -car.Rotation);
                        }
                        break;
                    #endregion
                    #region case states.loadGame:
                    case states.loadGame: 
                        {
                            //TODO carregar dados do arquivo data e ir para a garagem
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

                        textures2D["nave" + newCarIndex].drawOnScreen();

                        textures2D["wheel"].drawOnScreen(new Vector2(ConvertUnits.ToDisplayUnits(wheel1.Position.X), -ConvertUnits.ToDisplayUnits(wheel1.Position.Y)), -wheel1.Rotation);
                        textures2D["wheel"].drawOnScreen(new Vector2(ConvertUnits.ToDisplayUnits(wheel2.Position.X), -ConvertUnits.ToDisplayUnits(wheel2.Position.Y)), -wheel2.Rotation);

                        drawAtributeBar(positions["speed"], players.playerTypes.Values.ElementAt(newCarIndex).speedBonus + 10, 100, "Lime");
                        drawAtributeBar(positions["acceleration"], players.playerTypes.Values.ElementAt(newCarIndex).accelerationBonus + 10, 100, "Lime");
                        drawAtributeBar(positions["agility"], players.playerTypes.Values.ElementAt(newCarIndex).agilityBonus + 10, 100, "Lime");
                        drawAtributeBar(positions["armor"], players.playerTypes.Values.ElementAt(newCarIndex).armorBonus + 10, 100, "Lime");
                        drawAtributeBar(positions["shieldPower"], players.playerTypes.Values.ElementAt(newCarIndex).shieldPowerBonus + 10, 100, "Lime");
                        drawAtributeBar(positions["shieldRecharge"], players.playerTypes.Values.ElementAt(newCarIndex).shieldRechargeBonus + 10, 100, "Lime");
                        drawAtributeBar(positions["suspension"], players.playerTypes.Values.ElementAt(newCarIndex).suspensionBonus + 10, 100, "Lime");
                        drawAtributeBar(positions["tires"], players.playerTypes.Values.ElementAt(newCarIndex).tiresBonus + 10, 100, "Lime");
                        drawAtributeBar(positions["downforce"], players.playerTypes.Values.ElementAt(newCarIndex).downforceBonus + 10, 100, "Lime");

                        Game1.spriteBatch.DrawString(Game1.verdana20, "$" + 10000, positions["score"], Color.White);
                        Game1.spriteBatch.DrawString(Game1.verdana20, "Level: " + 1, positions["level"], Color.White);
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

#region New Game

        private void drawAtributeBar(Vector2 position, int health, int maxHealth, string color)
        {
            //TODO Melhorar design das barrinhas dos atributos
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
        /// <summary>
        /// Carrega um carro com suspensao e rodas, baseado no valor de newCarIndex
        /// </summary>
        private void loadCar()
        {
            //Create an array to hold the data from the texture
            data = new uint[textures2D["nave" + newCarIndex].width * textures2D["nave" + newCarIndex].height];

            //Transfer the texture data to the array
            textures2D["nave" + newCarIndex].texture.GetData(data);

            vertices.Clear();
            vertices = PolygonTools.CreatePolygon(data, textures2D["nave" + newCarIndex].width);
            vertices.Scale(ref scale);

            if (car != null) world.RemoveBody(car);
            car = BodyFactory.CreateCompoundPolygon(world, Triangulate.ConvexPartition(vertices, TriangulationAlgorithm.Bayazit), 2f);
            car.BodyType = BodyType.Dynamic;
            car.Position = new Vector2(ConvertUnits.ToSimUnits(560), -ConvertUnits.ToSimUnits(-100));
            car.AngularVelocity = Game1.randomBetween(-1f, 1f);

            if (wheel1 != null) world.RemoveBody(wheel1);
            wheel1 = new Body(world);
            wheel1.BodyType = BodyType.Dynamic;
            wheel1.Position = new Vector2(ConvertUnits.ToSimUnits(561 + (25f / 2f)), -ConvertUnits.ToSimUnits(-50 + (25f / 2f)));
            wheel1.CreateFixture(circle);

            if (wheel2 != null) world.RemoveBody(wheel2);
            wheel2 = new Body(world);
            wheel2.BodyType = BodyType.Dynamic;
            wheel2.Position = new Vector2(ConvertUnits.ToSimUnits(610 + (25f / 2f)), -ConvertUnits.ToSimUnits(-50 + (25f / 2f)));
            wheel2.CreateFixture(circle);

            spring1 = new WheelJoint(car, wheel1, wheel1.Position, axis, true);
            spring1.Frequency = hz;
            spring1.DampingRatio = zeta;
            world.AddJoint(spring1);

            spring2 = new WheelJoint(car, wheel2, wheel2.Position, axis, true);
            spring2.Frequency = hz;
            spring2.DampingRatio = zeta;
            world.AddJoint(spring2);

            //Freio de mão =}
            wheel1.AngularDamping = 100f;

            textures2D["nave" + newCarIndex].setPosition(ConvertUnits.ToDisplayUnits(car.Position.X), -ConvertUnits.ToDisplayUnits(car.Position.Y), -car.Rotation);
        }
#endregion
    }
}
