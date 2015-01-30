using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Race.src
{
    class Layer
    {
        internal ushort type;
        internal ushort[] data;

        public Layer(ushort type)
        {
            this.type = type;
        }
    }
}
