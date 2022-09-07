using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPAAgentura.Application.Enums;

public enum UploadType : byte
{
    [Description(@"Images\ProfilePictures")]
    ProfilePicture,

    [Description(@"Documents")]
    Document
}
