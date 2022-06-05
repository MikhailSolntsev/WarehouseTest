using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseApp.Data.DTO
{
    public class BoxDto : ScalableDto
    {
        public DateTime ExpirationDate { get; set; }
        public BoxDto()
        {
        }
    }
}
