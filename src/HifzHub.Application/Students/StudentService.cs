using HifzHub.Application.Abstractions;
using HifzHub.Domain.Common;
using HifzHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HifzHub.Application.Students;

public class StudentService(IAppDbContext db, ITenantContext tenant)
{
    public async Task<Result<StudentResponse>> CreateAsync(CreateStudentRequest req, CancellationToken ct = default)
    {
        if (tenant.CenterId is null)
            return Result<StudentResponse>.Validation("A center context is required.");
        if (req.HalaqaId is not null && !await db.Halaqat.AnyAsync(h => h.Id == req.HalaqaId, ct))
            return Result<StudentResponse>.NotFound("Halaqa not found.");

        var student = new Student
        {
            CenterId = tenant.CenterId.Value,
            HalaqaId = req.HalaqaId,
            FullName = req.FullName,
            NationalId = req.NationalId,
            DateOfBirth = req.DateOfBirth,
            WhatsApp = req.WhatsApp,
            IsOrphan = req.IsOrphan
        };
        db.Students.Add(student);
        await db.SaveChangesAsync(ct);
        return Result<StudentResponse>.Success(ToResponse(student));
    }

    public async Task<List<StudentResponse>> GetAllAsync(CancellationToken ct = default) =>
        await db.Students.Select(s => ToResponse(s)).ToListAsync(ct);

    public async Task<List<StudentResponse>> GetUnassignedAsync(CancellationToken ct = default) =>
        await db.Students.Where(s => s.HalaqaId == null)
                         .Select(s => ToResponse(s)).ToListAsync(ct);

    public async Task<Result<StudentResponse>> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var s = await db.Students.FirstOrDefaultAsync(x => x.Id == id, ct);
        return s is null ? Result<StudentResponse>.NotFound("Student not found.") : Result<StudentResponse>.Success(ToResponse(s));
    }

    public async Task<Result> AssignHalaqaAsync(Guid id, Guid halaqaId, CancellationToken ct = default)
    {
        var s = await db.Students.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (s is null) return Result.NotFound("Student not found.");
        if (!await db.Halaqat.AnyAsync(h => h.Id == halaqaId, ct)) return Result.NotFound("Halaqa not found.");
        s.HalaqaId = halaqaId;
        await db.SaveChangesAsync(ct);
        return Result.Success();
    }

    public async Task<Result> UpdateAsync(Guid id, UpdateStudentRequest req, CancellationToken ct = default)
    {
        var s = await db.Students.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (s is null) return Result.NotFound("Student not found.");
        s.FullName = req.FullName; s.NationalId = req.NationalId; s.DateOfBirth = req.DateOfBirth;
        s.WhatsApp = req.WhatsApp; s.IsOrphan = req.IsOrphan;
        await db.SaveChangesAsync(ct);
        return Result.Success();
    }

    public async Task<Result> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var s = await db.Students.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (s is null) return Result.NotFound("Student not found.");
        s.IsActive = false;
        await db.SaveChangesAsync(ct);
        return Result.Success();
    }

    private static StudentResponse Map(Student s) => ToResponse(s);
    private static StudentResponse ToResponse(Student s) => new(
        s.Id, s.FullName, s.HalaqaId, s.NationalId, s.DateOfBirth, s.WhatsApp, s.IsOrphan, s.IsActive);
}