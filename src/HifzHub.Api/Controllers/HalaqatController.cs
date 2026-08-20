using HifzHub.Application.Halaqat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HifzHub.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class HalaqatController(HalaqaService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateHalaqaRequest req, CancellationToken ct)
    {
        var result = await service.CreateAsync(req, ct);
        return result is null
            ? BadRequest(new { message = "Invalid stage or missing center context." })
            : CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct) =>
        Ok(await service.GetAllAsync(ct));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var result = await service.GetByIdAsync(id, ct);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateHalaqaRequest req, CancellationToken ct) =>
        await service.UpdateAsync(id, req, ct) ? NoContent() : NotFound();

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct) =>
        await service.DeleteAsync(id, ct) ? NoContent() : NotFound();
}