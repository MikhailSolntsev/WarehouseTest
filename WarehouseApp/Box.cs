using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseApp
{
    public class Box : Scalable
    {
        private const int period = 100;
        private DateTime? bestBefore;
        public DateTime? Produced { get; set; }

        public override DateTime ExpirationDate { get => Produced switch { null => (DateTime)bestBefore, _ => (DateTime)Produced + TimeSpan.FromDays(period) }; }
        public int BoxId { get; set; }

        public Box(int height, int width, int length, DateTime? bestBefore = null, DateTime? produced = null) : base(height, width, length)
        {
            if (bestBefore is null && produced is null)
            {
                string message = $"Both parametres \"produced\" and \"bestBefore\" is null";
                throw new ArgumentNullException(message);
            }
            this.bestBefore = bestBefore;
            Produced = produced;

        }
        
    }
}
