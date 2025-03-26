using OrisApi.Models;

namespace OrisApi;

public interface IOrisClient
{
    public Task<OrisResponse<OrisAuth>> AuthenticateAsync(string username, string password);
    public Task<OrisResponse<OrisUserClubs>> GetRegistrationsAsync(int orisID);
}
