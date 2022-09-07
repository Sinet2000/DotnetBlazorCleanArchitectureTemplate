using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FPAAgentura.Application.Interfaces.Serialization.Settings;

public interface IJsonSerializerSettings
{
    /// <summary>
    /// Settings for <see cref="Newtonsoft.Json"/>.
    /// </summary>
    public JsonSerializerSettings JsonSerializerSettings { get; }
}
