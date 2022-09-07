using System.Text.Json;
using PaperStop.Application.Interfaces.Serialization.Options;

namespace PaperStop.Application.Serialization.Options;

public class SystemTextJsonOptions : IJsonSerializerOptions
{
    public JsonSerializerOptions JsonSerializerOptions { get; } = new();
}
