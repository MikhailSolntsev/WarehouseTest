using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WarehouseApp
{
    internal class BoxJsonConverter : JsonConverter<Box>
    {
        public override Box? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
                            return BoxFromDictionary(dictionary);
                        }
                    default: break;
                }
            }
            if (!string.IsNullOrEmpty(pair.Item1) && pair.Item2 is not null)
            {
                dictionary.Add(pair.Item1, pair.Item2);
            }

            return BoxFromDictionary(dictionary);
        }

        private Box BoxFromDictionary(Dictionary<string, object> dictionary)
        {
            Box box = new Box(
                (int)dictionary["height"],
                (int)dictionary["width"],
                (int)dictionary["length"],
                (string)dictionary["expirationDate"],
                (string)dictionary["boxId"],
                (int)dictionary["weight"]
                );
            return box;
        }
        
        public override void Write(Utf8JsonWriter writer, Box value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("boxId", value.BoxId);
            writer.WriteString("expirationDate", value.ExpirationDate);
            writer.WriteNumber("height", value.Height);
            writer.WriteNumber("width", value.Width);
            writer.WriteNumber("length", value.Length);
            writer.WriteNumber("weight", value.Weight);
            writer.WriteEndObject();
        }
    }
}
