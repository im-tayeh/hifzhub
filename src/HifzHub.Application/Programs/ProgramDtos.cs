namespace HifzHub.Application.Programs;

public record CreateProgramRequest(string Name, string? Description);
public record ProgramResponse(Guid Id, string Name, string? Description);

public record CreateCourseRequest(string Name, Guid? ProgramId);
public record CourseResponse(Guid Id, string Name, Guid? ProgramId);

public record EnrollStudentRequest(Guid StudentId);
public record EnrollmentResponse(Guid Id, Guid CourseId, Guid StudentId);
