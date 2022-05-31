using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WarehouseApp.Converters
{
    internal class WarehouseJsonConverter<T>
    {
        public delegate T JsonReader(Dictionary<string, object> dictionary);
        public delegate void JsonWriter(Utf8JsonWriter writer, T value);

        public JsonReader jsonReader;
        public JsonWriter jsonWriter;

        public T Read(ref Utf8JsonReader reader)
        {
            Dictionary<string, object> dictionary = new();

            (string, object) pair = new();

            while (reader.Read())
            {
                switch (reader.TokenType)
                {
                    case JsonTokenType.PropertyName:
                        {
                            if (!string.IsNullOrEmpty(pair.Item1) && pair.Item2 is not null)
                            {
                                dictionary.Add(pair.Item1, pair.Item2);
                            }
                            pair = new() { Item1 = reader.GetString() ?? "" };
                            break;
                        }
                    case JsonTokenType.String:
                        {
                            pair.Item2 = reader.GetString() ?? "";
                            break;
                        }

                    case JsonTokenType.Number:
                        {
                            pair.Item2 = reader.GetInt32();
                            break;
                        }
                    case JsonTokenType.EndObject:
                        {
                            if (!string.IsNullOrEmpty(pair.Item1) && pair.Item2 is not null)
                            {
                                dictionary.Add(pair.Item1, pair.Item2);
                            }
                            pair = new();
                            return jsonReader.Invoke(dictionary);
                        }
                    default: break;
                }
            }
            if (!string.IsNullOrEmpty(pair.Item1) && pair.Item2 is not null)
            {
                dictionary.Add(pair.Item1, pair.Item2);
            }

            return jsonReader.Invoke(dictionary);
        }


        public void Write(Utf8JsonWriter writer, T value)
        {
            jsonWriter.Invoke(writer, value);
        }
    }
}
