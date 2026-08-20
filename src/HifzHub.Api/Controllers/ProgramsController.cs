using HifzHub.Application.Programs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HifzHub.Api.Controllers;

[Authorize]
[ApiController]
[Route("api")]
public class ProgramsController(ProgramService service) : ControllerBase
{
    [HttpPost("programs")]
    public async Task<IActionResult> CreateProgram(CreateProgramRequest req, CancellationToken ct)
    {
        var r = await service.CreateProgramAsync(req, ct);
        return r is null ? BadRequest(new { message = "Missing center context." }) : Ok(r);
    }

    [HttpGet("programs")]
    public async Task<IActionResult> GetPrograms(CancellationToken ct) =>
        Ok(await service.GetProgramsAsync(ct));

    [HttpPost("courses")]
    public async Task<IActionResult> CreateCourse(CreateCourseRequest req, CancellationToken ct)
    {
        var r = await service.CreateCourseAsync(req, ct);
        return r is null ? BadRequest(new { message = "Invalid program or missing center context." }) : Ok(r);
    }

    [HttpGet("courses")]
    public async Task<IActionResult> GetCourses(CancellationToken ct) =>
        Ok(await service.GetCoursesAsync(ct));

    [HttpPost("courses/{courseId:guid}/enroll")]
    public async Task<IActionResult> Enroll(Guid courseId, EnrollStudentRequest req, CancellationToken ct)
    {
        var r = await service.EnrollAsync(courseId, req.StudentId, ct);
        return r is null ? BadRequest(new { message = "Invalid course/student or already enrolled." }) : Ok(r);
    }

    [HttpGet("courses/{courseId:guid}/students")]
    public async Task<IActionResult> GetCourseStudents(Guid courseId, CancellationToken ct) =>
        Ok(await service.GetCourseStudentsAsync(courseId, ct));
}