using HifzHub.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HifzHub.Domain.Entities;

public class StaffProfile : BaseEntity
{
    public Guid CenterId { get; set; }
    public Center Center { get; set; } = null!;

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public string? Bio { get; set; }
    public string? Qualifications { get; set; }
    public string? Certificates { get; set; }
    public int? YearsOfService { get; set; }

    public bool IsPublic { get; set; }
}
