namespace GitVersion.Models
{
    public delegate IGitCredentials GitCredentialsHandler(
        string url,
        string usernameFromUrl,
        IGitSupportedCredentialTypes types);
}
