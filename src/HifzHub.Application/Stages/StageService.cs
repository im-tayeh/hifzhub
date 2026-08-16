using HifzHub.Application.Abstractions;
using HifzHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HifzHub.Application.Stages;

public class StageService(IAppDbContext db, ITenantContext tenant)
{
    public async Task<StageResponse?> CreateAsync(CreateStageRequest request, CancellationToken ct = default)
    {
        if (tenant.CenterId is null) return null;

        var stage = new Stage
        {
            CenterId = tenant.CenterId.Value,
            Name = request.Name,
            Order = request.Order
        };

        db.Stages.Add(stage);
        await db.SaveChangesAsync(ct);
        return Map(stage);
    }

    public async Task<List<StageResponse>> GetAllAsync(CancellationToken ct = default) =>
        await db.Stages
            .OrderBy(s => s.Order)
            .Select(s => new StageResponse(s.Id, s.Name, s.Order))
            .ToListAsync(ct);

    public async Task<StageResponse?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var stage = await db.Stages.FirstOrDefaultAsync(s => s.Id == id, ct);

        return stage is null ? null : Map(stage);
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateStageRequest request, CancellationToken ct = default)
    {
        var stage = await db.Stages.FirstOrDefaultAsync(s => s.Id == id, ct);

        if (stage is null) return false;

        stage.Name = request.Name;
        stage.Order = request.Order;

        await db.SaveChangesAsync(ct);

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var stage = await db.Stages.FirstOrDefaultAsync(s => s.Id == id, ct);

        if (stage is null) return false;

        db.Stages.Remove(stage);
        await db.SaveChangesAsync(ct);

        return true;
    }

    private static StageResponse Map(Stage s) => new(s.Id, s.Name, s.Order);
}