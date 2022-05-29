using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseApp
{
    public class Scalable
    {
        private int height;
        private int width;
        private int length;

        public Scalable(int height, int width, int length)
        {
            this.height = height;
            this.width = width;
            this.length = length;
        }

        virtual public int Volume { get { return height * width * length; } }
        virtual public int Weight { get; set; }
    }
}
