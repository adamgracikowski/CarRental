using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using CarRental.Provider.API.Authorization.JwtTokenService;
using CarRental.Provider.API.Authorization.TrustedClientService;
using CarRental.Provider.API.DTOs.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Provider.API.Controllers;

[Authorize]
[Route("[controller]")]
[ApiController]
public sealed class AuthController : ControllerBase
{
    private readonly ITrustedClientService trustedClientService;
    private readonly IJwtTokenService jwtTokenService;

    public AuthController(
        ITrustedClientService trustedClientService,
        IJwtTokenService jwtTokenService)
    {
        this.trustedClientService = trustedClientService;
        this.jwtTokenService = jwtTokenService;
    }

	/// <summary>
	/// Generates a JWT token for a trusted client.
	/// </summary>
	/// <param name="authRequestDto">The authentication request containing the client ID and secret.</param>
	/// <returns>A result containing the generated JWT token.</returns>
	/// <response code="200">The token was generated successfully.</response>
	/// <response code="401">The provided client ID or secret is invalid.</response>
	/// <response code="500">An internal server error occurred while generating the token.</response>
	[AllowAnonymous]
    [TranslateResultToActionResult]
    [HttpPost("token")]
    public Result<AuthResponseDto> GenerateToken(AuthRequestDto authRequestDto)
    {
        var client = this.trustedClientService.ValidateTrustedClient(authRequestDto.ClientId, authRequestDto.ClientSecret);

        if (client == null)
        {
            return Result<AuthResponseDto>.Unauthorized($"Invalid {nameof(AuthRequestDto.ClientId)} or {nameof(AuthRequestDto.ClientSecret)}");
        }

        var token = this.jwtTokenService.GenerateJwtToken(client);

        return Result<AuthResponseDto>.Success(new AuthResponseDto(token));
    }
}