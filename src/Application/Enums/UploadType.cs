using System.ComponentModel;

namespace PaperStop.Application.Enums;

public enum UploadType : byte
{
    [Description(@"Images\ProfilePictures")]
    ProfilePicture,

    [Description(@"Documents")]
    Document
}
