using HifzHub.Application.Auth;
using Microsoft.AspNetCore.Mvc;

namespace HifzHub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(LoginHandler loginHandler) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request, CancellationToken ct)
    {
        var result = await loginHandler.HandleAsync(request, ct);

        if (result is null)
            return Unauthorized(new { message = "Invalid username or password" });

        return Ok(result);
    }
}