using HifzHub.Domain.Common;

namespace HifzHub.Domain.Entities;

public class Stage : BaseEntity
{
    public Guid CenterId { get; set; }
    public Center Center { get; set; } = null!;

    public string Name { get; set; } = string.Empty;
    public int Order {  get; set; }
}