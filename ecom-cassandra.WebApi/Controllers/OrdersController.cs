using Asp.Versioning;
using ecom_cassandra.Application.UseCases.Orders.Create;
using ecom_cassandra.Application.UseCases.Orders.GetAll;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ecom_cassandra.WebApi.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[Controller]")]
public class OrdersController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("create")]
    [Authorize(Roles = "Customer")]
    [SwaggerOperation("Register a order in the application")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<string>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<string>))]
    public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        if (!response.IsSuccess)
            return BadRequest(response.ErrorMessages);

        return Ok(response.Messages);
    }

    [HttpGet("get-all")]
    [Authorize(Roles = "Admin")]
    [SwaggerOperation("Read all orders of the application")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetAllOrdersResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<string>))]
    public async Task<IActionResult> GetAllOrdersAsync(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetAllOrdersRequest(), cancellationToken);

        if (!response.IsSuccess)
            return BadRequest(response.ErrorMessages);

        return Ok(response.Value);
    }
}