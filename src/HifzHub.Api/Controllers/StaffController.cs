using System.Security.Claims;
using HifzHub.Application.Staff;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HifzHub.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/staff")]
public class StaffController(StaffProfileService service) : ControllerBase
{
    private Guid CurrentUserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)
                                            ?? User.FindFirstValue("sub")!);
    private bool IsManager => User.IsInRole("CenterAdmin") || User.IsInRole("CenterAmir") || User.IsInRole("DeputyAmir");

    [HttpPut("me/profile")]
    public async Task<IActionResult> UpdateMine(UpdateStaffProfileRequest req, CancellationToken ct)
    {
        var r = await service.UpdateMyProfileAsync(CurrentUserId, req, ct);
        return r is null ? BadRequest(new { message = "Center context required." }) : Ok(r);
    }

    [HttpPut("me/profile/visibility")]
    public async Task<IActionResult> SetVisibility([FromQuery] bool isPublic, CancellationToken ct) =>
        await service.SetVisibilityAsync(CurrentUserId, isPublic, ct) ? NoContent() : NotFound();

    [HttpGet("{userId:guid}/profile")]
    public async Task<IActionResult> GetProfile(Guid userId, CancellationToken ct)
    {
        var r = await service.GetProfileAsync(userId, CurrentUserId, IsManager, ct);
        return r is null ? NotFound() : Ok(r);
    }

    [HttpGet("{userId:guid}/stats")]
    public async Task<IActionResult> GetStats(Guid userId, CancellationToken ct) =>
        Ok(await service.GetStatsAsync(userId, ct));
}