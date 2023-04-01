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

namespace BrickOwlSharp.Client.Extensions
{
    internal static class EnumExtensions
    {
        internal static string ToDomainString(this Condition condition)
        {
            switch (condition)
            {
                case Condition.New: return "new";
                case Condition.NewSealed: return "news";
                case Condition.NewComplete: return "newc";
                case Condition.NewIncomplete: return "newi";
                case Condition.UsedComplete: return "usedc";
                case Condition.UsedIncomplete: return "usedi";
                case Condition.UsedLikeNew: return "usedn";
                case Condition.UsedGood: return "usedg";
                case Condition.UsedAcceptable: return "useda";
                default: return "other";
            }
        }
    }
}