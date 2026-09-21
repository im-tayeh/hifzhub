using HifzHub.Application.Halaqat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HifzHub.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
public class HalaqatController(HalaqaService service) : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateHalaqaRequest req, CancellationToken ct) =>
        HandleResult(await service.CreateAsync(req, ct));

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct) => Ok(await service.GetAllAsync(ct));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct) =>
        HandleResult(await service.GetByIdAsync(id, ct));

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateHalaqaRequest req, CancellationToken ct) =>
        HandleResult(await service.UpdateAsync(id, req, ct));

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct) =>
        HandleResult(await service.DeleteAsync(id, ct));
}