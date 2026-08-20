using HifzHub.Domain.Common;

namespace HifzHub.Domain.Entities;

public class CourseEnrollment : BaseEntity
{
    public Guid CenterId { get; set; }

    public Guid CourseId { get; set; }
    public Course Course { get; set; } = null!;

    public Guid StudentId { get; set; }
    public Student Student { get; set; } = null!;
}