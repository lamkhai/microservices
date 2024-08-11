namespace CoreAPI.Data.Core.Services;

public interface IConstantSQLService
{
    IList<string> ExtensionNames { get; }

    string DefaultGuid { get; }
    string DefaultUTCDateTime { get; }
}