namespace PaperStop.Application.Configurations;

public class AppConfiguration
{
    public string Secret { get; set; }
    public string ApiBaseUrl { get; set; }
    public bool RequireHttpsMetadata { get; set; }
    public bool CorsAllowAnyOrigin { get; set; }
    public string[] ClientAppOrigins { get; set; }
}