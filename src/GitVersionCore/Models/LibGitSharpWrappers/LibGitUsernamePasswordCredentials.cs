using GitVersion.Models.Abstractions;

namespace GitVersion.Models.LibGitSharpWrappers
{
    public class LibGitUsernamePasswordCredentials: IGitCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
