﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPAAgentura.Application.Interfaces.Services;

namespace Infrastructure.Shared.Services;

public class SystemDateTimeService : IDateTimeService
{
    public DateTime NowUtc => DateTime.UtcNow;
}