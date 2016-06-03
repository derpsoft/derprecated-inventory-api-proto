namespace BausCode.Api.Jobs.Authentication
{
    public interface IHasCredentials
    {
        string Credentials { get; }
    }
}