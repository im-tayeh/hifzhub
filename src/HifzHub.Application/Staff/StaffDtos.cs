namespace HifzHub.Application.Staff;

public record UpdateStaffProfileRequest(
    string? Bio,
    string? Qualifications,
    string? Certificates,
    int? YearsOfService);

public record StaffProfileResponse(
    Guid UserId, string FullName,
    string? Bio, string? Qualifications, string? Certificates,
    int? YearsOfService, bool IsPublic);

public record StaffStatsResponse(
    Guid UserId, int HalaqatCount, int StudentsCount, int RecitationsLogged);