using OrisApi.Models;
using OrisApi.Models.Enums;

namespace OrisApi;

public interface IOrisClient
{
    public Task<OrisResponse<OrisAuth>> AuthenticateAsync(string username, string password);
    public Task<OrisResponse<OrisUserClubs>> GetUserClubsAsync(int orisID, DateTime? date = null);
    public Task<OrisResponse<OrisUser>> GetUserAsync(string rgnum);
    public Task<OrisResponse<OrisListEventVersions>> GetEventListVersions(
            bool all = false,
            string? name = null,
            OrisSport? sport = null,
            OrisRegion? rg = null,
            IEnumerable<OrisLevel>? level = null,
            DateOnly? datefrom = null,
            DateOnly? dateto = null,
            int? club = null
        );
}

