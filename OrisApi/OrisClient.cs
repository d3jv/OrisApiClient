using OrisApi.Exception;
using OrisApi.Models;
using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;

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
            throw new OrisApiException("Not found");
        }

        if (response.Status != "OK") {
            // ORIS sends error messages in the Status field
            throw new OrisApiException(response.Status);
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
}
