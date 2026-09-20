using HifzHub.Application.Abstractions;
using HifzHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HifzHub.Application.Attendances;

public class AttendanceService(IAppDbContext db, ITenantContext tenant)
{
    public async Task<AttendanceResponse?> MarkAsync(MarkAttendanceRequest req, CancellationToken ct = default)
    {
        if (tenant.CenterId is null) return null;

        var student = await db.Students.FirstOrDefaultAsync(s => s.Id == req.StudentId, ct);
        if (student is null || student.HalaqaId is null) return null;

        var existing = await db.Attendances
            .FirstOrDefaultAsync(a => a.StudentId == req.StudentId && a.Date == req.Date, ct);

        if (existing is not null)
        {
            existing.Status = req.Status;
            existing.Note = req.Note;
            await db.SaveChangesAsync(ct);
            return Map(existing);
        }

        var att = new Attendance
        {
            CenterId = tenant.CenterId.Value,
            StudentId = req.StudentId,
            HalaqaId = student.HalaqaId.Value,
            Date = req.Date,
            Status = req.Status,
            Note = req.Note
        };
        db.Attendances.Add(att);
        await db.SaveChangesAsync(ct);
        return Map(att);
    }

    public async Task<int> MarkBulkAsync(BulkAttendanceRequest req, CancellationToken ct = default)
    {
        if (tenant.CenterId is null) return 0;

        if (!await db.Halaqat.AnyAsync(h => h.Id == req.HalaqaId, ct)) return 0;

        var already = await db.Attendances
            .Where(a => a.Date == req.Date && a.HalaqaId == req.HalaqaId)
            .Select(a => a.StudentId).ToListAsync(ct);

        var added = 0;
        foreach (var e in req.Entries)
        {
            if (already.Contains(e.StudentId)) continue;

            db.Attendances.Add(new Attendance
            {
                CenterId = tenant.CenterId.Value,
                StudentId = e.StudentId,
                HalaqaId = req.HalaqaId,
                Date = req.Date,
                Status = e.Status,
                Note = e.Note
            });
            added++;
        }

        await db.SaveChangesAsync(ct);
        return added;
    }

    public async Task<List<AttendanceResponse>> GetByHalaqaAndDateAsync(Guid halaqaId, DateOnly date, CancellationToken ct = default) =>
        await db.Attendances.Where(a => a.HalaqaId == halaqaId && a.Date == date)
            .Select(a => new AttendanceResponse(a.Id, a.StudentId, a.HalaqaId, a.Date, a.Status, a.Note))
            .ToListAsync(ct);

    private static AttendanceResponse Map(Attendance a) =>
        new(a.Id, a.StudentId, a.HalaqaId, a.Date, a.Status, a.Note);
}