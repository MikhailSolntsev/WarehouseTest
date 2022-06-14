using AutoMapper;

namespace WarehouseApp.Data.DTO;

public static class BoxExtensions
{
    public static BoxDto ToBoxDto(this Box box)
    {
        var configuration = new MapperConfiguration(cfg => cfg.CreateMap<Box, BoxDto>());
        var mapper = configuration.CreateMapper();
        return mapper.Map<BoxDto>(box);
    }

    public static Box ToBox(this BoxDto boxDto)
    {
        var configuration = new MapperConfiguration(cfg => cfg.CreateMap<BoxDto, Box>());
        var mapper = configuration.CreateMapper();
        return mapper.Map<Box>(boxDto);
    }
}
