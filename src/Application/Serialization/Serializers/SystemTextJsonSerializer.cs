using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FPAAgentura.Application.Interfaces.Serialization.Serializers;
using FPAAgentura.Application.Serialization.Options;
using Microsoft.Extensions.Options;

namespace FPAAgentura.Application.Serialization.Serializers;

public class SystemTextJsonSerializer : IJsonSerializer
{
    private readonly JsonSerializerOptions _options;

    public SystemTextJsonSerializer(IOptions<SystemTextJsonOptions> options)
    {
        _options = options.Value.JsonSerializerOptions;
    }

    public T Deserialize<T>(string data)
        => JsonSerializer.Deserialize<T>(data, _options);

    public string Serialize<T>(T data)
        => JsonSerializer.Serialize(data, _options);
}
