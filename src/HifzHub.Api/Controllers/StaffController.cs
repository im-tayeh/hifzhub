using System.Security.Claims;
using HifzHub.Application.Staff;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HifzHub.Api.Controllers;

[Authorize]
[Route("api/staff")]
public class StaffController(StaffProfileService service) : ApiControllerBase
{
    private Guid CurrentUserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)
                                            ?? User.FindFirstValue("sub")!);
    private bool IsManager => User.IsInRole("CenterAdmin") || User.IsInRole("CenterAmir") || User.IsInRole("DeputyAmir");

    [HttpPut("me/profile")]
    public async Task<IActionResult> UpdateMine(UpdateStaffProfileRequest req, CancellationToken ct) =>
        HandleResult(await service.UpdateMyProfileAsync(CurrentUserId, req, ct));

    [HttpPut("me/profile/visibility")]
    public async Task<IActionResult> SetVisibility([FromQuery] bool isPublic, CancellationToken ct) =>
        HandleResult(await service.SetVisibilityAsync(CurrentUserId, isPublic, ct));

    [HttpGet("{userId:guid}/profile")]
    public async Task<IActionResult> GetProfile(Guid userId, CancellationToken ct) =>
        HandleResult(await service.GetProfileAsync(userId, CurrentUserId, IsManager, ct));

    [HttpGet("{userId:guid}/stats")]
    public async Task<IActionResult> GetStats(Guid userId, CancellationToken ct) =>
        Ok(await service.GetStatsAsync(userId, ct));
}