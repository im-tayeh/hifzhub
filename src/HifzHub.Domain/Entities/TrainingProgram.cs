using HifzHub.Domain.Common;

namespace HifzHub.Domain.Entities;

public class TrainingProgram : BaseEntity
{
    public Guid CenterId { get; set; }
    public Center Center { get; set; } = null!;

    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}
