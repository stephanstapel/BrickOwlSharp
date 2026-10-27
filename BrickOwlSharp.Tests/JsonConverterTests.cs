#region License
// Copyright (c) 2024 Stephan Stapel
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
# endregion
using System.Globalization;
using System.Text.Json;
using BrickOwlSharp.Client;
using Xunit;

namespace BrickOwlSharp.Tests;

public class JsonConverterTests
{
    [Fact]
    public void DateTimeStringConverter_TimestampAfter2038_IsParsed()
    {
        // 4102444800 = 2100-01-01T00:00:00Z, does not fit into Int32
        var availability = JsonSerializer.Deserialize<CatalogItemAvailability>("{\"updated\":\"4102444800\"}");

        Assert.Equal(new DateTime(2100, 1, 1, 0, 0, 0, DateTimeKind.Utc), availability!.Updated.ToUniversalTime());
    }

    [Fact]
    public void DecimalStringConverter_Write_UsesInvariantCultureOnGermanSystem()
    {
        var previousCulture = CultureInfo.CurrentCulture;
        try
        {
            CultureInfo.CurrentCulture = new CultureInfo("de-DE");

            var json = JsonSerializer.Serialize(new CatalogCartBasicItemResponse { Price = 1.5m });

            Assert.Contains("\"price\":\"1.5\"", json);
        }
        finally
        {
            CultureInfo.CurrentCulture = previousCulture;
        }
    }
}
