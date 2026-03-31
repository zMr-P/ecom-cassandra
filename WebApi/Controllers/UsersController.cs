using Asp.Versioning;
using ecom_cassandra.Application.UseCases.Users.Create;
using ecom_cassandra.Application.UseCases.Users.GetAll;
using ecom_cassandra.Application.UseCases.Users.Login;
using ecom_cassandra.Application.UseCases.Users.UpdateRole;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

    [HttpGet("get-all")]
    [SwaggerOperation("Read all users of the application")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetAllUserResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<string>))]
    public async Task<IActionResult> GetAllUsersAsync(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetAllUserRequest(), cancellationToken);

        if (!response.IsSuccess)
            return BadRequest(response.ErrorMessages);

        return Ok(response.Value);
    }

    [HttpPatch("update-role")]
    [SwaggerOperation("Update the role of a user to change the permissions of the user in the application")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<string>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<string>))]
    public async Task<IActionResult> UpdateUserRoleAsync([FromBody] UpdateRoleRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        if (!response.IsSuccess)
            return BadRequest(response.ErrorMessages);

        return Ok(response.Messages);
    }

    [HttpPost("login")]
    [SwaggerOperation("Login a user in the application using email and password")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<string>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<string>))]
    public async Task<IActionResult> LoginUserAsync([FromBody] LoginUserRequest request)
    {
        var response = await _mediator.Send(request);

        if (!response.IsSuccess)
            return BadRequest(response.ErrorMessages);

        return Ok(response.Value);
    }
}