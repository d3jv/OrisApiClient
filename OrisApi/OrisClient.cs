using OrisApi.Exception;
using OrisApi.Models;
using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;
using OrisApi.Models.Enums;
using System.Text.Json;
using System.Text.Json.Serialization;
using OrisApi.JsonConverters;
using OrisApi.Models.GetEventList;

namespace OrisApi;

public class OrisClient : IOrisClient
{
    private readonly IFlurlClient _client;

    public OrisClient() : this("https://oris.orientacnisporty.cz/API/")
    {
    }

    public OrisClient(Url url)
    {
        _client = new FlurlClient(url.SetQueryParam("format", "json"));

        var options = new JsonSerializerOptions {
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
        };
        options.Converters.Add(new DateTimeJsonConverter());
        options.Converters.Add(new NullableDateTimeJsonConverter());
        options.Converters.Add(new DateOnlyJsonConverter());
        options.Converters.Add(new NullableDateOnlyJsonConverter());
        options.Converters.Add(new BoolJsonConverter());
        options.Converters.Add(new RetardedOrisResponseDataConverterFactory());

        _client.Settings.JsonSerializer = new DefaultJsonSerializer(options);
    }

    public OrisClient(IFlurlClient client)
    {
        _client = client;
    }

    public OrisClient(IFlurlClientCache clients)
    {
        _client = clients.Get("ORIS");
    }

    private void SuccessOrDie<T>(OrisResponse<T> response)
    {
        if (response is null) {
            throw new OrisApiException("ORIS did not respond");
        }
    }

    public async Task<OrisResponse<OrisAuth>> AuthenticateAsync(string username, string password)
    {
        var response = await _client
            .Request()
            .PostUrlEncodedAsync(new {
                // all query params need to be in the body
                format = "json",
                method = "loginUser",
                username,
                password })
            .ReceiveJson<OrisResponse<OrisAuth>>();

        SuccessOrDie(response);

        return response;
    }

    public async Task<OrisResponse<OrisUserClubs>> GetUserClubsAsync(int id, DateTime? date = null)
    {
        var request = _client.Request("method=getClubUsers")
            .SetQueryParam("user", id);

        if (date is not null) {
            request.SetQueryParam("date", date?.ToString("yyyy-mm-dd"));
        }

        var response = await request
            .GetJsonAsync<OrisResponse<OrisUserClubs>>();

        SuccessOrDie(response);

        return response;
    }

    public async Task<OrisResponse<OrisUser>> GetUserAsync(string rgnum)
    {
        var response = await _client.Request("method=getUser")
            .SetQueryParam("rgnum", rgnum)
            .GetJsonAsync<OrisResponse<OrisUser>>();

        SuccessOrDie(response);

        return response;
    }

    private IFlurlRequest EventListRequest(
            IFlurlRequest request,
            bool all = false,
            string? name = null,
            OrisSport? sport = null,
            OrisRegion? rg = null,
            IEnumerable<OrisLevel>? level = null,
            DateOnly? datefrom = null,
            DateOnly? dateto = null,
            string? club = null)
    {
        if (all) {
            request.AppendQueryParam("all", 1);
        }
        return request
            .AppendQueryParam("name", name, NullValueHandling.Ignore)
            .AppendQueryParam("sport", sport, NullValueHandling.Ignore)
            .AppendQueryParam("rg", rg?.ToString(), NullValueHandling.Ignore)
            .AppendQueryParam("level",
                    level?
                        .Select(x => ((int)x).ToString())
                        .Aggregate((a,b) => a + ',' + b),
                    NullValueHandling.Ignore)
            .AppendQueryParam("datefrom", datefrom?.ToString("yyyy-MM-dd"), NullValueHandling.Ignore)
            .AppendQueryParam("dateto", dateto?.ToString("yyyy-MM-dd"), NullValueHandling.Ignore)
            .AppendQueryParam("club", club, NullValueHandling.Ignore);
    }

