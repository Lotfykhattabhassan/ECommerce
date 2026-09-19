using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniECommerce.Modules.Identity.Application.DTOs.User;
using MiniECommerce.Modules.Identity.Application.Services.Abstractions;

namespace MiniECommerce.Modules.Identity.API.Controllers;

[ApiController]
[Route("api/users")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateUserDto dto,
        CancellationToken cancellationToken)
    {
        var userId = await _userService.CreateUserAsync(dto, cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = userId },
            new { id = userId });
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _userService.GetUserById(id, cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var result = await _userService.GetAllAsync(cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:guid}/roles")]
    public async Task<IActionResult> GetUserRoles(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _userService.GetUserRoles(id, cancellationToken);

        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateName(
        Guid id,
        [FromBody] UpdateUserDto dto,
        CancellationToken cancellationToken)
    {
        // Keep route id authoritative instead of trusting a different body id.
        dto.Id = id;

        var result = await _userService.UpdateUserName(dto, cancellationToken);

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _userService.DeleteUser(id, cancellationToken);

        return Ok(result);
    }
}
