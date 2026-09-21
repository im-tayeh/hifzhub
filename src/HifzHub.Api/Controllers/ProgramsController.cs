using HifzHub.Application.Programs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HifzHub.Api.Controllers;

[Authorize]
[Route("api")]
public class ProgramsController(ProgramService service) : ApiControllerBase
{
    [HttpPost("programs")]
    public async Task<IActionResult> CreateProgram(CreateProgramRequest req, CancellationToken ct) =>
        HandleResult(await service.CreateProgramAsync(req, ct));

    [HttpGet("programs")]
    public async Task<IActionResult> GetPrograms(CancellationToken ct) => Ok(await service.GetProgramsAsync(ct));

    [HttpPost("courses")]
    public async Task<IActionResult> CreateCourse(CreateCourseRequest req, CancellationToken ct) =>
        HandleResult(await service.CreateCourseAsync(req, ct));

    [HttpGet("courses")]
    public async Task<IActionResult> GetCourses(CancellationToken ct) => Ok(await service.GetCoursesAsync(ct));

    [HttpPost("courses/{courseId:guid}/enroll")]
    public async Task<IActionResult> Enroll(Guid courseId, EnrollStudentRequest req, CancellationToken ct) =>
        HandleResult(await service.EnrollAsync(courseId, req.StudentId, ct));

    [HttpGet("courses/{courseId:guid}/students")]
    public async Task<IActionResult> GetCourseStudents(Guid courseId, CancellationToken ct) =>
        Ok(await service.GetCourseStudentsAsync(courseId, ct));
}