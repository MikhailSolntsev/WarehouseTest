using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace WarehouseApp
{
    public class Pallet: Scalable
    {
        public const int OwnWeight = 30;
        [JsonInclude]
        private List<Box> Boxes = new();
        
        public int PalletId { get; set; }
        [JsonIgnore]
        public int Weight { get { return OwnWeight + Boxes.Sum(box => box.Weight); } }
        [JsonIgnore]
        public override int Volume { get { return base.Volume + Boxes.Sum(box => box.Volume); } }
        [JsonIgnore]
        public override DateTime ExpirationDate { get => Boxes.Count switch { 0 => DateTime.MinValue, _ => Boxes.Min(box => box.ExpirationDate) }; }

        public Pallet(int height, int width, int length) : base(height, width, length)
        {
        }
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
