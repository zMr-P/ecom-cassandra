using Asp.Versioning;
using ecom_cassandra.Application.UseCases.Categories.Create;
using ecom_cassandra.Application.UseCases.Categories.GetAll;
using ecom_cassandra.Application.UseCases.Products.GetAll;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ecom_cassandra.WebApi.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[Controller]")]
public class CategoriesController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("create")]
    [Authorize(Roles =  "Operator, Admin")]
    [SwaggerOperation("Register category in the application")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<string>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<string>))]
    public async Task<IActionResult> CreateCategoryBatchAsync([FromBody] CreateCategoryRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        if (!response.IsSuccess)
            return BadRequest(response.ErrorMessages);

        return Ok(response.Messages);
    }

    [HttpGet("get-all")]
    [SwaggerOperation("Read all categories of the application")]
    [Authorize(Roles = "Customer, Operator, Admin")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetAllProductsResponse>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<string>))]
    public async Task<IActionResult> GetAllCategoriesAsync(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetAllCategoriesRequest(), cancellationToken);

        if (!response.IsSuccess)
            return BadRequest(response.ErrorMessages);

        return Ok(response.Value);
    }
}