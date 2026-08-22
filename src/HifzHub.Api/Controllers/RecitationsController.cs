using HifzHub.Application.Recitations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HifzHub.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class RecitationsController(RecitationService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateRecitationRequest req, CancellationToken ct)
    {
        var (result, error) = await service.CreateAsync(req, ct);
        return result is null ? BadRequest(new { message = error }) : Ok(result);
    }

    [HttpGet("student/{studentId:guid}")]
    public async Task<IActionResult> GetByStudent(Guid studentId, CancellationToken ct) =>
        Ok(await service.GetByStudentAsync(studentId, ct));
}