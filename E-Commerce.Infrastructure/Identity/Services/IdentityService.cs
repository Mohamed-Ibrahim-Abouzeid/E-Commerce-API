using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Authentication;
using E_Commerce.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Infrastructure.Identity.Services
{
    public class IdentityService : IIdentityService
    {
     private readonly UserManager<ApplicationUser> _userManager;

        public IdentityService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<bool>> CheckPasswordAsync(string email, string password, CancellationToken ct = default)
        {
            var user =await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                return Result<bool>.Fail(Error.NotFound("User Not Found"));
            }
            var isValid = await _userManager.CheckPasswordAsync(user, password);
            return isValid;
        }

        public async Task<Result<IdentityUserResult>> CreateUserAsync(RegisterDto registerDto, CancellationToken ct = default)
        {
            var user = new ApplicationUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                NormalizedUserName = registerDto.DisplayName
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => new Error(e.Code, e.Description)).ToList();
                return Result<IdentityUserResult>.Fail(errors);
            }
            return Result<IdentityUserResult>.Ok(
                new IdentityUserResult(
                    id: user.Id,
                    userName: user.UserName,
                    email: user.Email,
                    displayName: user.UserName
                ));
        }
        public async Task<Result<IdentityUserResult>> FindByEmailAsync(
     string email,
     CancellationToken ct = default)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                return Result<IdentityUserResult>.Fail(
                    Error.NotFound("User Not Found"));
            }

            return Result<IdentityUserResult>.Ok(
                new IdentityUserResult(
                    id: user.Id,
                    userName: user.UserName,
                    email: user.Email,
                    displayName: user.UserName
                ));
        }
    }
}
