namespace HifzHub.Domain.Common;

// Marks an entity as owned by a tenant (center) — subject to automatic isolation
public interface ITenantOwned
{
    Guid CenterId { get; }
}