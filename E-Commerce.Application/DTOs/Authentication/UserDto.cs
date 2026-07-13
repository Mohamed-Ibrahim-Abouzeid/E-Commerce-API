using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.DTOs.Authentication
{
    public class UserDto
    {
        public string Email { get; set; }=default!;
        public string Token { get; set; } = default!;
        public string UserName { get; set; } = default!;
    }
}
