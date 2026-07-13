using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace E_Commerce.Application.DTOs.Authentication
{
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; } = default!;
        [Required,EmailAddress]

        public string Email { get; set; } = default!;
        [Required,MinLength(8)]

        public string Password { get; set; } = default!;
        
        [Phone]public string PhoneNumber { get; set; } = default!;
        [Required]

        public string DisplayName { get; set; } = default!;
    }
}
