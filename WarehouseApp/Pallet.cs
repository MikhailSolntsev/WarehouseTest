using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseApp
{
    internal class Pallet: Scalable
    {
        public Pallet(int height, int width, int length, int weight)
        {
        }

        public int PalletId;
        public List<Box> Boxes = new();
        override public int Volume { get { return base.Volume + Boxes.Sum(box => box.Volume); } }
    }
}
