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
        public override Pallet Read(ref Utf8JsonReader reader,
                                      Type typeToConvert,
                                      JsonSerializerOptions options)
        {
            var name = reader.GetString();

            var source = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(name);

            var category = new Pallet(0, 0, 0);

            var categoryType = category.GetType();
            var categoryProps = categoryType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var s in source.Keys)
            {
                var categoryProp = categoryProps.FirstOrDefault(x => x.Name == s);

                if (categoryProp != null)
                {
                    var value = JsonSerializer.Deserialize(source[s].GetRawText(), categoryProp.PropertyType);

                    categoryType.InvokeMember(categoryProp.Name,
                        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty | BindingFlags.Instance,
                        null,
                        category,
                        new object[] { value });
                }
            }

            return category;
        }

        public override void Write(Utf8JsonWriter writer,
                                   Pallet value,
                                   JsonSerializerOptions options)
        {
            var props = value.GetType()
                             .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                             .ToDictionary(x => x.Name, x => x.GetValue(value));

            var ser = JsonSerializer.Serialize(props);

            writer.WriteStringValue(ser);
        }
    }
}
