using System;
using System.Collections.Generic;

namespace GitVersion.Models.Abstractions
{
    public interface IGitRemote
    {
        string Name { get; }
        IEnumerable<IGitRefSpec> FetchRefSpecs { get; }
        Uri Url { get; }
    }
}