using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Services
{
    public class AuthentcationService:IAuthenticationService
    {
        private readonly IIdentityService _identityService;
        private readonly ITokenService _tokenService;

        public AuthentcationService(IIdentityService identityService, ITokenService tokenService)
        {
            _identityService = identityService;
            _tokenService = tokenService;
        }

        public async Task<Result<UserDto>> LoginAsync(LogDto logDto, CancellationToken ct = default)
        {
            var userResult = await _identityService.FindByEmailAsync(logDto.Email, ct);
            if (!userResult.IsSuccess)
            {
                return Result<UserDto>.Fail(userResult.Errors);
            }
            var passwordResult = await _identityService.CheckPasswordAsync(logDto.Email, logDto.Password, ct);
            if (!passwordResult.IsSuccess)
            {
                return Result<UserDto>.Fail(Error.Unauthorized("invalid Email or Password"));
            }
            return new UserDto
            {
                Email = userResult.Data.Email,
                UserName = userResult.Data.UserName,
                Token = "Token" 
            };
        }

        public async Task<Result<UserDto>> RegisterAsync(RegisterDto registerDto, CancellationToken ct = default)
        {
    var result=await _identityService.CreateUserAsync(registerDto, ct);
            if (!result.IsSuccess)
            {
                return Result<UserDto>.Fail(result.Errors);
            }
            return new UserDto
            {
                Email = result.Data.Email,
                UserName = result.Data.UserName,
                Token = "Token"
            };
        }
    }
}
