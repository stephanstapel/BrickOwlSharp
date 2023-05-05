using System;
using System.Collections.Generic;
using System.Text;

namespace BrickOwlSharp.Client
{
    public enum OrderStatus
    {
        Unknown = -1,
        Pending = 0,
        PaymentSubmitted = 1,
        PaymentReceived = 2,
        Processing = 3,
        Processed = 4,
        Shipped = 5,
        Received = 6,
        OnHold = 7,
        Cancelled = 8
    }
}
