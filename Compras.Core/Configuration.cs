namespace Compras.Core;

public static class Configuration
{
    public const int DefaultStatusCode = 200;
    public const int DefaultPageSize = 25;
    public const int DefaultPageNumber = 1;
    
    public static string ConnectionString { get; set; } = String.Empty;
    public static string FrontendUrl  { get; set; } = String.Empty;
    public static string BackendUrl  { get; set; } = String.Empty;
}