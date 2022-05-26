using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseApp
{
    internal class Scalable
    {
        private int height;
        private int width;
        private int length;
        private int weight;

        virtual public int Volume { get { return height * width * length; } }
        virtual public int Weight { get { return weight; } }
    }
}
