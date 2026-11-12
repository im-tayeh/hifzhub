using HifzHub.Domain.Enums;

namespace HifzHub.Application.Abstractions;

public interface ICurrentUser
{
    Guid? UserId { get; }
    Guid? CenterId { get; }
    bool IsSuperAdmin { get; }
    bool IsCenterManager { get; } // Admin / Amir / Deputy
    IReadOnlyCollection<RoleType> Roles { get; }
}