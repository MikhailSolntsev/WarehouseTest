using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace WarehouseApp
{
    [JsonConverter(typeof(PalletJsonConverter))]
    public class Pallet: Scalable
    {
        public const int OwnWeight = 30;
        
        private List<Box> boxes = new();
        
        public string PalletId { get; }
        
        public int Weight { get { return OwnWeight + boxes.Sum(box => box.Weight); } }
        
        public IReadOnlyList<Box> Boxes { get => boxes; }

        public override int Volume { get { return base.Volume + boxes.Sum(box => box.Volume); } }
        
        public override DateTime ExpirationDate { get => boxes.Count switch { 0 => DateTime.MinValue, _ => boxes.Min(box => box.ExpirationDate) }; }

        public Pallet(int height, int width, int length, string palletId) : base(height, width, length)
        {
            PalletId = palletId;
        }
        public Pallet(int height, int width, int length) : base(height, width, length)
        {
            PalletId = Guid.NewGuid().ToString();
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
            boxes.Add(box);
        }
    }
}
