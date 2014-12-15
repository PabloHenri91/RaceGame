using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MyGame.src
{
    class MemoryCard
    {
        private string line;

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

                        if (line.StartsWith("aux="))
                        {
                            int aux = Convert.ToInt32(line.Split('=')[1]);
                        }

                        //TODO Genérico
                        //fieldInfo = this.GetType().GetField(line.Split('=')[0]);
                        //fieldInfo.SetValue(this, Convert.ChangeType(line.Split('=')[1], fieldInfo.FieldType));
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
