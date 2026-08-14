using HifzHub.Domain.Entities;

namespace HifzHub.Application.Abstractions;

public interface IJwtTokenService
{
    string GenerateToken(User user);
}