using HifzHub.Application.Abstractions;
using System.Security.Claims;

namespace HifzHub.Api.Services;

public class TenantContext(IHttpContextAccessor accessor) : ITenantContext
{
    public Guid? CenterId
    {
        get
        {
            var value = accessor.HttpContext?.User
                .FindFirstValue("centerId");

            return Guid.TryParse(value, out var id) ? id : null;
        }
    }
}