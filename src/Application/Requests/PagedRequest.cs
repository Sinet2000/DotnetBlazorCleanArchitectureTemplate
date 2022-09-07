﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPAAgentura.Application.Requests;

public abstract class PagedRequest
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }

    public string[] Orderby { get; set; }
}
