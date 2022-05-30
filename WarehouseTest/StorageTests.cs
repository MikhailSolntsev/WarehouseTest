using Xunit;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
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

            JsonFileStorage storage = new JsonFileStorage(fileName);

            storage.WriteValues(values);

            string text = File.ReadAllText(fileName);

            Assert.Equal("[7]", text);

            FileHelper.DeleteFileIfExists(fileName);
        }
        [Fact]
        public void CanWriteObjectToFile()
        {
            string fileName = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            List<SimpleData> values = new()
            {
                new() { Name = "John", Value = 10 }
            };

            JsonFileStorage storage = new JsonFileStorage(fileName);

            storage.WriteValues(values);

            string text = File.ReadAllText(fileName);

            Assert.Equal("[{\"name\":\"John\",\"value\":10}]", text);

            FileHelper.DeleteFileIfExists(fileName);
        }

        [Fact]
        public void CanReadIntFromFile()
        {
            string fileName = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            JsonFileStorage storage = new JsonFileStorage(fileName);

            FileHelper.WriteStringToFile(fileName, "[7]");
            
            var values = storage.ReadValues<int>();

            Assert.Equal(typeof(List<int>), values.GetType());

            Assert.Single(values);

            Assert.Equal(7, values[0]);

            FileHelper.DeleteFileIfExists(fileName);
        }
        [Fact]
        public void CanReadObjectFromFile()
        {
            string fileName = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            JsonFileStorage storage = new JsonFileStorage(fileName);

            FileHelper.WriteStringToFile(fileName, "[{\"name\":\"John\",\"value\":10}]");

            var values = storage.ReadValues<SimpleData>();

            SimpleData sample = new() { Name = "John", Value = 10 };

            Assert.Equal(typeof(List<SimpleData>), values.GetType());

            Assert.Single(values);

            Assert.Equal(sample, values[0]);

            FileHelper.DeleteFileIfExists(fileName);
        }

        [Fact]
        public void BoxCanBereaded()
        {
            string fileName = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            JsonFileStorage storage = new JsonFileStorage(fileName);

            List<Box> toWrite = new()
            {
                new(3, 5, 7, DateTime.Today) { Weight = 13 },
                new(15, 17, 19, DateTime.Today) { Weight = 23 }
            };

            storage.WriteValues(toWrite);

            string text = File.ReadAllText(fileName);

            List<Box> readed = storage.ReadValues<Box>();

            Assert.Equal(2, readed.Count);

            Assert.Equal(5, readed[0].Width);

            Assert.Equal(23, readed[1].Weight);

            FileHelper.DeleteFileIfExists(fileName);
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

            if (fileContent == null)
            {
                throw new NullReferenceException("File content is null");
            }

            Dictionary<string, object> dictionary = fileContent[0];

            foreach (string key in dictionary.Keys)
            {
                Assert.Contains(key, possibleKeys);
            }

            FileHelper.DeleteFileIfExists(fileName);
        }

        private void StorageStoresAllMainFields(List<string> possibleKeys, Action<string> writeSampleAction)
        {
            string fileName = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            writeSampleAction(fileName);

            List<Dictionary<string, object>>? fileContent = ReadSampleFromFile(fileName);
            
            if (fileContent is null)
            {
                throw new NullReferenceException("File content is null");
            }
            
            Dictionary<string, object> dictionary = fileContent[0];

            foreach (string key in possibleKeys)
            {
                Assert.Contains(key, dictionary.Keys);
            }

            FileHelper.DeleteFileIfExists(fileName);
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
            JsonFileStorage storage = new JsonFileStorage(fileName);

            List<Box> toWrite = new() { new(3, 5, 7, DateTime.Today) { Weight = 13 } };

            storage.WriteValues(toWrite);
        }

        private static void WriteSamplePallet(string fileName)
        {
            JsonFileStorage storage = new JsonFileStorage(fileName);

            List<Pallet> toWrite = new() { new(3, 5, 7) };

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
                "palletId"
            };
        }

    }

    internal class SimpleData
    {
        public string? Name { get; set; }
        public int Value { get; set; }

        public override bool Equals(object? obj)
        {
            SimpleData? second = obj as SimpleData;
            if (string.IsNullOrEmpty(Name))
            {
                return false;
            }
            return Name.Equals(second?.Name) && Value == second.Value;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
