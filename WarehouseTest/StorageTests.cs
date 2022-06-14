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
        [Fact(DisplayName = "Serialization and deserialization of List<Any>")]
        public void CanWriteAndReadObjectWithJsonStream()
        {
            // Assign
            var written = SimpleData.SampleList();
            JsonStreamSerializer storage = new();

            using (MemoryStream stream = new())
            {
                // Act
                storage.SerializeList(stream, written);
                stream.Seek(0, SeekOrigin.Begin);
                List<SimpleData> readed = storage.DeserializeAsList<SimpleData>(stream);

                // Assert
                Assert.True(readed.SequenceEqual(written), "Written an readed collections are different");
            }
        }

        [Fact(DisplayName = "Reading and writing List<Any> in/from file")]
        public void CanWriteAndReadObjectWithFile()
        {
            // Assign
            string fileName = FileHelper.RandomFileName();
            FileStorage fileStorage = new FileStorage(fileName);
            var written = SimpleData.SampleList();
            try
            {
                // Act
                fileStorage.StoreValues(written);
                List<SimpleData> readed = fileStorage.ReadValues<SimpleData>();

                // Assert
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
            // Assign
            string fileName = "abirbalg*";
            FileStorage fileStorage = new FileStorage(fileName);
            var written = SimpleData.SampleList();

            // Act
            Action action = () => fileStorage.StoreValues(written);

            // Assert
            action.Should().Throw<Exception>("Wrong filename does not throw any exception");
        }

        [Fact(DisplayName = "Reading from file with incorrect name should throw exception")]
        public void ReadingWithWrongFileCauseException()
        {
            // Assign
            string fileName = "abirbalg*";
            FileStorage fileStorage = new FileStorage(fileName);

            // Act
            Action action = () => fileStorage.ReadValues<SimpleData>();

            // Assert
            action.Should().Throw<Exception>("Wrong filename should throw exception");
        }

        [Fact(DisplayName = "Reading from unexisted file should not throw any exception")]
        public void ReadingFromUnexistedFileShoulNotCauseExceptions()
        {
            // Assign
            string fileName = FileHelper.RandomFileName();
            FileStorage fileStorage = new FileStorage(fileName);

            // Act
            Func<List<Pallet>> action = () => fileStorage.ReadValues<Pallet>();

            // Assert
            action.Should().NotThrow<Exception>("Reading from empty file should not throw any exceptions");
        }

        [Fact(DisplayName = "Reading from unexisted file should return empty list")]
        public void ReadingFromUnexistedFileShoulReturnEmptyList()
        {
            // Assign
            string fileName = FileHelper.RandomFileName();
            FileStorage fileStorage = new FileStorage(fileName);

            // Act
            var values = fileStorage.ReadValues<Pallet>();

            // Assert
            values.Should()
                .BeOfType<List<Pallet>>("Reading from unexisted file should return list")
                .And
                .BeEmpty("Reading from unexisted file should return EMPTY list");
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
