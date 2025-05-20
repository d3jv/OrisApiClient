namespace OrisApi.Models;

public interface IOrisResponseData
{
    // Intentionally empty.
    //
    // This marks the classes valid for the Data field in OrisResponse
    //
    // TODO: If the retarded converter keeps causing stack overflows, we
    // could use this to prevent it from being able to convert literally
    // anything, but I can't reproduce the issue anymore so...
}
