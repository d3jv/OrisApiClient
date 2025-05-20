namespace OrisApi.Models;

public class OrisUser : IOrisResponseData
{
    public int ID { get; init; }

    public string FirstName { get; init; }

    public string LastName { get; init; }

    public string RefLicenceOB { get; init; }
}

