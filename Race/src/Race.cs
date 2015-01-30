using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Race.src
{
    class Race : State
    {
        internal enum states { race, loading, pause };
        states state;
        states nextState;

        internal Entity player;

        MapManager mapManager;
        public FarseerPhysics.Dynamics.World world;
        public Race()
            : base()
        {
            state = states.loading;
            nextState = states.race;

            player = new Entity(0, 0, 10, 10);
        }
        new internal bool load()
        {
            base.load();

            //race
            mapManager = new MapManager();
            mapManager.reLoadMap();

            Game1.matrix = new Vector2(90, 2480);
            //Game1.matrix = Vector2.Zero;
            return true;
        }

        internal void doLogic()
        {
            if (state == nextState)
            {
                switch (state)
                {
                    case states.race:
                        {
                            Console.WriteLine(Game1.matrix);
                            Game1.matrix = new Vector2((int)(Game1.matrix.X + Game1.input.dx), (int)(Game1.matrix.Y + Game1.input.dy));
                            mapManager.update();
                        }
                        break;
                    case states.pause: { }
                        break;
                }
            }
            else
            {
                //Reload nextState
                switch (nextState)
                {
                    case states.race: { }
                        break;
                    case states.pause: { }
                        break;
                }
                state = nextState;
            }
        }

        internal void draw()
        {
            if (state == nextState)
            {
                switch (state)
                {
                    case states.race:
                        {
                            Game1.graphicsDeviceManager.GraphicsDevice.Clear(Color.CornflowerBlue);
                            mapManager.draw();
                        }
                        break;
                    case states.pause: { }
                        break;
                }
            }
        }

        private void translateMatrix(float x, float y)
        {
            Game1.matrix.X = -x + Game1.display.widthOver2 - 0;
            Game1.matrix.Y = -y + -Game1.display.heightOver2 + 0;
        }
    }
}
