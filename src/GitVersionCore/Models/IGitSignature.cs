using System;

namespace GitVersion.Models
{
    public interface IGitSignature
    {
        DateTimeOffset When { get; }
    }
}
