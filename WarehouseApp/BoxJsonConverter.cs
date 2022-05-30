﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WarehouseApp
{
    internal class BoxJsonConverter : JsonConverter<Box>
    {
        private readonly WarehouseJsonConverter<Box> converter = new();

        public BoxJsonConverter()
        {
            converter.jsonReader = BoxFromDictioanry;
            converter.jsonWriter = JsonFromBox;
        }

        public override Box? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return converter.Read(ref reader);
        }

        public override void Write(Utf8JsonWriter writer, Box value, JsonSerializerOptions options)
        {
            converter.Write(writer, value);
        }

        private Box BoxFromDictioanry(Dictionary<string, object> dictionary)
        {
            Box box = new (
                (int)dictionary["height"],
                (int)dictionary["width"],
                (int)dictionary["length"],
                (string)dictionary["expirationDate"],
                (string)dictionary["boxId"],
                (int)dictionary["weight"]);
            return box;
        }
        private void JsonFromBox(Utf8JsonWriter writer, Box value)
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
