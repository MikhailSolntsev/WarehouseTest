using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace WarehouseApp.Data
{
    public class Pallet : Scalable
    {
        private const int OwnWeight = 30;
        private static int nextId = 0;
        public override int Weight { get => OwnWeight + boxes.Sum(box => box.Weight); }
        public override int Volume { get => base.Volume + boxes.Sum(box => box.Volume);  }
        public override DateTime ExpirationDate { get => boxes.Count switch { 0 => DateTime.MinValue, _ => boxes.Min(box => box.ExpirationDate) }; }

        private List<Box> boxes = new();
        public IReadOnlyList<Box> Boxes { get => boxes; }

        public Pallet(int height, int width, int length) : base(height, width, length, 0)
        {
            Id = Interlocked.Increment(ref nextId);
        }
        public Pallet(int height, int width, int length, int id) : base(height, width, length, 0)
        {
            Id = id;
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
