using Xunit;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq.Expressions;
using System.Linq;
using WarehouseApp;

namespace WarehouseTest
{
    public class StorageTests
    {
        [Fact]
        public void CanWriteIntToFile()
        {
            string fileName = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            List<int> values = new List<int>() { 7 };

            IStorage storage = new JsonFileStorage(fileName);

            storage.WriteValues(values);

            string text = File.ReadAllText(fileName);

            Assert.Equal("[7]", text);

            DeleteFileIfExists(fileName);
        }
        [Fact]
        public void CanWriteObjectToFile()
        {
            string fileName = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            List<SimpleData> values = new()
            {
                new() { Name = "John", Value = 10 }
            };

            IStorage storage = new JsonFileStorage(fileName);

            storage.WriteValues(values);

            string text = File.ReadAllText(fileName);

            Assert.Equal("[{\"name\":\"John\",\"value\":10}]", text);

            DeleteFileIfExists(fileName);
        }

        [Fact]
        public void CanReadIntFromFile()
        {
            string fileName = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            IStorage storage = new JsonFileStorage(fileName);

            WriteStringToFile(fileName, "[7]");
            
            var values = storage.ReadValues<int>();

            Assert.Equal(typeof(List<int>), values.GetType());

            Assert.Single(values);

            Assert.Equal(7, values[0]);

            DeleteFileIfExists(fileName);
        }
        [Fact]
        public void CanReadObjectFromFile()
        {
            string fileName = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            IStorage storage = new JsonFileStorage(fileName);

            WriteStringToFile(fileName, "[{\"name\":\"John\",\"value\":10}]");

            var values = storage.ReadValues<SimpleData>();

            SimpleData sample = new() { Name = "John", Value = 10 };

            Assert.Equal(typeof(List<SimpleData>), values.GetType());

            Assert.Single(values);

            Assert.Equal(sample, values[0]);

            DeleteFileIfExists(fileName);
        }
        [Fact]
        public void BoxStoresOnlyMainFields()
        {
            var possibleKeys  = BoxMainFields();
            StorageStoresOnlyMainFields(possibleKeys, WriteSampleBox);
        }
        [Fact]
        public void BoxStoresAllMainFields()
        {
            var possibleKeys = BoxMainFields();
            StorageStoresAllMainFields(possibleKeys, WriteSampleBox);
        }
        [Fact]
        public void PalletStoresOnlyMainFields()
        {
            var possibleKeys = PalletMainFields();
            StorageStoresOnlyMainFields(possibleKeys, WriteSamplePallet);
        }
        [Fact]
        public void PalletStoresAllMainFields()
        {
            var possibleKeys = PalletMainFields();
            StorageStoresAllMainFields(possibleKeys, WriteSamplePallet);
        }
        private void StorageStoresOnlyMainFields(List<string> possibleKeys, Action<string> writeSampleAction)
        {
            string fileName = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            writeSampleAction(fileName);
            
            List<Dictionary<string, object>>? fileContent = ReadSampleFromFile(fileName);

            Dictionary<string, object> dictionary = fileContent[0];

            foreach (string key in dictionary.Keys)
            {
                Assert.Contains(key, possibleKeys);
            }

            DeleteFileIfExists(fileName);
        }

        private void StorageStoresAllMainFields(List<string> possibleKeys, Action<string> writeSampleAction)
        {
            string fileName = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            writeSampleAction(fileName);

            List<Dictionary<string, object>>? fileContent = ReadSampleFromFile(fileName);

            Dictionary<string, object> dictionary = fileContent[0];

            foreach (string key in possibleKeys)
            {
                Assert.Contains(key, dictionary.Keys);
            }

            DeleteFileIfExists(fileName);
        }

        private static List<Dictionary<string, object>>? ReadSampleFromFile(string fileName)
        {
            string json = File.ReadAllText(fileName);

            JsonDocument document = JsonDocument.Parse(json);

            var rawFileContent = document.RootElement.Deserialize(typeof(List<Dictionary<string, object>>));

            Assert.NotNull(rawFileContent);

            var fileContent = rawFileContent as List<Dictionary<string, object>>;

            return fileContent;
        }

        private static void WriteSampleBox(string fileName)
        {
            IStorage storage = new JsonFileStorage(fileName);

            List<Box> toWrite = new() { new(3, 5, 7, DateTime.Today) { BoxId = 11, Weight = 13 } };

            storage.WriteValues(toWrite);
        }

        private static void WriteSamplePallet(string fileName)
        {
            IStorage storage = new JsonFileStorage(fileName);

            List<Pallet> toWrite = new() { new(3, 5, 7) { PalletId = 11} };

            storage.WriteValues(toWrite);
        }

        private List<string> BoxMainFields()
        {
            return new List<string>()
            {
                "length",
                "width",
                "height",
                "boxId",
                "weight",
                "expirationDate"
            };
        }

        private List<string> PalletMainFields()
        {
            return new List<string>()
            {
                "length",
                "width",
                "height",
                "palletId",
                "boxes"
            };
        }

        private void DeleteFileIfExists(string fileName)
        {
            FileInfo file = new FileInfo(fileName);
            if (!file.Exists)
            {
                File.Delete(file.FullName);
            }
        }

        private void WriteStringToFile(string fileName, string value)
        {
            StreamWriter text = File.CreateText(fileName);
            text.Write(value);
            text.Close();
        }

    }

    internal class SimpleData
    {
        public string Name { get; set; }
        public int Value { get; set; }

        public override bool Equals(object? obj)
        {
            SimpleData second = obj as SimpleData;

            return Name.Equals(second.Name) && Value == second.Value;
        }
    }

    internal class Storage
    {
        private Dictionary<int, Box> dict;

        public void Store()
        {
            IStorage storage = new JsonFileStorage("");
            storage.WriteValues(dict.Values.ToList());
        }
    }
}
