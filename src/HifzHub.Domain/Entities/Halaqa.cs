using HifzHub.Domain.Common;

namespace HifzHub.Domain.Entities;

public class Halaqa : BaseEntity
{
    public Guid CenterId { get; set; }
    public Center Center { get; set; } = null!;

    public Guid StageId { get; set; }
    public Stage Stage { get; set; } = null!;

    public string Name { get; set; } = string.Empty;
}