using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGame.src
{
    class Hangar : State
    {
        public enum states { hangar, loading };
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

            return true;
        }

        internal void doLogic()
        {
            if (state == nextState)
            {
                switch (state)
                {
                    case states.hangar:
                        if (Game1.input.backButtonClick)
                        {
                            Game1.nextState = Game1.states.mainMenu;
                            return;
                        }
                        break;
                }
            }
            else
            {
                //Reload nextState
                positions.Clear();
                switch (nextState)
                {
                    case states.hangar:
                        break;
                }
                state = nextState;
            }
        }

        internal void draw()
        {
            switch (state)
            {
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

                        }
                        else
                        {
                            //Desenhar todos os botões
                            textures2D["buttonRace"].drawOnScreen();
                            textures2D["buttonSupplyRoom"].drawOnScreen();
                            textures2D["buttonLevelUp"].drawOnScreen();
                        }
                    }
                    break;
            }
        }
    }
}
