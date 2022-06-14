using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WarehouseApp.Data;

namespace WarehouseApp.Storage;

public class JsonStreamSerializer : IStreamSerializer
{
    public List<T> DeserializeAsList<T>(Stream stream)
    {
        List<T>? result = null;

        if (stream != null)
        {
            JsonSerializerOptions options = SerializerOptions();
            result = JsonSerializer.Deserialize<List<T>>(stream, options);
        }
        
        return result ?? new List<T>();
    }

    public void SerializeList<T>(Stream stream, List<T> value)
    {
        JsonSerializerOptions options = SerializerOptions();
        JsonSerializer.Serialize(stream, value, options);
    }

    private JsonSerializerOptions SerializerOptions()
    {
        JsonSerializerOptions options = new JsonSerializerOptions()
        {
            IncludeFields = true,
            PropertyNameCaseInsensitive = true,
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        return options;
    }
}
