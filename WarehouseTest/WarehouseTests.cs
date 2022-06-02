using Xunit;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using WarehouseApp;
using WarehouseApp.Data;
using WarehouseApp.Storage;

namespace WarehouseTest
{
    public class WarehouseTests
    {
        [Fact(DisplayName ="Выборка работает красиво")]
        public void WarehouseSelectionCorrect()
        {
            Dictionary<int, Pallet> pallets = new();

            Pallet pallet = new(13, 17, 19);
            pallets.Add(pallet.Id, pallet);

            Box box = new(3, 5, 7, 11, DateTime.Now);
            pallet.AddBox(box);

            var palletsOnly = pallets.Values.ToList();

            var ids = palletsOnly
                    .Select(pallet => (pallet.Id, pallet.Boxes))
                    .Select(pair => pair.Boxes.Select(box => new PalletBoxContainer(pair.Id, box.Id)))
                    .SelectMany(list => list)
                    .ToList();

            Assert.Equal(typeof(List<PalletBoxContainer>), ids.GetType());
        }
        [Fact]
        public void WarehouseCanSaveInFiles()
        {
            string boxes = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            string pallets = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            string containers = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            Warehouse warehouse = new Warehouse(boxes, pallets, containers);

            Pallet pallet = new(13, 17, 19);
            warehouse.AddPallet(pallet);

            Box box = new(3, 5, 7, 11, DateTime.Now);
            //warehouse.AddBoxToPallet(pallet, box);

            warehouse.SaveToFiles();
            try
            {
                string data = File.ReadAllText(boxes);
                Assert.NotEmpty(data);

                data = File.ReadAllText(pallets);
                Assert.NotEmpty(data);

                data = File.ReadAllText(containers);
                Assert.NotEmpty(data);
            }
            finally
            {
                FileHelper.DeleteFileIfExists(boxes);
                FileHelper.DeleteFileIfExists(pallets);
                FileHelper.DeleteFileIfExists(containers);
            }
        }
        [Fact]
        public void WarehouseCanReadBoxesFromFile()
        {
            string boxes = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            JsonFileStorage storage = new JsonFileStorage(boxes);

            List<Box> toWrite = new()
            {
                new(3, 5, 7, 0, DateTime.Today),
                new(13, 17, 19, 0, DateTime.Today)
            };

            storage.WriteValues(toWrite);

            Warehouse warehouse = new(boxes, "", "");

            warehouse.ReadFromFiles();

            Assert.Equal(2, warehouse.Boxes.Count);

            FileHelper.DeleteFileIfExists(boxes);
        }
        [Fact]
        public void WarehouseCanReadPalletsFromFile()
        {
            string pallets = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            JsonFileStorage storage = new JsonFileStorage(pallets);

            List<Pallet> toWrite = new()
            {
                new(3, 5, 7),
                new(15, 17, 19)
            };

            storage.WriteValues(toWrite);

            Warehouse warehouse = new("", pallets, "");

            warehouse.ReadFromFiles();

            Assert.Equal(2, warehouse.Pallets.Count);
            FileHelper.DeleteFileIfExists(pallets);
        }
        [Fact]
        public void WarehouseCanReadContainersFromFile()
        {
            string boxes = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            string pallets = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            string containers = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            JsonFileStorage storage = new JsonFileStorage(pallets);
            Pallet pallet = new(15, 17, 19);
            List<Pallet> palletsToWrite = new() { pallet };
            storage.WriteValues(palletsToWrite);

            storage = new JsonFileStorage(boxes);
            Box box = new(3, 7, 5, 0, DateTime.Today);
            List<Box> boxesToWrite = new() { box };
            storage.WriteValues(boxesToWrite);

            storage = new JsonFileStorage(containers);
            List<PalletBoxContainer> containersToWrite = new() { new(pallet.Id, box.Id) };
            storage.WriteValues(containersToWrite);

            Warehouse warehouse = new(boxes, pallets, containers);

            warehouse.ReadFromFiles();

            Assert.Single(warehouse.Pallets);
            Assert.Single(warehouse.Boxes);
            //Assert.Equal(1, warehouse.Pallets.Count);

            FileHelper.DeleteFileIfExists(boxes);
            FileHelper.DeleteFileIfExists(pallets);
            FileHelper.DeleteFileIfExists(containers);
        }
        [Fact]
        public void WarehouseCantStoreBoxTwice()
        {
            Warehouse warehouse = new Warehouse("", "", "");

            Box box = new(3, 5, 7, 11, DateTime.Now);

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

            Assert.Single(warehouse.Pallets);
        }
        [Fact]
        public void WarehousePalletCantStoreBoxTwice()
        {
            Warehouse warehouse = new Warehouse();

            Pallet pallet = new(13, 17, 19);

            warehouse.AddPallet(pallet);

            Box box = new(3, 5, 7, 11, DateTime.Now);

            warehouse.AddBoxToPallet(pallet, box);
            warehouse.AddBoxToPallet(pallet, box);

            Assert.Single(warehouse.Boxes);
            Assert.Single(pallet.Boxes);
        }
    }
}
