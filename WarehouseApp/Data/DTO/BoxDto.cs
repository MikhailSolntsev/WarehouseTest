using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseApp.DTO
{
    public class BoxDto
    {
        public uint Id { get; set; }
        public uint Length { get; set; }
        public uint Width { get; set; }
        public uint Height { get; set; }
        public uint Volume { get; set; }
        public DateTime ExpirationDate { get; set; }
        public BoxDto()
        {
        }
    }
}
