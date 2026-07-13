using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Common
{
    public class IdentityUserResult
    {
        public IdentityUserResult(string id, string? userName, string? email, string displayName)
        {
            Id = id;
            UserName = userName;
            Email = email;
            DisplayName = displayName;
        }

        public string Id { get; set; } = default!;
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string DisplayName { get; set; } = default!;
    }
}
