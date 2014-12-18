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

            textures2Dlocations.Add("hangarBackground");
        }
        new internal bool load()
        {
            base.load();

            return true;
        }

        internal void doLogic()
        {
            if (state == nextState)
            {
                switch (state)
                {
                    case states.hangar:
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
                Game1.needToDraw = true;
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
                    }
                    break;
            }
        }
    }
}
