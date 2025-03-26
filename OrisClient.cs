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

        if (response.Status != "OK") {
            // ORIS sends error messages in the Status field
            throw new OrisApiException(response.Status);
        }

        return response;
    }

    public async Task<OrisResponse<OrisUserClub>> GetRegistrationsAsync(int orisID)
    {
        var response = await _client.Request("method=getClubUsers")
            .SetQueryParam("user", orisID)
            .GetJsonAsync<OrisResponse<OrisUserClub>>();

        if (response.Status != "OK") {
            throw new OrisApiException(response.Status);
        }

        return response;
    }
}
