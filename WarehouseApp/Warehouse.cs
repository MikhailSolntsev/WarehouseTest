using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Data;
using WarehouseApp.Data.Converters;
using WarehouseApp.Data.DTO;
using WarehouseApp.Storage;

namespace WarehouseApp
{
    public class Warehouse
    {
        private readonly string dataFileName;

        private readonly HashSet<Pallet> pallets = new();
        
        public IReadOnlySet<Pallet> Pallets{ get => pallets; }
        
        public Warehouse()
        {
            dataFileName = Path.Combine(Environment.CurrentDirectory, "warehouse.json");
        }

        public Warehouse(string dataFileName)
        {
            this.dataFileName = dataFileName;
        }

        public void SaveToFile()
        {
            FileStorage storage = new(dataFileName);

            List<PalletDto> values = new();

            foreach (var pallet in pallets)
            {
                PalletDto palletDto = DtoConverter.FromPallet(pallet);
                values.Add(palletDto);

                foreach (var box in pallet.Boxes)
                {
                    BoxDto boxDto = DtoConverter.FromBox(box);
                    palletDto.Boxes.Add(boxDto);
                }
            }

            storage.StoreValues(values);
        }

        public void ReadFromFile()
        {
            FileStorage storage = new(dataFileName);
            List<PalletDto> values = storage.ReadValues<PalletDto>();

            pallets.Clear();

            foreach (var palletDto in values)
            {
                Pallet pallet = DtoConverter.FromPalletDto(palletDto);
                pallets.Add(pallet);

                foreach (var boxDto in palletDto.Boxes)
                {
                    Box box = DtoConverter.FromBoxDto(boxDto);
                    pallet.AddBox(box);
                }
            }
        }

        public Pallet AddPallet(Pallet pallet)
        {
            if (!pallets.Contains(pallet))
            {
                pallets.Add(pallet);
            }
            return pallet;
        }
        public Box AddBoxToPallet(Pallet pallet, Box box)
        {
            if (!pallets.Contains(pallet))
            {
                pallets.Add(pallet);
            }
            if (!pallet.Boxes.Contains(box))
            {
                pallet.AddBox(box);
            }
            return box;
        }

    }
}
