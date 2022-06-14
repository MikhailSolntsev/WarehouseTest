using AutoMapper;

namespace WarehouseApp.Data.DTO
{
    public static class PalletExtensions
    {
        public static PalletDto ToPalletDto(this Pallet pallet)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Pallet, PalletDto>()
                    .ForMember(pallet => pallet.Boxes, opt => opt.Ignore());
            });
            var mapper = configuration.CreateMapper();
            return mapper.Map<PalletDto>(pallet);
        }
        
        public static Pallet ToPallet(this PalletDto palletDto)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PalletDto, Pallet>()
                    .ForMember(pallet => pallet.Boxes, opt => opt.Ignore());
            });
            var mapper = configuration.CreateMapper();
            return mapper.Map<Pallet>(palletDto);
        }
    }
}
