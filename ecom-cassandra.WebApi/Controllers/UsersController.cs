using Asp.Versioning;
using ecom_cassandra.Application.UseCases.Users.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ecom_cassandra.WebApi.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[Controller]")]
public class UsersController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("create")]
    [SwaggerOperation("Register a user in the application")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<string>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<string>))]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        if (!response.IsSuccess)
            return BadRequest(response.ErrorMessages);

        return Ok(response.Messages);
    }
}