using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseApp.Data.DTO
{
    public class PalletDto : ScalableDto
    {
        public List<BoxDto> Boxes { get; set; } = new();
    }
}
