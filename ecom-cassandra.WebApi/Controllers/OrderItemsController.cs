using Asp.Versioning;
using ecom_cassandra.Application.UseCases.OrderItems.GetByOrder;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ecom_cassandra.WebApi.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[Controller]")]
public class OrderITemsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("get-by-order-id/{orderId:guid}")]
    [SwaggerOperation("Read order items by order id")]
    [Authorize(Roles = "Customer, Operator, Admin")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetByOrderResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<string>))]
    public async Task<IActionResult> GetItemsByOrderAsync([FromRoute] Guid orderId, CancellationToken cancellationToken)
    {
        var request = new GetByOrderRequest(orderId);
        var response = await _mediator.Send(request, cancellationToken);

        if (!response.IsSuccess)
            return BadRequest(response.ErrorMessages);

        return Ok(response.Value);
    }
}