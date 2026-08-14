using HifzHub.Domain.Common;
using HifzHub.Domain.Enums;

namespace HifzHub.Domain.Entities;

public class Center : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public CenterStatus Status { get; set; } = CenterStatus.Active;
    public bool IsDemo { get; set; }

    public ICollection<User> Users { get; set; } = new List<User>();
}
