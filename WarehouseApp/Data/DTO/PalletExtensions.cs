namespace WarehouseApp.Data.DTO
{
    public static class PalletExtensions
    {
        public static PalletDto ToPalletDto(this Pallet pallet) => new() {
            Id = pallet.Id,
            Length = pallet.Length,
            Height = pallet.Height,
            Width = pallet.Width};

        public static Pallet ToPallet(this PalletDto palletDto) => new(
            palletDto.Height,
            palletDto.Width,
            palletDto.Length,
            palletDto.Id);
    }
}
