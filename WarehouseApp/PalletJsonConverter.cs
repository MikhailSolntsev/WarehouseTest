using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WarehouseApp
{
    public class PalletJsonConverter : JsonConverter<Pallet>
    {
        private readonly WarehouseJsonConverter<Pallet> converter = new();

        public PalletJsonConverter()
        {
            converter.jsonReader = PalletFromDictionary;
            converter.jsonWriter = JsonFromPallet;
        }

        public override Pallet? Read(ref Utf8JsonReader reader,
                                      Type typeToConvert,
                                      JsonSerializerOptions options)
        {
           return converter.Read(ref reader);
        }

        public override void Write(Utf8JsonWriter writer,
                                   Pallet value,
                                   JsonSerializerOptions options)
        {
            converter.Write(writer, value);
        }

        private Pallet PalletFromDictionary(Dictionary<string, object> dictionary)
        {
            Pallet pallet= new(
                (int)dictionary["height"],
                (int)dictionary["width"],
                (int)dictionary["length"],
                (string)dictionary["palletId"]);
            return pallet;
        }
        private void JsonFromPallet(Utf8JsonWriter writer, Pallet value)
        {
            writer.WriteStartObject();

            writer.WriteString("palletId", value.PalletId);
            writer.WriteNumber("height", value.Height);
            writer.WriteNumber("width", value.Width);
            writer.WriteNumber("length", value.Length);
            
            writer.WriteEndObject();
        }
    }
}
