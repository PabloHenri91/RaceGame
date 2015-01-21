using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Race.src
{
    class State
    {
        internal Dictionary<string, Sprite> textures2D;

        protected List<string> textures2Dlocations;

        protected int textures2DlocationsCount;

        protected Dictionary<string, Vector2> positions;

        public State()
        {
            textures2D = new Dictionary<string, Sprite>();
            textures2Dlocations = new List<string>();

            positions = new Dictionary<string, Vector2>();
        }

        public bool load()
        {
            textures2DlocationsCount = textures2Dlocations.Count;

            foreach (string reference in textures2Dlocations)
            {
                if (!textures2D.ContainsKey(reference))
                {
                    textures2D.Add(reference, new Sprite(reference));
                }
            }
            return true;
        }
    }
}