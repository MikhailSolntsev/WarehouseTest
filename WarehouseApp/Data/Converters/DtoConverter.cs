using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Data.DTO;

namespace WarehouseApp.Data.Converters
{
    public class DtoConverter
    {
        public static BoxDto FromBox(Box box)
        {
            BoxDto boxDto = new();
            boxDto.Id = box.Id;
            boxDto.Length = box.Length;
            boxDto.Height = box.Height;
            boxDto.Width = box.Width;
            boxDto.Weight = box.Weight;
            boxDto.ExpirationDate = box.ExpirationDate;

            return boxDto;
        }
        public static Box FromBoxDto(BoxDto boxDto)
        {
            Box box = new(
                boxDto.Height,
                boxDto.Width,
                boxDto.Length,
                boxDto.Weight,
                boxDto.ExpirationDate,
                boxDto.Id);

            return box;
        }
        public static PalletDto FromPallet(Pallet pallet)
        {
            PalletDto palletDto = new();
            palletDto.Id = pallet.Id;
            palletDto.Length = pallet.Length;
            palletDto.Height = pallet.Height;
            palletDto.Width = pallet.Width;

            return palletDto;
        }
        public static Pallet FromPalletDto(PalletDto palletDto)
        {
            Pallet pallet = new(
                palletDto.Height,
                palletDto.Width,
                palletDto.Length,
                palletDto.Id);

            return pallet;
        }
    }
}
