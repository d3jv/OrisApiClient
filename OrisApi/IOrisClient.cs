using OrisApi.Models;
using OrisApi.Models.Enums;
using OrisApi.Models.GetEventList;

namespace OrisApi;

public interface IOrisClient
{
    public Task<OrisResponse<OrisAuth>> AuthenticateAsync(string username, string password);
    public Task<OrisResponse<OrisUserClubs>> GetUserClubsAsync(int orisID, DateTime? date = null);
    public Task<OrisResponse<OrisUser>> GetUserAsync(string rgnum);
    public Task<OrisResponse<OrisEventList_Versions>> GetEventListVersions(
            bool all = false,
            string? name = null,
            OrisSport? sport = null,
            OrisRegion? rg = null,
            IEnumerable<OrisLevel>? level = null,
            DateOnly? datefrom = null,
            DateOnly? dateto = null,
            string? club = null);
    public Task<OrisResponse<OrisEventList>> GetEventList(
            bool all = false,
            string? name = null,
            OrisSport? sport = null,
            OrisRegion? rg = null,
            IEnumerable<OrisLevel>? level = null,
            DateOnly? datefrom = null,
            DateOnly? dateto = null,
            string? club = null);
    public Task<OrisResponse<OrisEventList_ClubEntries>> GetEventList(
            bool all = false,
            string? name = null,
            OrisSport? sport = null,
            OrisRegion? rg = null,
            IEnumerable<OrisLevel>? level = null,
            DateOnly? datefrom = null,
            DateOnly? dateto = null,
            string? club = null,
            int? myClubId = null);
    public Task<OrisResponse<OrisEventEntries>> GetEventEntries(
            int eventId,
            int? classId = null,
            string? className = null,
            string? clubId = null,
            int? entryStop = null,
            int? entryStopOut = null);
    public Task<OrisResponse<OrisEventBalance>> GetEventBalance(int eventId);
    public Task<OrisResponse<OrisEventServiceEntries>> GetEventServiceEntries(
            int eventId,
            string? clubId = null);
    public Task<OrisResponse<OrisEvent>> GetEvent(int id);
}