    public async Task<OrisResponse<OrisEventList_Versions>> GetEventListVersions(
            bool all = false,
            string? name = null,
            OrisSport? sport = null,
            OrisRegion? rg = null,
            IEnumerable<OrisLevel>? level = null,
            DateOnly? datefrom = null,
            DateOnly? dateto = null,
            string? club = null)
    {
        var response = await EventListRequest(
            _client.Request("method=getEventListVersions"),
            all, name, sport, rg, level, datefrom, dateto, club
        ).GetJsonAsync<OrisResponse<OrisEventList_Versions>>();

        SuccessOrDie(response);

        return response;
    }

    public async Task<OrisResponse<OrisEventList>> GetEventList(
            bool all = false,
            string? name = null,
            OrisSport? sport = null,
            OrisRegion? rg = null,
            IEnumerable<OrisLevel>? level = null,
            DateOnly? datefrom = null,
            DateOnly? dateto = null,
            string? club = null)
    {
        var response = await EventListRequest(
            _client.Request("method=getEventList"),
            all, name, sport, rg, level, datefrom, dateto, club
        ).GetJsonAsync<OrisResponse<OrisEventList>>();

        SuccessOrDie(response);

        return response;
    }

    public async Task<OrisResponse<OrisEventList_ClubEntries>> GetEventList(
            bool all = false,
            string? name = null,
            OrisSport? sport = null,
            OrisRegion? rg = null,
            IEnumerable<OrisLevel>? level = null,
            DateOnly? datefrom = null,
            DateOnly? dateto = null,
            string? club = null,
            int? myClubId = null)
    {
        var response = await EventListRequest(
            _client.Request("method=getEventList"),
            all, name, sport, rg, level, datefrom, dateto, club
        )
            .AppendQueryParam("myClubId", myClubId, NullValueHandling.Ignore)
            .GetJsonAsync<OrisResponse<OrisEventList_ClubEntries>>();

        SuccessOrDie(response);

        return response;
    }

    public async Task<OrisResponse<OrisEventEntries>> GetEventEntries(
            int eventId,
            int? classId = null,
            string? className = null,
            string? clubId = null,
            int? entryStop = null,
            int? entryStopOut = null
            //TODO: authorization in a separate method
            )
    {
        var response = await _client.Request("method=getEventEntries")
            .AppendQueryParam("eventid", eventId)
            .AppendQueryParam("classid", classId, NullValueHandling.Ignore)
            .AppendQueryParam("classname", className, NullValueHandling.Ignore)
            .AppendQueryParam("clubid", clubId, NullValueHandling.Ignore)
            .AppendQueryParam("entrystop", entryStop, NullValueHandling.Ignore)
            .AppendQueryParam("entrystopout", entryStopOut, NullValueHandling.Ignore)
            .GetJsonAsync<OrisResponse<OrisEventEntries>>();

        SuccessOrDie(response);

        return response;
    }

    public async Task<OrisResponse<OrisEventBalance>> GetEventBalance(int eventId)
    {
        var response = await _client.Request("method=getEventBalance")
            .AppendQueryParam("eventid", eventId)
            .GetJsonAsync<OrisResponse<OrisEventBalance>>();

        SuccessOrDie(response);

        return response;
    }

    public async Task<OrisResponse<OrisEventServiceEntries>> GetEventServiceEntries(
            int eventId,
            string? clubId = null)
    {
        var response = await _client.Request("method=getEventServiceEntries")
            .AppendQueryParam("eventid", eventId)
            .AppendQueryParam("clubid", clubId, NullValueHandling.Ignore)
            .GetJsonAsync<OrisResponse<OrisEventServiceEntries>>();

        SuccessOrDie(response);

        return response;
    }

    public async Task<OrisResponse<OrisEvent>> GetEvent(int id)
    {
        var response = await _client.Request("method=getEvent")
            .AppendQueryParam("id", id)
            .GetJsonAsync<OrisResponse<OrisEvent>>();

        SuccessOrDie(response);

        return response;
    }
}
