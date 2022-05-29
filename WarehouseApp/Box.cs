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

        private DateTime? expirationDate;
        private DateTime? productionDate { get; set; }
        
        public int BoxId { get; set; }
        public int Weight { get; set; }

        public override DateTime ExpirationDate { get => productionDate switch { null => (DateTime)expirationDate, _ => (DateTime)productionDate + TimeSpan.FromDays(period) }; }

        public Box(int height, int width, int length, DateTime? bestBefore = null, DateTime? produced = null) : base(height, width, length)
        {
            if (bestBefore is null && produced is null)
            {
                string message = $"Both parametres \"produced\" and \"bestBefore\" is null";
                throw new ArgumentNullException(message);
            }
            this.expirationDate = bestBefore;
            productionDate = produced;

        }
    }
}
