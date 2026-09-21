using HifzHub.Application.Abstractions;
using HifzHub.Domain.Common;
using HifzHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HifzHub.Application.Halaqat;

public class HalaqaService(IAppDbContext db, ITenantContext tenant)
{
    public async Task<Result<HalaqaResponse>> CreateAsync(CreateHalaqaRequest req, CancellationToken ct = default)
    {
        if (tenant.CenterId is null)
            return Result<HalaqaResponse>.Validation("A center context is required.");
        if (!await db.Stages.AnyAsync(s => s.Id == req.StageId, ct))
            return Result<HalaqaResponse>.NotFound("Stage not found.");

        var h = new Halaqa { CenterId = tenant.CenterId.Value, StageId = req.StageId, Name = req.Name };
        db.Halaqat.Add(h);
        await db.SaveChangesAsync(ct);
        return Result<HalaqaResponse>.Success(Map(h));
    }

    public async Task<List<HalaqaResponse>> GetAllAsync(CancellationToken ct = default) =>
        await db.Halaqat.Select(h => new HalaqaResponse(h.Id, h.StageId, h.Name)).ToListAsync(ct);

    public async Task<Result<HalaqaResponse>> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var h = await db.Halaqat.FirstOrDefaultAsync(x => x.Id == id, ct);
        return h is null ? Result<HalaqaResponse>.NotFound("Halaqa not found.") : Result<HalaqaResponse>.Success(Map(h));
    }

    public async Task<Result> UpdateAsync(Guid id, UpdateHalaqaRequest req, CancellationToken ct = default)
    {
        var h = await db.Halaqat.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (h is null) return Result.NotFound("Halaqa not found.");
        if (!await db.Stages.AnyAsync(s => s.Id == req.StageId, ct))
            return Result.NotFound("Stage not found.");
        h.StageId = req.StageId;
        h.Name = req.Name;
        await db.SaveChangesAsync(ct);
        return Result.Success();
    }

    public async Task<Result> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var h = await db.Halaqat.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (h is null) return Result.NotFound("Halaqa not found.");
        db.Halaqat.Remove(h);
        await db.SaveChangesAsync(ct);
        return Result.Success();
    }

    private static HalaqaResponse Map(Halaqa h) => new(h.Id, h.StageId, h.Name);
}