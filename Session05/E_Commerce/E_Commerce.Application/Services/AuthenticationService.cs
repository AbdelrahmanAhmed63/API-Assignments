using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Identity;

namespace E_Commerce.Application.Services;

internal class AuthenticationService : IAuthenticationService
{
    private readonly IIdentityService _identityService;

    public AuthenticationService(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<UserDto>> LoginAsync(LoginDto loginDto, CancellationToken ct = default)
    {
        // Get USer By Email
        var userResult = await _identityService.FindUserByEmailAsync(loginDto.Email);
        if (!userResult.IsSuccess)
            return Result<UserDto>.Fail(userResult.Errors);

        // Check Password
        var passwordResult = await _identityService.CheckPasswordAsync(loginDto.Email, loginDto.Password, ct);
        if(!passwordResult.IsSuccess)
            return Result<UserDto>.Fail(passwordResult.Errors);
        if (!passwordResult.Data)
            return Result<UserDto>.Fail(Error.Unauthorized("Invalid Email or Password"));

        return new UserDto
        {
            Email = loginDto.Email,
            DisplayName = userResult.Data.DisplayName,
            Token = "Token"
        };
        // Return Result + User Dto
    }
}