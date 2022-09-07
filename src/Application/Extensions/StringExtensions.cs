using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace FPAAgentura.Application.Extensions;

public static class StringExtensions
{
    public static SecureString ToSecureString(this string value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        var securePassword = new SecureString();

        foreach (var c in value)
        {
            securePassword.AppendChar(c);
        }

        securePassword.MakeReadOnly();
        return securePassword;
    }

    public static bool IsNullOrWhiteSpace(this string? value)
    {
        return string.IsNullOrWhiteSpace(value);
    }
}
