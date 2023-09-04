using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace BrickOwlSharp.Client
{
    public interface IBrickOwlRequestHandler
    {
        Task OnRequestAsync(CancellationToken ct);
    }
}
