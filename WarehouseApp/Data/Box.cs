using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace WarehouseApp.Data
{
    public class Box : Scalable
    {
        private const int Period = 100;
        static private int nextId;
        
        public Box(int height, int width, int length, int weight, DateTime? expirationDate = null, DateTime? produced = null) : base(height, width, length, weight)
        {
            if (expirationDate is null && produced is null)
            {
                string message = $"Both parametres \"produced\" and \"bestBefore\" is null";
                throw new ArgumentNullException(message);
            }

            ExpirationDate = expirationDate ?? produced + TimeSpan.FromDays(Period) ?? DateTime.Today;
            Id = Interlocked.Increment(ref nextId);
        }
        public Box(int height, int width, int length, int weight, DateTime expirationDate, int id) : base(height, width, length, weight)
        {
            ExpirationDate = expirationDate;
            Id = id;
        }
    }
}
