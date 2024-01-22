using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace BrickOwlSharp.Client.Json
{
    internal class TierPriceListConverter : JsonConverter<List<TierPrice>>
    {
        public override List<TierPrice> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var tierPrices = new List<TierPrice>();

            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new JsonException();
            }

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndArray)
                {
                    break;
                }

                if (reader.TokenType != JsonTokenType.StartArray)
                {
                    throw new JsonException();
                }

                reader.Read();
                var threshold = reader.GetString();
                reader.Read();
                var price = reader.GetString();
                tierPrices.Add(new TierPrice
                {
                    Quantity = int.Parse(threshold),
                    Price = decimal.Parse(price)
                });

                reader.Read();
                if (reader.TokenType != JsonTokenType.EndArray)
                {
                    throw new JsonException();
                }
            }

            return tierPrices;
        }


        public override void Write(Utf8JsonWriter writer, List<TierPrice> value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}

