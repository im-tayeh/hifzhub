using HifzHub.Domain.Common;
using HifzHub.Domain.Enums;

namespace HifzHub.Domain.Entities;

public class UserRole : BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public Guid? CenterId { get; set; }
    public RoleType Role { get; set; }
    public ScopeType ScopeType { get; set; }
    public Guid? ScopeId { get; set; }
}