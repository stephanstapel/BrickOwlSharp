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
using BrickOwlSharp.Client.Extensions;
using BrickOwlSharp;

namespace BrickOwlSharp.Client.Json
{
    internal class ConditionStringConverter : JsonConverter<Condition>
    {
        public override Condition Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var stringValue = reader.GetString();
            switch (stringValue)
            {
                case "new": return Condition.New;
                case "news": return Condition.NewSealed;
                case "newc": return Condition.NewComplete;
                case "newi": return Condition.NewIncomplete;
                case "usedc": return Condition.UsedComplete;
                case "usedi": return Condition.UsedIncomplete;
                case "usedn": return Condition.UsedLikeNew;
                case "usedg": return Condition.UsedGood;
                case "useda": return Condition.UsedAcceptable;
                case "other": return Condition.Other;
                default:
                    return Condition.Other;
            }
        }

        public override void Write(Utf8JsonWriter writer, Condition value, JsonSerializerOptions options)
        {
            var typeString = value.ToDomainString();
            writer.WriteStringValue(typeString);
        }
    }
}