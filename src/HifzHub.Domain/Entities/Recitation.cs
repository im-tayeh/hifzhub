using HifzHub.Domain.Common;
using HifzHub.Domain.Enums;

namespace HifzHub.Domain.Entities;

public class Recitation : BaseEntity
{
    public Guid CenterId { get; set; }
    public Center Center { get; set; } = null!;

    public Guid StudentId { get; set; }
    public Student Student { get; set; } = null!;

    public Guid HalaqaId { get; set; }
    public Halaqa Halaqa { get; set; } = null!;

    public DateOnly Date { get; set; }

    public int FromSurah { get; set; }
    public int FromAyah { get; set; }
    public int ToSurah { get; set; }
    public int ToAyah { get; set; }

    public RecitationType Type { get; set; }
    public RecitationGrade Grade { get; set; }
    public string? Note { get; set; }
}