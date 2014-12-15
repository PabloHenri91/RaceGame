using System.Collections.Generic;

namespace MyGame.src
{
    class Players
    {
        internal Dictionary<int, PlayerType> playerTypes;
        private PlayerType playerType;

        internal Players()
        {
            playerTypes = new Dictionary<int, PlayerType>();
            this.load();
        }

        internal void load()
        {
            playerTypes.Clear();
            int i = 1;

            //Definindo atributos base dos carros que o jogador vai poder comprar
            playerType = new PlayerType(i++, 0, 0, 0, 0, 0, 0, 30, 30, 30);//1 Boa estabilidade
            playerTypes.Add(playerType.type, playerType);

            playerType = new PlayerType(i++, 30, 30, 30, 0, 0, 0, 0, 0, 0);//2 Bom motor
            playerTypes.Add(playerType.type, playerType);

            playerType = new PlayerType(i++, 0, 0, 0, 30, 30, 30, 0, 0, 0);//3 Boa defesa
            playerTypes.Add(playerType.type, playerType);

            playerType = new PlayerType(i++, 20, 50, 0, 0, 0, 0, 0, 20, 0);//4 Ótima aceleração
            playerTypes.Add(playerType.type, playerType);

            playerType = new PlayerType(i++, 0, 0, 0, 50, 20, 20, 0, 0, 0);//5 Ótima Blindagem
            playerTypes.Add(playerType.type, playerType);

            playerType = new PlayerType(i++, 10, 10, 10, 10, 10, 10, 10, 10, 10);//6
            playerTypes.Add(playerType.type, playerType);
        }
    }
}
