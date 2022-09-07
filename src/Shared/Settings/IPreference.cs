using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPAAgentura.Shared.Settings;

public interface IPreference
{
    public string LanguageCode { get; set; }
}
