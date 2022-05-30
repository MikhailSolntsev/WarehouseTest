using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace WarehouseApp
{
    [JsonConverter(typeof(BoxJsonConverter))]
    public class Box : Scalable
    {
        private const int period = 100;

        private DateTime? expirationDate;
        private DateTime? productionDate { get; set; }
        
        public string BoxId { get; }
        public int Weight { get; set; }

        public override DateTime ExpirationDate { get => productionDate switch { null => (DateTime?)expirationDate?? DateTime.Today, _ => (DateTime)productionDate + TimeSpan.FromDays(period) }; }

        public Box(int height, int width, int length, DateTime? bestBefore = null, DateTime? produced = null) : base(height, width, length)
        {
            if (bestBefore is null && produced is null)
            {
                string message = $"Both parametres \"produced\" and \"bestBefore\" is null";
                throw new ArgumentNullException(message);
            }
            this.expirationDate = bestBefore;
            productionDate = produced;

            BoxId = Guid.NewGuid().ToString();
        }

        [JsonConstructor]
        public Box(int height, int width, int length, DateTime? expirationDate, string boxId, int weight) : base(height, width, length)
        {
            this.expirationDate = expirationDate;
            this.BoxId = boxId;
            Weight = weight;
        }
        [JsonConstructor]
        public Box(int height, int width, int length, string expirationDate, string boxId, int weight) : base(height, width, length)
        {
            this.expirationDate = DateTime.Parse(expirationDate);
            this.BoxId = boxId;
            Weight = weight;
        }
    }
}
