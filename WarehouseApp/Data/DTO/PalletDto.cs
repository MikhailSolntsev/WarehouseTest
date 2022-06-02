using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseApp.Data.DTO
{
    public class PalletDto : BoxDto
    {
        //public int Id { get; set; }
        //public int Length { get; set; }
        //public int Width { get; set; }
        //public int Height { get; set; }
        //public int Volume { get; set; }
        //public DateTime ExpirationDate { get; set; }
        public List<BoxDto> Boxes { get; set; } = new();
        public PalletDto()
        {
        }
    }
}
