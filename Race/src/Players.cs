using System.Collections.Generic;

namespace Race.src
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
            int i = 0;

            //Definindo atributos base dos carros que o jogador vai poder comprar
            playerType = new PlayerType(i++, 10, 10, 10, 10, 10, 10, 10, 10, 10);//0
            playerTypes.Add(playerType.type, playerType);

            playerType = new PlayerType(i++, 30, 40, 0, 0, 0, 0, 0, 20, 0);//1 Ótima aceleração
            playerTypes.Add(playerType.type, playerType);

            playerType = new PlayerType(i++, 30, 30, 30, 0, 0, 0, 0, 0, 0);//2 Bom motor
            playerTypes.Add(playerType.type, playerType);

            playerType = new PlayerType(i++, 0, 0, 0, 30, 30, 30, 0, 0, 0);//3 Boa defesa
            playerTypes.Add(playerType.type, playerType);

            playerType = new PlayerType(i++, 0, 0, 0, 0, 0, 0, 30, 30, 30);//4 Boa estabilidade
            playerTypes.Add(playerType.type, playerType);

            playerType = new PlayerType(i++, 0, 0, 0, 40, 30, 20, 0, 0, 0);//5 Ótima Blindagem
            playerTypes.Add(playerType.type, playerType);
        }
    }
}
