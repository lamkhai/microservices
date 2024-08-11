using CoreAPI.Data.Core.Services;

namespace CoreAPI.Data.DBContext.Services;

public class ConstantPostgreSQLService : IConstantSQLService
{
    public IList<string> ExtensionNames { get; } = new List<string>()
    {
        "uuid-ossp",
    };

    public string DefaultGuid { get; } = "uuid_generate_v4()";

    public string DefaultUTCDateTime { get; } = "timezone('utc', now())";
}