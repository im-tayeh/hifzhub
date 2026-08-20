namespace HifzHub.Application.Students;

public record CreateStudentRequest(
    string FullName,
    Guid? HalaqaId,
    string? NationalId,
    DateOnly? DateOfBirth,
    string? WhatsApp,
    bool IsOrphan);

public record UpdateStudentRequest(
    string FullName,
    string? NationalId,
    DateOnly? DateOfBirth,
    string? WhatsApp,
    bool IsOrphan);

public record AssignHalaqaRequest(Guid HalaqaId);

public record StudentResponse(
    Guid Id,
    string FullName,
    Guid? HalaqaId,
    string? NationalId,
    DateOnly? DateOfBirth,
    string? WhatsApp,
    bool IsOrphan,
    bool IsActive);
