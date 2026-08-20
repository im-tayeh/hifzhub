using HifzHub.Application.Abstractions;
using HifzHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HifzHub.Application.Students;

public class StudentService(IAppDbContext db, ITenantContext tenant)
{
    public async Task<StudentResponse?> CreateAsync(CreateStudentRequest req, CancellationToken ct = default)
    {
        if (tenant.CenterId is null) return null;

        if (req.HalaqaId is not null &&
            !await db.Halaqat.AnyAsync(h => h.Id == req.HalaqaId, ct))
            return null;

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
        return Map(student);
    }

    public async Task<List<StudentResponse>> GetAllAsync(CancellationToken ct = default) =>
        await db.Students.Select(s => ToResponse(s)).ToListAsync(ct);

    public async Task<List<StudentResponse>> GetUnassignedAsync(CancellationToken ct = default) =>
        await db.Students.Where(s => s.HalaqaId == null)
                         .Select(s => ToResponse(s)).ToListAsync(ct);

    public async Task<StudentResponse?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var s = await db.Students.FirstOrDefaultAsync(x => x.Id == id, ct);
        return s is null ? null : Map(s);
    }

    public async Task<bool> AssignHalaqaAsync(Guid id, Guid halaqaId, CancellationToken ct = default)
    {
        var s = await db.Students.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (s is null) return false;
        if (!await db.Halaqat.AnyAsync(h => h.Id == halaqaId, ct)) return false;
        s.HalaqaId = halaqaId;
        await db.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateStudentRequest req, CancellationToken ct = default)
    {
        var s = await db.Students.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (s is null) return false;
        s.FullName = req.FullName;
        s.NationalId = req.NationalId;
        s.DateOfBirth = req.DateOfBirth;
        s.WhatsApp = req.WhatsApp;
        s.IsOrphan = req.IsOrphan;
        await db.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var s = await db.Students.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (s is null) return false;
        s.IsActive = false;
        await db.SaveChangesAsync(ct);
        return true;
    }

    private static StudentResponse Map(Student s) => ToResponse(s);
    private static StudentResponse ToResponse(Student s) => new(
        s.Id, s.FullName, s.HalaqaId, s.NationalId, s.DateOfBirth, s.WhatsApp, s.IsOrphan, s.IsActive);
}