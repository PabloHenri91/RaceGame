using Microsoft.Xna.Framework;
namespace Race.src
{
    class Config
    {
        //Display
        internal static string title = "Teste =}";

        //FPS
        internal static int fps = 60;

        //Input
        public static int displayWidth = 1000;
        public static int displayHeight = 700;
        public static bool IsFullScreen = false;

        //Hangar
        public static int hpPerLevel = 2;
        public static int spPerLevel = 1;
        public static int hpPerArmor = 4;
        public static int spPerPower = 3;
        public static int baseWeitght = ((64 * 64) / 10) + 100;//Peso máximo do primeiro carro

        //Medidas do cenário
        public static int tileSize = 16;
        public static float tilesPerChunk = 64;
        public static float chunksPerMap = 64;

        public static float chunkSize = tilesPerChunk * tileSize;
        public static float mapSize = chunksPerMap * chunkSize;
        public static Vector2 scale = new Vector2(4);
    }
}
