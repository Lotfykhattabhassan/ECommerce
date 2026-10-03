using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniECommerce.Modules.Identity.Application.Abstractions.Security;
using MiniECommerce.Modules.Identity.Application.DTOs.Auth;
using MiniECommerce.Modules.Identity.Application.Services.Abstractions;

namespace MiniECommerce.Modules.Identity.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterUserDto dto,
        CancellationToken cancellationToken)
    {
        var userId = await _authService.RegisterUser(dto, cancellationToken);

        return CreatedAtAction(
            nameof(Register),
            new { id = userId },
            new { id = userId });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginDto dto,
        CancellationToken cancellationToken)
    {
        var result = await _authService.Login(dto, cancellationToken);

        return Ok(result);
    }

    [Authorize]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword(
        [FromBody] ChangePasswordDto dto,
        CancellationToken cancellationToken)
    {
        var result = await _authService.ChangeUserPassword(dto, cancellationToken);

        return Ok(result);
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetMe(
        CancellationToken cancellationToken)
    {
        var result = await _authService.GetMe(cancellationToken);

        return Ok(result);
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<JwtTokenResult>> Refresh(
    RefreshTokenRequest request,
    CancellationToken cancellationToken)
    {
        var result = await _authService.RefreshAsync(
            request,
            cancellationToken);

        return Ok(result);
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout(
    RefreshTokenRequest request,
    CancellationToken cancellationToken)
    {
        await _authService.LogoutAsync(
            request,
            cancellationToken);

        return NoContent();
    }
}
