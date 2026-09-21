using HifzHub.Application.Abstractions;
using HifzHub.Domain.Common;
using HifzHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HifzHub.Application.Stages;

public class StageService(IAppDbContext db, ITenantContext tenant)
{
    public async Task<Result<StageResponse>> CreateAsync(CreateStageRequest request, CancellationToken ct = default)
    {
        if (tenant.CenterId is null)
            return Result<StageResponse>.Validation("A center context is required.");

        var stage = new Stage
        {
            CenterId = tenant.CenterId.Value,
            Name = request.Name,
            Order = request.Order
        };

        db.Stages.Add(stage);
        await db.SaveChangesAsync(ct);
        return Result<StageResponse>.Success(Map(stage));
    }

    public async Task<List<StageResponse>> GetAllAsync(CancellationToken ct = default) =>
        await db.Stages
            .OrderBy(s => s.Order)
            .Select(s => new StageResponse(s.Id, s.Name, s.Order))
            .ToListAsync(ct);

    public async Task<Result<StageResponse>> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var stage = await db.Stages.FirstOrDefaultAsync(s => s.Id == id, ct);

        return stage is null
            ? Result<StageResponse>.NotFound("Stage not found.")
            : Result<StageResponse>.Success(Map(stage));
    }

    public async Task<Result> UpdateAsync(Guid id, UpdateStageRequest request, CancellationToken ct = default)
    {
        var stage = await db.Stages.FirstOrDefaultAsync(s => s.Id == id, ct);

        if (stage is null)
            return Result.NotFound("Stage not found.");

        stage.Name = request.Name;
        stage.Order = request.Order;

        await db.SaveChangesAsync(ct);

        return Result.Success();
    }

    public async Task<Result> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var stage = await db.Stages.FirstOrDefaultAsync(s => s.Id == id, ct);

        if (stage is null)
            return Result.NotFound("Stage not found.");

        db.Stages.Remove(stage);
        await db.SaveChangesAsync(ct);

        return Result.Success();
    }

    private static StageResponse Map(Stage s) => new(s.Id, s.Name, s.Order);
}