using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace E_Commerce.Application.DTOs.Authentication
{
    public class LogDto
    {
        [Required,EmailAddress] public string Email { get; set; } = default!;
        [Required] public string Password { get; set; } = default!;
    }
}
