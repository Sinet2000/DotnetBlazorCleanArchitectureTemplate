using Newtonsoft.Json;
using PaperStop.Application.Interfaces.Serialization.Settings;

namespace PaperStop.Application.Serialization.Settings;

public class NewtonsoftJsonSettings : IJsonSerializerSettings
{
    public JsonSerializerSettings JsonSerializerSettings { get; } = new();
}
