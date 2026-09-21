using HifzHub.Application.Recitations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HifzHub.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
public class RecitationsController(RecitationService service) : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateRecitationRequest req, CancellationToken ct) =>
    HandleResult(await service.CreateAsync(req, ct));

    [HttpGet("student/{studentId:guid}")]
    public async Task<IActionResult> GetByStudent(Guid studentId, CancellationToken ct) =>
        Ok(await service.GetByStudentAsync(studentId, ct));
}