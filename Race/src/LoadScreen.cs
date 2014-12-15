using Microsoft.Xna.Framework.Content;

namespace MyGame.src
{
    class LoadScreen : State
    {
        //Esta classe é utilizada para o caso de implementação de uma tela de load animada
        public bool load(ContentManager Content)
        {
            return true;
        }

        public void draw() { }
    }
}
