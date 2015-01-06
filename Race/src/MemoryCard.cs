using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MyGame.src
{
    class MemoryCard
    {
        //Auxiliar
        private string line;

        //Dados do jogador
        internal int carIndex;
        internal int score;
        internal int level;
        internal int race;
        internal int raceUnlocked;
        internal int sectorIndex;

        internal void newGame(int newCarIndex)
        {
            carIndex = newCarIndex;//grava o carro que foi escolhido
            score = 10000;//Jogo inicia com 10000 pontos
            level = 1;//Primeiro carro inicia level 1
            race = 0;
            raceUnlocked = 0;
            sectorIndex = 0;
        }

        //TODO função para salvar o jogo no arquivo data

        internal bool loadGame()
        {
            try
            {
#if WINDOWS_PHONE
                IsolatedStorageFile isolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication();
                using (StreamReader streamReader = new StreamReader(new IsolatedStorageFileStream("data", FileMode.Open, isolatedStorageFile)))
#endif

#if WINDOWS || LINUX
                using (StreamReader streamReader = new StreamReader("data"))
#endif
                {
                    while (!streamReader.EndOfStream)
                    {
                        line = streamReader.ReadLine();

                        //Exemplo
                        if (line.StartsWith("aux="))
                        {
                            int aux = Convert.ToInt32(line.Split('=')[1]);
                        }

                        //TO DO Fazer um método genérico para setar os valores desta classe
                        //fieldInfo = this.GetType().GetField(line.Split('=')[0]);
                        //fieldInfo.SetValue(this, Convert.ChangeType(line.Split('=')[1], fieldInfo.FieldType));
                    }
                }
            }
            catch (Exception)
            {
                //Retorna falso se o arquivo não foi encontrado ou por algum outro motivo o arquivo de dados não pode ser carregado
                return false;
            }
            return true;
        }
    }
}
