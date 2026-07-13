using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Infrastructure.Identity.Entities
{
    public class ApplicationUser: IdentityUser
    {
        public string UserName { get; set; } = default!;
        public Address? Address { get; set; }
    }
}
