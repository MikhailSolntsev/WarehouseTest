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
            Weight = ownWeight;
        }
        public override int Volume { get { return base.Volume + Boxes.Sum(box => box.Volume); } }
        public override int Weight { get { return base.Weight + Boxes.Sum(box => box.Weight); } }
        public override DateTime ExpirationDate { get => Boxes.Count switch { 0 => DateTime.MinValue, _ => Boxes.Min(box => box.ExpirationDate) }; }
        public void AddBox(Box box)
        {
            if (box.Length > Length)
            {
                string message = $"Box lenght larger than pallete lenght";
                throw new ArgumentOutOfRangeException(message);
            }
            if (box.Width > Width)
            {
                string message = $"Box width larger than pallete width";
                throw new ArgumentOutOfRangeException(message);
            }
            if (box.Height > Height)
            {
                string message = $"Box height larger than pallete height";
                throw new ArgumentOutOfRangeException(message);
            }
            Boxes.Add(box);
        }
    }
}
