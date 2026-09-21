using HifzHub.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace HifzHub.Api.Controllers;

[ApiController]
public class ApiControllerBase : ControllerBase
{
    protected IActionResult Problem(Error error)
    {
        var status = error.Type switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(detail: error.Message, statusCode: status);
    }

    protected IActionResult HandleResult<T>(Result<T> result) =>
        result.IsSuccess ? Ok(result.Value) : Problem(result.Error);

    protected IActionResult HandleResult(Result result) =>
        result.IsSuccess ? NoContent() : Problem(result.Error);
}