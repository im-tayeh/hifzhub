using HifzHub.Domain.Common;

namespace HifzHub.Domain.Entities;

public class Course : BaseEntity
{
    public Guid CenterId { get; set; }
    public Center Center { get; set; } = null!;

    public Guid? ProgramId { get; set; }
    public TrainingProgram? Program { get; set; }

    public string Name { get; set; } = string.Empty;

    public ICollection<CourseEnrollment> Enrollments { get; set; } = new List<CourseEnrollment>();
}