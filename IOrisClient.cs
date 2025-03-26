using OrisApi.Models;
using static OrisApi.Models.OrisUserClubs;

namespace OrisApi;

public interface IOrisClient
{
    public Task<OrisResponse<OrisAuth>> AuthenticateAsync(string username, string password);
    public Task<OrisResponse<OrisUserClub>> GetUserClubsAsync(int orisID, DateTime? date = null);
    public Task<OrisResponse<OrisUser>> GetUserAsync(string rgnum);
}

