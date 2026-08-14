using HifzHub.Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace HifzHub.Application.Auth;

public record LoginRequest(string Username, string Password);
public record LoginResponse(string Token, string Username, string FullName);
public class LoginHandler(IAppDbContext db, IPasswordHasher hasher, IJwtTokenService jwt)
{
    public async Task<LoginResponse?> HandleAsync(LoginRequest request, CancellationToken ct = default)
    {
        var user = await db.Users
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(u => u.Username == request.Username && u.IsActive, ct);

        if (user is null) return null;

        if (!hasher.Verify(request.Password, user.PasswordHash))
            return null;

        var token = jwt.GenerateToken(user);

        return new LoginResponse(token, user.Username, user.FullName);
    }
}