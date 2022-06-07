using Xunit;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using WarehouseApp.Data;
using WarehouseApp.Storage;
using FluentAssertions;

namespace WarehouseTest
{
    public class StorageTests
    {
        [Fact(DisplayName = "Cериализация и десериализация произвольных объектов List<T>")]
        public void CanWriteAndReadObjectWithJsonStream()
        {
            var written = SimpleData.SampleList();

            JsonStreamSerializer storage = new();

            using (MemoryStream stream = new())
            {
                storage.Serialize(stream, written);

                stream.Seek(0, SeekOrigin.Begin);

                List<SimpleData> readed = storage.Deserialize<SimpleData>(stream);

                Assert.True(readed.SequenceEqual(written), "Written an readed arrays are different");
            }
        }

        [Fact(DisplayName = "Сохранение и чтение произвольных объектов в файл")]
        public void CanWriteAndReadObjectWithFile()
        {
            string fileName = FileHelper.RandomFileName();

            FileStorage fileStorage = new FileStorage(fileName);

            var written = SimpleData.SampleList();

            try
            {
                fileStorage.StoreValues(written);

                List<SimpleData> readed = fileStorage.ReadValues<SimpleData>();

                Assert.True(readed.SequenceEqual(written), "Written an readed arrays are different");
            }
            finally
            {
                FileHelper.DeleteFileIfExists(fileName);
            }
        }

        [Fact(DisplayName = "Writing in file with incorrect name should throw exception")]
        public void WritingWithWrongFileCauseException()
        {
            string fileName = "abirbalg*";

            FileStorage fileStorage = new FileStorage(fileName);

            var written = SimpleData.SampleList();

            Action action = () => fileStorage.StoreValues(written);

            action.Should().Throw<Exception>("Wrong filename does not throw any exception");
            
        }

        [Fact(DisplayName = "Чтение из некорректного файла бросает исключение")]
        public void ReadingWithWrongFileCauseException()
        {
            string fileName = "abirbalg*";

            FileStorage fileStorage = new FileStorage(fileName);

            try
            {
                List<SimpleData> readed = fileStorage.ReadValues<SimpleData>();
                Assert.True(false, "Wrong filename does not throw any exception");
            }
            catch
            {
                
            }
        }

        [Fact(DisplayName = "Чтение из пустого файлы не вызывает исключения, возвращает пустой список")]
        public void CorrectReadintFromNonexistingFile()
        {
            string fileName = FileHelper.RandomFileName();

            FileStorage fileStorage = new FileStorage(fileName);

            var values = fileStorage.ReadValues<Pallet>();
        }
    }

    internal class SimpleData
    {
        public string? Name { get; set; }
        public int Value { get; set; }

        public static List<SimpleData> SampleList()
        {
            List<SimpleData> sampleList = new()
            {
                new() { Name = "John Doe", Value = 13 },
                new() { Name = "Jane Smith", Value = 151 }
            };
            return sampleList;
        }
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
