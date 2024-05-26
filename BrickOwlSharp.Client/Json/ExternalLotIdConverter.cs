#region License
// Copyright (c) 2020 Jens Eisenbach
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
#endregion

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using BrickOwlSharp;

namespace BrickOwlSharp.Client.Json
{
    internal class ExternalLotIdConverter : JsonConverter<List<Reference>>
    {
        public override List<Reference> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            List<Reference> references = new List<Reference>();

            if (reader.TokenType == JsonTokenType.StartArray)
            {
                // the array is only used when the external_lot_ids property is empty
                reader.Read();
            }
            else if (reader.TokenType == JsonTokenType.StartObject)
            {
                JsonNode node = JsonObject.Parse(ref reader);
                
                if (node["other"] != null)
                {
                    references.Add(new Reference()
                    {
                        Type = IdType.Other,
                        Id = node["other"].ToString()
                    });
                }
            }

            return references;
        } // !Read()


        public override void Write(Utf8JsonWriter writer, List<Reference> value, JsonSerializerOptions options)
        {            
            writer.WriteStringValue(""); // ???
        } // !Write()
    }
}