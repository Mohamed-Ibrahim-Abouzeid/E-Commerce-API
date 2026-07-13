using E_Commerce.Domain.Contracts;
using E_Commerce.Infrastructure.Identity.Data;
using E_Commerce.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Infrastructure.Seeding
{
    internal class IdentityDataSeeder : IDataSeeder
    {
        private readonly StoreIdentityDbContext _storeIdentityDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<IdentityDataSeeder> _logger;

        public IdentityDataSeeder(StoreIdentityDbContext storeIdentityDbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<IdentityDataSeeder> logger)
        {
            _storeIdentityDbContext = storeIdentityDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task SeedAync(CancellationToken ct = default)
        {
            try
            {
                var pendingMigrations = await _storeIdentityDbContext.Database.GetPendingMigrationsAsync(ct);
                if (pendingMigrations.Any())
                {
                    await _storeIdentityDbContext.Database.MigrateAsync(ct);
                }
                if (!await _roleManager.Roles.AnyAsync(ct)) { 
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }
                if (! await _userManager.Users.AnyAsync(ct))
                {
                    var Admin = new ApplicationUser
                    {
                        UserName = "Mohamed",
                        Email = "abozed987@gmail.com",
                        PhoneNumber = "01000000000",
                        NormalizedUserName = "MOHAMED",
                    };
                    var createResult = await _userManager.CreateAsync(Admin, "Admin@123");
                    if (createResult.Succeeded) 
                    {
                        await _userManager.AddToRoleAsync(Admin, "Admin");
                    }
                    else
                    {
                        _logger.LogWarning("Failed to create Admin user: {Errors}", string.Join(", ", createResult.Errors.Select(e => e.Description)));
                    }
                }
                {
                    
                }
            }
            catch (Exception ex){ 
                _logger.LogError(ex, "An error occurred while seeding identity data.");
            }
        }
    }
}
