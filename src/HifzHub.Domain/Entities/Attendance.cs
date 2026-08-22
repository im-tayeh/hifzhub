using HifzHub.Domain.Common;
using HifzHub.Domain.Enums;

namespace HifzHub.Domain.Entities;

public class Attendance : BaseEntity
{
    public Guid CenterId { get; set; }
    public Center Center { get; set; } = null!;

    public Guid StudentId { get; set; }
    public Student Student { get; set; } = null!;

    public Guid HalaqaId { get; set; }
    public Halaqa Halaqa { get; set; } = null!;

    public DateOnly Date { get; set; }
    public AttendanceStatus Status { get; set; }
    public string? Note { get; set; }
}