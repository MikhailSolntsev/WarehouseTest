using System;
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

            if (fileName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                throw new FileNotFoundException();
            }

            FileInfo fileInfo = new FileInfo(fileName);
            if (!fileInfo.Exists)
            {
                return new List<T>();
            }

            using (Stream fileStream = File.OpenRead(fileName))
            {
                values = serializer.DeserializeAsList<T>(fileStream);
            }

            return values ?? new List<T>();
        }

        public void StoreValues<T>(List<T> values)
        {
            using (Stream fileStream = File.OpenWrite(fileName))
            {
                serializer.SerializeList(fileStream, values);
            }
        }
    }
}
