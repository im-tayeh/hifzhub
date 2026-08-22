using HifzHub.Application.Attendances;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HifzHub.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AttendancesController(AttendanceService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Mark(MarkAttendanceRequest req, CancellationToken ct)
    {
        var result = await service.MarkAsync(req, ct);

        return result is null
            ? BadRequest(new { message = "Invalid student, student has no halaqa, or already marked today." })
            : Ok(result);
    }

    [HttpPost("bulk")]
    public async Task<IActionResult> MarkBulk(BulkAttendanceRequest req, CancellationToken ct)
    {
        var count = await service.MarkBulkAsync(req, ct);
        return Ok(new { added = count });
    }

    [HttpGet]
    public async Task<IActionResult> GetByHalaqaAndDate([FromQuery] Guid halaqaId, [FromQuery] DateOnly date, CancellationToken ct) =>
        Ok(await service.GetByHalaqaAndDateAsync(halaqaId, date, ct));

}