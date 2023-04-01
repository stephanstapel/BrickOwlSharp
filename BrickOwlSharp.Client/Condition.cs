using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickOwlSharp.Client
{
    public enum Condition
    {
        New,
        NewSealed,
        NewComplete,
        NewIncomplete,
        UsedComplete,
        UsedIncomplete,
        UsedLikeNew,
        UsedGood,
        UsedAcceptable,
        Other
    }
}
