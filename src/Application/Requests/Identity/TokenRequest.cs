﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPAAgentura.Application.Requests.Identity;

public class TokenRequest
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}