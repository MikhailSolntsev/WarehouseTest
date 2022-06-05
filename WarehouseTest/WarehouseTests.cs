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
        [Fact(DisplayName = "Хранилище сохраняет и читает из файла корректно")]
        public void WarehouseCanSaveInFiles()
        {
            string fileName = FileHelper.RandomFileName();

            Warehouse toFile = new Warehouse(fileName);

            Pallet pallet = new(13, 17, 19);
            toFile.AddPallet(pallet);

            Box box = new(3, 5, 7, 11, DateTime.Now);
            toFile.AddBoxToPallet(pallet, box);

            Warehouse fromFile = new Warehouse(fileName);

            try
            {
                toFile.SaveToFile();
                fromFile.ReadFromFile();
            }
            finally
            {
                FileHelper.DeleteFileIfExists(fileName);
            }

            Assert.Single(fromFile.Pallets);

            Assert.Single(fromFile.Pallets.First(p => true).Boxes);
            
        }
    }
}
