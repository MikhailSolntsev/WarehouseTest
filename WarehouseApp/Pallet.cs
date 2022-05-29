using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseApp
{
    public class Pallet: Scalable
    {
        private const int ownWeight = 30;

        public int PalletId { get; set; }

        private List<Box> Boxes = new();
        public Pallet(int height, int width, int length) : base(height, width, length)
        {
        }
        override public int Volume { get { return base.Volume + Boxes.Sum(box => box.Volume); } }
        
    }
}
