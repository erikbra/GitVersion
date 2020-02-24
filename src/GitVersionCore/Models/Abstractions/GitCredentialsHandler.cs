namespace GitVersion.Models.Abstractions
{
    public delegate IGitCredentials GitCredentialsHandler(
        string url,
        string usernameFromUrl,
        IGitSupportedCredentialTypes types);
}
