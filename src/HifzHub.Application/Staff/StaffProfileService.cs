using HifzHub.Application.Abstractions;
using HifzHub.Domain.Entities;
using HifzHub.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace HifzHub.Application.Staff;

public class StaffProfileService(IAppDbContext db, ITenantContext tenant)
{
    public async Task<StaffProfileResponse?> UpdateMyProfileAsync(
        Guid currentUserId, UpdateStaffProfileRequest req, CancellationToken ct = default)
    {
        if (tenant.CenterId is null) return null;

        var profile = await db.StaffProfiles.FirstOrDefaultAsync(p => p.UserId == currentUserId, ct);
        if (profile is null)
        {
            profile = new StaffProfile { CenterId = tenant.CenterId.Value, UserId = currentUserId };
            db.StaffProfiles.Add(profile);
        }
        profile.Bio = req.Bio;
        profile.Qualifications = req.Qualifications;
        profile.Certificates = req.Certificates;
        profile.YearsOfService = req.YearsOfService;
        await db.SaveChangesAsync(ct);

        return await BuildResponseAsync(profile.UserId, ct);
    }

    public async Task<bool> SetVisibilityAsync(Guid currentUserId, bool isPublic, CancellationToken ct = default)
    {
        var profile = await db.StaffProfiles.FirstOrDefaultAsync(p => p.UserId == currentUserId, ct);
        if (profile is null) return false;
        profile.IsPublic = isPublic;
        await db.SaveChangesAsync(ct);
        return true;
    }

    public async Task<StaffProfileResponse?> GetProfileAsync(
        Guid targetUserId, Guid currentUserId, bool currentIsManager, CancellationToken ct = default)
    {
        var profile = await db.StaffProfiles.FirstOrDefaultAsync(p => p.UserId == targetUserId, ct);
        if (profile is null) return null;

        if (!profile.IsPublic && targetUserId != currentUserId && !currentIsManager)
            return null;

        return await BuildResponseAsync(targetUserId, ct);
    }

    public async Task<StaffStatsResponse?> GetStatsAsync(Guid userId, CancellationToken ct = default)
    {
        var halaqaIds = await db.UserRoles
            .Where(r => r.UserId == userId && r.ScopeType == ScopeType.Halaqa && r.ScopeId != null)
            .Select(r => r.ScopeId!.Value).ToListAsync(ct);

        var halaqatCount = halaqaIds.Count;
        var studentsCount = await db.Students.CountAsync(s => s.HalaqaId != null && halaqaIds.Contains(s.HalaqaId.Value), ct);
        var recitationsLogged = await db.Recitations.CountAsync(r => halaqaIds.Contains(r.HalaqaId), ct);

        return new StaffStatsResponse(userId, halaqatCount, studentsCount, recitationsLogged);
    }

    private async Task<StaffProfileResponse?> BuildResponseAsync(Guid userId, CancellationToken ct)
    {
        var p = await db.StaffProfiles.FirstOrDefaultAsync(x => x.UserId == userId, ct);
        if (p is null) return null;
        var user = await db.Users.FirstOrDefaultAsync(u => u.Id == userId, ct);
        return new StaffProfileResponse(
            p.UserId, user?.FullName ?? "", p.Bio, p.Qualifications, p.Certificates, p.YearsOfService, p.IsPublic);
    }
}