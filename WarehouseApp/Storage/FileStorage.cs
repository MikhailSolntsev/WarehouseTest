﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseApp.Storage
{
    public class FileStorage : IStorage
    {
        private IStreamSerializer serializer;
        private string fileName = "";

        public FileStorage(string fileName, IStreamSerializer serializer)
        {
            this.serializer = serializer;
            this.fileName = fileName;
        }
        public FileStorage(string fileName)
        {
            this.fileName = fileName;
            serializer = new JsonStreamSerializer();
        }

        public List<T> ReadValues<T>()
        {
            List<T>? values = null;

            using (Stream fileStream = File.OpenRead(fileName))
            {
                values = serializer.Deserialize<T>(fileStream);
            }

            return values ?? new List<T>();
        }

        public void StoreValues<T>(List<T> values)
        {
            using (Stream fileStream = File.OpenWrite(fileName))
            {
                serializer.Serialize(fileStream, values);
            }
        }
    }
}