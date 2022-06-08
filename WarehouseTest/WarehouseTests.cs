using Xunit;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using WarehouseApp;
using WarehouseApp.Data;
using WarehouseApp.Storage;
using FluentAssertions;

namespace WarehouseTest
{
    public class WarehouseTests
    {
        [Fact(DisplayName = "Storage write and read the same file correctly")]
        public void WarehouseCanSaveInFiles()
        {
            // Arrange
            string fileName = FileHelper.RandomFileName();
            Warehouse toFile = new Warehouse(fileName);

            Pallet pallet = new(13, 17, 19);
            toFile.AddPallet(pallet);

            Box box = new(3, 5, 7, 11, DateTime.Now);
            toFile.AddBoxToPallet(pallet, box);

            Warehouse fromFile = new Warehouse(fileName);

            // Act
            try
            {
                toFile.SaveToFile();
                fromFile.ReadFromFile();
            }
            finally
            {
                FileHelper.DeleteFileIfExists(fileName);
            }

            // Assert
            Assert.Single(fromFile.Pallets);
            Assert.Single(fromFile.Pallets.First(p => true).Boxes);
            
        }
        [Fact(DisplayName ="Warehouse do not add second same pallet")]
        public void WarehouseDontAddSecondPallet()
        {
            // Arrange
            Warehouse warehouse = new(FileHelper.RandomFileName());
            Pallet pallet1 = new(13, 17, 19, 153);
            Pallet pallet2 = new(13, 17, 19, 153);

            // Act
            warehouse.AddPallet(pallet1);
            warehouse.AddPallet(pallet2);

            // Assert
            warehouse.Pallets.Should().HaveCount(1, "Warehouse should not contains two identical pallets");
        }
    }
}
