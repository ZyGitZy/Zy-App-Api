using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zy.App.Common.ZyJsonFormatExtensions.ZyJsonConverts
{
    public class LongConvert : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(long) || objectType == typeof(long?);
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            string? value = null;

            if (reader.Value == null) 
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(reader.Value.ToString())) 
            {
                return 0L;
            }

            if (reader.Value != null) 
            {
                value = reader.Value.ToString();
            }

            if (long.TryParse(value, out var oValue)) 
            {
                return oValue;
            }

            throw new JsonException("Unable to deserialize a long.");
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value is long longValue)
            {
                if (longValue == 0)
                {
                    writer.WriteValue(string.Empty);
                    return;
                }
            }

            var nullableLongValue = value as long?;
            if (nullableLongValue == 0)
            {
                writer.WriteValue(string.Empty);
                return;
            }

            writer.WriteValue(value?.ToString());
        }
    }
}
