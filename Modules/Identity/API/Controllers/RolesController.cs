using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniECommerce.Modules.Identity.Application.DTOs.Role;
using MiniECommerce.Modules.Identity.Application.Services.Abstractions;

namespace MiniECommerce.Modules.Identity.API.Controllers;

[ApiController]
[Route("api/roles")]
[Authorize]
public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RolesController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateRoleDto dto,
        CancellationToken cancellationToken)
    {
        var roleId = await _roleService.CreateRoleAsync(dto, cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = roleId },
            new { id = roleId });
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var result = await _roleService.GetRoleById(id, cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var result = await _roleService.GetAllRoles(cancellationToken);

        return Ok(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateRoleDto dto,
        CancellationToken cancellationToken)
    {
        // Keep route id authoritative instead of trusting a different body id.
        dto.RoleId = id;

        var result = await _roleService.UpdateRoleAsync(dto, cancellationToken);

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken cancellationToken)
    {
        var result = await _roleService.DeleteRoleAsync(id, cancellationToken);

        return Ok(result);
    }
}
