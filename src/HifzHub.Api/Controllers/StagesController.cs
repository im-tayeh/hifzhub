using HifzHub.Application.Stages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HifzHub.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
public class StagesController(StageService service) : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateStageRequest request, CancellationToken ct) =>
        HandleResult(await service.CreateAsync(request, ct));

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct) =>
        Ok(await service.GetAllAsync(ct));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct) =>
        HandleResult(await service.GetByIdAsync(id, ct));

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateStageRequest req, CancellationToken ct) =>
        HandleResult(await service.UpdateAsync(id, req, ct));

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct) =>
        HandleResult(await service.DeleteAsync(id, ct));
}