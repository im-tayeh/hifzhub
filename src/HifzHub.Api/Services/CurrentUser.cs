using System.Security.Claims;
using HifzHub.Application.Abstractions;
using HifzHub.Domain.Enums;

namespace HifzHub.Api.Services;

public class CurrentUser(IHttpContextAccessor accessor) : ICurrentUser
{
    private ClaimsPrincipal? User => accessor.HttpContext?.User;

    public Guid? UserId =>
        Guid.TryParse(User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? User?.FindFirstValue("sub"), out var id)
            ? id : null;

    public Guid? CenterId =>
        Guid.TryParse(User?.FindFirstValue("centerId"), out var id) ? id : null;

    public IReadOnlyCollection<RoleType> Roles =>
        User?.FindAll(ClaimTypes.Role)
            .Select(c => Enum.TryParse<RoleType>(c.Value, out var r) ? r : (RoleType?)null)
            .Where(r => r is not null).Select(r => r!.Value).ToArray()
        ?? [];

    public bool IsSuperAdmin => Roles.Contains(RoleType.SuperAdmin);

    public bool IsCenterManager =>
        Roles.Contains(RoleType.CenterAdmin) ||
        Roles.Contains(RoleType.CenterAmir) ||
        Roles.Contains(RoleType.DeputyAmir);
}