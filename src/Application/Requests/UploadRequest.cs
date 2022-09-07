using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPAAgentura.Application.Enums;

namespace FPAAgentura.Application.Requests;

public class UploadRequest
{
    public string FileName { get; set; }
    public string Extension { get; set; }
    public UploadType UploadType { get; set; }
    public byte[] Data { get; set; }
}
