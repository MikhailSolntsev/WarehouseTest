using WarehouseApp;
using static System.Console;

Warehouse warehouse = PrepareCollection();

WriteList(warehouse.Pallets.Values);

GroupByDateSortByDateByWeight(warehouse);

ThreePalletsWithMaximumExpirationDate(warehouse);

void GroupByDateSortByDateByWeight(Warehouse warehouse)
{
    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine("Grouped and sorted by expiration date and sorted in groups by weight");

    var pallets = warehouse.Pallets.Values;

    var result = pallets
        .GroupBy(pallet => pallet.ExpirationDate)
        .OrderBy(group => group.Key)
        .Select(group => new { Date = group.Key, Pallets = group.OrderBy(pallet => pallet.Weight) })
        ;

    foreach (var group in result)
    {
        Console.WriteLine($"Group expiration date: {group.Date}");
        foreach (var pallet in group.Pallets)
        {
            pallet.WritePallet("   ");
            //foreach (var box in pallet.Boxes)
            //{
            //    box.WriteBox("      ");
            //}
        }
    }

}

void ThreePalletsWithMaximumExpirationDate(Warehouse warehouse)
{
    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine("Three pallets with maximum expiration dates sorted by weight");

    var pallets = warehouse.Pallets.Values;

    var result = pallets
        .OrderBy(pallet => pallet.ExpirationDate)
        .Take(3)
        .OrderBy(pallet => pallet.Weight);

    WriteList(result);

}

static Warehouse PrepareCollection()
{
    Warehouse warehouse = new Warehouse();

    warehouse.ReadFromFiles();

    if (warehouse.Boxes.Count == 0)
        CreateCollection(warehouse);

    warehouse.SaveToFiles();

    return warehouse;
}

static void CreateCollection(Warehouse warehouse)
{
    const int maxPallets = 4;
    const int maxDates = 10;

    List<DateTime> dates = Enumerable
        .Range(1, maxDates)
        .Select(value => DateTime.Today + TimeSpan.FromDays(-value))
        .ToList();

    List<Pallet> pallets = new();
    for (int i = 0; i < maxPallets; i++)
    {
        pallets.Add(new(23, 29, 31));
    }

    Random random = new Random();

    for (int i = 0; i < dates.Count; i++)
    { 
        int randomPallet = random.Next(maxPallets);
        Pallet pallet = pallets[randomPallet];

        int randomDate = random.Next(maxDates);
        Box box = new(2, 3, 4, dates[randomDate]) { Weight = random.Next(10, 20)};
        warehouse.AddBoxToPallet(pallet, box);
    }
}

static void WriteList(IEnumerable<Pallet> pallets)
{
    WriteLine("Pallets and their boxes:");

    foreach (Pallet pallet in pallets)
    {
        pallet.WritePallet();

        foreach (Box box in pallet.Boxes)
        {
            box.WriteBox("   ");
        }
    }
}