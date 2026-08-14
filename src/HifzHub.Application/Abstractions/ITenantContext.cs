namespace HifzHub.Application.Abstractions;

public interface ITenantContext
{
    Guid? CenterId { get; }
}