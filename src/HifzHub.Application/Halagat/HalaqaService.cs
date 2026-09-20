using HifzHub.Application.Abstractions;
using HifzHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HifzHub.Application.Halaqat;

public class HalaqaService(IAppDbContext db, ITenantContext tenant)
{
    public async Task<HalaqaResponse?> CreateAsync(CreateHalaqaRequest req, CancellationToken ct = default)
    {
        if (tenant.CenterId is null) return null;

        var stageExists = await db.Stages.AnyAsync(s => s.Id == req.StageId, ct);
        if (!stageExists) return null;

        var halaqa = new Halaqa
        {
            CenterId = tenant.CenterId.Value,
            StageId = req.StageId,
            Name = req.Name
        };
        db.Halaqat.Add(halaqa);
        await db.SaveChangesAsync(ct);
        return Map(halaqa);
    }

    public async Task<List<HalaqaResponse>> GetAllAsync(CancellationToken ct = default) =>
        await db.Halaqat
            .Select(h => new HalaqaResponse(h.Id, h.StageId, h.Name))
            .ToListAsync(ct);

    public async Task<HalaqaResponse?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var h = await db.Halaqat.FirstOrDefaultAsync(x => x.Id == id, ct);
        return h is null ? null : Map(h);
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateHalaqaRequest req, CancellationToken ct = default)
    {
        var h = await db.Halaqat.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (h is null) return false;

        if (!await db.Stages.AnyAsync(s => s.Id == req.StageId, ct))
            return false;

        h.StageId = req.StageId;
        h.Name = req.Name;
        await db.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var h = await db.Halaqat.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (h is null) return false;
        db.Halaqat.Remove(h);
        await db.SaveChangesAsync(ct);
        return true;
    }

    private static HalaqaResponse Map(Halaqa h) => new(h.Id, h.StageId, h.Name);
}