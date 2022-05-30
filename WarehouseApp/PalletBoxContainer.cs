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
        public string PalletId { get; set; }
        [JsonInclude]
        public string BoxId { get; set; }

        public PalletBoxContainer(string palletId, string boxId)
        {
            PalletId = palletId;
            BoxId = boxId;
        }
    }
}
