namespace Client;

public class ClientAppConfiguration
{
    public const string ConfigKey = "ClientAppConfiguration";
        
    public string ApiAddress { get; set; } = null!;
    public string HttpClient { get; set; } = null!;
}