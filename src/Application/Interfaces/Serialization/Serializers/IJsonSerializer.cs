using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPAAgentura.Application.Interfaces.Serialization.Serializers;

public interface IJsonSerializer
{
    string Serialize<T>(T obj);
    T Deserialize<T>(string text);
}
