using OrisApi.Models;
using static OrisApi.Models.OrisUserClubs;

namespace OrisApi;

public interface IOrisClient
{
    public Task<OrisResponse<OrisAuth>> AuthenticateAsync(string username, string password);
    public Task<OrisResponse<OrisUserClub>> GetRegistrationsAsync(int orisID);
    public Task<OrisResponse<OrisUser>> GetUser(string rgnum);
}

