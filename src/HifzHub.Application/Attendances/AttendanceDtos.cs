using HifzHub.Domain.Enums;

namespace HifzHub.Application.Attendances;

public record MarkAttendanceRequest(Guid StudentId, DateOnly Date, AttendanceStatus Status, string? Note);

public record BulkAttendanceRequest(Guid HalaqaId, DateOnly Date, List<StudentAttendance> Entries);
public record StudentAttendance(Guid StudentId, AttendanceStatus Status, string? Note);

public record AttendanceResponse(Guid Id, Guid StudentId, Guid HalaqaId, DateOnly Date, AttendanceStatus Status, string? Note);