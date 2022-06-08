namespace WarehouseApp.Data.DTO;

public static class BoxExtensions
{
    public static BoxDto ToBoxDto(this Box box) => new()
    {
        Id = box.Id,
        Length = box.Length,
        Height = box.Height,
        Width = box.Width,
        Weight = box.Weight,
        ExpirationDate = box.ExpirationDate
    };

    public static Box ToBox(this BoxDto boxDto) => new(
        boxDto.Height,
        boxDto.Width,
        boxDto.Length,
        boxDto.Weight,
        boxDto.ExpirationDate,
        boxDto.Id);
}
