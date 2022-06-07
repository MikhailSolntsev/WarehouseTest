namespace WarehouseApp.Data.DTO;

public static class BoxExtensions
{
    public static BoxDto ToBoxDto(this Box box)
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

    public static Box ToBox(this BoxDto boxDto)
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
}
