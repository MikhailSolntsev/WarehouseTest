using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WarehouseApp
{
    public class Scalable
    {
        public int Height { get; }
        public int Width { get; }
        public int Length { get; }

        public Scalable(int height, int width, int length)
        {
            Height = height;
            Width = width;
            Length = length;
        }
        virtual public int Volume { get { return Height * Width * Length; } }
        virtual public DateTime ExpirationDate { get; }
    }
}
