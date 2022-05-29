using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WarehouseApp
{
    public class JsonFileStorage: IStorage
    {
        private string fileName;
        public JsonFileStorage(string fileName)
        {
            this.fileName = fileName;
        }

        public List<T> ReadValues<T>()
        {
            List<T> result;

            using (Stream fileStream = File.OpenRead(fileName))
            {
                JsonSerializerOptions options = SerializerOptions();
                result = JsonSerializer.Deserialize<List<T>>(fileStream, options);
            }

            return result ?? new List<T>();
        }
        
        public void WriteValues<T>(List<T> value)
        {
            using(Stream fileStream = File.OpenWrite(fileName))
            {
                JsonSerializerOptions options = SerializerOptions();
                JsonSerializer.Serialize(fileStream, value, options);
            }
        }

        private JsonSerializerOptions SerializerOptions()
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                IncludeFields = true,
                PropertyNameCaseInsensitive = true,
                WriteIndented = false,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            return options;
        }
    }
}
