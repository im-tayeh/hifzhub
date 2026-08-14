using HifzHub.Domain.Common;

namespace HifzHub.Domain.Entities;

public class User : BaseEntity
{
    public Guid? CenterId { get; set; }
    public Center? Center { get; set; }

    public string Username { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    public ICollection<UserRole> Roles { get; set; } = new List<UserRole>();
}
