using Xunit;
using System;
using System.IO;
using System.Collections.Generic;
using WarehouseApp;

namespace WarehouseTest
{
    public class WarehouseTests
    {
        [Fact]
        public void WarehouseCanSaveInFiles()
        {
            string boxes = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            string palletes = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            string containers = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            Warehouse warehouse = new Warehouse(boxes, palletes, containers);

            Pallet pallet = new(13, 17, 19);
            warehouse.AddPallet(pallet);

            Box box = new(3, 5, 7, DateTime.Now);
            warehouse.AddBoxToPallet(pallet, box);

            warehouse.SaveToFiles();

            string data = File.ReadAllText(boxes);
            Assert.NotEmpty(data);

            data = File.ReadAllText(palletes);
            Assert.NotEmpty(data);

            data = File.ReadAllText(containers);
            Assert.NotEmpty(data);

            FileHelper.DeleteFileIfExists(boxes);
            FileHelper.DeleteFileIfExists(palletes);
            FileHelper.DeleteFileIfExists(containers);
        }
        [Fact]
        public void WarehouseCanReadBoxesFromFile()
        {
            string boxes = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            JsonFileStorage storage = new JsonFileStorage(boxes);

            List<Box> toWrite = new()
            {
                new(3, 5, 7, DateTime.Today) { Weight = 13 },
                new(15, 17, 19, DateTime.Today) { Weight = 23 }
            };

            storage.WriteValues(toWrite);

            Warehouse warehouse = new(boxes, "", "");

            warehouse.ReadFromFiles();

            Assert.Equal(2, warehouse.Boxes.Count);

            FileHelper.DeleteFileIfExists(boxes);
        }
        [Fact]
        public void WarehouseCanReadPalletesFromFile()
        {
            string palletes = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            JsonFileStorage storage = new JsonFileStorage(palletes);

            List<Pallet> toWrite = new()
            {
                new(3, 5, 7),
                new(15, 17, 19)
            };

            storage.WriteValues(toWrite);

            Warehouse warehouse = new("", palletes, "");

            warehouse.ReadFromFiles();

            Assert.Equal(2, warehouse.Palletes.Count);
            FileHelper.DeleteFileIfExists(palletes);
        }
        [Fact]
        public void WarehouseCantStoreBoxTwice()
        {
            Warehouse warehouse = new Warehouse("", "", "");

            Box box = new(3, 5, 7, DateTime.Now);

            warehouse.AddBox(box);
            warehouse.AddBox(box);

            Assert.Single(warehouse.Boxes);
        }
        [Fact]
        public void WarehouseCantStorePalleteTwice()
        {
            Warehouse warehouse = new Warehouse();

            Pallet pallet = new(3, 5, 7);

            warehouse.AddPallet(pallet);
            warehouse.AddPallet(pallet);

            Assert.Single(warehouse.Palletes);
        }
        [Fact]
        public void WarehousePalletCantStoreBoxTwice()
        {
            Warehouse warehouse = new Warehouse();

            Pallet pallet = new(13, 17, 19);

            warehouse.AddPallet(pallet);

            Box box = new(3, 5, 7, DateTime.Now);

            warehouse.AddBoxToPallet(pallet, box);
            warehouse.AddBoxToPallet(pallet, box);

            Assert.Single(warehouse.Boxes);
            Assert.Single(pallet.Boxes);
        }
    }
}
