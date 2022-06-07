namespace WarehouseApp.Data.DTO
{
    public static class PalletExtensions
    {
        public static PalletDto ToPalletDto(this Pallet pallet)
        {
            PalletDto palletDto = new();
            palletDto.Id = pallet.Id;
            palletDto.Length = pallet.Length;
            palletDto.Height = pallet.Height;
            palletDto.Width = pallet.Width;

            return palletDto;
        }

        public static Pallet ToPallet(this PalletDto palletDto)
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
