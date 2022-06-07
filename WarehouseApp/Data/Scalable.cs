using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WarehouseApp.Data
{
    public class Scalable
    {
        public virtual int Id { get; protected set; }
        public int Height { get; }
        public int Width { get; }
        public int Length { get; }
        public virtual int Weight { get; }
        public virtual int Volume { get { return Height * Width * Length; } }
        public virtual DateTime ExpirationDate { get; protected set; }
        public Scalable(int height, int width, int length, int weight)
        {
            Height  = height;
            Width   = width;
            Length  = length;
            Weight  = weight;
        }

        public override int GetHashCode() => Id;
        public override bool Equals(object? obj) => obj is Scalable &&  ((Scalable)obj).Id == Id;
    }
}
