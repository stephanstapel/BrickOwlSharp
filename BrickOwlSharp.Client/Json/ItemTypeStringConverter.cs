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
    internal class ItemTypeStringConverter : JsonConverter<ItemType>
    {
        public override ItemType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var stringValue = reader.GetString();

            switch (stringValue)
            {
                case "Part": return ItemType.Part;
                case "Minibuild": return ItemType.Minibuild;
                case "Packaging": return ItemType.Packaging;
                case "Set": return ItemType.Set;
                case "Instructions": return ItemType.Instructions;
                case "Gear": return ItemType.Gear;
                case "Minifigure": return ItemType.Minifigure;
                case "Sticker": return ItemType.Sticker;
            }

            return ItemType.Unknown;
        }

        public override void Write(Utf8JsonWriter writer, ItemType value, JsonSerializerOptions options)
        {
            string typeString = "";

            switch (value)
            {
                case ItemType.Part: typeString = "Part"; break;
                case ItemType.Minibuild: typeString = "Minibuild"; break;
                case ItemType.Packaging: typeString = "Packaging"; break;
                case ItemType.Set: typeString = "Set"; break;
                case ItemType.Instructions: typeString = "Instructions"; break;
                case ItemType.Gear: typeString = "Gear"; break;
                case ItemType.Minifigure: typeString = "Minifigure"; break;
                case ItemType.Sticker: typeString = "Sticker"; break;
            }


            writer.WriteStringValue(typeString);
        }
    }
}