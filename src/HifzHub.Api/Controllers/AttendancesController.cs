using HifzHub.Application.Attendances;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HifzHub.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
public class AttendancesController(AttendanceService service) : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Mark(MarkAttendanceRequest req, CancellationToken ct) =>
        HandleResult(await service.MarkAsync(req, ct));

    [HttpPost("bulk")]
    public async Task<IActionResult> MarkBulk(BulkAttendanceRequest req, CancellationToken ct) =>
        Ok(new { added = await service.MarkBulkAsync(req, ct) });

    [HttpGet]
    public async Task<IActionResult> GetByHalaqaAndDate([FromQuery] Guid halaqaId, [FromQuery] DateOnly date, CancellationToken ct) =>
        Ok(await service.GetByHalaqaAndDateAsync(halaqaId, date, ct));

}