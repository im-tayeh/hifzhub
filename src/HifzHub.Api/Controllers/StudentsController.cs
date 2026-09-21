using HifzHub.Api.Controllers;
using HifzHub.Application.Students;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[Route("api/[controller]")]
public class StudentsController(StudentService service) : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateStudentRequest req, CancellationToken ct) =>
        HandleResult(await service.CreateAsync(req, ct));

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct) => Ok(await service.GetAllAsync(ct));

    [HttpGet("unassigned")]
    public async Task<IActionResult> GetUnassigned(CancellationToken ct) => Ok(await service.GetUnassignedAsync(ct));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct) =>
        HandleResult(await service.GetByIdAsync(id, ct));

    [HttpPut("{id:guid}/assign-halaqa")]
    public async Task<IActionResult> AssignHalaqa(Guid id, AssignHalaqaRequest req, CancellationToken ct) =>
        HandleResult(await service.AssignHalaqaAsync(id, req.HalaqaId, ct));

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateStudentRequest req, CancellationToken ct) =>
        HandleResult(await service.UpdateAsync(id, req, ct));

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct) =>
        HandleResult(await service.DeleteAsync(id, ct));
}