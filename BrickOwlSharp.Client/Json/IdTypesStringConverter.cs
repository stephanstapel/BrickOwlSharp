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
using System.Text.Json;
using System.Text.Json.Serialization;

using BrickOwlSharp;

namespace BrickOwlSharp.Client.Json
{
    internal class IdTypesStringConverter : JsonConverter<IdType>
    {
        public override IdType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var stringValue = reader.GetString();

            switch (stringValue)
            {
                case "design_id": return IdType.DesignId;
                case "ldraw": return IdType.LDraw;
                case "boid": return IdType.BOID;
                case "item_no": return IdType.ItemNo;
            }

            return IdType.Unknown;
        }

        public override void Write(Utf8JsonWriter writer, IdType value, JsonSerializerOptions options)
        {
            string typeString = "";
            switch (value)
            {
                case IdType.DesignId: typeString = "design_id"; break;
                case IdType.LDraw: typeString = "ldraw"; break;
                case IdType.BOID: typeString = "boid"; break;
                case IdType.ItemNo: typeString = "item_no"; break;
            }

            writer.WriteStringValue(typeString);
        }
    }
}