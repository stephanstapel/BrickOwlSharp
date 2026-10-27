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
using System.Threading;
using System.Threading.Tasks;
using BrickOwlSharp.Client;

namespace BrickOwlSharp.Tests;

/// <summary>
/// Request handler that completes asynchronously (or throws), to verify the client awaits it.
/// </summary>
internal sealed class RecordingRequestHandler : IBrickOwlRequestHandler
{
    private readonly Exception? _exceptionToThrow;

    public RecordingRequestHandler(Exception? exceptionToThrow = null)
    {
        _exceptionToThrow = exceptionToThrow;
    }

    public int CompletedCalls { get; private set; }

    public async Task OnRequestAsync(ResourceType resourceType, CancellationToken ct)
    {
        await Task.Delay(20, ct);
        if (_exceptionToThrow != null)
        {
            throw _exceptionToThrow;
        }
        CompletedCalls++;
    }
}
