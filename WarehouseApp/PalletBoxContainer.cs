using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WarehouseApp
{
    public class PalletBoxContainer
    {
        [JsonInclude]
        public int PalletId { get; set; }
        [JsonInclude]
        public int BoxId { get; set; }

        public PalletBoxContainer(int palletId, int boxId)
        {
            PalletId = palletId;
            BoxId = boxId;
        }
    }
}
