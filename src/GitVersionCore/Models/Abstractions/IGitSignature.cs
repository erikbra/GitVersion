using System;

namespace GitVersion.Models.Abstractions
{
    public interface IGitSignature
    {
        DateTimeOffset When { get; }
    }
}
