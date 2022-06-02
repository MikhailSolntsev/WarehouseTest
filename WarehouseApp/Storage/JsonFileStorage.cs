using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WarehouseApp.Storage
{
    public class JsonFileStorage
    {
        private string fileName;
        public JsonFileStorage(string fileName)
        {
            this.fileName = fileName;
        }

        public List<T> ReadValues<T>()
        {
            List<T>? result = null;

            using (Stream fileStream = File.OpenRead(fileName))
            {
                if (fileStream != null)
                {
                    JsonSerializerOptions options = SerializerOptions();
                    result = System.Text.Json.JsonSerializer.Deserialize<List<T>>(fileStream, options);
                }
            }

            return result ?? new List<T>();
        }

        public void WriteValues<T>(List<T> value)
        {
            using (Stream fileStream = File.OpenWrite(fileName))
            {
                JsonSerializerOptions options = SerializerOptions();
                System.Text.Json.JsonSerializer.Serialize(fileStream, value, options);
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
