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
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace BrickOwlSharp.Client
{
    public enum IdType
    {
        Unknown,
        DesignId,
        LDraw,
        BOID,
        ItemNo,
        SetNumber,
        BLItemNo,
        UPC,
        EAN,
        Other
    }


    internal static class IdTypeExtensions
    {
        public static IdType FromString(this IdType _, string s)
        {
            switch (s)
            {
                case "design_d": return IdType.DesignId;
                case "ldraw": return IdType.LDraw;
                case "boid": return IdType.BOID;
                case "item_no": return IdType.ItemNo;
                case "set_number": return IdType.SetNumber;
                case "bl_item_no": return IdType.BLItemNo;
                case "ean": return IdType.EAN;
                case "upc": return IdType.UPC;
                case "other": return IdType.Other;
                default: return IdType.Unknown;
            }
        } // !FromString()


        public static string EnumToString(this IdType c)
        {
            switch (c)
            {                        
                case IdType.DesignId: return "design_id";
                case IdType.LDraw: return "ldraw";
                case IdType.BOID: return "boid";
                case IdType.ItemNo: return "item_no";
                case IdType.SetNumber: return "set_number";
                case IdType.BLItemNo: return "bl_item_no";
                case IdType.EAN: return "ean";
                case IdType.UPC:return "upc";
                case IdType.Other: return "other";
                default: return "";
            }
        } // !EnumToString()
    }
}
