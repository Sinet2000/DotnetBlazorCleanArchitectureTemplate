﻿using System.ComponentModel.DataAnnotations;

namespace PaperStop.Application.Requests.Identity;

public class ForgotPasswordRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
