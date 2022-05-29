using Xunit;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using WarehouseApp;

namespace WarehouseTest
{
    public class StorageTests
    {
        [Fact]
        public void CanWriteIntToFile()
        {
            string fileName = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            List<int> values = new() { 7 };

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
}
