using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPAAgentura.Application.Interfaces.Services.Storage;

[ExcludeFromCodeCoverage]
public class ChangedEventArgs
{
    public string Key { get; set; }
    public object OldValue { get; set; }
    public object NewValue { get; set; }
}
