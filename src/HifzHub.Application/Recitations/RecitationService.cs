using HifzHub.Application.Abstractions;
using HifzHub.Domain.Common;
using HifzHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HifzHub.Application.Recitations;

public class RecitationService(IAppDbContext db, ITenantContext tenant)
{
    public async Task<Result<RecitationResponse>> CreateAsync(CreateRecitationRequest req, CancellationToken ct = default)
    {
        if (tenant.CenterId is null) return Result<RecitationResponse>.Validation("A center context is required.");
        var student = await db.Students.FirstOrDefaultAsync(s => s.Id == req.StudentId, ct);
        if (student is null || student.HalaqaId is null)
            return Result<RecitationResponse>.NotFound("Student not found or has no halaqa.");

        if (req.FromSurah is < 1 or > 114 || req.ToSurah is < 1 or > 114)
            return Result<RecitationResponse>.Validation("Surah number must be between 1 and 114.");

        var fromSurah = await db.Surahs.FirstOrDefaultAsync(s => s.Number == req.FromSurah, ct);
        var toSurah = await db.Surahs.FirstOrDefaultAsync(s => s.Number == req.ToSurah, ct);
        if (fromSurah is null || toSurah is null) return Result<RecitationResponse>.Validation("Invalid surah.");
        if (req.FromAyah < 1 || req.FromAyah > fromSurah.AyahCount)
            return Result<RecitationResponse>.Validation($"From ayah must be between 1 and {fromSurah.AyahCount}.");
        if (req.ToAyah < 1 || req.ToAyah > toSurah.AyahCount)
            return Result<RecitationResponse>.Validation($"To ayah must be between 1 and {toSurah.AyahCount}.");

        var rec = new Recitation
        {
            CenterId = tenant.CenterId.Value,
            StudentId = req.StudentId,
            HalaqaId = student.HalaqaId.Value,
            Date = req.Date,
            FromSurah = req.FromSurah,
            FromAyah = req.FromAyah,
            ToSurah = req.ToSurah,
            ToAyah = req.ToAyah,
            Type = req.Type,
            Grade = req.Grade,
            Note = req.Note
        };
        db.Recitations.Add(rec);
        await db.SaveChangesAsync(ct);
        return Result<RecitationResponse>.Success(Map(rec));
    }

    public async Task<List<RecitationResponse>> GetByStudentAsync(Guid studentId, CancellationToken ct = default) =>
        await db.Recitations.Where(r => r.StudentId == studentId)
            .OrderByDescending(r => r.Date)
            .Select(r => Map(r)).ToListAsync(ct);

    private static RecitationResponse Map(Recitation r) => new(
        r.Id, r.StudentId, r.Date, r.FromSurah, r.FromAyah, r.ToSurah, r.ToAyah, r.Type, r.Grade, r.Note);
}