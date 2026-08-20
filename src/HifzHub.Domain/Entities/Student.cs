using HifzHub.Domain.Common;

namespace HifzHub.Domain.Entities;

public class Student : BaseEntity
{
    public Guid CenterId { get; set; }
    public Center Center { get; set; } = null!;

    public Guid? HalaqaId { get; set; }
    public Halaqa? Halaqa { get; set; }

    public string FullName { get; set; } = string.Empty;
    public string? NationalId { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string? WhatsApp { get; set; }
    public bool IsOrphan { get; set; }
    public bool IsActive { get; set; } = true;
}